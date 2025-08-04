CREATE OR REPLACE PROCEDURE public.hard_delete_apartment_data(IN p_apartment_id uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_company_id uuid;
BEGIN
    -- Get the company_id from the apartment
    SELECT company_id INTO v_company_id
    FROM apartments
    WHERE id = p_apartment_id;

    -- === Delete apartment-linked data ===

    DELETE FROM visitors
    WHERE apartment_id = p_apartment_id;

    DELETE FROM units
    WHERE apartment_id = p_apartment_id;

    DELETE FROM tickets
    WHERE apartment_id = p_apartment_id;

    DELETE FROM ticket_workflow
    WHERE apartment_id = p_apartment_id;

    DELETE FROM service_providers
    WHERE apartment_id = p_apartment_id;

    DELETE FROM residents
    WHERE apartment_id = p_apartment_id;

    DELETE FROM resident_requests
    WHERE apartment_id = p_apartment_id;

    DELETE FROM gates
    WHERE apartment_id = p_apartment_id;

    DELETE FROM floors
    WHERE apartment_id = p_apartment_id;

    DELETE FROM buildings
    WHERE apartment_id = p_apartment_id;

    -- === Delete company-linked data (derived from apartment) ===

    IF v_company_id IS NOT NULL THEN
        DELETE FROM events
        WHERE company_id = v_company_id;

        DELETE FROM users
        WHERE company_id = v_company_id;
    END IF;

    -- Delete apartment record last
    DELETE FROM apartments
    WHERE id = p_apartment_id;

    RAISE NOTICE 'Deleted data for apartment_id: % and company_id: %', p_apartment_id, v_company_id;
END;
$procedure$
