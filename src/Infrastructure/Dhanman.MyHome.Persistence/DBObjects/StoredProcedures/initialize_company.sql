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