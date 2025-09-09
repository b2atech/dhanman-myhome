CREATE OR REPLACE PROCEDURE public.hard_delete_org_sales(IN p_organization_id uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_company_id uuid;
BEGIN
    FOR v_company_id IN SELECT id FROM companies WHERE organization_id = p_organization_id LOOP

        -- ========== DELETE CUSTOMER NOTE DETAILS ==========
        DELETE FROM customer_note_details
        WHERE customer_note_header_id IN (
            SELECT id FROM customer_note_headers WHERE company_id = v_company_id
        );

        DELETE FROM customer_note_headers WHERE company_id = v_company_id;
        DELETE FROM customer_note_work_flows WHERE company_id = v_company_id;

        -- ========== DELETE CUSTOMERS ==========
        DELETE FROM customers WHERE company_id = v_company_id;

        -- ========== DELETE GROUP INVOICES ==========
        DELETE FROM group_invoice_details WHERE company_id = v_company_id;
        DELETE FROM group_invoice_header_ids WHERE company_id = v_company_id;
        DELETE FROM group_invoice_headers WHERE company_id = v_company_id;
        DELETE FROM group_invoice_templates WHERE company_id = v_company_id;

        -- ========== DELETE INVOICE ENTRIES ==========
        DELETE FROM invoice_details
        WHERE invoice_header_id IN (
            SELECT id FROM invoice_headers WHERE company_id = v_company_id
        );
        DELETE FROM invoice_headers WHERE company_id = v_company_id;
        DELETE FROM invoice_header_ids WHERE company_id = v_company_id;

        DELETE FROM invoice_payment_details
        WHERE invoice_payment_header_id IN (
            SELECT id FROM invoice_payment_headers WHERE company_id = v_company_id
        );
        DELETE FROM invoice_payment_headers WHERE company_id = v_company_id;
        DELETE FROM invoice_payment_header_ids WHERE company_id = v_company_id;

        DELETE FROM invoice_voucher_ids WHERE company_id = v_company_id;
        DELETE FROM invoice_workflow WHERE company_id = v_company_id;

        -- ========== DELETE DRAFT INVOICES ==========
        DELETE FROM draft_invoice_details
        WHERE invoice_header_id IN (
            SELECT id FROM draft_invoice_headers WHERE company_id = v_company_id
        );
        DELETE FROM draft_invoice_headers WHERE company_id = v_company_id;
        DELETE FROM draft_invoice_header_ids WHERE company_id = v_company_id;

        -- ========== DELETE GATE PASS ==========
        DELETE FROM gate_pass_details
        WHERE gate_pass_header_id IN (
            SELECT id FROM gate_pass_headers WHERE company_id = v_company_id
        );
        DELETE FROM gate_pass_headers WHERE company_id = v_company_id;

        -- ========== DELETE CONFIG & USERS ==========
        DELETE FROM penalty_configs WHERE company_id = v_company_id;
        DELETE FROM recurring_sales_schedules WHERE company_id = v_company_id;
        DELETE FROM users WHERE company_id = v_company_id;
        DELETE FROM companies WHERE id = v_company_id;

        RAISE NOTICE 'Deleted sales data for company_id %', v_company_id;
    END LOOP;

    DELETE FROM organizations WHERE id = p_organization_id;

    RAISE NOTICE 'Deleted sales data for organization_id %', p_organization_id;
END;
$procedure$
