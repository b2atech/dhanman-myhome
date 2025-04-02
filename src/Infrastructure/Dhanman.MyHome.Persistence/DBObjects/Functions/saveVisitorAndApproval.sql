-- FUNCTION: public.save_visitor_and_approval(uuid, text, text, integer, integer, date, date, time without time zone, time without time zone, uuid)

-- DROP FUNCTION IF EXISTS public.save_visitor_and_approval(uuid, text, text, integer, integer, date, date, time without time zone, time without time zone, uuid);

CREATE OR REPLACE FUNCTION public.save_visitor_and_approval(
	p_apartment_id uuid,
	p_first_name text,
	p_contact_number text,
	p_visitor_type_id integer,
	p_visit_type_id integer,
	p_start_date date,
	p_end_date date,
	p_entry_time time without time zone,
	p_exit_time time without time zone,
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

    -- If visitor exists, use the existing visitor_id, else insert a new visitor
    IF NOT FOUND THEN
        -- Insert into visitors table (auto-generated id will be used)
        INSERT INTO public.visitors (
            first_name,
            contact_number,
            visitor_type_id,
            created_on_utc,
            created_by,
            apartment_id
        )
        VALUES (
            p_first_name,
            p_contact_number,
            p_visitor_type_id,
            NOW(),
            p_created_by,
            p_apartment_id
        )
        RETURNING id INTO v_visitor_id;  -- Capture the auto-generated id
    END IF;

    -- Insert into visitor_approvals table using the visitor_id
    INSERT INTO public.visitor_approvals (
        visitor_id,
        visit_type_id,
        start_date,
        end_date,
        entry_time,
        exit_time,
        created_on_utc,
        created_by
    )
    VALUES (
        v_visitor_id,
        p_visit_type_id,
        p_start_date,
        p_end_date,
        p_entry_time,
        p_exit_time,
        NOW(),
        p_created_by
    );
END;
$BODY$;

ALTER FUNCTION public.save_visitor_and_approval(uuid, text, text, integer, integer, date, date, time without time zone, time without time zone, uuid)
    OWNER TO devdhanman;
