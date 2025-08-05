CREATE OR REPLACE FUNCTION public.get_meeting_data(p_event_uuid uuid, p_occ_date date)
 RETURNS jsonb
 LANGUAGE plpgsql
AS $function$
DECLARE
    v_agenda jsonb;
    v_participants jsonb;
    v_actions jsonb;
    v_notes jsonb;
BEGIN
    -- Get agenda items, or empty array if no data
    SELECT COALESCE(
               jsonb_agg(
            jsonb_build_object(
                'id', id,
                'item_text', item_text,
                'order_no', order_no)
               ), '[]'::jsonb
           )
    INTO v_agenda
    FROM meeting_agenda_items
    WHERE occurrence_id = (
        SELECT id FROM event_occurrences WHERE event_id = p_event_uuid AND occurrence_date = p_occ_date
    ) AND is_deleted = false;

    -- Get participants, or empty array if no data

    SELECT COALESCE(
               jsonb_agg(
                   jsonb_build_object(
				'id', mp.id,
                'user_id', user_id,
                'user_name', CONCAT(u.first_name, ' ', u.last_name)  -- Concatenate first and last name
                )
               ), '[]'::jsonb
           )
    INTO v_participants
    FROM meeting_participants mp
    JOIN users u ON mp.user_id = u.id  -- Join with the users table to get first_name and last_name
    WHERE occurrence_id = (
        SELECT id FROM event_occurrences WHERE event_id = p_event_uuid AND occurrence_date = p_occ_date
    ) AND mp.is_deleted = false;

    -- Get actions, or empty array if no data

    SELECT COALESCE(
               jsonb_agg(
                   jsonb_build_object(
                'id', mat.id,
                'action_description', action_description,
                'assigned_to_user_id', assigned_to_user_id,
                'assigned_to_user_name', CONCAT(u.first_name, ' ', u.last_name),
                'due_date', due_date,
                'status', status
                   )
               ), '[]'::jsonb
           )
    INTO v_actions
    FROM meeting_action_items mat
	JOIN users u ON mat.assigned_to_user_id = u.id  -- Join with the users table to get first_name and last_name
    WHERE occurrence_id = (
        SELECT id FROM event_occurrences WHERE event_id = p_event_uuid AND occurrence_date = p_occ_date
    ) AND mat.is_deleted = false;

    -- Get notes
    SELECT jsonb_agg(
            jsonb_build_object(
                'note_text', note_text
            )
        )
    INTO v_notes
    FROM meeting_notes
    WHERE occurrence_id = (
        SELECT id FROM event_occurrences WHERE event_id = p_event_uuid AND occurrence_date = p_occ_date
    ) AND is_deleted = false;

    -- Return all data in a single JSON object
    RETURN jsonb_build_object(
        'agenda', v_agenda,
        'participants', v_participants,
        'actions', v_actions,
        'notes', v_notes
    );
END;
$function$
