CREATE OR REPLACE FUNCTION public.generate_random_pin()
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
    pin_code VARCHAR(6);
BEGIN
    -- Generate a random 6-digit number
    pin_code := LPAD(CAST(FLOOR(RANDOM() * 1000000) AS VARCHAR), 6, '0');
    RETURN pin_code;
END;
$function$