CREATE OR REPLACE FUNCTION public.get_water_tanker_summary(p_company_id uuid, p_start_date date, p_end_date date)
 RETURNS TABLE(start_date date, end_date date, total_tankers integer, total_liters numeric)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT
		p_start_date,
        p_end_date,
        COUNT(*)::INT AS total_tankers,  -- ✅ cast to INT
        COALESCE(SUM(actual_received_liters)::NUMERIC(14,2), 0) AS total_liters
    FROM public.water_tanker_deliveries
    WHERE company_id = p_company_id
      AND delivery_date BETWEEN p_start_date AND p_end_date
      AND NOT is_deleted;
END;
$function$
