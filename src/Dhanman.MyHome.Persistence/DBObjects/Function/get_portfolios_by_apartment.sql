CREATE OR REPLACE FUNCTION public.get_portfolios_by_apartment(p_apartment_id uuid)
 RETURNS TABLE(id integer, apartment_id uuid, name text, description text)
 LANGUAGE sql
AS $function$
    SELECT id, apartment_id, name, description
    FROM public.portfolios
    WHERE apartment_id = p_apartment_id
    ORDER BY id;
$function$
