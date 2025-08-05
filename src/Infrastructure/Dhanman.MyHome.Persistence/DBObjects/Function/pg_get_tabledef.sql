CREATE OR REPLACE FUNCTION public.pg_get_tabledef(p_table_name text)
 RETURNS text
 LANGUAGE plpgsql
AS $function$
                            DECLARE
                                col RECORD;
                                col_defs TEXT := '';
                                pk_cols TEXT := '';
                                result TEXT;
                            BEGIN
                                FOR col IN
                                    SELECT 
                                        column_name,
                                        data_type,
                                        character_maximum_length,
                                        numeric_precision,
                                        numeric_scale,
                                        is_nullable,
                                        column_default
                                    FROM information_schema.columns
                                    WHERE table_schema = 'public' AND table_name = p_table_name
                                    ORDER BY ordinal_position
                                LOOP
                                    col_defs := col_defs || 
                                        format('"%s" %s%s%s%s, ',
                                            col.column_name,
                                            CASE 
                                                WHEN col.data_type = 'character varying' THEN format('varchar(%s)', col.character_maximum_length)
                                                WHEN col.data_type = 'numeric' THEN format('numeric(%s,%s)', col.numeric_precision, col.numeric_scale)
                                                ELSE col.data_type
                                            END,
                                            CASE WHEN col.column_default IS NOT NULL THEN ' DEFAULT ' || col.column_default ELSE '' END,
                                            CASE WHEN col.is_nullable = 'NO' THEN ' NOT NULL' ELSE '' END,
                                            ''
                                        );
                                END LOOP;

                                -- Get primary key columns
                                SELECT string_agg(format('"%s"', kcu.column_name), ', ')
                                INTO pk_cols
                                FROM information_schema.table_constraints tc
                                JOIN information_schema.key_column_usage kcu 
                                  ON tc.constraint_name = kcu.constraint_name
                                WHERE tc.table_schema = 'public' 
                                  AND tc.table_name = p_table_name 
                                  AND tc.constraint_type = 'PRIMARY KEY';

                                IF pk_cols IS NOT NULL THEN
                                    col_defs := col_defs || format('PRIMARY KEY (%s), ', pk_cols);
                                END IF;

                                col_defs := left(col_defs, length(col_defs) - 2);
                                result := format('CREATE TABLE "%s" (%s);', p_table_name, col_defs);
                                RETURN result;
                            END;
                            $function$
