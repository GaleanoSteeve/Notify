using System;
using CapaNegocios;
using System.Web.UI;

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
                if (Session["IdPerfil"] != null && Convert.ToInt16(Session["IdPerfil"]) > 0)
                {
                    string Usuario = Session["Usuario"].ToString();
                    int IdPerfil = Convert.ToInt32(Session["IdPerfil"]);
                    string Menu = objMenu.CrearMenu(IdPerfil, Usuario);
                    labMenu.Text = Menu;
                }
                else
                {
                    Response.Redirect("frmLogin.aspx");
                }
            }
            catch (Exception ex)
            {
                string titulo = "Error";
                string mensaje = "Error tratando de crear el menú: " + ex.Message.ToString().Replace("'", "");
                string str = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", str, true);
            }
        }

        #endregion
    }
}