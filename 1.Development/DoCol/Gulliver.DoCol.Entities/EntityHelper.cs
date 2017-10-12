//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Gulliver.DoCol.Entities
{
	public class EntityHelper<T> where T : new()
	{
		public static List<T> GetListObject( DataTable dt )
		{
			List<T> lstObject = new List<T>();

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				T obj = new T();
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					int index = IndexOfField( dt.Columns[j].ColumnName );
					if (index != -1)
					{
						PropertyInfo pi = obj.GetType().GetProperties()[index];
						Type propType = pi.PropertyType;

						if ((propType.IsGenericType) && (propType.GetGenericTypeDefinition() == typeof( Nullable<> )))
						{
							propType = propType.GetGenericArguments()[0];
						}

						if (dt.Columns[j].DataType.Equals( propType ) &&
							dt.Rows[i][j] != DBNull.Value)
						{
							pi.SetValue( obj, dt.Rows[i][j], null );
						}
					}
				}
				lstObject.Add( obj );
			}
			return lstObject;
		}

		public static DataTable ConvertToDataTable( IList<T> list )
		{
			if (list == null || list.Count == 0)
			{
				return null;
			}

			DataTable table = new DataTable();
			foreach (var prop in list[0].GetType().GetProperties())
			{
				Type colType = prop.PropertyType;

				if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof( Nullable<> )))
				{
					colType = colType.GetGenericArguments()[0];
				}

				table.Columns.Add( new DataColumn( prop.Name, colType ) );
			}

			foreach (T item in list)
			{
				DataRow row = table.NewRow();

				foreach (DataColumn column in table.Columns)
				{
					int index = IndexOfField( column.ColumnName );
					if (index != -1)
					{
						PropertyInfo info = item.GetType().GetProperties()[index];
						Type propType = info.PropertyType;

						if ((propType.IsGenericType) && (propType.GetGenericTypeDefinition() == typeof( Nullable<> )))
						{
							propType = propType.GetGenericArguments()[0];
						}

						if (propType.Equals( column.DataType ))
						{
							var colValue = info.GetValue( item, null );
							if (colValue != null)
							{
								row[column.ColumnName] = colValue;
							}
							else
							{
								row[column.ColumnName] = DBNull.Value;
							}
						}
					}
				}
				table.Rows.Add( row );
			}
			return table;
		}

		private static int IndexOfField( string colName )
		{
			T o = new T();
			PropertyInfo[] pi = o.GetType().GetProperties();
			for (int i = 0; i < pi.Length; i++)
				if (pi[i].Name == colName)
					return i;
			return -1;
		}
	}
}