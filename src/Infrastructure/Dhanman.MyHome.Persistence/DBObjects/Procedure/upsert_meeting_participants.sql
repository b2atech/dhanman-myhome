CREATE OR REPLACE PROCEDURE public.upsert_meeting_participants(IN p_event_uuid uuid, IN p_occ_date date, IN p_user_uuid uuid, IN p_participant_user_ids jsonb)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_occurrence_id INT;
    v_user_id UUID;
    v_existing_ids UUID[];
BEGIN
    -- Get or create occurrence
    v_occurrence_id := check_or_create_event_occurrence(p_event_uuid, p_occ_date, p_user_uuid);
    RAISE NOTICE 'Occurrence ID: %', v_occurrence_id;

    -- Existing participants
    SELECT array_agg(user_id) INTO v_existing_ids
    FROM meeting_participants
    WHERE occurrence_id = v_occurrence_id AND is_deleted = false;
    RAISE NOTICE 'Existing participants count: %', COALESCE(array_length(v_existing_ids, 1), 0);

    -- Loop input participants
    FOR v_user_id IN
        SELECT (elem->>'user_id')::uuid
        FROM jsonb_array_elements(p_participant_user_ids) AS elem
    LOOP
        RAISE NOTICE 'Processing user: %', v_user_id;

        IF v_existing_ids IS NULL OR NOT v_user_id = ANY(v_existing_ids) THEN
            INSERT INTO meeting_participants (
                occurrence_id, user_id, created_on_utc, created_by, is_deleted
            )
            VALUES (v_occurrence_id, v_user_id, now(), p_user_uuid, false);
            RAISE NOTICE 'Inserted user %', v_user_id;
        ELSE
            UPDATE meeting_participants
            SET modified_on_utc = now(),
                modified_by = p_user_uuid
            WHERE occurrence_id = v_occurrence_id
              AND user_id = v_user_id;
            RAISE NOTICE 'Updated user %', v_user_id;

            v_existing_ids := array_remove(v_existing_ids, v_user_id);
        END IF;
    END LOOP;

    -- Soft delete removed participants
    IF v_existing_ids IS NOT NULL THEN
        UPDATE meeting_participants
        SET is_deleted = true,
            deleted_on_utc = now(),
            modified_by = p_user_uuid
        WHERE occurrence_id = v_occurrence_id
          AND user_id = ANY(v_existing_ids);
        RAISE NOTICE 'Soft deleted users: %', v_existing_ids;
    END IF;

END;
$procedure$
