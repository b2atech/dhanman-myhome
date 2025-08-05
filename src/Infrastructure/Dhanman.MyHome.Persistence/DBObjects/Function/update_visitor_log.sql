CREATE OR REPLACE FUNCTION public.update_visitor_log(p_visitor_log_id integer)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
    UPDATE public.visitor_logs
    SET
        exit_time = NOW(),
        visitor_status_id = 4,
        modified_on_utc = NOW()
    WHERE id = p_visitor_log_id;
END;
$function$
