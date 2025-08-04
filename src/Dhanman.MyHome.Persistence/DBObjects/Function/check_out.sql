CREATE OR REPLACE FUNCTION public.check_out(p_visitor_log_ids integer[])
 RETURNS SETOF visitor_logs
 LANGUAGE plpgsql
AS $function$
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
$function$
