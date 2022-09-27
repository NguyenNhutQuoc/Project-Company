using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace WebAPIBiz4Company.DA
{
    public class DbUtils
    {
        public static SqlConnection ConnectionDatabase(String connectString)
        {
            return new SqlConnection(connectString);
        }

        public static List<T> GetDataInTable<T>(String spName, String connectionString, List<SqlParameter>? parameters = null) where T : new()
        {
            using (SqlConnection conn = ConnectionDatabase(connectionString))
            {
                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand(spName, conn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                da.SelectCommand.Parameters.Add(parameter);
                            }
                        }
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Table");
                        DataTable? dataTable = ds.Tables["Table"];
                        List<T> list = new List<T>();
                        if (dataTable is not null)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                int count = 0;
                                T t = new T();
                                while (count < row.ItemArray.Length)
                                {
                                    Type type = t.GetType();
                                    PropertyInfo[] propertyInfos = type.GetProperties();
                                    propertyInfos[count].SetValue(t,
                                        row[count].GetType().ToString() == "System.DBNull"
                                            ? row[count].ToString()
                                            : row[count]);
                                    count++;
                                }
                                list.Add(t);
                            }
                        }
                        return list;
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return new List<T>();
        }

        public static string? RunSpToModifiedDataInTables(string spName, List<SqlParameter> parameters,
            string connectionString)
        {
            using (SqlConnection sqlConnection = ConnectionDatabase(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(spName, sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                        command.Parameters.Add(new SqlParameter()
                        {
                            Direction = ParameterDirection.Output,
                            ParameterName = "@notify",
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 255
                        });
                        sqlConnection.Open();
                        command.ExecuteNonQuery();
                        return command.Parameters["@notify"].Value.ToString();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }

                return null;
            } 
        }
    }
}

