CREATE OR REPLACE FUNCTION public.get_unit_info_by_user_apartment(p_user_id uuid, p_apartment_id uuid)
 RETURNS TABLE(id integer, unit_id integer, unit_name text, user_name text, apartment_id uuid)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT
        ru.id,          -- added resident_units primary key
        ru.unit_id,
        u.name,
        usr.first_name || ' ' || usr.last_name AS user_name,
        r.apartment_id
    FROM resident_units ru
    INNER JOIN residents r ON ru.resident_id = r.id
    INNER JOIN units u ON ru.unit_id = u.id
    INNER JOIN users usr ON r.user_id = usr.id
    WHERE r.user_id = p_user_id
      AND r.apartment_id = p_apartment_id;
END;
$function$
