-- FUNCTION: public.check_out(integer[])

-- DROP FUNCTION IF EXISTS public.check_out(integer[]);
CREATE OR REPLACE FUNCTION public.check_out(
	p_visitor_log_ids integer[])
    RETURNS SETOF visitor_logs 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY
    UPDATE public.visitor_logs
    SET
        exit_time = (NOW() AT TIME ZONE 'UTC') AT TIME ZONE 'Asia/Kolkata',
        visitor_status_id = 4,
        modified_on_utc = (NOW() AT TIME ZONE 'UTC') AT TIME ZONE 'Asia/Kolkata'
    WHERE id = ANY(p_visitor_log_ids)
    RETURNING *;
END;
$BODY$;

ALTER FUNCTION public.check_out(integer[])
    OWNER TO devdhanman;
