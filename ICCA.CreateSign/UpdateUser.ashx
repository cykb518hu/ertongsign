<%@ WebHandler Language="C#" Class="UpdateUser" %>

using System;
using System.Web;

public class UpdateUser : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        var userId = context.Request.Form["id"];
        var userKey = context.Request.Form["ukey"];
        var userImg = context.Request.Form["imgStr"];

        SqlRepository sqlRepository = new SqlRepository();
        var result = sqlRepository.UpdateUserImg(userId, userKey, userImg);

        string msg = "{{\"code\":\"{0}\",\"msg\":\"{1}\"}}";

        context.Response.ContentType = "text/plain";
        if (result)
        {
            context.Response.Write(string.Format(msg, 1, "成功"));
        }
        else
        {
            context.Response.Write(string.Format(msg, 0, "失败"));
        }
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}