CREATE OR REPLACE PROCEDURE public.upsert_meeting_note(IN p_event_uuid uuid, IN p_occ_date date, IN p_user_uuid uuid, IN p_note_text text)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_occurrence_id INT;
    v_note_id INT;
BEGIN
    -- Get or create occurrence
    v_occurrence_id := check_or_create_event_occurrence(p_event_uuid, p_occ_date, p_user_uuid);

    -- Check if note exists for occurrence
    SELECT id INTO v_note_id
    FROM meeting_notes
    WHERE occurrence_id = v_occurrence_id AND is_deleted = false;

    IF v_note_id IS NOT NULL THEN
        -- Update existing note
        UPDATE meeting_notes
        SET note_text = p_note_text,
            modified_on_utc = now(),
            modified_by = p_user_uuid
        WHERE id = v_note_id;
    ELSE
        -- Insert new note
        INSERT INTO meeting_notes (
            occurrence_id, note_text, created_on_utc, created_by, is_deleted
        ) VALUES (
            v_occurrence_id, p_note_text, now(), p_user_uuid, false
        );
    END IF;
END;
$procedure$
