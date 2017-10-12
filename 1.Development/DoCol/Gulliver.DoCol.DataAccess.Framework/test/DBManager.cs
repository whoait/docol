//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------
//---------------------------------------------------------------------------
// Version			: 002
// Designer			: DungNH6-FPT
// Programmer		: DungNH6-FPT
// Date				: 2015/04/24
// Comment			: Update connection external string and config function GetDataTableFromExternal
//---------------------------------------------------------------------------

namespace Gulliver.CarUpgrade.DataAccess.Framework
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web;
	using System.Xml;

	using Gulliver.CarUpgrade.Constants;

	/// <summary>
	/// Summary description for DBManager
	/// </summary>
	public class DBManager : IDisposable
	{
		#region Private variable
		/// <summary>
		/// The connection string.
		/// </summary>
		private static string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

		/// <summary>
		/// The connection string futai.
		/// </summary>
		private static string connectionStringFutai = ConfigurationManager.ConnectionStrings["FutaiConnection"].ConnectionString;

		/// <summary>
		/// The connection string car db.
		/// </summary>
		private static string connectionStringCarDB = ConfigurationManager.ConnectionStrings["CarDbConnection"].ConnectionString;

		/// <summary>
		/// The connection string car db.
		/// </summary>
		private static string connectionStringTapsDB = ConfigurationManager.ConnectionStrings["TapsDbConnection"].ConnectionString;

		/// <summary>
		/// The connection string liliput db.
		/// </summary>
		private static string connectionStringLilliputDB = ConfigurationManager.ConnectionStrings["LilliputDbConnection"].ConnectionString;
		#endregion

		#region Constructor ( 2+ overloads)

		public DBManager()
		{
			SetSqlCommand();
		}

		/// <summary>
		/// DBManager constructor
		/// </summary>
		/// <param name="DbConnection">name of connection string in web config</param>
		/// <param name="CommandText">string to be executed</param>
		public DBManager( string DbConnection, string CommandText )
		{
			connectionString = ConfigurationManager.ConnectionStrings[DbConnection].ConnectionString;
			this.CommandText = CommandText;
			SetSqlCommand();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DBManager"/> class. 
		/// DBManager constructor
		/// </summary>
		/// <param name="CommandText">
		/// string to be executed
		/// </param>
		public DBManager( string CommandText )
		{
			this.CommandText = CommandText;
		}

		/// <summary>
		/// DBManager constructor
		/// </summary>
		/// <param name="CommandText">string to be executed</param>
		/// <param name="CurrentCommandType">CommandType</param>
		public DBManager( string CommandText, CommandType CurrentCommandType )
			: this( CommandText )
		{
			this.CurrentCommandType = CurrentCommandType;
		}

		/// <summary>
		/// Finalizer
		/// </summary>
		~DBManager()
		{
			Dispose( false );
		}

		#endregion Constructor ( 2+ overloads)

		#region Dispose

		/// <summary>
		/// Releases the resources used by the Component.
		/// </summary>
		/// <param name="disposing">Send true value when Dispose Method is called by the program</param>
		public void Dispose( bool disposing )
		{
			if (disposing)
			{
				GC.SuppressFinalize( this );
			}
			if (_oSqlCommand != null)
			{
				_oSqlCommand.Parameters.Clear();
				//if (_oSqlCommand.Connection != null)
				//{
				//	if (_oSqlCommand.Connection.State == System.Data.ConnectionState.Open)
				//	{
				//		_oSqlCommand.Connection.Close();
				//	}
				//}
				_oSqlCommand.Dispose();
			}
		}

		/// <summary>
		/// Releases the resources used by the Component.
		/// </summary>
		public void Dispose()
		{
			this.Dispose( true );
		}

		#region IDisposable Members

		/// <summary>
		/// Releases the resources used by the Component.
		/// </summary>
		void IDisposable.Dispose()
		{
			Dispose( true );
		}

		#endregion IDisposable Members

		#endregion Dispose

		#region SetSqlCommand

		/// <summary>
		/// Sets up the sql Command
		/// </summary>
		private void SetSqlCommand()
		{
			if (_oSqlCommand == null) { _oSqlCommand = new SqlCommand( _CommandText ); }
			else { _oSqlCommand.CommandText = _CommandText; }
			_oSqlCommand.CommandType = DefaultCommandType;
			_oSqlCommand.CommandTimeout = 120;
		}

		#endregion SetSqlCommand

		#region Add Method

		/// <summary>
		/// Adds parameter name and value for the stored procedures. Parameter name will be same as found in stored procedures
		/// </summary>
		/// <param name="parameterName">Name of the parameter used in stored procedures</param>
		/// <param name="value">Value to be passed in stored procedures</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		public SqlParameter Add( string parameterName, object value )
		{
			return this.Add( new SqlParameter( parameterName, value != null ? value : DBNull.Value ) );
		}

		/// <summary>
		/// Adds parameter name ,value and direction for the stored procedures. Parameter name will be same as found in stored procedures
		/// </summary>
		/// <param name="parameterName">Name of the parameter used in stored procedures</param>
		/// <param name="value">Value to be passed in stored procedures</param>
		/// <param name="direction">One of the value of ParameterDirection for the parameter</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		public SqlParameter Add( string parameterName, object value, System.Data.ParameterDirection direction )
		{
			return this.Add( new SqlParameter( parameterName, value != null ? value : DBNull.Value ), direction );
		}

		/// <summary>
		/// Adds parameter name, datatype and direction for the stored procedures. Parameter name will be same as found in stored procedures
		/// </summary>
		/// <param name="parameterName">Name of the parameter used in stored procedures</param>
		/// <param name="sqlDbType">One of the SqlDbType values used in stored procedures</param>
		/// <param name="direction">One of the ParameterDirection values</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		public SqlParameter Add( string parameterName, System.Data.SqlDbType sqlDbType, System.Data.ParameterDirection direction )
		{
			return this.Add( new SqlParameter( parameterName, sqlDbType ), direction );
		}

		/// <summary>
		/// Adds parameter name, datatype and direction for the stored procedures. Parameter name will be same as found in stored procedures
		/// </summary>
		/// <param name="parameterName">Name of the parameter used in stored procedures</param>
		/// <param name="sqlDbType">One of the SqlDbType values used in stored procedures</param>
		/// <param name="direction">One of the ParameterDirection values</param>
		/// <param name="value">Value to be passed in stored procedures</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		public SqlParameter Add( string parameterName, System.Data.SqlDbType sqlDbType, object value )
		{
			return this.Add( new SqlParameter( parameterName, sqlDbType ), ParameterDirection.Input, value != null ? value : DBNull.Value );
		}

		/// <summary>
		/// Adds parameter name, datatype and direction for the stored procedures. Parameter name will be same as found in stored procedures
		/// </summary>
		/// <param name="parameterName">Name of the parameter used in stored procedures</param>
		/// <param name="sqlDbType">One of the SqlDbType values used in stored procedures</param>
		/// <param name="direction">One of the ParameterDirection values</param>
		/// <param name="value">Value to be passed in stored procedures</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		public SqlParameter Add( string parameterName, System.Data.SqlDbType sqlDbType, System.Data.ParameterDirection direction, object value )
		{
			return this.Add( new SqlParameter( parameterName, sqlDbType ), direction, value != null ? value : DBNull.Value );
		}

		/// <summary>
		/// Adds parameter name, datatype, size and direction for the stored procedures. Parameter name will be same as found in stored procedures
		/// </summary>
		/// <param name="parameterName">Name of the parameter used in stored procedures</param>
		/// <param name="sqlDbType">One of the SqlDbType values used in stored procedures</param>
		/// <param name="size">The length of the column.</param>
		/// <param name="direction">One of the ParameterDirection values</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		public SqlParameter Add( string parameterName, System.Data.SqlDbType sqlDbType, int size, System.Data.ParameterDirection direction )
		{
			return this.Add( new SqlParameter( parameterName, sqlDbType, size ), direction );
		}

		/// <summary>
		/// Adds parameter name, datatype, size and direction for the stored procedures. Parameter name will be same as found in stored procedures
		/// </summary>
		/// <param name="parameterName">Name of the parameter used in stored procedures</param>
		/// <param name="sqlDbType">One of the SqlDbType values used in stored procedures</param>
		/// <param name="size">The length of the column. </param>
		/// <param name="direction">One of the ParameterDirection values</param>
		/// <param name="value">Value to be passed in stored procedures</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		public SqlParameter Add( string parameterName, System.Data.SqlDbType sqlDbType, int size, object value )
		{
			return this.Add( new SqlParameter( parameterName, sqlDbType, size ), ParameterDirection.Input, value != null ? value : DBNull.Value );
		}

		/// <summary>
		/// Adds parameter name, datatype, size and direction for the stored procedures. Parameter name will be same as found in stored procedures
		/// </summary>
		/// <param name="parameterName">Name of the parameter used in stored procedures</param>
		/// <param name="sqlDbType">One of the SqlDbType values used in stored procedures</param>
		/// <param name="size">The length of the column. </param>
		/// <param name="direction">One of the ParameterDirection values</param>
		/// <param name="value">Value to be passed in stored procedures</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		public SqlParameter Add( string parameterName, System.Data.SqlDbType sqlDbType, int size, System.Data.ParameterDirection direction, object value )
		{
			return this.Add( new SqlParameter( parameterName, sqlDbType, size ), direction, value != null ? value : DBNull.Value );
		}

		public SqlParameter Add( string parameterName, System.Data.SqlDbType sqlDbType, int size, System.Data.ParameterDirection direction, bool isNullable )
		{
			SqlParameter sParam = new SqlParameter( parameterName, sqlDbType, size );
			sParam.Direction = direction;
			sParam.IsNullable = isNullable;
			return this.Add( sParam );
		}

		/// <summary>
		///  Adds SqlParameter object
		/// </summary>
		/// <param name="SqlPar">The SqlParameter to add to the collection.</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		public SqlParameter Add( SqlParameter SqlPar )
		{
			_oSqlCommand.CommandType = CommandType.StoredProcedure;
			return _oSqlCommand.Parameters.Add( SqlPar );
		}

		/// <summary>
		/// Adds SqlParameter object
		/// </summary>
		/// <param name="SqlPar">The SqlParameter to add to the collection.</param>
		/// <param name="direction">One of the value of ParameterDirection for the parameter</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		private SqlParameter Add( SqlParameter SqlPar, System.Data.ParameterDirection direction )
		{
			_oSqlCommand.CommandType = CommandType.StoredProcedure;
			SqlPar.Direction = direction;
			return _oSqlCommand.Parameters.Add( SqlPar );
		}

		/// <summary>
		/// Adds SqlParameter object
		/// </summary>
		/// <param name="value">The SqlParameter to add to the collection.</param>
		/// <param name="direction">One of the value of ParameterDirection for the parameter</param>
		/// <param name="value">Value to be passed in stored procedures</param>
		/// <returns>Initializes a new instance of the SqlParameter class that uses the parameter name and a value of the new SqlParameter.</returns>
		private SqlParameter Add( SqlParameter SqlPar, System.Data.ParameterDirection direction, object value )
		{
			_oSqlCommand.CommandType = CommandType.StoredProcedure;
			SqlPar.Value = value ?? DBNull.Value;
			SqlPar.Direction = direction;
			return _oSqlCommand.Parameters.Add( SqlPar );
		}

		#endregion Add Method

		#region Clear Method

		/// <summary>
		/// Removes all Parameter added.
		/// </summary>
		public void Clear()
		{
			_oSqlCommand.Parameters.Clear();
			//mCommandType = CommandType.Text;
		}

		#endregion Clear Method

		#region Contains

		/// <summary>
		/// Gets a value indicating whether a SqlParameter exists in the collection
		/// </summary>
		/// <param name="value">The value of the SqlParameter object to find</param>
		/// <returns>true if the collection contains the parameter; otherwise, false.</returns>
		public bool Contains( object value )
		{
			return _oSqlCommand.Parameters.Contains( value );
		}

		/// <summary>
		/// Gets a value indicating whether a SqlParameter with the specified parameter name exists in the collection.
		/// </summary>
		/// <param name="value">The name of the SqlParameter object to find. </param>
		/// <returns>true if the collection contains the parameter; otherwise, false.</returns>
		public bool Contains( string value )
		{
			return _oSqlCommand.Parameters.Contains( value );
		}

		#endregion Contains

		#region GetEnumerator

		public System.Collections.IEnumerator GetEnumerator()
		{
			return _oSqlCommand.Parameters.GetEnumerator();
		}

		#endregion GetEnumerator

		#region IndexOf Method

		/// <summary>
		/// Gets the location of a SqlParameter in the collection.
		/// </summary>
		/// <param name="value">The SqlParameter object to locate. </param>
		/// <returns>The zero-based location of the SqlParameter in the collection.</returns>
		public int IndexOf( object value )
		{
			return _oSqlCommand.Parameters.IndexOf( value );
		}

		/// <summary>
		/// Gets the location of the SqlParameter in the collection with a specific parameter name.
		/// </summary>
		/// <param name="parameterName">The name of the SqlParameter object to retrieve. </param>
		/// <returns>The zero-based location of the SqlParameter in the collection.</returns>
		public int IndexOf( string parameterName )
		{
			return _oSqlCommand.Parameters.IndexOf( parameterName );
		}

		#endregion IndexOf Method

		#region Insert Method

		/// <summary>
		/// Inserts a SqlParameter into the collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index where the parameter is to be inserted within the collection.</param>
		/// <param name="value">The SqlParameter to add to the collection. </param>
		public void Insert( int index, object value )
		{
			_oSqlCommand.Parameters.Insert( index, value );
		}

		#endregion Insert Method

		#region Remove Method

		/// <summary>
		/// Removes the specified SqlParameter from the collection.
		/// </summary>
		/// <param name="value">A SqlParameter object to remove from the collection. </param>
		public void Remove( object value )
		{
			//if (_oSqlCommand.Parameters.Count == 0)
			//    mCommandType = CommandType.Text;
			_oSqlCommand.Parameters.Remove( value );
		}

		#endregion Remove Method

		#region RemoveAt Method

		/// <summary>
		/// Removes the specified SqlParameter from the collection using the parameter name.
		/// </summary>
		/// <param name="parameterName">The name of the SqlParameter object to retrieve.</param>
		public void RemoveAt( string parameterName )
		{
			//if (_oSqlCommand.Parameters.Count == 0)
			//    mCommandType = CommandType.Text;
			_oSqlCommand.Parameters.RemoveAt( parameterName );
		}

		/// <summary>
		/// Removes the specified SqlParameter from the collection using a specific index.
		/// </summary>
		/// <param name="index">The zero-based index of the parameter.</param>k
		public void RemoveAt( int index )
		{
			_oSqlCommand.Parameters.RemoveAt( index );
		}

		#endregion RemoveAt Method

		#region ReturnValue

		/// <summary>
		/// Returns the value from Stored Procedures
		/// </summary>
		public int ReturnValue
		{
			get
			{
				if (this["@ReturnValue"].Value == DBNull.Value)
					return int.MinValue;
				return (int)this["@ReturnValue"].Value;
			}
		}

		#endregion ReturnValue

		#region SQL Execution By Return Object

		private System.Data.SqlClient.SqlConnection PrepareExecution( string CommandText )
		{
			if (_oSqlCommand == null)
			{
				_oSqlCommand = new System.Data.SqlClient.SqlCommand();
			}
			if (_oSqlCommand.Connection == null)
			{
				if (GetShortLiveCache( "DbConnection" ) == null)
				{
					SaveShortLiveCache( "DbConnection", new System.Data.SqlClient.SqlConnection( connectionString ) );
				}
				_oSqlCommand.Connection = (SqlConnection)GetShortLiveCache( "DbConnection" );
			}
			if (_oSqlCommand.Connection.ConnectionString.Length == 0)
			{
				_oSqlCommand.Connection.ConnectionString = connectionString;
			}
			if (_oSqlCommand.Connection.State != System.Data.ConnectionState.Open)
			{
				_oSqlCommand.Connection.Open();
			}

			_oSqlCommand.Transaction = DBManager.BeginTransaction();
			_oSqlCommand.CommandText = CommandText;
			if (_oSqlCommand.CommandType == CommandType.StoredProcedure)
			{
				Add( "@ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue );
			}
			return _oSqlCommand.Connection;
		}

		#endregion SQL Execution By Return Object

		#region Property of ExecuteNonQuery

		/// <summary>
		/// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
		/// </summary>
		/// <param name="CommandText">Executes the Transact-SQL statement or stored procedure against the connection.</param>
		/// <returns>The number of rows affected.</returns>
		public int ExecuteNonQuery()
		{
			PrepareExecution( CommandText );
			try
			{
				return _oSqlCommand.ExecuteNonQuery();
			}
			catch (SqlException ex)
			{
				RollbackTransaction();
				if (ex.Number == -2)
				{
					HttpContext.Current.Session["GLV_SYS_SQLTimeOut"] = "1";
					return 0;
				}
				else
				{
					throw new Exception( ex.Message );
					// throw new Exception( "GLV_SYS_DBException" );
				}
			}
			catch (Exception)
			{
				RollbackTransaction();
				throw new Exception( "GLV_SYS_DBException" );
			}
		}

		#endregion Property of ExecuteNonQuery

		#region public object ExecuteScalar

		/// <summary>
		/// Executes a Transact-SQL statement against the connection and returns the first column of the first row in the result set returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="CommandText">Executes the Transact-SQL statement or stored procedure against the connection.</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		public object ExecuteScalar()
		{
			PrepareExecution( CommandText );
			try
			{
				return _oSqlCommand.ExecuteScalar();
			}
			catch (SqlException ex)
			{
				RollbackTransaction();
				if (ex.Number == -2)
				{
					HttpContext.Current.Session["GLV_SYS_SQLTimeOut"] = "1";
					return null;
				}
				else
				{
					throw new Exception( "GLV_SYS_DBException" );
				}
			}
			catch (Exception)
			{
				RollbackTransaction();
				throw new Exception( "GLV_SYS_DBException" );
			}
		}

		#endregion public object ExecuteScalar

		#region public SqlDataReader ExecuteReader ( 1+ overloads)

		/// <summary>
		/// Executes a Transact-SQL statement against the connection and returns a stream of (reading & forward-only) rows from a SQL Server database.
		/// </summary>
		/// <param name="CommandText">Executes the Transact-SQL statement or stored procedure against the connection.</param>
		/// <returns>A SqlDataReader object.</returns>
		public SqlDataReader ExecuteReader()
		{
			PrepareExecution( CommandText );
			try
			{
				return _oSqlCommand.ExecuteReader();
			}
			catch (SqlException ex)
			{
				RollbackTransaction();
				if (ex.Number == -2)
				{
					HttpContext.Current.Session["GLV_SYS_SQLTimeOut"] = "1";
					return null;
				}
				else
				{
					throw new Exception( "GLV_SYS_DBException" );
				}
			}
			catch (Exception)
			{
				RollbackTransaction();
				throw new Exception( "GLV_SYS_DBException" );
			}
		}

		/// <summary>
		/// Executes a Transact-SQL statement against the connection and returns a stream of (reading & forward-only) rows from a SQL Server database.
		/// </summary>
		/// <param name="CommandText">Executes the Transact-SQL statement or stored procedure against the connection.</param>
		/// <param name="behavior">One of the CommandBehavior values.</param>
		/// <returns>A SqlDataReader object.</returns>
		public SqlDataReader ExecuteReader( CommandBehavior behavior )
		{
			PrepareExecution( CommandText );
			return _oSqlCommand.ExecuteReader( behavior );
		}

		#endregion public SqlDataReader ExecuteReader ( 1+ overloads)

		#region DataTable GetDataTable

		/// <summary>
		/// Executes a Transact-SQL statement against the connection and returns represents one table of in-memory data
		/// </summary>
		/// <returns>
		/// An DataTable object
		/// </returns>
		public DataTable GetDataTable()
		{
			PrepareExecution( CommandText );
			System.Data.SqlClient.SqlDataAdapter SqlAdp = new System.Data.SqlClient.SqlDataAdapter( _oSqlCommand );
			System.Data.DataTable dataTable = new System.Data.DataTable();
			try
			{
				SqlAdp.Fill( dataTable );
			}
			catch (SqlException ex)
			{
				RollbackTransaction();
				if (ex.Number == -2)
				{
					HttpContext.Current.Session["GLV_SYS_SQLTimeOut"] = "1";
					return new DataTable();
				}
				else
				{
					throw new Exception( "GLV_SYS_DBException" );
				}
			}
			catch (Exception)
			{
				RollbackTransaction();
				throw new Exception( "GLV_SYS_DBException" );
			}
			return dataTable;
		}

		#endregion DataTable GetDataTable

		#region DataTable GetDataTableFromSecondDB

		/// <summary>
		/// Get data table from other Database 
		/// </summary>
		/// <param name="commandType">
		/// The command Type.
		/// </param>
		/// <param name="commandText">
		/// </param>
		/// <param name="connectDatabase">
		/// The connect string type of other database.
		/// </param>
		/// <param name="sqlParameter">
		/// The sql Parameter.
		/// </param>
		/// <returns>
		/// A DataTable object
		/// </returns>
		public DataTable GetDataTableFromExternal( CommandType commandType = CommandType.Text, string commandText = "", ConnectDatabase connectDatabase = ConnectDatabase.Local, List<SqlParameter> sqlParameter = null )
		{
			#region Set connectionExternalString with param connectDatabase is type ConnectDatabase
			string connectionExternalString;
			switch (connectDatabase)
			{
				case ConnectDatabase.Local:
					connectionExternalString = connectionString;
					break;
				case ConnectDatabase.ExternalCarDB:
					connectionExternalString = connectionStringCarDB;
					break;
				case ConnectDatabase.ExternalDBFutai:
					connectionExternalString = connectionStringFutai;
					break;
				case ConnectDatabase.ExternalTapsDB:
					connectionExternalString = connectionStringTapsDB;
					break;
				case ConnectDatabase.ExternalLilliputDB:
					connectionExternalString = connectionStringLilliputDB;
					break;
				default:
					throw new Exception( "GLV_SYS_DBException" );
			}
			#endregion

			commandText = string.IsNullOrEmpty( commandText ) ? this.CommandText : commandText;
			var dataTable = new DataTable();
			var sqlCommand = new SqlCommand
			{
				Connection = new SqlConnection( connectionExternalString ),
				CommandText = commandText,
				CommandType = commandType,
				CommandTimeout = 300
			};
			try
			{
				if (sqlCommand.Connection.ConnectionString.Length == 0)
				{
					sqlCommand.Connection.ConnectionString = connectionString;
				}

				if (sqlCommand.Connection.State != ConnectionState.Open)
				{
					sqlCommand.Connection.Open();
				}

				if (sqlParameter != null)
				{
					foreach (SqlParameter parameter in sqlParameter)
					{
						sqlCommand.Parameters.Add( parameter );
					}
				}

				var sqlAdp = new SqlDataAdapter( sqlCommand );
				sqlAdp.Fill( dataTable );
			}
			catch (SqlException ex)
			{
				if (ex.Number == -2)
				{
					HttpContext.Current.Session["GLV_SYS_SQLTimeOut"] = "1";
					return new DataTable();
				}

				throw new Exception( "GLV_SYS_DBException:" + ex.Message, ex );
			}
			catch (Exception ex)
			{
				throw new Exception( "GLV_SYS_DBException:" + ex.Message, ex );
			}
			finally
			{
				if (sqlCommand.Connection.State == ConnectionState.Open)
				{
					sqlCommand.Connection.Close();
				}
				sqlCommand.Dispose();
			}

			return dataTable;
		}

		#endregion DataTable GetDataTableFromSecondDB

		#region DataSet GetDataSet

		/// <summary>
		/// Executes a Transact-SQL statement against the connection and returns dataset of in-memory data
		/// </summary>
		/// <param name="CommandText">Executes the Transact-SQL statement or stored procedure against the connection.</param>
		/// <returns>A Dataset object</returns>
		public DataSet GetDataSet()
		{
			PrepareExecution( CommandText );
			System.Data.SqlClient.SqlDataAdapter SqlAdp = new System.Data.SqlClient.SqlDataAdapter( _oSqlCommand );
			System.Data.DataSet dataSet = new System.Data.DataSet();
			try
			{
				SqlAdp.Fill( dataSet );
			}
			catch (SqlException ex)
			{
				RollbackTransaction();
				if (ex.Number == -2)
				{
					HttpContext.Current.Session["GLV_SYS_SQLTimeOut"] = "1";
					return new DataSet();
				}
				else
				{
					throw new Exception( "GLV_SYS_DBException" );
				}
			}
			catch (Exception)
			{
				RollbackTransaction();
				throw new Exception( "GLV_SYS_DBException" );
			}

			if (dataSet.Tables.Count > 0)
			{
				return dataSet;
			}
			else
			{
				return (new DataSet());
			}
		}

		#endregion DataSet GetDataSet

		#region XmlReader ExecuteXmlReader

		/// <summary>
		/// Executes a Transact-SQL statement against the connection and returns provides a stream of XML data which is forward-only, read-only access.
		/// </summary>
		/// <param name="CommandText">Executes the Transact-SQL statement or stored procedure against the connection.</param>
		/// <returns>An XmlReader object.</returns>
		public XmlReader ExecuteXmlReader()
		{
			PrepareExecution( CommandText );
			try
			{
				return _oSqlCommand.ExecuteXmlReader();
			}
			catch (SqlException ex)
			{
				RollbackTransaction();
				if (ex.Number == -2)
				{
					HttpContext.Current.Session["GLV_SYS_SQLTimeOut"] = "1";
					return null;
				}
				else
				{
					throw new Exception( "GLV_SYS_DBException" );
				}
			}
			catch (Exception)
			{
				RollbackTransaction();
				throw new Exception( "GLV_SYS_DBException" );
			}
		}

		#endregion XmlReader ExecuteXmlReader

		#region Get Values

		public T GetValue<T>( string columnName )
		{
			if (this[columnName].Value != DBNull.Value)
			{
				return (T)this[columnName].Value;
			}
			return default( T );
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public string GetValueInString( string columnName )
		{
			if (this[columnName].Value != DBNull.Value)
			{
				return this[columnName].Value as string;
			}
			return null;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public int? GetValueInInt( string columnName )
		{
			if (this[columnName].Value != DBNull.Value)
			{
				int i = Convert.ToInt32( this[columnName].Value );
				return i;
			}
			return null;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public long? GetValueInLong( string columnName )
		{
			if (this[columnName].Value != DBNull.Value)
			{
				long i = Convert.ToInt64( this[columnName].Value );
				return i;
			}
			return null;
		}

		// <summary>
		///
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public char? GetValueInChar( string columnName )
		{
			if (this[columnName].Value != DBNull.Value)
			{
				if (this[columnName].Value.GetType() == typeof( string ))
				{
					if (this[columnName].Value.ToString().Length > 0)
					{
						return this[columnName].Value.ToString()[0];
					}
					else
					{
						return null;
					}
				}
				return (char)this[columnName].Value;
			}
			return null;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public byte? GetValueInByte( string columnName )
		{
			if (this[columnName].Value != DBNull.Value)
			{
				return (byte)this[columnName].Value;
			}
			return null;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public DateTime? GetValueInDateTime( string columnName )
		{
			if (this[columnName].Value != DBNull.Value)
			{
				return (DateTime)this[columnName].Value;
			}
			return null;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public bool GetValueInBool( string columnName )
		{
			if (this[columnName].Value != DBNull.Value)
			{
				return (bool)this[columnName].Value;
			}
			return false;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public decimal? GetValueInDecimal( string columnName )
		{
			if (this[columnName].Value != DBNull.Value)
			{
				return (decimal)this[columnName].Value;
			}
			return null;
		}

		#endregion Get Values

		#region Properties

		#region public string CommandText

		private string _CommandText;

		/// <summary>
		///
		/// </summary>
		public string CommandText
		{
			get { return _CommandText; }
			set { _CommandText = value; SetSqlCommand(); }
		}

		#endregion public string CommandText

		#region CommandType DefaultCommandType

		private CommandType _DefaultCommandType = CommandType.StoredProcedure;

		public CommandType DefaultCommandType
		{
			get { return _DefaultCommandType; }
			set { _DefaultCommandType = value; }
		}

		#endregion CommandType DefaultCommandType

		#region CommandType CurrentCommandType

		public CommandType CurrentCommandType
		{
			get { return _oSqlCommand.CommandType; }
			set { _oSqlCommand.CommandType = value; }
		}

		#endregion CommandType CurrentCommandType

		#region public SqlCommand oSqlCommand

		private SqlCommand _oSqlCommand;

		/// <summary>
		///
		/// </summary>
		public SqlCommand oSqlCommand
		{
			get { return _oSqlCommand; }
			set { _oSqlCommand = value; }
		}

		#endregion public SqlCommand oSqlCommand

		//#region public SqlTransaction oSqlTransaction
		//private SqlTransaction _oSqlTransaction;
		///// <summary>
		/////
		///// </summary>
		//public SqlTransaction oSqlTransaction
		//{
		//	get { return _oSqlTransaction; }
		//	set { _oSqlTransaction = value; }
		//}
		//#endregion

		#region Count Property

		/// <summary>
		/// Gets the number of SqlParameter objects in the collection.
		/// </summary>
		public int Count
		{
			get
			{
				return _oSqlCommand.Parameters.Count;
			}
		}

		#endregion Count Property

		#region Indexers Property

		/// <summary>
		/// The parameters of the Transact-SQL statement or stored procedure. The default is an empty collection.
		/// </summary>
		public SqlParameter this[int index]
		{
			get { return _oSqlCommand.Parameters[index]; }
			set { _oSqlCommand.Parameters[index] = value; }
		}

		/// <summary>
		/// The parameters of the Transact-SQL statement or stored procedure. The default is an empty collection.
		/// </summary>
		public SqlParameter this[string parameterName]
		{
			get { return _oSqlCommand.Parameters[parameterName]; }
			set { _oSqlCommand.Parameters[parameterName] = value; }
		}

		#endregion Indexers Property

		public static SqlTransaction BeginTransaction()
		{
			if (GetShortLiveCache( "DbConnection" ) == null)
			{
				return null;
			}

			SqlConnection sqlConnection = (SqlConnection)GetShortLiveCache( "DbConnection" );
			if (sqlConnection.State != System.Data.ConnectionState.Open
					|| (GetShortLiveCache( "DbTransaction" ) != null && ((SqlTransaction)GetShortLiveCache( "DbTransaction" )).Connection != null))
			{
				return (SqlTransaction)GetShortLiveCache( "DbTransaction" );
			}

			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			SaveShortLiveCache( "DbTransaction", sqlTransaction );

			return sqlTransaction;
		}

		public static bool CommitTransaction()
		{
			if (GetShortLiveCache( "DbConnection" ) == null)
			{
				return false;
			}

			SqlConnection sqlConnection = (SqlConnection)GetShortLiveCache( "DbConnection" );
			if (sqlConnection.State != System.Data.ConnectionState.Open
					|| GetShortLiveCache( "DbTransaction" ) == null)
			{
				return false;
			}

			SqlTransaction sqlTransaction = (SqlTransaction)GetShortLiveCache( "DbTransaction" );

			sqlTransaction.Commit();

			SaveShortLiveCache( "DbTransaction", null );

			return true;
		}

		public static bool RollbackTransaction()
		{
			if (GetShortLiveCache( "DbConnection" ) == null)
			{
				return false;
			}

			SqlConnection sqlConnection = (SqlConnection)GetShortLiveCache( "DbConnection" );
			if (sqlConnection.State != System.Data.ConnectionState.Open
					|| GetShortLiveCache( "DbTransaction" ) == null)
			{
				return false;
			}

			SqlTransaction sqlTransaction = (SqlTransaction)GetShortLiveCache( "DbTransaction" );

			sqlTransaction.Rollback();

			SaveShortLiveCache( "DbTransaction", null );

			return true;
		}

		public static void CloseConnection()
		{
			if (GetShortLiveCache( "DbConnection" ) == null)
			{
				return;
			}

			SqlConnection sqlConnection = (SqlConnection)GetShortLiveCache( "DbConnection" );
			if (sqlConnection.State == System.Data.ConnectionState.Open)
			{
				sqlConnection.Close();
			}

			SaveShortLiveCache( "DbConnection", null );
			SaveShortLiveCache( "DbTransaction", null );
			return;
		}

		#endregion Properties

		#region CacheUtil
		/// <summary>
		/// Short Live Cache is cache that existed during live of a request
		/// After server response to client, the Short Live Cache will be clear
		/// </summary>
		/// <param name="key">key string</param>
		/// <returns>object value</returns>
		private static object GetShortLiveCache( string key )
		{
			Dictionary<string, Dictionary<string, object>> cachedDictionary
				= (Dictionary<string, Dictionary<string, object>>)HttpContext.Current.Application["shortLiveCache"];
			if (cachedDictionary.ContainsKey( HttpContext.Current.Session.SessionID ))
			{
				Dictionary<string, object> sessionCachedDictionary = cachedDictionary[HttpContext.Current.Session.SessionID];
				if (sessionCachedDictionary.ContainsKey( key ))
				{
					return sessionCachedDictionary[key];
				}
			}
			return null;
		}

		/// <summary>
		/// Short Live Cache is cache that existed during live of a request
		/// After server response to client, the Short Live Cache will be clear
		/// </summary>
		/// <param name="key">key string</param>
		/// <param name="value">object value</param>
		private static void SaveShortLiveCache( string key, object value )
		{
			Dictionary<string, Dictionary<string, object>> cachedDictionary
				= (Dictionary<string, Dictionary<string, object>>)HttpContext.Current.Application["shortLiveCache"];
			Dictionary<string, object> sessionCachedDictionary;
			if (cachedDictionary.ContainsKey( HttpContext.Current.Session.SessionID ))
			{
				sessionCachedDictionary = cachedDictionary[HttpContext.Current.Session.SessionID];
				if (sessionCachedDictionary.ContainsKey( key ))
				{
					sessionCachedDictionary[key] = value;
				}
				else
				{
					sessionCachedDictionary.Add( key, value );
				}
			}
			else
			{
				sessionCachedDictionary = new Dictionary<string, object>();
				sessionCachedDictionary.Add( key, value );
				cachedDictionary.Add( HttpContext.Current.Session.SessionID, sessionCachedDictionary );
			}
		}
		#endregion
	}
}