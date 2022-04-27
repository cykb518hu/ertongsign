using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SqlRepository
/// </summary>
public class SqlRepository
{
    public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Philips_ICCA"].ToString();
    public SqlRepository()
    {
        var logFilePath = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "Log");
        LogUtil.Initialize(logFilePath, "debug", 30);
        //
        // TODO: Add constructor logic here
        //
    }

    public bool UpdateUserImg(string userId,string userKey,string userImg)
    {
        bool result = true;
        try
        {
            LogUtil.DebugLog("UpdateUserImg userid:" + userId + ", userKey:" + userKey);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                var sql = "update [Philips_ICCA].[dbo].[User] set  SignImg='" + userImg + "', UKeyCertID='" + userKey + "' where UserID=" + userId;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }
        catch(Exception ex)
        {
            result = false;
            LogUtil.ErrorLog("UpdateUserImg 报错:" + ex.ToString());
        }
        return result;
    }

    public void SearchUser(string name,out string userName, out int userId, out string userKey, out string userImg)
    {
        userName = "";
        userId = 0;
        userKey = "";
        userImg = "";
        try
        {
            LogUtil.DebugLog("SearchUser name:" + name);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                var sql = @"  select printuser.*, iccauser.userDomainName from Philips_ICCA.dbo.[User] printuser left join CISPrimaryDB.dbo.Users iccauser on printuser.LoginName = iccauser.userId where userDomainName = N'" + name + "'";
                cmd.CommandText = sql;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userId = DBNull.Value == reader["UserID"] ? 0 : Convert.ToInt32(reader["UserID"]);
                    userName = DBNull.Value == reader["Name"] ? "" : reader["Name"].ToString();
                    userKey = DBNull.Value == reader["UKeyCertID"] ? "" : reader["UKeyCertID"].ToString();
                    userImg = DBNull.Value == reader["SignImg"] ? "" : reader["SignImg"].ToString();
                }
            }
        }
        catch(Exception ex)
        {
            LogUtil.ErrorLog("SearchUser 报错:" + ex.ToString());
        }
    }
}