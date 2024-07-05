using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
namespace YTSDAL
{
    public class DurakIhlal
    {
        public DurakIhlal()
        {
        }

        public void Deneme()
        {
        }

        public string getDurakIhlalleri(int companyId, DateTime dtBaslama, DateTime dtBitme)
        {
            DataSet dataSet = new DataSet();
            SqlConnection sqlConnection = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);

            try
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SP_DurakIhlalleribyCompanyId", sqlConnection);
                    sqlCommand.Parameters.Add("companyId", SqlDbType.Int).Value = companyId;
                    sqlCommand.Parameters.Add("baslamatarihi", SqlDbType.DateTime).Value = dtBaslama;
                    sqlCommand.Parameters.Add("bitmetarihi", SqlDbType.DateTime).Value = dtBitme;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
                }
                catch (Exception exception)
                {
                    dataSet = new DataSet();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return JsonConvert.SerializeObject(dataSet.Tables[0]);
        }
    }
}