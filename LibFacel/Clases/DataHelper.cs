

#region Referencias

using System;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace DBE.Net.Facel.LibFacel.Clases
{
    public class DataHelper
    {
        public static DataSet GetData(DataSet dataset)
        {
            string TEMPLATE_ERROR = "Excepcion en metodo '{0}': {1}";
            string errorMessage = string.Empty;
            string conexion = string.Empty;

            try
            {
                conexion = dataset.ExtendedProperties["Conexion"].ToString();
                using (SqlConnection connSql = new SqlConnection(conexion))
                {
                    connSql.Open();
                    foreach (DataTable dt in dataset.Tables)
                    {
                        string errorDT = string.Empty;
                        string sentencia = dt.ExtendedProperties["Metodo"].ToString();
                        SqlDataAdapter daSqlServer = null;

                        try
                        {
                            daSqlServer = new SqlDataAdapter(sentencia, connSql);
                            daSqlServer.SelectCommand.CommandTimeout = connSql.ConnectionTimeout;
                            daSqlServer.Fill(dt);
                        }
                        catch (Exception excepcionDT)
                        {
                            errorDT = string.Format(TEMPLATE_ERROR, "GetDataDT", excepcionDT.Message);
                        }
                        finally
                        {
                            dt.ExtendedProperties["Error"] = errorDT;
                        }
                    }
                    connSql.Close();
                }
            }
            catch (Exception excepcion)
            {
                errorMessage = string.Format(TEMPLATE_ERROR, "GetData", excepcion.Message);
            }
            finally
            {
                dataset.ExtendedProperties["Error"] = errorMessage;
            }

            return dataset;
        }
    }
}
