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