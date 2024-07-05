using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace YTSDAL
{
    public class DbHelper
    {
        public DbHelper()
        {
        }

        public string AracBilgi(int terminalId)
        {
            TBL_TERMINALDAL terminal = new TBL_TERMINALDAL()
            {
                obj = new TBL_TERMINAL()
                {
                    TerminalID = terminalId
                }
            };
            terminal.Read();
            TBL_ARCHIVEDAL arsiv = new TBL_ARCHIVEDAL()
            {
                obj = new TBL_ARCHIVE()
                {
                    ArchiveID = terminal.obj.ArchiveId
                }
            };
            arsiv.Read();
            List<string> list = new List<string>()
            {
                terminal.obj.PlateNo
            };
            decimal distance = arsiv.obj.Distance;
            list.Add(distance.ToString());
            distance = arsiv.obj.Speed;
            list.Add(distance.ToString());
            return "";
        }

        public DataTable ExecuteQueryToDataTable<T>(IEnumerable<T> varlist)
        {
            PropertyInfo pi;
            DataTable dataTable;
            PropertyInfo[] propertyInfoArray;
            int i;
            object value;
            DataTable dtReturn = new DataTable();
            PropertyInfo[] oProps = null;
            if (varlist != null)
            {
                foreach (T rec in varlist)
                {
                    if (oProps == null)
                    {
                        oProps = rec.GetType().GetProperties();
                        propertyInfoArray = oProps;
                        for (i = 0; i < (int) propertyInfoArray.Length; i++)
                        {
                            pi = propertyInfoArray[i];
                            if (!pi.Name.ToUpper().StartsWith("TBL"))
                            {
                                Type colType = pi.PropertyType;
                                if ((!colType.IsGenericType ? false : colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                    DataRow dr = dtReturn.NewRow();
                    propertyInfoArray = oProps;
                    for (i = 0; i < (int) propertyInfoArray.Length; i++)
                    {
                        pi = propertyInfoArray[i];
                        if (!pi.Name.ToUpper().StartsWith("TBL"))
                        {
                            DataRow dataRow = dr;
                            string name = pi.Name;
                            if (pi.GetValue(rec, null) == null)
                            {
                                value = DBNull.Value;
                            }
                            else
                            {
                                value = pi.GetValue(rec, null);
                            }
                            dataRow[name] = value;
                        }
                    }
                    dtReturn.Rows.Add(dr);
                }
                dataTable = dtReturn;
            }
            else
            {
                dataTable = dtReturn;
            }
            return dataTable;
        }

        public DataSet GecmisIzleme(int terminalId, string baslamaZamani, string bitmeZamani)
        {
            DateTime dtBaslama = new DateTime();
            DateTime dtBitme = new DateTime();
            DataSet ds = new DataSet();
            try
            {
                dtBaslama = DateTime.Parse(baslamaZamani);
                dtBitme = DateTime.Parse(bitmeZamani);
                SqlParameter[] sqlParameterArray = new SqlParameter[3];
                SqlParameter sqlParameter = new SqlParameter("TerminalId", SqlDbType.Int)
                {
                    Value = terminalId
                };
                sqlParameterArray[0] = sqlParameter;
                SqlParameter sqlParameter1 = new SqlParameter("BaslamaZamani", SqlDbType.DateTime)
                {
                    Value = dtBaslama
                };
                sqlParameterArray[1] = sqlParameter1;
                SqlParameter sqlParameter2 = new SqlParameter("BitmeZamani", SqlDbType.DateTime)
                {
                    Value = dtBitme
                };
                sqlParameterArray[2] = sqlParameter2;
                ds = this.GetDataFromStoredProcedure("SP_Gecmis_Izleme", sqlParameterArray);
            }
            catch (Exception exception)
            {
                ds = new DataSet();
            }
            return ds;
        }

        public DataSet GetAktiviteRaporuByTerminalId(int terminalId, DateTime dtBaslama, DateTime dtBitme)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_AktiviteRaporuByTerminalId", con);
                    cmd.Parameters.Add("TerminalId", SqlDbType.Int).Value = terminalId;
                    cmd.Parameters.Add("DateBaslama", SqlDbType.DateTime).Value = dtBaslama;
                    cmd.Parameters.Add("DateBitme", SqlDbType.DateTime).Value = dtBitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            DataTable dt = new DataTable();
            DataSet dsDonus = new DataSet();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (i != 0)
                {
                    dt.Merge(ds.Tables[i]);
                }
                else
                {
                    dt = ds.Tables[i];
                }
            }
            DataTable dtCopy = dt.Copy();
            dsDonus.Tables.Add(dtCopy);
            return dsDonus;
        }

        public DataSet GetAracInfo(int terminalId)
        {
            DateTime now = DateTime.Now;
            DateTime dtBaslama = DateTime.Parse(now.ToShortDateString());
            now = DateTime.Now;
            now = DateTime.Parse(now.ToShortDateString());
            now = now.AddHours(23);
            now = now.AddMinutes(59);
            DateTime dtBitme = now.AddSeconds(59);
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    now = DateTime.Now;
                    dtBaslama = DateTime.Parse(now.ToShortDateString());
                    SqlCommand cmd = new SqlCommand("SP_AracBilgileriByTerminalId", con);
                    cmd.Parameters.Add("terminalID", SqlDbType.Int).Value = terminalId;
                    cmd.Parameters.Add("baslamatarihi", SqlDbType.DateTime).Value = dtBaslama;
                    cmd.Parameters.Add("bitmetarihi", SqlDbType.DateTime).Value = dtBitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetAracListesi(int companyId, int durum)
        {
            SqlCommand cmd;
            DateTime now;
            DateTime dtBaslama = new DateTime();
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    if (durum == 0)
                    {
                        now = DateTime.Now;
                        dtBaslama = DateTime.Parse(now.ToShortDateString());
                        cmd = new SqlCommand("SP_AracListesi", con);
                        cmd.Parameters.Add("CompanyId", SqlDbType.VarChar, 100).Value = companyId;
                        cmd.Parameters.Add("DateBaslama", SqlDbType.DateTime).Value = dtBaslama;
                        cmd.CommandType = CommandType.StoredProcedure;
                        (new SqlDataAdapter(cmd)).Fill(ds);
                    }
                    if (durum == 1)
                    {
                        cmd = new SqlCommand("SP_TumAraclarByCompanyId", con);
                        cmd.Parameters.Add("CompanyId", SqlDbType.VarChar, 100).Value = companyId;
                        cmd.CommandType = CommandType.StoredProcedure;
                        (new SqlDataAdapter(cmd)).Fill(ds);
                    }
                    if (durum == 2)
                    {
                        cmd = new SqlCommand("SP_AnaKullaniciAraclari", con);
                        cmd.Parameters.Add("UpperCompany", SqlDbType.Int).Value = companyId;
                        cmd.CommandType = CommandType.StoredProcedure;
                        (new SqlDataAdapter(cmd)).Fill(ds);
                    }
                    if (durum == 3)
                    {
                        now = DateTime.Now;
                        dtBaslama = DateTime.Parse(now.ToShortDateString());
                        cmd = new SqlCommand("SP_AracListesiByUpperCompanyId", con);
                        cmd.Parameters.Add("CompanyId", SqlDbType.VarChar, 100).Value = companyId;
                        cmd.Parameters.Add("DateBaslama", SqlDbType.DateTime).Value = dtBaslama;
                        cmd.CommandType = CommandType.StoredProcedure;
                        (new SqlDataAdapter(cmd)).Fill(ds);
                    }
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetCalismaRaporuByCompanyId(int companyId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_CalismaSuresiByCompanyId", con);
                    cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = companyId;
                    cmd.Parameters.Add("bastarihi", SqlDbType.DateTime).Value = dtbaslama;
                    cmd.Parameters.Add("bittarihi", SqlDbType.DateTime).Value = dtbitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            DataTable dt = new DataTable();
            DataSet dsDonus = new DataSet();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (i != 0)
                {
                    dt.Merge(ds.Tables[i]);
                }
                else
                {
                    dt = ds.Tables[i];
                }
            }
            DataTable dtCopy = dt.Copy();
            dsDonus.Tables.Add(dtCopy);
            return dsDonus;
        }

        public DataSet GetCalismaRaporuByTerminalId(int TerminalId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_CalismaRaporuByTerminalId", con);
                    cmd.Parameters.Add("terminalID", SqlDbType.Int).Value = TerminalId;
                    cmd.Parameters.Add("baslamatarihi", SqlDbType.DateTime).Value = dtbaslama;
                    cmd.Parameters.Add("bitmetarihi", SqlDbType.DateTime).Value = dtbitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            DataTable dt = new DataTable();
            DataSet dsDonus = new DataSet();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (i != 0)
                {
                    dt.Merge(ds.Tables[i]);
                }
                else
                {
                    dt = ds.Tables[i];
                }
            }
            DataTable dtCopy = dt.Copy();
            dsDonus.Tables.Add(dtCopy);
            return dsDonus;
        }

        public DataSet GetDataFromStoredProcedure(string _procname, SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(_procname, con);
                    for (int i = 0; i < (int) parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataTable GetDataTableFromQuery(string query)
        {
            DataTable dataTable;
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            try
            {
                try
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    dt.Load(cmd.ExecuteReader());
                    dataTable = dt;
                }
                catch (Exception exception)
                {
                    dt = new DataTable();
                    dataTable = dt;
                }
            }
            finally
            {
                con.Dispose();
                cmd.Dispose();
            }
            return dataTable;
        }

        public DataSet GetDurmaRaporuByCompanyId(int companyId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DurmaSuresiByCompanyId", con);
                    cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = companyId;
                    cmd.Parameters.Add("bastarihi", SqlDbType.DateTime).Value = dtbaslama;
                    cmd.Parameters.Add("bittarihi", SqlDbType.DateTime).Value = dtbitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            DataTable dt = new DataTable();
            DataSet dsDonus = new DataSet();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (i != 0)
                {
                    dt.Merge(ds.Tables[i]);
                }
                else
                {
                    dt = ds.Tables[i];
                }
            }
            DataTable dtCopy = dt.Copy();
            dsDonus.Tables.Add(dtCopy);
            return dsDonus;
        }

        public DataSet GetDurmaRaporuByTerminalId(int TerminalId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DurmaRaporuByTerminalId", con);
                    cmd.Parameters.Add("terminalID", SqlDbType.Int).Value = TerminalId;
                    cmd.Parameters.Add("baslamatarihi", SqlDbType.DateTime).Value = dtbaslama;
                    cmd.Parameters.Add("bitmetarihi", SqlDbType.DateTime).Value = dtbitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            DataTable dt = new DataTable();
            DataSet dsDonus = new DataSet();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (i != 0)
                {
                    dt.Merge(ds.Tables[i]);
                }
                else
                {
                    dt = ds.Tables[i];
                }
            }
            DataTable dtCopy = dt.Copy();
            dsDonus.Tables.Add(dtCopy);
            return dsDonus;
        }

        public DataSet GetIhlalListesiByCompanylId(int companyId)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    DateTime now = DateTime.Now;
                    DateTime dtBaslama = DateTime.Parse(now.ToShortDateString());
                    now = DateTime.Now;
                    now = DateTime.Parse(now.ToShortDateString());
                    now = now.AddHours(23);
                    now = now.AddMinutes(59);
                    DateTime dtBitme = now.AddSeconds(59);
                    SqlCommand cmd = new SqlCommand("SP_IhlalListesiByCompanyId", con);
                    cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = companyId;
                    cmd.Parameters.Add("DtBaslama", SqlDbType.DateTime).Value = dtBaslama;
                    cmd.Parameters.Add("DtBitme", SqlDbType.DateTime).Value = dtBitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            DataTable dt = new DataTable();
            DataSet dsDonus = new DataSet();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (i != 0)
                {
                    dt.Merge(ds.Tables[i]);
                }
                else
                {
                    dt = ds.Tables[i];
                }
            }
            DataTable dtCopy = dt.Copy();
            dsDonus.Tables.Add(dtCopy);
            return dsDonus;
        }

        public DataSet GetIhlalListesiByTerminalId(int terminalId, DateTime dtBaslama, DateTime dtBitme)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_IhlalListesiByTerminalId", con);
                    cmd.Parameters.Add("TerminalId", SqlDbType.Int).Value = terminalId;
                    cmd.Parameters.Add("DateBaslama", SqlDbType.DateTime).Value = dtBaslama;
                    cmd.Parameters.Add("DateBitme", SqlDbType.DateTime).Value = dtBitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public Kullanici GetKullaniciBilgileri()
        {
            Kullanici k = new Kullanici();
            if (HttpContext.Current.Request.Headers.Get("userInfo") == null)
            {
                k = new Kullanici()
                {
                    CompanyId = -1,
                    KullaniciId = -1,
                    UpperCompanyId = -1,
                    UserName = "",
                    LastName = "",
                    FirstName = "",
                    CompanyName = ""
                };
            }
            else
            {
                string[] kullanici = HttpContext.Current.Request.Headers.Get("userInfo").ToString().Split(new char[] { '\u005F' });
                k.KullaniciId = int.Parse(kullanici[0]);
                k.CompanyId = int.Parse(kullanici[1]);
                k.CompanyName = kullanici[2];
                k.role = kullanici[3];
                k.UpperCompanyId = int.Parse(kullanici[4]);
            }
            return k;
        }

        public List<TBL_SEFER_PAKETI> GetMesafeRaporuByTerminalId(int TerminalId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DetayliMesafeRaporu", con);
                    cmd.Parameters.Add("terminalId", SqlDbType.Int).Value = TerminalId;
                    cmd.Parameters.Add("DateBaslama", SqlDbType.DateTime).Value = dtbaslama;
                    cmd.Parameters.Add("DateBitme", SqlDbType.DateTime).Value = dtbitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            DataTable dt = new DataTable();
            DataSet dsDonus = new DataSet();
            List<TBL_SEFER_PAKETI> list = new List<TBL_SEFER_PAKETI>();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                dt = ds.Tables[i];
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow bas = dt.Rows[j];
                    DataRow bit = dt.Rows[j + 1];
                    TBL_SEFER_PAKETI _obj = new TBL_SEFER_PAKETI()
                    {
                        SeferBaslama = DateTime.Parse(bas["DateTimeGps"].ToString()),
                        BaslangicEnlem = decimal.Parse(bas["CoordinateX"].ToString()),
                        BaslangicBoylam = decimal.Parse(bas["CoordinateY"].ToString()),
                        SeferBitme = DateTime.Parse(bit["DateTimeGps"].ToString()),
                        BitisEnlem = decimal.Parse(bit["CoordinateX"].ToString()),
                        BitisBoylam = decimal.Parse(bit["CoordinateY"].ToString()),
                        GidilenYol = decimal.Parse(bit["Distance"].ToString()) - decimal.Parse(bas["Distance"].ToString())
                    };
                    double hours = (_obj.SeferBitme - _obj.SeferBaslama).TotalMinutes;
                    _obj.SeferSuresi = decimal.Parse(hours.ToString());
                    string[] str = new string[] { bas["Quarter"].ToString(), " ", bas["Street"].ToString(), " ", bas["District"].ToString(), " ", bas["City"].ToString(), " ", bas["Country"].ToString() };
                    _obj.BaslangicNoktasi = string.Concat(str);
                    str = new string[] { bit["Quarter"].ToString(), " ", bit["Street"].ToString(), " ", bit["District"].ToString(), " ", bit["City"].ToString(), " ", bit["Country"].ToString() };
                    _obj.BitisNoktasi = string.Concat(str);
                    list.Add(_obj);
                    j++;
                }
            }
            return list;
        }

        public DataSet GetPersonelTakipByTerminalId(int terminalId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_PersonelTakipBilgileriByTerminalId", con);
                    cmd.Parameters.Add("TerminalId", SqlDbType.Int).Value = terminalId;
                    cmd.Parameters.Add("DateBaslama", SqlDbType.DateTime).Value = dtbaslama;
                    cmd.Parameters.Add("DateBitme", SqlDbType.DateTime).Value = dtbitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetRolantiRaporuByCompanyId(int companyId, DateTime dtbaslama, DateTime dtbitme)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_RolantiSuresiByCompanyId", con);
                    cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = companyId;
                    cmd.Parameters.Add("bastarihi", SqlDbType.DateTime).Value = dtbaslama;
                    cmd.Parameters.Add("bittarihi", SqlDbType.DateTime).Value = dtbitme;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            DataTable dt = new DataTable();
            DataSet dsDonus = new DataSet();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (i != 0)
                {
                    dt.Merge(ds.Tables[i]);
                }
                else
                {
                    dt = ds.Tables[i];
                }
            }
            DataTable dtCopy = dt.Copy();
            dsDonus.Tables.Add(dtCopy);
            return dsDonus;
        }

        public DataSet GetSeferPaketi(DateTime dtBaslama, DateTime dtBitme)
        {
            Kullanici b = this.GetKullaniciBilgileri();
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_SeferPaketi", con);
                    cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = b.CompanyId;
                    cmd.Parameters.Add("DateBaslama", SqlDbType.DateTime).Value = dtBaslama;
                    cmd.Parameters.Add("DateBitme", SqlDbType.DateTime).Value = dtBitme;
                    if (!(b.role != "Normal"))
                    {
                        cmd.Parameters.Add("Role", SqlDbType.Int).Value = 0;
                    }
                    else
                    {
                        cmd.Parameters.Add("Role", SqlDbType.Int).Value = 1;
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public int GetTerminalId(string unitId)
        {
            TBL_TERMINAL original = (new AracTakipDb()).TBL_TERMINAL.SingleOrDefault<TBL_TERMINAL>((TBL_TERMINAL x) => x.UnitId == unitId);
            return (original == null ? 0 : original.TerminalID);
        }
        public string GetTerminalip(int TerminalIDs)
        {
            TBL_TERMINAL original = (new AracTakipDb()).TBL_TERMINAL.SingleOrDefault<TBL_TERMINAL>((TBL_TERMINAL x) => x.TerminalID == TerminalIDs);
            return (original == null ? "" : original.UnitIpAddress);
        }

        public int GetTerminalIdFromArchive(int ArchiveId)
        {
            TBL_ARCHIVE original = (new AracTakipDb()).TBL_ARCHIVE.SingleOrDefault<TBL_ARCHIVE>((TBL_ARCHIVE x) => x.ArchiveID == ArchiveId);
            return (original == null ? 0 : original.ArchiveID);
        }

        public Kullanici Login(string username, string password)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_LoginBilgileri", con);
                    cmd.Parameters.Add("username", SqlDbType.NVarChar, 50).Value = username;
                    cmd.Parameters.Add("password", SqlDbType.NVarChar, 50).Value = password;
                    cmd.CommandType = CommandType.StoredProcedure;
                    (new SqlDataAdapter(cmd)).Fill(ds);
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            Kullanici k = new Kullanici();
            if (dt.Rows.Count > 0)
            {
                IEnumerator enumerator = dt.Rows.GetEnumerator();
                try
                {
                    if (enumerator.MoveNext())
                    {
                        DataRow item = (DataRow) enumerator.Current;
                        k.CompanyId = int.Parse(item["CompanyID"].ToString());
                        k.CompanyName = item["CompanyName"].ToString();
                        k.FirstName = item["FirstName"].ToString();
                        k.KullaniciId = int.Parse(item["UserID"].ToString());
                        k.UpperCompanyId = int.Parse(item["UpperCompany"].ToString());
                        k.UserName = item["UserName"].ToString();
                        k.role = "Normal";
                        if (k.CompanyId == k.UpperCompanyId)
                        {
                            k.role = "Ana";
                        }
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            }
            return k;
        }

        public void mesaj_gonder(string _telno, string _msg)
        {
            string apino = "1";
            string user = "5444463535";
            string pass = "5444463535";
            string numaralar = _telno;
            string mesaj = _msg;
            string baslik = "DSB TAKIP";
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create("http://kurecell.com.tr/kurecellapiV2/api-center/index.php");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            object[] objArray = new object[] { req, "&apiNo=", apino, "&user=", user, "&pass=", pass, "&numaralar=", numaralar, "&mesaj=", mesaj, "&baslik=", baslik };
            string strNewValue = string.Concat(objArray);
            req.ContentLength = (long) strNewValue.Length;
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
            stOut.Write(strNewValue);
            stOut.Close();
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            stIn.ReadToEnd();
            stIn.Close();
        }

        public DataSet PopDataBaseName(int terminalId, int archiveID)
        {
            SqlCommand cmd;
            Kullanici b = this.GetKullaniciBilgileri();
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection((new AracTakipDb()).Database.Connection.ConnectionString);
            try
            {
                try
                {
                    DateTime dt = DateTime.Now.AddSeconds(-60);
                    if (terminalId != 0)
                    {
                        cmd = new SqlCommand("SP_GetAnimatedDataByTerminalId", con);
                        cmd.Parameters.Add("TerminalId", SqlDbType.VarChar, 100).Value = terminalId;
                        cmd.Parameters.Add("archiveID", SqlDbType.Int).Value = archiveID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        (new SqlDataAdapter(cmd)).Fill(ds);
                    }
                    else
                    {
                        cmd = new SqlCommand("SP_Sample", con);
                        cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = b.CompanyId;
                        cmd.Parameters.Add("zaman", SqlDbType.DateTime).Value = dt;
                        cmd.CommandType = CommandType.StoredProcedure;
                        (new SqlDataAdapter(cmd)).Fill(ds);
                    }
                }
                catch (Exception exception)
                {
                    ds = new DataSet();
                }
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public string SendPostDataXML(string url, string data)
        {
            string str;
            WebClient wb = new WebClient();
            try
            {
                NameValueCollection ldata = new NameValueCollection();
                ldata["data"] = data;
                byte[] response = wb.UploadValues(url, "POST", ldata);
                str = Encoding.Default.GetString(response);
            }
            finally
            {
                if (wb != null)
                {
                    ((IDisposable) wb).Dispose();
                }
            }
            return str;
        }

        public void SendSMS(int terminalId, string _mesaj)
        {
            TBL_TERMINALDAL dal = new TBL_TERMINALDAL()
            {
                obj = new TBL_TERMINAL()
                {
                    TerminalID = terminalId
                }
            };
            if (dal.Read() == ReturnMessage.OkumaBasarili.ToString())
            {
                int companyId = dal.obj.CompanyID;
                TBL_COMPANYDAL comp = new TBL_COMPANYDAL()
                {
                    obj = new TBL_COMPANY()
                    {
                        CompanyID = companyId
                    }
                };
                if (comp.Read() == ReturnMessage.OkumaBasarili.ToString())
                {
                    string _telno = comp.obj.ContactPhone;
                    if (_telno != "0")
                    {
                        string _m = string.Concat("Plaka: ", dal.obj.PlateNo);
                        _m = string.Concat(_m, " ");
                        _m = string.Concat(_m, "Zaman: ");
                        _m = string.Concat(_m, dal.obj.PositionDate);
                        _m = string.Concat(_m, " Mesaj: ");
                        this.mesaj_gonder(_telno, string.Concat(_m, _mesaj));
                    }
                }
            }
        }

        public string XMLParse(string Val)
        {
            XDocument document = XDocument.Parse(Val);
            return document.Root.Element("TOKEN").Value;
        }
    }
}