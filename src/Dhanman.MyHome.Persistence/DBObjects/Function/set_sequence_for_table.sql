CREATE OR REPLACE FUNCTION public.set_sequence_for_table(p_table_name text, p_sequence_name text)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
DECLARE
    max_id bigint;
    is_identity boolean;
    sql_alter text;
    sql_setval text;
BEGIN
    -- Get max id from the table
    EXECUTE format('SELECT COALESCE(MAX(id), 0) FROM %I', p_table_name) INTO max_id;

    -- Check if the id column is identity
    SELECT EXISTS (
        SELECT 1 FROM information_schema.columns c
        WHERE c.table_name = p_table_name
          AND c.column_name = 'id'
          AND c.is_identity = 'YES'
    ) INTO is_identity;

    IF NOT is_identity THEN
        -- ALTER TABLE to set default nextval only if not identity
        sql_alter := format('ALTER TABLE %I ALTER COLUMN id SET DEFAULT nextval(''%I'')', p_table_name, p_sequence_name);
        EXECUTE sql_alter;
    END IF;

    -- Debug: raise notice of max id
    RAISE NOTICE 'Setting sequence % to max id % for table %', p_sequence_name, max_id, p_table_name;

    -- Set the sequence value to max_id with is_called = true
    sql_setval := format('SELECT setval(''%I'', %s, true)', p_sequence_name, max_id);
    EXECUTE sql_setval;
END;
$function$
