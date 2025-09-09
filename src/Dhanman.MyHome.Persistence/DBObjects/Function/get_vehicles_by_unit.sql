CREATE OR REPLACE FUNCTION public.get_vehicles_by_unit(p_unit_id integer)
 RETURNS TABLE(id integer, vehicle_number text, vehicle_type_id integer, unit_id integer, vehicle_rf_id text, vehicle_rf_id_secretcode text, created_on_utc timestamp without time zone, modified_on_utc timestamp without time zone, deleted_on_utc timestamp without time zone, is_deleted boolean, created_by uuid, modified_by uuid)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT 
        v.id, v.vehicle_number, v.vehicle_type_id, v.unit_id,
        v.vehicle_rf_id, v.vehicle_rf_id_secretcode,
        v.created_on_utc, v.modified_on_utc, v.deleted_on_utc,
        v.is_deleted, v.created_by, v.modified_by
    FROM public.vehicles v
    WHERE v.is_deleted = false
      AND v.unit_id = p_unit_id;
END;
$function$
