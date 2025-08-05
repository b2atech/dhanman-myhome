CREATE OR REPLACE FUNCTION public.check_or_create_event_occurrence(p_event_uuid uuid, p_occ_date date, p_user_uuid uuid)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
DECLARE
    v_occurrence_id INT;
    v_start_time TIME;
    v_end_time TIME;
BEGIN
    SELECT id INTO v_occurrence_id
    FROM event_occurrences
    WHERE event_id = p_event_uuid AND occurrence_date = p_occ_date AND is_deleted = false;

    IF v_occurrence_id IS NOT NULL THEN
        RETURN v_occurrence_id;
    END IF;

    -- Extract only the TIME part from the event's start and end time
    SELECT start_time::time, end_time::time
    INTO v_start_time, v_end_time
    FROM events
    WHERE id = p_event_uuid;

    -- Insert new occurrence with time applied to p_occ_date
    INSERT INTO event_occurrences (
        event_id, occurrence_date, start_time, end_time,
        generated_from_recurrence, created_on_utc, created_by, is_deleted
    )
    VALUES (
        p_event_uuid,
        p_occ_date,
        (p_occ_date + v_start_time)::timestamp,
        (p_occ_date + v_end_time)::timestamp,
        (SELECT is_recurring FROM events WHERE id = p_event_uuid),
        now(),
        p_user_uuid,
        false
    )
    RETURNING id INTO v_occurrence_id;

    RETURN v_occurrence_id;
END;
$function$
