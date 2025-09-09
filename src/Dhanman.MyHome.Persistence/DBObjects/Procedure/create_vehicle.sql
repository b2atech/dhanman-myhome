CREATE OR REPLACE PROCEDURE public.create_vehicle(IN p_vehicle_number text, IN p_vehicle_type_id integer, IN p_unit_id integer, IN p_vehicle_rf_id text, IN p_vehicle_rf_id_secretcode text, IN p_created_by uuid)
 LANGUAGE plpgsql
AS $procedure$
BEGIN
    INSERT INTO public.vehicles (
        vehicle_number,
        vehicle_type_id,
        unit_id,
        vehicle_rf_id,
        vehicle_rf_id_secretcode,
        created_on_utc,
        is_deleted,
        created_by
    ) VALUES (
        p_vehicle_number,
        p_vehicle_type_id,
        p_unit_id,
        p_vehicle_rf_id,
        p_vehicle_rf_id_secretcode,
        now() at time zone 'utc',
        false,
        p_created_by
    );
END;
$procedure$
