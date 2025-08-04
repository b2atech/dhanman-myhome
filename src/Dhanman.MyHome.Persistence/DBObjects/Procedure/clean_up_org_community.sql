CREATE OR REPLACE PROCEDURE public.clean_up_org_community(IN p_organization_id uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_company_id uuid;
    v_apartment_id uuid;
    v_unit_id uuid;
    v_resident_id uuid;
    v_visitor_id uuid;
    v_apartment_count integer;
BEGIN
    -- Step 1: Loop through all companies under the organization
    FOR v_company_id IN
        SELECT id FROM companies WHERE organization_id = p_organization_id
    LOOP
        -- ====== Company-level deletes ======
        DELETE FROM users WHERE company_id = v_company_id;
        DELETE FROM events WHERE company_id = v_company_id;
        DELETE FROM event_participants WHERE event_id IN (
            SELECT id FROM events WHERE company_id = v_company_id
        );
        -- (Add other company-level tables here if any)

        -- ====== Apartment-level deletes ======
        v_apartment_count := 0;
        FOR v_apartment_id IN
            SELECT id FROM apartments WHERE id = v_apartment_id
        LOOP
            v_apartment_count := v_apartment_count + 1;
            -- Delete from visitors and their contacts
            FOR v_visitor_id IN
                SELECT id FROM visitors WHERE apartment_id = v_apartment_id
            LOOP
                DELETE FROM visitor_contacts WHERE visitor_id = v_visitor_id;
            END LOOP;
            DELETE FROM visitors WHERE apartment_id = v_apartment_id;

            -- Delete resident_units by residents and units
            FOR v_resident_id IN
                SELECT id FROM residents WHERE apartment_id = v_apartment_id
            LOOP
                DELETE FROM resident_units WHERE resident_id = v_resident_id;
            END LOOP;
            FOR v_unit_id IN
                SELECT id FROM units WHERE apartment_id = v_apartment_id
            LOOP
                DELETE FROM resident_units WHERE unit_id = v_unit_id;
            END LOOP;

            -- Now the rest of the apartment-level deletes
            DELETE FROM units WHERE apartment_id = v_apartment_id;
            DELETE FROM tickets WHERE apartment_id = v_apartment_id;
            DELETE FROM ticket_workflow WHERE apartment_id = v_apartment_id;
            DELETE FROM service_providers WHERE apartment_id = v_apartment_id;
            DELETE FROM residents WHERE apartment_id = v_apartment_id;
            DELETE FROM resident_requests WHERE apartment_id = v_apartment_id;
            DELETE FROM gates WHERE apartment_id = v_apartment_id;
            DELETE FROM floors WHERE apartment_id = v_apartment_id;
            DELETE FROM buildings WHERE apartment_id = v_apartment_id;
            -- (Add other apartment-level tables here if any)
            DELETE FROM apartments WHERE id = v_apartment_id;
        END LOOP;

        IF v_apartment_count = 0 THEN
            RAISE NOTICE 'apartment_id not found for apartment %', v_apartment_id;
        END IF;

        -- ====== Delete company itself (after dependents) ======
        DELETE FROM companies WHERE id = v_company_id;
    END LOOP;

    -- Step 2: Delete the organization itself
    DELETE FROM organizations WHERE id = p_organization_id;

    RAISE NOTICE 'Organization % and all related company/apartment data deleted from Community DB.', p_organization_id;
END;
$procedure$
