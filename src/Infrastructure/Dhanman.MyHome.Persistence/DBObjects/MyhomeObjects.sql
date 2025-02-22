CREATE OR REPLACE PROCEDURE public.initialize_company(IN p_company_id uuid, IN p_organization_id uuid, IN p_is_apartment boolean, IN p_name text, IN p_created_by uuid)
 LANGUAGE plpgsql
AS $procedure$
  
BEGIN
-- Insert into companies table
   INSERT INTO public.companies (
        id, 
        organization_id, 
		is_apartment,
        name,        
        created_on_utc, 
        created_by
    ) VALUES (
        p_company_id, 
        p_organization_id, 
		p_is_apartment,
        p_name,
        NOW(), 
        p_created_by 
    ); 

END;
$procedure$

CREATE OR REPLACE PROCEDURE public.hard_delete_organization(IN p_organization_id uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    p_company_id UUID; -- Variable to hold company IDs associated with the organization
BEGIN
    -- Get the associated company IDs for the organization
    FOR p_company_id IN
        SELECT c.id FROM public.companies c WHERE c.organization_id = p_organization_id
    LOOP     
		 -- Delete users associated with the company
		DELETE FROM public.users
		WHERE users.company_id = p_company_id;
	
        -- Delete from companies
        DELETE FROM public.companies
        WHERE public.companies.id = p_company_id;
    END LOOP;  

    -- Delete the organization itself
    DELETE FROM public.organizations
    WHERE public.organizations.id = p_organization_id;

    -- Log the operation
    RAISE NOTICE 'Organization with ID % and all related data have been hard deleted.', p_organization_id;

END;
$procedure$

CREATE OR REPLACE PROCEDURE public.create_service_provider(IN p_first_name character varying, IN p_last_name character varying, IN p_email character varying, IN p_visiting_from character varying, IN p_contact_number character varying, IN p_permanent_address_id uuid, IN p_present_address_id uuid, IN p_service_provider_type_id integer, IN p_service_provider_sub_type_id integer, IN p_vehicle_number character varying, IN p_identity_type_id integer, IN p_identity_number character varying, IN p_validity_date date, IN p_police_verification_status boolean, IN p_is_hireable boolean, IN p_is_visible boolean, IN p_is_frequent_visitor boolean, IN p_apartment_id uuid, IN p_created_by uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    pin_code character varying;
BEGIN
    -- Generate the random pin code
    pin_code := public.generate_random_pin();

    INSERT INTO public.service_providers (
        first_name,
        last_name,
        email,
        visiting_from,
        contact_number,
        permanent_address_id,
        present_address_id,
        service_provider_type_id,
        service_provider_sub_type_id,
        vehicle_number,
        identity_type_id,
        identity_number,
        validity_date,
        policeverification_status,
        is_hireable,
        is_visible,
        is_frequent_visitor,
        apartment_id,
        pin,
        created_on_utc,
        created_by
    )
    VALUES (
        p_first_name,
        p_last_name,
        p_email,
        p_visiting_from,
        p_contact_number,
        p_permanent_address_id,
        p_present_address_id,
        p_service_provider_type_id,
        p_service_provider_sub_type_id,
        p_vehicle_number,
        p_identity_type_id,
        p_identity_number,
        p_validity_date,
        p_police_verification_status,
        p_is_hireable,
        p_is_visible,
        p_is_frequent_visitor,
        p_apartment_id,
        pin_code,
        current_timestamp,
        p_created_by
    );
END; 
$procedure$

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

CREATE OR REPLACE PROCEDURE public.initialize_organization(IN p_id uuid, IN p_name text, IN p_company_guids text, IN p_company_names text, IN p_user_id uuid, IN p_user_first_name text, IN p_user_last_name text, IN p_phone_number character varying, IN p_email character varying, IN p_created_by uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_company_id uuid;              -- Variable for iterating through company IDs
    v_company_name text;            -- Variable for iterating through company names
    v_company_ids uuid[];           -- Array to hold parsed company IDs
    v_company_names text[];         -- Array to hold parsed company names
    i integer;  
BEGIN
	-- Parse company IDs and names
    v_company_ids := string_to_array(p_company_guids, ',');
    v_company_names := string_to_array(p_company_names, ',');

   -- Insert into organizations table
   INSERT INTO public.organizations (
        id,         
        name,        
        created_on_utc, 
        created_by
    ) VALUES (
        p_id,       
        p_name,      
        NOW(), 
        p_created_by 
    );

	RAISE NOTICE 'Initialized organization: % with ID: %', p_name, p_id;

	 -- Loop through each company and initialize
    FOR i IN 1..array_length(v_company_ids, 1) LOOP
        v_company_id := v_company_ids[i];
        v_company_name := v_company_names[i];

        -- Call initialize_company for each company
        CALL public.initialize_company(
            v_company_id, 
            p_id,          -- Organization ID
            true,          -- Indicates it's an apartment
            v_company_name, 
            p_created_by
        );

        RAISE NOTICE 'Initialized company: % with ID: %', v_company_name, v_company_id;
    END LOOP;

	 -- Assign the first company ID from the array
    v_company_id := v_company_ids[1];

	-- Insert into users table (create the user for this organization and link to address)
    INSERT INTO public.users (
        id, 
        email, 
        contact_number, 
        first_name, 
        last_name, 
        password_hash, 
        created_on_utc, 
        created_by,
        company_id,                              -- Pass the company ID to associate the user
	    is_owner   
	) VALUES (
	    p_user_id,                           -- Generated user ID
	    p_email,			                  -- Email of the user
	    p_phone_number,                      -- p_phone_number (Phone number of the user)
	    p_user_first_name,
	    p_user_last_name,                                  -- Last name of the user (if not provided)
	    '',                                  -- Default empty password hash
	    NOW(),                               -- Created on timestamp
	    p_created_by,                        -- Created by
	    v_company_id,                        -- Link user to the company
		false                                
	);
		
END;
$procedure$
