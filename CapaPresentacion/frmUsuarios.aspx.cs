using System;
using System.Data;
using CapaNegocios;
using System.Web.UI;
using System.Transactions;

namespace CapaPresentacion
{
    public partial class frmUsuarios : System.Web.UI.Page
    {
        #region Variables

        NegUsuarios objUsuarios = new NegUsuarios();
        NegPerfiles objPerfiles = new NegPerfiles();

        
        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            labError.Visible = false;

            if (!IsPostBack)
            {
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
                cboPuedeEliminar.SelectedValue = "1";
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
            ListarComboPerfiles();
            ListarComboPermisos();
            ListarComboEstados();
            txtNombres.Focus();
            modUsuarios.Show();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        //Guardar
        private bool ValidarCampos()
        {
            try
            {
                if (txtCodigo.Text.Trim() == "")
                {
                    txtCodigo.Focus();
                    labError.Visible = true;
                    labMensaje.Text = "El campo Código es obligatorio.";
                    modUsuarios.Show();
                    return false;
                }
                else if (txtNombres.Text.Trim() == "")
                {
                    txtNombres.Focus();
                    labError.Visible = true;
                    labMensaje.Text = "El campo Nombres es obligatorio.";
                    modUsuarios.Show();
                    return false;
                }
                else if (txtUsuario.Text.Trim() == "")
                {
                    txtUsuario.Focus();
                    labError.Visible = true;
                    labMensaje.Text = "El campo Usuario es obligatorio.";
                    modUsuarios.Show();
                    return false;
                }
                else if (ValidarUsuario())
                {
                    txtUsuario.Focus();
                    labError.Visible = true;
                    labMensaje.Text = "El Usuario ingresado ya existe en base de datos.";
                    modUsuarios.Show();
                    return false;
                }
                else if (txtContrasena.Text.Trim() == "")
                {
                    txtContrasena.Focus();
                    labError.Visible = true;
                    labMensaje.Text = "El campo Contraseña es obligatorio.";
                    modUsuarios.Show();
                    return false;
                }
                else if (txtConfirmacionContrasena.Text.Trim() == "")
                {
                    labError.Visible = true;
                    txtConfirmacionContrasena.Focus();
                    labMensaje.Text = "Debe confirmar la Contraseña.";
                    modUsuarios.Show();
                    return false;
                }
                else if (Convert.ToInt16(cboPerfiles.SelectedValue) <= 0)
                {
                    txtContrasena.Focus();
                    labError.Visible = true;
                    labMensaje.Text = "Debe seleccionar un Perfil.";
                    modUsuarios.Show();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                labError.Visible = true;
                labMensaje.Text = "Error tratando de validar los campos: " + ex.Message;
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
                    DataTable dtUsuario = objPerfiles.ListarPerfilPorNombre(txtUsuario.Text.Trim());

                    if (dtUsuario.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else //No hubo cambio de perfil
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                labError.Visible = true;
                labMensaje.Text = "Error tratando de validar el usuario: " + ex.Message;
                modUsuarios.Show();
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    //oPerfil = new ObjPerfiles();
                    //oPerfil.IdPerfil = Convert.ToInt32(txtIdPerfil.Text);
                    //oPerfil.Nombre = txtNombre.Text.Trim();
                    //oPerfil.Estado = cboEstado.SelectedValue == "1" ? true : false;
                    //oPerfil.UsuarioCreacion = Session["Usuario"].ToString();
                    //oPerfil.EquipoCreacion = System.Environment.MachineName;
                    //oPerfil.UsuarioModificacion = Session["Usuario"].ToString();
                    //oPerfil.EquipoModificacion = System.Environment.MachineName;

                    //using (TransactionScope tsTransaction = new TransactionScope())
                    //{
                    //    if (objPerfiles.Guardar(oPerfil)) //Guardar perfil
                    //    {
                    //        if (objPerfiles.EliminarModulosPerfil(oPerfil)) //Eliminar modulos perfil
                    //        {
                    //            foreach (GridViewRow gvRow in gvModulos.Rows)
                    //            {
                    //                if (gvRow.RowType == DataControlRowType.DataRow)
                    //                {
                    //                    int IdModulo = Convert.ToInt32(gvRow.Cells[0].Text);
                    //                    CheckBox chkAplica = (gvRow.Cells[3].FindControl("chkAgregar") as CheckBox);

                    //                    oModulo = new ObjModulos();
                    //                    oModulo.IdModulo = IdModulo;
                    //                    oModulo.TienePermiso = false;
                    //                    oModulo.IdPerfil = oPerfil.IdPerfil;

                    //                    if (chkAplica.Checked)
                    //                    {
                    //                        oModulo.TienePermiso = true;
                    //                    }
                    //                }
                    //                objPerfiles.GuardarModulosPerfil(oModulo);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            tsTransaction.Dispose();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        tsTransaction.Dispose();
                    //    }
                    //    tsTransaction.Complete();
                    //}

                    //HiddenField.Value = string.Empty;
                    //string strMensaje = labCrear.Text == "1" ? "creado" : "actualizado";

                    //string Titulo = "Información";
                    //string Mensaje = "Perfil " + strMensaje + " correctamente.";
                    //string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmUsuarios.aspx'});";
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Mensaje = labCrear.Text == "1" ? "crear" : "modificar";

                labUsuario.Text = "";
                txtCodigo.Text = "";

                labError.Visible = true;
                labMensaje.Text = "Error tratando de " + Mensaje + " el usuario: " + ex.Message;
                modUsuarios.Show();
            }
        }
    }
}