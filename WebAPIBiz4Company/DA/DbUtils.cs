using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPIBiz4Company.Models;

namespace WebAPIBiz4Company.DA
{
    public class DbUtils
    {
        public DbUtils()
        {
        }

        public static SqlConnection ConnectionDatabase(String connectString)
        {
            return new SqlConnection(connectString);
        }

        public static List<User> GetAllDataInTable(String spName, String connectionString)
        { 
            using (SqlConnection conn = ConnectionDatabase(connectionString))
            {
                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand(spName, conn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Table");

                        DataTable dataTable = ds.Tables["Table"];
                        int count = 0;
                        List<User> list = new List<User>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            User user = new User();
                            count = 0;
                            while (count < row.ItemArray.Length)
                            {
                                Type type = user.GetType();
                                PropertyInfo[] propertyInfos = type.GetProperties();
                                propertyInfos[count].SetValue(user, row[count].GetType().ToString() == "System.DBNull" ? row[count].ToString(): row[count]);
                                count++;
                            }
                            list.Add(user);
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

            return new List<User>();
        }
    }
}

