CREATE OR REPLACE FUNCTION public.get_committee_members_by_apartment(p_apartment_id uuid)
 RETURNS TABLE(id integer, apartment_id uuid, role_id integer, user_id uuid, member_name text)
 LANGUAGE plpgsql
AS $function$
BEGIN
   RETURN QUERY
  SELECT
        cm.id,
        cm.apartment_id,
        cm.role_id,
        cm.user_id,
		 u.first_name || ' ' || u.last_name AS member_name
    FROM
        committee_members cm
		inner join users u on u.id = cm.user_id
    WHERE
        cm.is_deleted = FALSE
        AND cm.apartment_id = p_apartment_id
        AND cm.effective_start_date <= NOW()
        AND (cm.effective_end_date IS NULL OR cm.effective_end_date >= NOW());

END;
$function$
