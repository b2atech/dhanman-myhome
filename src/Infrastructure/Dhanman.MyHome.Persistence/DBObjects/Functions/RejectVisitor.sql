-- FUNCTION: public.reject(integer)

-- DROP FUNCTION IF EXISTS public.reject(integer);

CREATE OR REPLACE FUNCTION public.reject(
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
        visitor_status_id = 6,
        modified_on_utc = (NOW() AT TIME ZONE 'UTC') AT TIME ZONE 'Asia/Kolkata'
    WHERE id = p_visitor_log_id
    RETURNING * INTO updated_log;

    RETURN updated_log;
END;
$BODY$;

ALTER FUNCTION public.reject(integer)
    OWNER TO devdhanman;
