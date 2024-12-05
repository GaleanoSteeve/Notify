using System;

namespace CapaPresentacion
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }
        protected void Session_End(object sender, EventArgs e)
        {
            Session.Contents.RemoveAll();
            Session["CodigoUsuario"] = null;
            Session["IdPerfil"] = null;
            Session.Clear();
            Session.Abandon();
        }
        protected void Application_End(object sender, EventArgs e)
        {
            Session.Contents.RemoveAll();
            Session["CodigoUsuario"] = null;
            Session["IdPerfil"] = null;
            Session.Clear();
            Session.Abandon();
        }
    }
}