-- FUNCTION: public.check_out(integer)

-- DROP FUNCTION IF EXISTS public.check_out(integer);

CREATE OR REPLACE FUNCTION public.check_out(
	p_visitor_log_id integer)
    RETURNS visitor_logs
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    updated_log visitor_logs;
BEGIN
    UPDATE public.visitor_logs
    SET
        exit_time = (NOW() AT TIME ZONE 'UTC') AT TIME ZONE 'Asia/Kolkata',
        visitor_status_id = 4,
        modified_on_utc = (NOW() AT TIME ZONE 'UTC') AT TIME ZONE 'Asia/Kolkata'
    WHERE id = p_visitor_log_id
    RETURNING * INTO updated_log;

    RETURN updated_log;
END;
$BODY$;

ALTER FUNCTION public.check_out(integer)
    OWNER TO devdhanman;
