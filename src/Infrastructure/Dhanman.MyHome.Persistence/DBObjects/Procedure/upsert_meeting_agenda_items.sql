CREATE OR REPLACE PROCEDURE public.upsert_meeting_agenda_items(IN p_event_uuid uuid, IN p_occ_date date, IN p_user_uuid uuid, IN p_agenda_items jsonb)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_occurrence_id INT;
    v_item jsonb;
    v_existing_ids INT[];
BEGIN
    -- Step 1: Get or create occurrence
    v_occurrence_id := check_or_create_event_occurrence(p_event_uuid, p_occ_date, p_user_uuid);

    -- Step 2: Track existing agenda item IDs for this occurrence
    SELECT array_agg(id)
    INTO v_existing_ids
    FROM meeting_agenda_items
    WHERE occurrence_id = v_occurrence_id AND is_deleted = false;

    -- Step 3: Loop through input items
    FOR v_item IN SELECT * FROM jsonb_array_elements(p_agenda_items)
    LOOP
        IF (v_item->>'id')::int < 0 THEN
            -- Insert new item (negative ID indicates new)
            INSERT INTO meeting_agenda_items (
                occurrence_id, item_text, order_no, created_on_utc, created_by, is_deleted
            )
            VALUES (
                v_occurrence_id,
                v_item->>'item_text',
                (v_item->>'order_no')::int,
                now(),
                p_user_uuid,
                false
            );
        ELSE
            -- Update existing item (positive ID)
            UPDATE meeting_agenda_items
            SET item_text = v_item->>'item_text',
                order_no = (v_item->>'order_no')::int,
                modified_on_utc = now(),
                modified_by = p_user_uuid
            WHERE id = (v_item->>'id')::int AND occurrence_id = v_occurrence_id;

            -- Remove updated ID from existing ID list (so it’s not soft-deleted later)
            v_existing_ids := array_remove(v_existing_ids, (v_item->>'id')::int);
        END IF;
    END LOOP;

    -- Step 4: Soft delete missing items (not sent in input)
    UPDATE meeting_agenda_items
    SET is_deleted = true,
        deleted_on_utc = now(),
        modified_by = p_user_uuid
    WHERE id = ANY(v_existing_ids);

END;
$procedure$
