CREATE OR REPLACE FUNCTION public.get_water_tanker_deliveries(p_company_id uuid, p_start_date date, p_end_date date, p_vendor_id uuid DEFAULT NULL::uuid)
 RETURNS TABLE(id integer, delivery_date date, delivery_time time without time zone, vendor_id uuid, vendor_name text, tanker_capacity_liters integer, actual_received_liters integer, created_by uuid, created_by_name text, created_on_utc timestamp with time zone, modified_by uuid, modified_by_name text, modified_on_utc timestamp with time zone)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT
        d.id,
        d.delivery_date,
        d.delivery_time,
        d.vendor_id,
        vu.first_name || ' ' || vu.last_name AS vendor_name,
        d.tanker_capacity_liters,
        d.actual_received_liters,
        d.created_by,
        cu.first_name || ' ' || cu.last_name AS created_by_name,
        d.created_on_utc,
        d.modified_by,
        mu.first_name || ' ' || mu.last_name AS modified_by_name,
        d.modified_on_utc
    FROM public.water_tanker_deliveries d
    LEFT JOIN public.users vu ON vu.id = d.vendor_id AND NOT vu.is_deleted
    LEFT JOIN public.users cu ON cu.id = d.created_by AND NOT cu.is_deleted
	LEFT JOIN public.users mu ON mu.id = d.modified_by AND NOT mu.is_deleted
    WHERE d.company_id = p_company_id
      AND (p_vendor_id IS NULL OR d.vendor_id = p_vendor_id)
      AND d.delivery_date BETWEEN p_start_date AND p_end_date
      AND NOT d.is_deleted
    ORDER BY d.delivery_date, d.delivery_time;
END;
$function$
