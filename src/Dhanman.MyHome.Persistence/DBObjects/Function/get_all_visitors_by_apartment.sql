CREATE OR REPLACE FUNCTION public.get_all_visitors_by_apartment(p_apartment_id uuid)
 RETURNS TABLE(id integer, first_name text, last_name text, email text, visiting_from text, contact_number text, visitor_type_id integer, visitor_type_name character varying, vehicle_number text, identity_type_id integer, identity_number text, created_by uuid, created_on_utc timestamp without time zone, modified_by uuid, modified_on_utc timestamp without time zone)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT
        v.id,
        v.first_name,
        v.last_name,
        v.email,
        v.visiting_from,
        v.contact_number,
        v.visitor_type_id,
        vt.name AS visitor_type_name,
        v.vehicle_number,
        v.identity_type_id,
        v.identity_number,
        v.created_by,
        v.created_on_utc,
        v.modified_by,
        v.modified_on_utc
    FROM
        public.visitors v
    INNER JOIN
        public.visitor_types vt ON v.visitor_type_id = vt.id
    WHERE
        v.apartment_id = p_apartment_id
        AND v.is_deleted = false
        AND (vt.is_deleted = false OR vt.is_deleted IS NULL);
END;
$function$
