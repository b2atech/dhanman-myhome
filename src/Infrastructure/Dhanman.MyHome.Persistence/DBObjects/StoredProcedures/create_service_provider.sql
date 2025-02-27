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