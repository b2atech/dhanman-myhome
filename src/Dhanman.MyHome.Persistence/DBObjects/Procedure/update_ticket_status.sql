CREATE OR REPLACE PROCEDURE public.update_ticket_status(IN p_apartment_id uuid, IN p_ticket_status_id integer, IN p_ticket_ids uuid[], IN p_modified_by uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_ticket RECORD;
BEGIN
    -- Process each ticket in the given list
    FOR v_ticket IN
        SELECT id, ticket_status_id
        FROM public.tickets
        WHERE id = ANY(p_ticket_ids)
    LOOP
        -- Validate that the status transition is allowed
        IF EXISTS (
            SELECT 1 FROM public.ticket_workflow
            WHERE status = v_ticket.ticket_status_id
            AND next_status = p_ticket_status_id
            AND apartment_id = p_apartment_id
        ) THEN
            -- Update the ticket status
            UPDATE public.tickets
            SET ticket_status_id = p_ticket_status_id,
                modified_by = p_modified_by,
                modified_on_utc = now()
            WHERE id = v_ticket.id;

            RAISE NOTICE 'Ticket % updated to status %', v_ticket.id, p_ticket_status_id;
        ELSE
            RAISE WARNING 'Invalid status transition for Ticket ID: %', v_ticket.id;
        END IF;
    END LOOP;
END;
$procedure$
