CREATE OR REPLACE FUNCTION public.get_all_committee_members_by_apartment(p_apartment_id uuid)
 RETURNS TABLE(id integer, user_id uuid, member_name text, effective_start_date timestamp without time zone, effective_end_date timestamp without time zone, role_id integer, role_name character varying, portfolio_id integer, portfolio_name text)
 LANGUAGE sql
AS $function$
    SELECT
        cm.id,
        cm.user_id,
        CONCAT(res.first_name, ' ', res.last_name) AS member_name,
        cm.effective_start_date,
        cm.effective_end_date,
        cm.role_id,
        r.name AS role_name,
        cm.portfolio_id,
        p.name AS portfolio_name
    FROM
        committee_members cm
        LEFT JOIN roles r ON r.id = cm.role_id
        LEFT JOIN portfolios p ON p.id = cm.portfolio_id
        LEFT JOIN (
            SELECT DISTINCT ON (user_id) user_id, first_name, last_name
            FROM residents
            WHERE is_deleted = false
            ORDER BY user_id, id
        ) res ON res.user_id = cm.user_id
    WHERE
        cm.apartment_id = p_apartment_id
        AND cm.is_deleted = false;
$function$
