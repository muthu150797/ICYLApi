using ICYL.Repository;
using System.Data;
using System.Data.SqlClient;

namespace ICYL.API.Entity
{
	public class LookUpModel
	{
		public LookUpValueModel GetLookupValueById(int Id)
		{
			LookUpValueModel tc = new LookUpValueModel();
			try
			{
				var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
				var connectionstring = config.GetSection("DbConnection").Value;
				using (SqlConnection connection = new SqlConnection(connectionstring))
				{
					SqlDataAdapter categoryDA;
					categoryDA = new SqlDataAdapter("select * from LookupValue where ValueId=" + Id, connection);
					categoryDA.SelectCommand.CommandType = CommandType.Text;
					//Using Data Table
					DataTable userDataTable = new DataTable();
					categoryDA.Fill(userDataTable);
					if (userDataTable.Rows != null)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							tc.ValueId = (int)row["ValueId"];
							tc.Value = (string)row["Value"];
							//tc.Active = (dynamic)row["Active"];
							if (!string.IsNullOrEmpty((string)row["APIId"]))
							{
								
								tc.APIId = (string)row["APIId"];
							}
							if (!string.IsNullOrEmpty((string)row["APIKey"]))
							{
								tc.APIKey = (string)row["APIKey"];
							}
						}
					}
					else
					{
					}
				}
			}
			catch (Exception ex)
			{
			}
			return tc;
		}
	}
}
