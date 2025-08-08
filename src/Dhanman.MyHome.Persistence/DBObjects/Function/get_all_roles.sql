CREATE OR REPLACE FUNCTION public.get_all_roles()
 RETURNS TABLE(id integer, name character varying, description character varying)
 LANGUAGE sql
AS $function$
    SELECT id, name, description
    FROM public.roles
    WHERE true
    ORDER BY id;
$function$
