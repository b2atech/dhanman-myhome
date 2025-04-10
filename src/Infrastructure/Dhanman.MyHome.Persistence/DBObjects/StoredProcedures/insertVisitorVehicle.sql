-- PROCEDURE: public.insert_visitor_vehicle(integer, text, text)
-- DROP PROCEDURE IF EXISTS public.insert_visitor_vehicle(integer, text, text);

CREATE OR REPLACE PROCEDURE public.insert_visitor_vehicle(
    IN p_visitor_log_id integer,
    IN p_vehicle_number text DEFAULT NULL::text,
    IN P_vehicle_type text DEFAULT NULL::text
)
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE
    v_created_by UUID;  -- Declare variable to store created_by ID
BEGIN
    -- Fetch created_by from the visitor_log table based on the visitor_log_id
    SELECT created_by INTO v_created_by
    FROM public.visitor_logs
    WHERE id = p_visitor_log_id;

    -- If no created_by found (i.e., visitor_log_id does not exist), set default value (e.g., 0 or NULL)
    IF v_created_by IS NULL THEN
        RAISE NOTICE 'No created_by found for visitor_log_id: %, using default value', p_visitor_log_id;
        v_created_by := 'dd4f94f2-0f79-4748-b94b-bf935e3944c7';  -- System Id
    END IF;

    -- Insert the data into the VisitorVehicles table
    INSERT INTO public.visitor_vehicles (visitor_log_id, vehicle_number, vehicle_type, created_on_utc, created_by)
    VALUES (p_visitor_log_id, p_vehicle_number, P_vehicle_type, NOW(), v_created_by);

    -- Optionally, raise a notice confirming the insertion
    RAISE NOTICE 'Inserted Vehicle: %, % for VisitorLogId: %, CreatedBy: %', 
                 COALESCE(p_vehicle_number, 'NULL'), 
                 COALESCE(P_vehicle_type, 'NULL'), 
                 p_visitor_log_id, 
                 v_created_by;
END;
$BODY$;

ALTER PROCEDURE public.insert_visitor_vehicle(integer, text, text)
    OWNER TO doadmin;
