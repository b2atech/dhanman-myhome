CREATE OR REPLACE FUNCTION public.get_visitor_multiple_unit_logs(v_apartment_id uuid, log_date timestamp without time zone)
 RETURNS TABLE(id integer, visitor_id integer, first_name text, last_name text, unit_id integer, unit_name text, latest_entry_time timestamp without time zone, latest_exit_time timestamp without time zone, visiting_from text, contact_number text, visitor_type_id integer, visitor_type_name text)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT 
        vl.id AS id,
        v.id AS visitor_id,
        v.first_name,
        v.last_name,
        muv.unit_id,
        u.name AS unit_name,
        MAX(vl.entry_time) AS latest_entry_time,
        MAX(vl.exit_time) AS latest_exit_time,
        vl.visiting_from,
        v.contact_number,
        v.visitor_type_id,
        vt.name :: text AS visitor_type_name
    FROM 
        visitors v
    JOIN 
        visitor_logs vl ON v.id = vl.visitor_id
    LEFT JOIN 
        multi_unit_visits muv ON muv.visitor_log_id = vl.id AND muv.is_deleted = FALSE
    LEFT JOIN 
        units u ON u.id = muv.unit_id
    LEFT JOIN 
        visitor_types vt ON vt.id = v.visitor_type_id  
    WHERE 
        v.apartment_id = v_apartment_id
        AND DATE(vl.entry_time) = DATE(log_date)
        AND v.is_deleted = FALSE
        AND vl.is_deleted = FALSE
    GROUP BY 
        vl.id, v.id, v.first_name, v.last_name, muv.unit_id, u.name, 
        vl.visiting_from, v.contact_number, v.visitor_type_id, vt.name;
END;
$function$
