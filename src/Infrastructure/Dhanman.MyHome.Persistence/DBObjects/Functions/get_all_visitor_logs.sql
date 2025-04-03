-- FUNCTION: public.get_all_visitor_logs(uuid, timestamp without time zone)

-- DROP FUNCTION IF EXISTS public.get_all_visitor_logs(uuid, timestamp without time zone);

CREATE OR REPLACE FUNCTION public.get_all_visitor_logs(
	v_apartment_id uuid,
	log_date timestamp without time zone)
    RETURNS TABLE(id integer, visitor_id integer, first_name text, last_name text, unit_id integer, unit_name text, latest_entry_time timestamp without time zone, latest_exit_time timestamp without time zone, visiting_from text, contact_number text, visitor_type_id integer, visitor_type_name text) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY
    SELECT 
		vl.id as id,
        v.id AS visitor_id,
        v.first_name,
        v.last_name,
        vul.unit_id,
        u.name AS unit_name,
        MAX(vl.entry_time) AS latest_entry_time,
        MAX(vl.exit_time) AS latest_exit_time,
        vl.visiting_from,
        v.contact_number,
        v.visitor_type_id,
		vt.name :: text
    FROM 
        visitors v
    JOIN 
        visitor_logs vl ON v.id = vl.visitor_id
    LEFT JOIN 
        visitor_unit_logs vul ON vul.visitor_log_id = vl.id  -- Join visitor_logs to visitor_unit_logs
    LEFT JOIN 
        units u ON u.id = vul.unit_id  -- Join visitor_unit_logs to units to get unit_name
	LEFT JOIN 
        visitor_types vt ON vt.id = v.visitor_type_id  
    WHERE 
        v.apartment_id = v_apartment_id  -- Use the renamed parameter 'v_apartment_id'
        AND DATE(vl.entry_time) = DATE(log_date) -- Match the date part of timestamp
        AND v.is_deleted = FALSE
        AND vl.is_deleted = FALSE
        AND vul.is_deleted = FALSE  -- Ensure visitor_unit_logs is not deleted
    GROUP BY 
        vl.Id, v.id, v.first_name, v.last_name, vul.unit_id, u.name, v.identity_number,
        vl.visiting_from, v.contact_number, v.visitor_type_id, vt.name;
END;
$BODY$;

ALTER FUNCTION public.get_all_visitor_logs(uuid, timestamp without time zone)
    OWNER TO devdhanman;
