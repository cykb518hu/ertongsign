<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        var name = context.Request.Form["name"];
        var userId = 0;
        var userName = "";
        var userKey = "";
        var userImg = "";
        string msg = "{{\"code\":\"{0}\",\"userId\":\"{1}\",\"userName\":\"{2}\",\"userKey\":\"{3}\",\"userImg\":\"{4}\"}}";

        SqlRepository sqlRepository = new SqlRepository();
        sqlRepository.SearchUser(name, out userName, out userId, out userKey, out userImg);

        context.Response.ContentType = "text/plain";
        if (userId > 0)
        {
            context.Response.Write(string.Format(msg, 1, userId, userName, userKey, userImg));
        }
        else
        {
            context.Response.Write(string.Format(msg, 0, userId, userName, userKey, userImg));
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}