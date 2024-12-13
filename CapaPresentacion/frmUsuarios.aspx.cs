using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;
using System.Transactions;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class frmUsuarios : System.Web.UI.Page
    {
        #region Variables

        ObjUsuarios oUsuario = new ObjUsuarios();

        NegPerfiles objPerfiles = new NegPerfiles();
        NegUsuarios objUsuarios = new NegUsuarios();
        NegCriptografia objCriptografia = new NegCriptografia();


        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            labError.Visible = false;

            if (!IsPostBack)
            {
                ListarComboPerfiles();
                ListarComboPermisos();
                ListarComboEstados();
                ListarUsuarios();
            }
        }

        #endregion

        //Metodos
        private void ListarUsuarios()
        {
            try
            {
                DataTable dtUsuarios = objUsuarios.ListarUsuarios();

                if (dtUsuarios.Rows.Count > 0)
                {
                    gvUsuarios.DataSource = dtUsuarios;
                    gvUsuarios.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen usuarios creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmUsuarios.aspx'});";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Usuarios";
                string Mensaje = "Error tratando de listar los usuarios: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmUsuarios.aspx'});";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        private void ListarComboEstados()
        {
            try
            {
                DataTable dtEstados = new DataTable();

                //Agregar columnas
                dtEstados.Columns.Add("IdEstado", typeof(int));
                dtEstados.Columns.Add("Estado", typeof(string));

                //Agregar filas
                dtEstados.Rows.Add(0, "NO");
                dtEstados.Rows.Add(1, "SI");

                dtEstados.AcceptChanges(); //Aceptar cambios

                cboEstado.DataSource = dtEstados;
                cboEstado.DataValueField = "IdEstado";
                cboEstado.DataTextField = "Estado";
                cboEstado.DataBind();
                cboEstado.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                txtCodigo.Text = "";
                labError.Visible = true;
                labMensaje.Text = "Error tratando de listar el ComboBox de los estados: " + ex.Message;
                modUsuarios.Show();
            }
        }
        private void ListarMaximoCodigo()
        {
            try
            {
                DataTable dtDatos = objUsuarios.ListarMaximoCodigo();

                if (dtDatos.Rows.Count > 0)
                {
                    int Codigo = Convert.ToInt16(dtDatos.Rows[0]["Codigo"]);
                    txtCodigo.Text = Codigo.ToString();
                }
                else
                {
                    labMensaje.Text = "No se pudo listar el máximo código de usuario. Por favor intente de nuevo.";
                    labError.Visible = true;
                    txtCodigo.Text = "";
                    modUsuarios.Show();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar el código del perfil: " + ex.Message;
                labError.Visible = true;
                txtCodigo.Text = "";
                modUsuarios.Show();
            }
        }
        private void ListarComboPerfiles()
        {
            try
            {
                DataTable dtPerfiles = objPerfiles.ListarComboPerfiles();

                if (dtPerfiles.Rows.Count > 0)
                {
                    cboPerfiles.DataSource = dtPerfiles;
                    cboPerfiles.DataValueField = "IdPerfil";
                    cboPerfiles.DataTextField = "Nombre";
                    cboPerfiles.DataBind();
                }
                else
                {
                    txtCodigo.Text = "";
                    labError.Visible = true;
                    labMensaje.Text = "Error tratando de listar el ComboBox de los perfiles: no existen perfiles creados en base de datos.";
                    modUsuarios.Show();
                }
            }
            catch (Exception ex)
            {
                txtCodigo.Text = "";
                labError.Visible = true;
                labMensaje.Text = "Error tratando de cargar el ComboBox de los perfiles: " + ex.Message;
                modUsuarios.Show();
            }
        }
        private void ListarComboPermisos()
        {
            try
            {
                DataTable dtPermisos = new DataTable();

                //Agregar columnas
                dtPermisos.Columns.Add("IdPermiso", typeof(int));
                dtPermisos.Columns.Add("Permiso", typeof(string));

                //Agregar filas
                dtPermisos.Rows.Add(0, "NO");
                dtPermisos.Rows.Add(1, "SI");

                dtPermisos.AcceptChanges(); //Aceptar cambios

                cboPuedeEliminar.DataSource = dtPermisos;
                cboPuedeEliminar.DataValueField = "IdPermiso";
                cboPuedeEliminar.DataTextField = "Permiso";
                cboPuedeEliminar.DataBind();
                cboPuedeEliminar.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                txtCodigo.Text = "";
                labError.Visible = true;
                labMensaje.Text = "Error tratando de listar el ComboBox de los permisos para eliminar: " + ex.Message;
                modUsuarios.Show();
            }
        }

        //Administrar
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            labUsuario.Text = "default";
            labCrear.Text = "1";
            ListarMaximoCodigo();
            txtNombres.Focus();
            modUsuarios.Show();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEditar = (LinkButton)sender;
                string strDatos = btnEditar.CommandArgument;
                string[] Datos = strDatos.Split(',');
                labCrear.Text = "0";

                int Codigo = Convert.ToInt32(Datos[0]);

                if (Codigo > 0)
                {
                    DataTable dtUsuario = objUsuarios.ListarUsuarioCodigo(Codigo);

                    if (dtUsuario.Rows.Count > 0) //Perfil existe
                    {
                        txtCodigo.Text = dtUsuario.Rows[0]["Codigo"].ToString();
                        txtNombres.Text = dtUsuario.Rows[0]["Nombres"].ToString();
                        txtUsuario.Text = dtUsuario.Rows[0]["Usuario"].ToString();
                        labUsuario.Text = dtUsuario.Rows[0]["Usuario"].ToString();
                        cboPerfiles.SelectedValue = dtUsuario.Rows[0]["IdPerfil"].ToString();
                        cboPuedeEliminar.SelectedValue = Convert.ToBoolean(dtUsuario.Rows[0]["PuedeEliminar"]) ? "1" : "0";
                        cboEstado.SelectedValue = Convert.ToBoolean(dtUsuario.Rows[0]["Estado"]) ? "1" : "0";
                        modUsuarios.Show();
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "El usuario seleccionado no existe en base de datos.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmUsuarios.aspx'});";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "El usuario seleccionado no tiene código asignado.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmUsuarios.aspx'});";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Modificando Usuario";
                string Mensaje = "Error tratando de modificar el usuario: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmUsuarios.aspx'});";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }

        //Guardar
        private bool ValidarDatos()
        {
            try
            {
                if (txtCodigo.Text.Trim() == "")
                {
                    txtCodigo.Focus();
                    labMensaje.Text = "El campo Código es obligatorio.";
                    labError.Visible = true;
                    modUsuarios.Show();
                    return false;
                }
                if (Convert.ToInt32(txtCodigo.Text.Trim()) <= 0)
                {
                    txtCodigo.Focus();
                    labMensaje.Text = "El campo Código debe ser mayor que cero.";
                    labError.Visible = true;
                    modUsuarios.Show();
                    return false;
                }
                else if (txtNombres.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Nombres es obligatorio.";
                    labError.Visible = true;
                    txtNombres.Focus();
                    modUsuarios.Show();
                    return false;
                }
                else if (txtUsuario.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Usuario es obligatorio.";
                    labError.Visible = true;
                    txtUsuario.Focus();
                    modUsuarios.Show();
                    return false;
                }
                else if (ValidarUsuario())
                {
                    return false;
                }
                else if (ValidarContrasena())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar los datos: " + ex.Message;
                labError.Visible = true;
                modUsuarios.Show();
                return false;
            }
        }
        private bool ValidarUsuario()
        {
            try
            {
                bool Crear = labCrear.Text == "1" ? true : false;

                if (Crear) //Crear
                {
                    DataTable dtUsuario = objUsuarios.ListarExisteUsuario(txtUsuario.Text.Trim());

                    if (dtUsuario.Rows.Count > 0)
                    {
                        labMensaje.Text = "El Usuario ingresado ya existe en base de datos.";
                        labError.Visible = true;
                        txtUsuario.Focus();
                        modUsuarios.Show();
                        return true;
                    }
                    return false;
                }
                else //Actualizar
                {
                    string UsuarioIngresado = txtUsuario.Text.Trim();
                    string UsuarioSeleccionado = labUsuario.Text.Trim();

                    if (UsuarioIngresado != UsuarioSeleccionado) //Hubo cambio de perfil
                    {
                        DataTable dtUsuario = objPerfiles.ListarPerfilPorNombre(txtNombres.Text.Trim());

                        if (dtUsuario.Rows.Count > 0)
                        {
                            labMensaje.Text = "El Usuario ingresado ya existe en base de datos.";
                            labError.Visible = true;
                            txtUsuario.Focus();
                            modUsuarios.Show();
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar el usuario: " + ex.Message;
                labError.Visible = true;
                modUsuarios.Show();
                return false;
            }
        }
        private bool ValidarContrasena()
        {
            try
            {
                bool Crear = labCrear.Text == "1" ? true : false;

                if (Crear) //Crear
                {
                    if (txtContrasena.Text.Trim() == "")
                    {
                        labMensaje.Text = "El campo Contraseña es obligatorio.";
                        labError.Visible = true;
                        txtContrasena.Focus();
                        modUsuarios.Show();
                        return true;
                    }
                    else if (txtConfirmacionContrasena.Text.Trim() == "")
                    {
                        labMensaje.Text = "Debe confirmar la Contraseña.";
                        txtConfirmacionContrasena.Focus();
                        labError.Visible = true;
                        modUsuarios.Show();
                        return true;
                    }
                    else if (txtContrasena.Text.Trim() != txtConfirmacionContrasena.Text.Trim())
                    {
                        labMensaje.Text = "Las contraseñas no coinciden.";
                        txtConfirmacionContrasena.Focus();
                        labError.Visible = true;
                        modUsuarios.Show();
                        return true;
                    }
                    return false;
                }
                else //Actualizar
                {
                    if (txtContrasena.Text.Trim() != "" || txtConfirmacionContrasena.Text.Trim() != "") //Modificar contraseña
                    {
                        if (txtContrasena.Text.Trim() != txtConfirmacionContrasena.Text.Trim())
                        {
                            labMensaje.Text = "Las contraseñas no coinciden.";
                            txtConfirmacionContrasena.Focus();
                            labError.Visible = true;
                            modUsuarios.Show();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar los campos de la contraseña: " + ex.Message;
                labError.Visible = true;
                modUsuarios.Show();
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string strMensaje = labCrear.Text == "1" ? "creado" : "actualizado";

            try
            {
                if (ValidarDatos())
                {
                    oUsuario = new ObjUsuarios();
                    oUsuario.Codigo = Convert.ToInt32(txtCodigo.Text.Trim());
                    oUsuario.Nombres = txtNombres.Text.Trim();
                    oUsuario.Usuario = txtUsuario.Text.Trim();
                    oUsuario.IdPerfil = Convert.ToInt32(cboPerfiles.SelectedValue);
                    oUsuario.Perfil = cboPerfiles.SelectedItem.Text;
                    oUsuario.PuedeEliminar = cboPuedeEliminar.SelectedValue == "1" ? true : false;
                    oUsuario.Estado = cboEstado.SelectedValue == "1" ? true : false;
                    oUsuario.UsuarioCreacion = Session["Usuario"].ToString();
                    oUsuario.EquipoCreacion = System.Environment.MachineName;
                    oUsuario.UsuarioModificacion = Session["Usuario"].ToString();
                    oUsuario.EquipoModificacion = System.Environment.MachineName;

                    if (txtContrasena.Text.Trim() != "" || txtConfirmacionContrasena.Text.Trim() != "") //Contrasena ingresada
                    {
                        oUsuario.Contrasena = objCriptografia.EncriptarContrasena(txtContrasena.Text.Trim());
                    }

                    if (objUsuarios.AlmacenarUsuario(oUsuario))
                    {
                        string Titulo = "Información";
                        string Mensaje = "Usuario " + strMensaje + " correctamente.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmUsuarios.aspx'});";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                    else
                    {
                        string Titulo = "Información";
                        string Mensaje = "El usuario no pudo ser " + strMensaje + ".";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmUsuarios.aspx'});";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de " + strMensaje + " el usuario: " + ex.Message;
                labError.Visible = true;
                labUsuario.Text = "";
                txtCodigo.Text = "";
                modUsuarios.Show();
            }
        }
    }
}