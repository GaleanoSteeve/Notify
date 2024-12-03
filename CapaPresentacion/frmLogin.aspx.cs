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
        private NegCriptografia objCrypto = new NegCriptografia();

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
                txtUsuario.Focus();
                txtContrasena.Text = "";

                string sLoginOut = Request.QueryString["CerrarSesion"] ?? "0";

                if (Convert.ToBoolean(sLoginOut == "-1" ? "True" : "False"))
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
                string titulo = "Advertencia";
                string mensaje = "El campo usuario es obligatorio.";
                string script = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", script, true);
                txtUsuario.Focus();
                return false;
            }
            else if (txtContrasena.Text.Trim() == "")
            {
                string titulo = "Advertencia";
                string mensaje = "El campo contraseña es obligatorio.";
                string script = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", script, true);
                txtContrasena.Focus();
                return false;
            }
            return true;
        }

        //Boton
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
                            string ContrasenaIngresada = objCrypto.EncriptarContrasena(txtContrasena.Text.Trim());

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
                                            string titulo = "Advertencia";
                                            string mensaje = "Perfil de usuario inactivo. Por favor comuníquese con el administrador.";
                                            string script = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", script, true);
                                            btnIngresar.Focus();
                                        }
                                    }
                                    else
                                    {
                                        string titulo = "Advertencia";
                                        string mensaje = "Usuario no tiene perfil asignado. Por favor comuníquese con el administrador.";
                                        string script = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", script, true);
                                        btnIngresar.Focus();
                                    }
                                }
                                else
                                {
                                    string titulo = "Advertencia";
                                    string mensaje = "Usuario inactivo. Por favor comuníquese con el administrador.";
                                    string script = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", script, true);
                                    btnIngresar.Focus();
                                }
                            }
                            else
                            {
                                string titulo = "Advertencia";
                                string mensaje = "Contraseña inválida.";
                                string script = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", script, true);
                                txtContrasena.Text = "";
                                txtContrasena.Focus();
                            }
                        }
                        else
                        {
                            string titulo = "Advertencia";
                            string mensaje = "Credenciales inválidas.";
                            string script = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", script, true);
                            txtUsuario.Focus();
                        }
                    }
                    else
                    {
                        string titulo = "Advertencia";
                        string mensaje = "La consulta no arrojó datos. Por favor comuníquese al administrador del sistema.";
                        string script = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", script, true);
                        txtUsuario.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string titulo = "Error";
                string mensaje = "Error tratando de validar el usuario: " + ex.Message.ToString().Replace("'", "");
                string str = "alertify.alert('" + titulo + "', '" + mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", str, true);
                txtUsuario.Focus();
            }
        }
    }
}