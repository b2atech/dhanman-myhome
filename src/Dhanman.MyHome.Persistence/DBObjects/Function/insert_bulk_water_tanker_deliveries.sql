CREATE OR REPLACE FUNCTION public.insert_bulk_water_tanker_deliveries(p_deliveries jsonb, p_created_by uuid, p_created_on_utc timestamp with time zone)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
DECLARE
    inserted_count INTEGER := 0;
    delivery_record RECORD;
BEGIN
    FOR delivery_record IN
        SELECT *
        FROM jsonb_to_recordset(p_deliveries)
        AS (
            company_id UUID,
            vendor_id UUID,
            delivery_date DATE,
            delivery_time TIME,
            tanker_capacity_liters INTEGER,
            actual_received_liters INTEGER
        )
    LOOP
        INSERT INTO public.water_tanker_deliveries (
            company_id,
            vendor_id,
            delivery_date,
            delivery_time,
            tanker_capacity_liters,
            actual_received_liters,
            created_by,
            created_on_utc
        )
        VALUES (
            delivery_record.company_id,
            delivery_record.vendor_id,
            delivery_record.delivery_date,
            delivery_record.delivery_time,
            delivery_record.tanker_capacity_liters,
            delivery_record.actual_received_liters,
            p_created_by,
            p_created_on_utc
        );

        inserted_count := inserted_count + 1;
    END LOOP;

    RETURN inserted_count;
END;
$function$
