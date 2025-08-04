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
