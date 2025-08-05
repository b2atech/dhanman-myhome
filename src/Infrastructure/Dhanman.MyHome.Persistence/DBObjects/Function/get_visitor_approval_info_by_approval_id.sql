CREATE OR REPLACE FUNCTION public.get_visitor_approval_info_by_approval_id(approval_id integer)
 RETURNS TABLE(visitor_id integer, first_name text, last_name text, contact_number text, start_date date, end_date date, entry_time time without time zone, exit_time time without time zone, created_by_first_name text, created_by_last_name text, unit_name text, apartment_name text, city_name text)
 LANGUAGE plpgsql
AS $function$
BEGIN
    -- Correct syntax for returning the query result
    RETURN QUERY
    SELECT 
        v.id AS visitor_id,
        v.first_name,
        v.last_name,
        v.contact_number,
        av.start_date,
        av.end_date,
        av.entry_time,
        av.exit_time,
        u.first_name::text AS created_by_first_name,
        u.last_name::text AS created_by_last_name,
        ut.name,  -- Fetching unit_name from resident-units table,
		ap.name::text as apartment_name,
		ct.name::text as apartment_name
    FROM 
        public.visitors v
    JOIN 
        public.visitor_approvals av ON v.id = av.visitor_id
    LEFT JOIN 
        public.users u ON av.created_by = u.id
	LEFT JOIN 
        public.residents r ON u.id = r.user_id 
	LEFT JOIN 
        public.resident_units ru ON r.id = ru.resident_id  
	LEFT JOIN 
        public.units ut ON ut.id = ru.unit_id  
	LEFT JOIN 
        public.apartments ap ON ap.id = ut.apartment_id  	
	LEFT JOIN 
        public.addresses ad ON ad.id = ap.address_id  	
	LEFT JOIN 
        public.cities ct ON ct.id = ad.city_id  	
    WHERE 
        av.id = approval_id AND av.is_deleted = false;
END;
$function$
