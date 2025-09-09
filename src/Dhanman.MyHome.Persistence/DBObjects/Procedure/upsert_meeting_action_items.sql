CREATE OR REPLACE PROCEDURE public.upsert_meeting_action_items(IN p_event_uuid uuid, IN p_occ_date date, IN p_user_uuid uuid, IN p_action_items jsonb)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_occurrence_id INT;
    v_item jsonb;
    v_existing_ids INT[];
BEGIN
    -- Step 1: Get or create event occurrence
    v_occurrence_id := check_or_create_event_occurrence(p_event_uuid, p_occ_date, p_user_uuid);

    -- Step 2: Get current action item IDs
    SELECT array_agg(id)
    INTO v_existing_ids
    FROM meeting_action_items
    WHERE occurrence_id = v_occurrence_id AND is_deleted = false;

    -- Step 3: Loop through each item
    FOR v_item IN SELECT * FROM jsonb_array_elements(p_action_items)
    LOOP
        IF (v_item->>'id')::int < 0 THEN
            -- Insert new item (negative ID indicates new)
            INSERT INTO meeting_action_items (
                occurrence_id,
                action_description,
                assigned_to_user_id,
                due_date,
                status,
                created_on_utc,
                created_by,
                is_deleted
            )
            VALUES (
                v_occurrence_id,
                v_item->>'action_description',
                (v_item->>'assigned_to_user_id')::uuid,
                NULLIF(v_item->>'due_date', '')::timestamp,
                v_item->>'status',
                now(),
                p_user_uuid,
                false
            );
        ELSE
            -- Update existing action item (positive ID)
            UPDATE meeting_action_items
            SET
                action_description = v_item->>'action_description',
                assigned_to_user_id = (v_item->>'assigned_to_user_id')::uuid,
                due_date = NULLIF(v_item->>'due_date', '')::timestamp,
                status = v_item->>'status',
                modified_on_utc = now(),
                modified_by = p_user_uuid
            WHERE id = (v_item->>'id')::int
              AND occurrence_id = v_occurrence_id;

            -- Mark as handled
            v_existing_ids := array_remove(v_existing_ids, (v_item->>'id')::int);
        END IF;
    END LOOP;

    -- Step 4: Soft delete action items that were removed
    IF v_existing_ids IS NOT NULL THEN
        UPDATE meeting_action_items
        SET is_deleted = true,
            deleted_on_utc = now(),
            modified_by = p_user_uuid
        WHERE id = ANY(v_existing_ids);
    END IF;
END;
$procedure$
