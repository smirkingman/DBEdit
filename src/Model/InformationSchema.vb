Public Module InformationSchema
	Public Const SQLColumns As String = "
		SELECT INFORMATION_SCHEMA.COLUMNS.TABLE_SCHEMA AS TableSchema,
			INFORMATION_SCHEMA.COLUMNS.TABLE_NAME AS TableName,
			INFORMATION_SCHEMA.COLUMNS.ORDINAL_POSITION AS Ordinal,
			INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME AS ColumnName,
			INFORMATION_SCHEMA.COLUMNS.IS_NULLABLE,
			INFORMATION_SCHEMA.COLUMNS.DATA_TYPE AS DataType,
			INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH as Length,
			INFORMATION_SCHEMA.COLUMNS.DOMAIN_NAME AS DomainName
		FROM INFORMATION_SCHEMA.COLUMNS
		INNER JOIN INFORMATION_SCHEMA.TABLES
			ON INFORMATION_SCHEMA.COLUMNS.TABLE_SCHEMA = INFORMATION_SCHEMA.TABLES.TABLE_SCHEMA
				AND INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = INFORMATION_SCHEMA.TABLES.TABLE_NAME
		WHERE (INFORMATION_SCHEMA.COLUMNS.TABLE_NAME <> N'sysdiagrams')
			AND (INFORMATION_SCHEMA.TABLES.TABLE_TYPE = 'BASE TABLE')
		ORDER BY TableSchema,
			TableName,
			Ordinal
		"
	Public Const SQLComputedColumns As String = "
		SELECT 
			OBJECT_SCHEMA_NAME(objects.object_id, db_id()) AS SchemaName,
			objects.name AS TableName, 
			columns.name AS ColumnName
		FROM 
			sys.computed_columns columns INNER JOIN
			sys.objects objects ON objects.object_id = columns.object_id
		"
	Public Const SQLForeignKeys As String = "
		SELECT
			SCHEMA_NAME(tr.schema_id) AS 'ParentSchema',
			tr.name 'ParentTable',
			cr.name 'ParentColumn',
			cr.column_id 'ParentOrdinal',
			SCHEMA_NAME(tp.schema_id) AS 'ChildSchema',
			fk.name 'ConstraintName',
			tp.name 'ChildTable',
			cp.name 'ChildColumn',
			cp.column_id 'ChildOrdinal'
		FROM
			sys.foreign_keys fk
		INNER JOIN
			sys.tables tp ON fk.parent_object_id = tp.object_id
		INNER JOIN
			sys.tables tr ON fk.referenced_object_id = tr.object_id
		INNER JOIN
			sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
		INNER JOIN
			sys.columns cp ON fkc.parent_column_id = cp.column_id AND 
			fkc.parent_object_id = cp.object_id
		INNER JOIN
			sys.columns cr ON fkc.referenced_column_id = cr.column_id AND 
			fkc.referenced_object_id = cr.object_id
		ORDER BY
	tp.name, cp.column_id
		"
	Public Const SQLIdentityColumns As String = "
		SELECT 
			OBJECT_SCHEMA_NAME(tables.object_id, db_id()) AS SchemaName,
			tables.name As TableName,
			columns.name as ColumnName
		FROM 
			sys.tables tables 
			JOIN sys.columns columns 
			ON tables.object_id=columns.object_id
		WHERE 
			tables.name != N'sysdiagrams' AND
			columns.is_identity = 1
		"
	Public Const SQLPrimaryKeys As String = "
		SELECT 
			INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_SCHEMA AS TableSchema,
			INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME AS TableName,
			INFORMATION_SCHEMA.KEY_COLUMN_USAGE.ORDINAL_POSITION AS OrdinalPosition,
			INFORMATION_SCHEMA.KEY_COLUMN_USAGE.COLUMN_NAME AS ColumnName,
			INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME AS ConstraintName
		FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
		INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE
			ON INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.CONSTRAINT_NAME
		WHERE (INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME <> N'sysdiagrams')
			AND (INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_TYPE = 'PRIMARY KEY')
		ORDER BY TableSchema,
			TableName,
			OrdinalPosition,
			ColumnName
		"
	Public Const SQLSchemas As String = "
		SELECT 
			COUNT(DISTINCT TABLE_SCHEMA) AS schemas
		FROM 
			INFORMATION_SCHEMA.TABLES
		WHERE 
			(TABLE_TYPE = 'BASE TABLE') AND
			(TABLE_NAME <> N'sysdiagrams')
		"
	Public Const SQLTables As String = "
		SELECT 
			TABLE_SCHEMA AS SchemaName,
			TABLE_NAME AS TableName
		FROM 
			INFORMATION_SCHEMA.TABLES
		WHERE 
			TABLE_TYPE = N'BASE TABLE' AND
			TABLE_NAME <> N'sysdiagrams'
		ORDER BY SchemaName, tableName
		"
End Module
