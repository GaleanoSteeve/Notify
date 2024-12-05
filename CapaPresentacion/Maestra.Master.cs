using System;
using CapaNegocios;
using System.Web.UI;
using System.Web.Security;

namespace CapaPresentacion
{
    public partial class Maestra : System.Web.UI.MasterPage
    {
        #region Variables

        NegMenu objMenu = new NegMenu();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CodigoUsuario"] != null && Session["IdPerfil"] != null) //Sesion activa
                {
                    string Usuario = Session["Usuario"].ToString();
                    int IdPerfil = Convert.ToInt32(Session["IdPerfil"]);

                    string Menu = objMenu.CrearMenu(IdPerfil, Usuario);
                    labMenu.Text = Menu;
                }
                else
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("frmLogin.aspx");
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Creando Menú";
                string Mensaje = "Error tratando de crear el Menú: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmLogin.aspx'});";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }

        #endregion
    }
}