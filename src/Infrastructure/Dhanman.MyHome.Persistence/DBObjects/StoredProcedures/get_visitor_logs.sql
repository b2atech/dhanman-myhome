-- FUNCTION: public.get_visitor_logs(uuid, integer, integer)

-- DROP FUNCTION IF EXISTS public.get_visitor_logs(uuid, integer, integer);

CREATE OR REPLACE FUNCTION public.get_visitor_logs(
	p_apartment_id uuid,
	p_visitor_id integer,
	p_visitor_type_id integer)
    RETURNS TABLE(visitor_log_id integer, visitor_id integer, visitor_first_name text, unit_id integer, unit_name text, visitor_type_id integer, visitor_type_name text, visiting_from text, current_status_id integer, entry_time timestamp without time zone, exit_time timestamp without time zone) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

BEGIN
    RETURN QUERY
    SELECT
        vl.id AS visitor_log_id,
        vl.visitor_id,
        v.first_name AS visitor_first_name,
        u.id AS unit_id,
        u.name AS unit_name,
        vl.visitor_type_id,
        (SELECT vt.name::TEXT  -- Explicitly cast visitor type name to TEXT
         FROM visitor_types vt
         WHERE vt.id = vl.visitor_type_id
         LIMIT 1) AS visitor_type_name,
        vl.visiting_from,
        vl.current_status_id,
        vl.entry_time,
        vl.exit_time
    FROM visitor_logs vl
    INNER JOIN visitors v ON vl.visitor_id = v.id
    INNER JOIN visitor_unit_logs vut ON vl.id = vut.visitor_log_id
    INNER JOIN units u ON vut.unit_id = u.id
    WHERE v.apartment_id = p_apartment_id
      AND vl.visitor_id = p_visitor_id
      AND vl.visitor_type_id = p_visitor_type_id
    ORDER BY vl.id;
END;
$BODY$;

ALTER FUNCTION public.get_visitor_logs(uuid, integer, integer)
    OWNER TO devdhanman;
