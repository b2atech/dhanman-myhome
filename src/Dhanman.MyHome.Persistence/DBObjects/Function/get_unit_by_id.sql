CREATE OR REPLACE FUNCTION public.get_unit_by_id(p_unit_id integer)
 RETURNS TABLE(unit_id integer, unit_name text, floor_id integer, building_id integer, unit_type_id integer, customer_id uuid, occupant_type_id integer, occupancy_type_id integer, area numeric, bhk_type text, phone_extension integer, e_intercom integer, created_on_utc timestamp without time zone, modified_on_utc timestamp without time zone, created_by uuid, modified_by uuid, created_by_user_name text, modified_by_user_name text, customer_name text, customer_contact_number text)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT 
        unit.id AS unit_id,
        unit.name AS unit_name,
        unit.floor_id,
        unit.building_id,
        unit.unit_type_id,
        unit.customer_id,  -- UUID
        unit.occupant_type_id,
        unit.occupancy_type_id,
        unit.area::NUMERIC,  -- Explicit casting to NUMERIC
        unit.bhk_type,
        unit.phone_extention::INT AS phone_extension,  -- Explicit casting to INT
        unit.e_intercom::INT AS e_intercom,  -- Explicit casting to INT
        unit.created_on_utc,
        unit.modified_on_utc,
        unit.created_by,  -- UUID
        unit.modified_by,  -- UUID
        COALESCE(created_by_user.first_name, '') || ' ' || COALESCE(created_by_user.last_name, '') AS created_by_user_name,
        COALESCE(modified_by_user.first_name, '') || ' ' || COALESCE(modified_by_user.last_name, '') AS modified_by_user_name,
        u.first_name || ' ' || u.last_name AS customer_name,
        u.contact_number AS customer_contact_number
    FROM units unit
    LEFT JOIN users created_by_user 
        ON unit.created_by = created_by_user.id
    LEFT JOIN users modified_by_user 
        ON unit.modified_by = modified_by_user.id
    JOIN resident_units ru 
        ON unit.id = ru.unit_id
    JOIN residents r 
        ON ru.resident_id = r.id
    JOIN users u 
        ON r.user_id = u.id
    WHERE unit.id = p_unit_id
      AND ru.is_deleted = false;
END;
$function$
