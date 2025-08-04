CREATE OR REPLACE PROCEDURE public.hard_delete_org_inventory(IN p_organization_id uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_company_id uuid;
BEGIN
    FOR v_company_id IN SELECT id FROM companies WHERE organization_id = p_organization_id LOOP
        DELETE FROM orders WHERE company_id = v_company_id;
        DELETE FROM company_products WHERE company_id = v_company_id;
        DELETE FROM company_categories WHERE company_id = v_company_id;
        DELETE FROM product_tax_categories WHERE company_id = v_company_id;
        DELETE FROM units WHERE company_id = v_company_id;
        DELETE FROM users WHERE company_id = v_company_id;
        DELETE FROM companies WHERE id = v_company_id;
    END LOOP;

    DELETE FROM organizations WHERE id = p_organization_id;

    RAISE NOTICE 'Deleted inventory data for organization_id: %', p_organization_id;
END;
$procedure$
