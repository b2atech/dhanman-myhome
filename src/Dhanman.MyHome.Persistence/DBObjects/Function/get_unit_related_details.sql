CREATE OR REPLACE FUNCTION public.get_unit_related_details(unit_id integer)
 RETURNS jsonb
 LANGUAGE sql
AS $function$
  SELECT jsonb_build_object(
    'id', u.id,
    'name', u.name,
    'unit_type_id', u.unit_type_id,
    'phone_extension', u.phone_extention,
    'area', u.area,
    'bhk_type', u.bhk_type,
    'occupant_type_id', u.occupant_type_id,
    'occupancy_type_id', u.occupancy_type_id,
    'residents', COALESCE(
      (SELECT jsonb_agg(DISTINCT jsonb_build_object(
        'id', r.id,
        'first_name', r.first_name,
        'last_name', r.last_name,
        'email', r.email,
        'contact_number', r.contact_number,
        'resident_type', rt.name
      ))
       FROM resident_units ru
       JOIN residents r ON r.id = ru.resident_id AND r.is_deleted = false
       JOIN resident_types rt ON rt.id = r.resident_type_id AND rt.is_deleted = false
       WHERE ru.unit_id = u.id AND ru.is_deleted = false),
      '[]'::jsonb),
    'vehicles', COALESCE(
      (SELECT jsonb_agg(DISTINCT jsonb_build_object(
        'id', v.id,
        'vehicle_number', v.vehicle_number,
        'vehicle_type', vt.name
      ))
       FROM vehicles v
       JOIN vehicle_types vt ON vt.id = v.vehicle_type_id AND vt.is_deleted = false
       WHERE v.unit_id = u.id AND v.is_deleted = false),
      '[]'::jsonb)
  )
  FROM units u
  WHERE u.id = unit_id AND u.is_deleted = false;
$function$
