-- FUNCTION: public.save_visitor_and_pending(uuid, text, text, text, text, text, integer, text, integer, text, uuid)

-- DROP FUNCTION IF EXISTS public.save_visitor_and_pending(uuid, text, text, text, text, text, integer, text, integer, text, uuid);

CREATE OR REPLACE FUNCTION public.save_visitor_and_pending(
	p_apartment_id uuid,
	p_first_name text,
	p_last_name text,
	p_email text,
	p_contact_number text,
	p_vehicle_number text,
	p_identity_type_id integer,
	p_identity_number text,
	p_visitor_type_id integer,
	p_visiting_from text,
	p_created_by uuid)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_visitor_id INTEGER;
BEGIN
    -- Check if the visitor already exists based on first_name and contact_number
    SELECT id INTO v_visitor_id
    FROM public.visitors
    WHERE first_name = p_first_name
      AND contact_number = p_contact_number
      AND is_deleted = false;

IF FOUND THEN
        RAISE NOTICE 'Visitor already exists with ID: %, using existing ID.', v_visitor_id;
    ELSE
        RAISE NOTICE 'Visitor does not exist. Inserting new visitor.';
    END IF;
    -- If visitor exists, use the existing visitor_id, else insert a new visitor
    IF NOT FOUND THEN
        -- Insert into visitors table (auto-generated id will be used)
        INSERT INTO public.visitors (
            first_name,
			last_name,
			email,
            contact_number,
			visiting_from,
            visitor_type_id,
			vehicle_number,
			identity_type_id,
			identity_number,
            created_on_utc,
            created_by,
            apartment_id
        )
        VALUES (
            p_first_name,
			p_last_name,
			p_email,
            p_contact_number,
			p_visiting_from,
            p_visitor_type_id,
			p_vehicle_number,
			p_identity_type_id,
			p_identity_number,
            NOW(),
            p_created_by,
            p_apartment_id
        )
        RETURNING id INTO v_visitor_id;  -- Capture the auto-generated id
		RAISE NOTICE 'New visitor inserted with ID: %', v_visitor_id;
    END IF;

    -- Insert into visitor_approvals table using the visitor_id
    INSERT INTO public.visitor_logs (
        visitor_id,
        visitor_type_id,
		visiting_from,
		visitor_status_id,
        entry_time,
        exit_time,
        created_on_utc,
        created_by
    )
    VALUES (
        v_visitor_id,
        p_visitor_type_id,
        p_visiting_from,
		1,
        null,
        null,
        NOW(),
        p_created_by
    );
	RAISE NOTICE 'Visitor log created for visitor ID: %', v_visitor_id;
END;
$BODY$;

ALTER FUNCTION public.save_visitor_and_pending(uuid, text, text, text, text, text, integer, text, integer, text, uuid)
    OWNER TO devdhanman;
