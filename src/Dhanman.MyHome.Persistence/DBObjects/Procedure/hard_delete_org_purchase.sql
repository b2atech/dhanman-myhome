CREATE OR REPLACE PROCEDURE public.hard_delete_org_purchase(IN p_organization_id uuid)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_company_id uuid;
BEGIN
    FOR v_company_id IN SELECT id FROM companies WHERE organization_id = p_organization_id LOOP

        -- Bill Details
        DELETE FROM bill_details
        WHERE bill_header_id IN (
            SELECT id FROM bill_headers WHERE company_id = v_company_id
        );

        -- Bill Payment Details
        DELETE FROM bill_payment_details
        WHERE bill_payment_header_id IN (
            SELECT id FROM bill_payment_headers WHERE company_id = v_company_id
        );

        -- Draft Bill Details
        DELETE FROM draft_bill_details
        WHERE bill_header_id IN (
            SELECT id FROM draft_bill_headers WHERE company_id = v_company_id
        );

        -- Vendor Note Details
        DELETE FROM vendor_note_details
        WHERE vendor_note_header_id IN (
            SELECT id FROM vendor_note_headers WHERE company_id = v_company_id
        );

        -- Gate Pass Details
        DELETE FROM gate_pass_details
        WHERE gate_pass_header_id IN (
            SELECT id FROM gate_pass_headers WHERE company_id = v_company_id
        );

        -- Remaining headers and master data
        DELETE FROM bill_workflow WHERE company_id = v_company_id;
        DELETE FROM bill_payment_header_ids WHERE company_id = v_company_id;
        DELETE FROM bill_payment_headers WHERE company_id = v_company_id;

        DELETE FROM bill_header_ids WHERE company_id = v_company_id;
        DELETE FROM bill_headers WHERE company_id = v_company_id;

        DELETE FROM draft_bill_header_ids WHERE company_id = v_company_id;
        DELETE FROM draft_bill_headers WHERE company_id = v_company_id;

        DELETE FROM recurring_bill_schedules WHERE company_id = v_company_id;

        DELETE FROM gate_pass_headers WHERE company_id = v_company_id;

        DELETE FROM vendor_note_work_flows WHERE company_id = v_company_id;
        DELETE FROM vendor_note_statuses WHERE company_id = v_company_id;
        DELETE FROM vendor_note_headers WHERE company_id = v_company_id;

        DELETE FROM vendor_categories WHERE company_id = v_company_id;
        DELETE FROM vendors WHERE company_id = v_company_id;

        DELETE FROM users WHERE company_id = v_company_id;

        DELETE FROM companies WHERE id = v_company_id;
    END LOOP;

    DELETE FROM organizations WHERE id = p_organization_id;

    RAISE NOTICE 'Deleted purchase data for organization_id: %', p_organization_id;
END;
$procedure$
