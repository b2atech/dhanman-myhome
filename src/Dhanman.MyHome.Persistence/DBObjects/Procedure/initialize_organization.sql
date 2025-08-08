CREATE OR REPLACE PROCEDURE public.initialize_organization(IN p_id uuid, IN p_name text, IN p_company_guids text, IN p_company_names text, IN p_user_id uuid, IN p_user_first_name text, IN p_user_last_name text, IN p_phone_number character varying, IN p_email character varying, IN p_created_by uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_company_id uuid;
    v_company_name text;
    v_company_ids uuid[];
    v_company_names text[];
    i integer;
    v_organization_exists boolean;
    v_user_exists boolean;
    v_company_exists boolean;
    v_existing_user_id uuid;
BEGIN
    -- Check if organization already exists
    SELECT EXISTS (
        SELECT 1 FROM public.organizations 
        WHERE id = p_id OR ( id = p_id AND name = p_name)
    ) INTO v_organization_exists;
    
    IF v_organization_exists THEN
        RAISE NOTICE 'Organization with ID % or Name % already exists. Skipping organization creation.', p_id, p_name;
    ELSE
        -- Insert organization if it doesn't exist
        INSERT INTO public.organizations (
            id, name, created_on_utc, created_by
        ) VALUES (
            p_id, p_name, NOW(), p_created_by
        );
        RAISE NOTICE 'Initialized organization: % with ID: %', p_name, p_id;
    END IF;

  -- Parse company IDs and names
    v_company_ids := string_to_array(p_company_guids, ',');
    v_company_names := string_to_array(p_company_names, ',');
   
    -- Initialize companies with duplicate checks
    FOR i IN 1..array_length(v_company_ids, 1) LOOP
        v_company_id := v_company_ids[i];
        v_company_name := v_company_names[i];
      
        -- Check if company already exists
        SELECT EXISTS (
            SELECT 1 FROM public.companies 
            WHERE id = v_company_id 
        ) INTO v_company_exists;
        
        IF NOT v_company_exists THEN
            CALL public.initialize_company(
                v_company_id, 
                p_id,
                true, -- Indicates it's an apartment
                v_company_name, 
                p_created_by
            );
            RAISE NOTICE 'Initialized company: % with ID: %', v_company_name, v_company_id;
        ELSE
            RAISE NOTICE 'Company % (ID: %) already exists for organization %. Skipping initialization.', 
                v_company_name, v_company_id, p_id;
        END IF;
    END LOOP;

   -- Assign the first company ID from the array
    v_company_id := v_company_ids[1];

    -- Check if user already exists and get the existing ID if found
    SELECT id INTO v_existing_user_id FROM public.users 
    WHERE id = p_user_id OR email = p_email
    LIMIT 1;
    
    v_user_exists := (v_existing_user_id IS NOT NULL);
    
    IF NOT v_user_exists THEN
        -- Create user if they don't exist
        INSERT INTO public.users (
            id, email, contact_number, first_name, last_name, 
            password_hash, created_on_utc, created_by, company_id, is_owner
        ) VALUES (
            p_user_id, p_email, p_phone_number, p_user_first_name, p_user_last_name,
            '', NOW(), p_created_by, v_company_id, false
        );
        RAISE NOTICE 'Created user % (%) for organization %', p_email, p_user_id, p_id;
    ELSE
        -- Update existing user's company association
        UPDATE public.users 
        SET company_id = v_company_id
        WHERE id = v_existing_user_id;
		 RAISE NOTICE 'updated user % (%) for organization %', p_email, p_user_id, p_id;
    END IF;
    RAISE NOTICE 'Organization initialization process completed for % (ID: %)', p_name, p_id;
END;
$procedure$
