-- FUNCTION: public.update_visitor_approval(integer, integer, date, date, time without time zone, time without time zone, text, text, uuid)

-- DROP FUNCTION IF EXISTS public.update_visitor_approval(integer, integer, date, date, time without time zone, time without time zone, text, text, uuid);

CREATE OR REPLACE FUNCTION public.update_visitor_approval(
	p_visitor_approval_id integer,
	p_visit_type_id integer,
	p_start_date date,
	p_end_date date,
	p_entry_time time without time zone,
	p_exit_time time without time zone,
	p_vehicle_number text,
	p_company_name text,
	p_created_by uuid)
    RETURNS TABLE(visitor_approve_id integer, visitor_id integer, visit_type_id integer, start_date date, end_date date, entry_time time without time zone, exit_time time without time zone, vehicle_number text, company_name text, created_on_utc timestamp without time zone, modified_on_utc timestamp without time zone, created_by uuid, modified_by uuid) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    -- Update the record
    UPDATE public.visitor_approvals va
    SET 
        visit_type_id = p_visit_type_id,
        start_date = COALESCE(p_start_date, va.start_date),
        end_date = COALESCE(p_end_date, va.end_date),
        entry_time = COALESCE(p_entry_time, va.entry_time),
        exit_time = COALESCE(p_exit_time, va.exit_time),
        vehicle_number = COALESCE(p_vehicle_number, va.vehicle_number),
        company_name = COALESCE(p_company_name, va.company_name),
        modified_on_utc = NOW(),
        modified_by = p_created_by
    WHERE va.id = p_visitor_approval_id;

    -- Check if update happened
    IF NOT FOUND THEN
        RAISE EXCEPTION 'Visitor approval not found for id: %', p_visitor_approval_id;
    END IF;

    -- Return the updated row
    RETURN QUERY 
    SELECT 
        va.id AS visitor_approve_id,
        va.visitor_id,
        va.visit_type_id,
        va.start_date,
        va.end_date,
        va.entry_time,
        va.exit_time,
        va.vehicle_number,
        va.company_name,
        va.created_on_utc,
        va.modified_on_utc,
        va.created_by,
        va.modified_by
    FROM public.visitor_approvals va
    WHERE va.id = p_visitor_approval_id;
END;
$BODY$;

ALTER FUNCTION public.update_visitor_approval(integer, integer, date, date, time without time zone, time without time zone, text, text, uuid)
    OWNER TO doadmin;
