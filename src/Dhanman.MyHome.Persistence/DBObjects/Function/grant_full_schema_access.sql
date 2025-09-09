CREATE OR REPLACE FUNCTION public.grant_full_schema_access(p_schema text, p_user text)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
DECLARE
    obj RECORD;
BEGIN
    -- Grant on tables
    FOR obj IN
        SELECT table_name
        FROM information_schema.tables
        WHERE table_schema = p_schema
          AND table_type = 'BASE TABLE'
    LOOP
        EXECUTE format('GRANT SELECT, INSERT, UPDATE, DELETE ON TABLE %I.%I TO %I;', p_schema, obj.table_name, p_user);
    END LOOP;

    -- Grant on sequences
    FOR obj IN
        SELECT sequence_name
        FROM information_schema.sequences
        WHERE sequence_schema = p_schema
    LOOP
        EXECUTE format('GRANT USAGE, SELECT ON SEQUENCE %I.%I TO %I;', p_schema, obj.sequence_name, p_user);
    END LOOP;

    -- Grant on functions
    FOR obj IN
        SELECT routine_name, specific_name
        FROM information_schema.routines
        WHERE routine_schema = p_schema
          AND routine_type = 'FUNCTION'
    LOOP
        EXECUTE format('GRANT EXECUTE ON FUNCTION %I.%I() TO %I;', p_schema, obj.routine_name, p_user);
        -- Note: This only grants for functions without parameters. For overloaded functions, you may need to manually grant.
    END LOOP;

    -- Grant on procedures (Postgres 11+)
    FOR obj IN
        SELECT routine_name, specific_name
        FROM information_schema.routines
        WHERE routine_schema = p_schema
          AND routine_type = 'PROCEDURE'
    LOOP
        EXECUTE format('GRANT EXECUTE ON PROCEDURE %I.%I() TO %I;', p_schema, obj.routine_name, p_user);
        -- Note: As above, only for procedures without params.
    END LOOP;

END;
$function$
