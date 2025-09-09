CREATE OR REPLACE FUNCTION public.get_all_apartment_or_org_residents(apartmentid uuid, isgetall boolean)
 RETURNS TABLE(id integer, resident_name text, user_id uuid)
 LANGUAGE plpgsql
AS $function$
DECLARE
    org_id uuid;
BEGIN
    IF isGetAll THEN
        SELECT a.organization_id INTO org_id
        FROM public.apartments a
        WHERE a.id = apartmentid
          AND a.is_deleted = false;
        RETURN QUERY
        SELECT 
            row_number() OVER ()::int AS id,
            r.first_name || ' ' || r.last_name AS resident_name,
            r.user_id
        FROM public.apartments a
        INNER JOIN public.residents r ON r.apartment_id = a.id
        WHERE a.organization_id = org_id
          AND r.is_deleted = false
          AND a.is_deleted = false;
    ELSE
        RETURN QUERY
        SELECT 
            row_number() OVER ()::int AS id,
            r.first_name || ' ' || r.last_name AS resident_name,
            r.user_id
        FROM public.apartments a
        INNER JOIN public.residents r ON r.apartment_id = a.id
        WHERE a.id = apartmentid
          AND r.is_deleted = false
          AND a.is_deleted = false;
    END IF;
END;
$function$
