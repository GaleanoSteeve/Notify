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
            if (!IsPostBack)
            {
                try
                {
                    if (Session["CodigoUsuario"] != null && Session["IdPerfil"] != null && Session["NombreUsuario"] != null) //Sesion activa
                    {
                        string Usuario = Session["NombreUsuario"].ToString();
                        int IdPerfil = Convert.ToInt32(Session["IdPerfil"]);

                        string Menu = objMenu.CrearMenu(IdPerfil, Usuario);
                        labMenu.Text = Menu;
                    }
                    else
                    {
                        FormsAuthentication.SignOut();
                        string Titulo = "Error en Autenticación";
                        string Mensaje = "Algunos datos del usuario no se cargaron correctamente. Por favor intenta de nuevo.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmLogin.aspx'});";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
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
        }

        #endregion
    }
}