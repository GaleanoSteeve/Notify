using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;

namespace CapaPresentacion
{
    public partial class FormLogin : System.Web.UI.Page
    {
        #region Variables

        private ObjUsuarios oUsuario = new ObjUsuarios();
        private NegUsuarios objUsuario = new NegUsuarios();
        private NegCriptografia objCriptografia = new NegCriptografia();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Contents.RemoveAll();
            Session["CodigoUsuario"] = null;
            Session["IdPerfil"] = null;
            Session["Usuario"] = null;

            if (!IsPostBack)
            {
                txtContrasena.Text = "";

                string SalirLogin = Request.QueryString["CerrarSesion"] ?? "0";

                if (Convert.ToBoolean(SalirLogin == "-1" ? "True" : "False"))
                {
                    Session.Contents.RemoveAll();
                    Session["CodigoUsuario"] = null;
                    Session["IdPerfil"] = null;
                    Session["Usuario"] = null;
                }
            }
            txtUsuario.Focus();
        }

        #endregion

        //Metodos
        private bool ValidarCampos()
        {
            if (txtUsuario.Text.Trim() == "")
            {
                string Titulo = "Advertencia";
                string Mensaje = "El campo usuario es obligatorio.";
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                txtUsuario.Focus();
                return false;
            }
            else if (txtContrasena.Text.Trim() == "")
            {
                string Titulo = "Advertencia";
                string Mensaje = "El campo contraseña es obligatorio.";
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                txtContrasena.Focus();
                return false;
            }
            return true;
        }

        //Ingresar
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    oUsuario.Usuario = txtUsuario.Text.Trim();
                    oUsuario.Contrasena = txtContrasena.Text.Trim();
                    DataSet dsUsuario = objUsuario.ListarUsuario(oUsuario);

                    if (dsUsuario.Tables.Count == 2)
                    {
                        DataTable dtUsuario = dsUsuario.Tables[0];

                        if (dtUsuario.Rows.Count > 0) //Usuario existe
                        {
                            string ContrasenaAlmacenada = dtUsuario.Rows[0]["Contrasena"].ToString();
                            string ContrasenaIngresada = objCriptografia.EncriptarContrasena(txtContrasena.Text.Trim());

                            if (ContrasenaAlmacenada == ContrasenaIngresada) //Contrasena correcta
                            {
                                bool EstadoUsuario = Convert.ToBoolean(dtUsuario.Rows[0]["Estado"]);

                                if (EstadoUsuario) //Usuario activo
                                {
                                    DataTable dtPerfil = dsUsuario.Tables[1];

                                    if (dtPerfil.Rows.Count > 0) //Usuario tiene perfil asignado
                                    {
                                        bool PerfilActivo = Convert.ToBoolean(dtPerfil.Rows[0]["Estado"]);

                                        if (PerfilActivo) //Perfil usuario activo
                                        {
                                            Session["CodigoUsuario"] = dtUsuario.Rows[0]["Codigo"].ToString();
                                            Session["IdPerfil"] = dtUsuario.Rows[0]["IdPerfil"].ToString();
                                            Session["Usuario"] = dtUsuario.Rows[0]["Usuario"].ToString();
                                            Response.Redirect("frmInicio.aspx");
                                        }
                                        else
                                        {
                                            string Titulo = "Advertencia";
                                            string Mensaje = "El perfil del usuario está inactivo. Por favor comuníquese con el administrador del sistema.";
                                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                                            txtContrasena.Text = "";
                                            txtUsuario.Text = "";
                                            txtUsuario.Focus();
                                        }
                                    }
                                    else
                                    {
                                        string Titulo = "Advertencia";
                                        string Mensaje = "Usuario no tiene perfil asignado. Por favor comuníquese con el administrador del sistema.";
                                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                                        txtContrasena.Text = "";
                                        txtUsuario.Text = "";
                                        txtUsuario.Focus();
                                    }
                                }
                                else
                                {
                                    string Titulo = "Advertencia";
                                    string Mensaje = "Usuario inactivo. Por favor comuníquese con el administrador del sistema.";
                                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                                    txtContrasena.Text = "";
                                    txtUsuario.Text = "";
                                    txtUsuario.Focus();
                                }
                            }
                            else
                            {
                                string Titulo = "Advertencia";
                                string Mensaje = "Contraseña incorrecta.";
                                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                                txtContrasena.Text = "";
                                txtContrasena.Focus();
                            }
                        }
                        else
                        {
                            string Titulo = "Advertencia";
                            string Mensaje = "Usuario o contraseña incorrectos.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                            txtUsuario.Focus();
                        }
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "La consulta no arrojó datos. Por favor comuníquese al administrador del sistema.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        txtUsuario.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Validando Usuario";
                string Mensaje = "Error tratando de validar el usuario: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmLogin.aspx'});";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
    }
}