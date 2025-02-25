CREATE OR REPLACE FUNCTION public.insert_pin_record(p_effective_start_date_time timestamp without time zone, p_service_provider_id integer DEFAULT NULL::integer, p_visitor_id integer DEFAULT NULL::integer, p_delivery_id integer DEFAULT NULL::integer, p_effective_end_date_time timestamp without time zone DEFAULT NULL::timestamp without time zone)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
DECLARE
    v_pin_code VARCHAR(6);
    is_unique BOOLEAN := FALSE;
    max_attempts INT := 1000;  -- Limit the number of attempts to avoid infinite loop
    attempts INT := 0;
BEGIN
    -- Loop until a unique PIN is found or max attempts are reached
    WHILE NOT is_unique AND attempts < max_attempts LOOP
        v_pin_code := generate_random_pin();
        
        -- Check if the PIN is unique
        PERFORM 1 FROM pins WHERE pins.pin_code = v_pin_code;
        
        IF NOT FOUND THEN
            is_unique := TRUE;
        ELSE
            attempts := attempts + 1;
        END IF;
    END LOOP;

    IF NOT is_unique THEN
        RAISE EXCEPTION 'Could not generate a unique PIN after % attempts', max_attempts;
    END IF;

    -- Insert the new record
    INSERT INTO pins (service_provider_id, visitor_id, delivery_id, pin_code, effective_start_date_time, effective_end_date_time)
    VALUES (p_service_provider_id, p_visitor_id, p_delivery_id, v_pin_code, p_effective_start_date_time, p_effective_end_date_time);
END;
$function$