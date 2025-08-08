CREATE OR REPLACE PROCEDURE public.hard_delete_org_community(IN p_organization_id uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_apartment_id uuid;
BEGIN
    -- Step 1: Loop over all "apartments" (companies) under this org
    FOR v_apartment_id IN
        SELECT id FROM companies WHERE organization_id = p_organization_id
    LOOP
        -- Delete data linked to apartment (i.e., company)
        DELETE FROM visitors WHERE apartment_id = v_apartment_id;
        DELETE FROM units WHERE apartment_id = v_apartment_id;
        DELETE FROM tickets WHERE apartment_id = v_apartment_id;
        DELETE FROM ticket_workflow WHERE apartment_id = v_apartment_id;
        DELETE FROM service_providers WHERE apartment_id = v_apartment_id;
        DELETE FROM residents WHERE apartment_id = v_apartment_id;
        DELETE FROM resident_requests WHERE apartment_id = v_apartment_id;
        DELETE FROM gates WHERE apartment_id = v_apartment_id;
        DELETE FROM floors WHERE apartment_id = v_apartment_id;
        DELETE FROM buildings WHERE apartment_id = v_apartment_id;

        -- Apartment entity (i.e., company-level data)
        DELETE FROM apartments WHERE id = v_apartment_id;
        DELETE FROM events WHERE company_id = v_apartment_id;
        DELETE FROM users WHERE company_id = v_apartment_id;
        DELETE FROM companies WHERE id = v_apartment_id;
    END LOOP;

    -- Step 2: Delete the organization
    DELETE FROM organizations WHERE id = p_organization_id;

    RAISE NOTICE 'Deleted community data for organization_id: %', p_organization_id;
END;
$procedure$
