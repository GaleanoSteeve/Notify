using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class frmClientes : System.Web.UI.Page
    {
        #region Variables

        ObjClientes oCliente = new ObjClientes();

        NegClientes objClientes = new NegClientes();
        NegUsuarios objUsuarios = new NegUsuarios();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            labError.Visible = false;
            txtFiltro.Focus();

            if (!IsPostBack)
            {
                ListarTipoDocumentos();
                ListarComboEstados();
                ListarClientes();
            }
        }

        #endregion

        //Metodos
        private void ListarClientes()
        {
            try
            {
                DataTable dtClientes = objClientes.ListarClientes();

                if (dtClientes.Rows.Count > 0)
                {
                    gvClientes.DataSource = dtClientes;
                    gvClientes.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen clientes creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Clientes";
                string Mensaje = "Error tratando de listar los clientes: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
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
                string Titulo = "Error Cargando ComboBox";
                string Mensaje = "Error tratando de listar el ComboBox de los estados: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        private void ListarTipoDocumentos()
        {
            try
            {
                DataTable dtTipoDocumentos = objClientes.ListarComboTipoDocumentos();

                if (dtTipoDocumentos.Rows.Count > 0)
                {
                    cboTipoDocumento.DataSource = dtTipoDocumentos;
                    cboTipoDocumento.DataValueField = "IdTipoDocumento";
                    cboTipoDocumento.DataTextField = "TipoDocumento";
                    cboTipoDocumento.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen tipos de documentos creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando ComboBox";
                string Mensaje = "Error tratando de listar el ComboBox de los tipos de documentos: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }

        //Administrar
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            cboTipoDocumento.Enabled = true;
            txtDocumento.Enabled = true;
            cboTipoDocumento.Focus();
            labDocumento.Text = "2020";
            labCrear.Text = "1";
            modClientes.Show();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                labCrear.Text = "0";
                LinkButton btnEditar = (LinkButton)sender;
                long Documento = Convert.ToInt64(btnEditar.Text);

                if (Documento > 0)
                {
                    DataTable dtCliente = objClientes.ListarCliente(Documento);

                    if (dtCliente.Rows.Count > 0) //Cliente existe
                    {
                        cboTipoDocumento.SelectedValue = dtCliente.Rows[0]["TipoDocumento"].ToString();
                        txtDocumento.Text = dtCliente.Rows[0]["Documento"].ToString();
                        labDocumento.Text = dtCliente.Rows[0]["Documento"].ToString();
                        txtNombres.Text = dtCliente.Rows[0]["Nombres"].ToString();
                        txtApellidos.Text = dtCliente.Rows[0]["Apellidos"].ToString();
                        txtDireccion.Text = dtCliente.Rows[0]["Direccion"].ToString();
                        txtTelefono.Text = dtCliente.Rows[0]["Telefono"].ToString();
                        txtEmail.Text = dtCliente.Rows[0]["Email"].ToString();
                        cboEstado.SelectedValue = Convert.ToBoolean(dtCliente.Rows[0]["Estado"]) ? "1" : "0";
                        cboTipoDocumento.Enabled = false;
                        txtDocumento.Enabled = false;
                        txtNombres.Focus();
                        modClientes.Show();
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "El cliente seleccionado no existe en base de datos.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "El cliente seleccionado no tiene documento asignado.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Modificando Cliente";
                string Mensaje = "Error tratando de modificar el cliente: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Filtro = txtFiltro.Text.Trim();

                if (Filtro != "")
                {
                    DataTable dtClientes = objClientes.ListarClienteParametros(Filtro);

                    if (dtClientes.Rows.Count > 0)
                    {
                        gvClientes.DataSource = dtClientes;
                        gvClientes.DataBind();
                        txtFiltro.Focus();
                    }
                    else
                    {
                        gvClientes.DataSource = null;
                        gvClientes.DataBind();
                        txtFiltro.Focus();

                        string Titulo = "Advertencia";
                        string Mensaje = "No existen clientes creados en base de datos con los valores ingresados.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    ListarClientes();
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Ejecutando Consulta";
                string Mensaje = "Error tratando de filtrar los datos: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }

        //Guardar
        private bool ValidarEmail()
        {
            try
            {
                string Email = txtEmail.Text.Trim();
                string Patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (System.Text.RegularExpressions.Regex.IsMatch(Email, Patron))
                {
                    return true;
                }
                else
                {
                    labMensaje.Text = "El campo Email no tiene el formato correcto.";
                    labError.Visible = true;
                    txtEmail.Focus();
                    modClientes.Show();
                    return false;
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar el Email: " + ex.Message;
                labError.Visible = true;
                modClientes.Show();
                return false;
            }
        }
        private bool ValidarCampos()
        {
            try
            {
                if (cboTipoDocumento.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar un Tipo de Documento.";
                    cboTipoDocumento.Focus();
                    labError.Visible = true;
                    modClientes.Show();
                    return false;
                }
                else if (txtDocumento.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Documento es obligatorio.";
                    labError.Visible = true;
                    txtDocumento.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (Convert.ToInt64(txtDocumento.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Documento debe ser mayor que cero.";
                    labError.Visible = true;
                    txtDocumento.Text = "";
                    txtDocumento.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtDocumento.Text.Trim().Length < 7)
                {
                    labMensaje.Text = "El campo Documento no tiene el formato correcto.";
                    labError.Visible = true;
                    txtDocumento.Text = "";
                    txtDocumento.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (ExisteDocumento())
                {
                    return false;
                }
                else if (txtNombres.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Nombres es obligatorio.";
                    labError.Visible = true;
                    txtNombres.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtApellidos.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Apellidos es obligatorio.";
                    labError.Visible = true;
                    txtApellidos.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtDireccion.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Dirección es obligatorio.";
                    labError.Visible = true;
                    txtDireccion.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtTelefono.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Teléfono es obligatorio.";
                    labError.Visible = true;
                    txtTelefono.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (Convert.ToInt64(txtTelefono.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Teléfono debe ser mayor que cero.";
                    labError.Visible = true;
                    txtTelefono.Text = "";
                    txtTelefono.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtTelefono.Text.Trim().Length < 7)
                {
                    labMensaje.Text = "El campo Teléfono no tiene el formato correcto.";
                    labError.Visible = true;
                    txtTelefono.Text = "";
                    txtTelefono.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtEmail.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Email es obligatorio.";
                    labError.Visible = true;
                    txtEmail.Text = "";
                    txtEmail.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (!ValidarEmail())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar los campos: " + ex.Message;
                labError.Visible = true;
                modClientes.Show();
                return false;
            }
        }
        private bool ExisteDocumento()
        {
            try
            {
                bool Crear = labCrear.Text == "1" ? true : false;

                if (Crear) //Crear
                {
                    oCliente = new ObjClientes();
                    oCliente.Documento = Convert.ToInt64(txtDocumento.Text.Trim());

                    DataTable dtDatos = objClientes.ExisteDocumento(oCliente);

                    if (dtDatos.Rows.Count > 0)
                    {
                        labMensaje.Text = "El Documento ingresado ya existe en base de datos.";
                        labError.Visible = true;
                        txtDocumento.Focus();
                        modClientes.Show();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar el Documento: " + ex.Message;
                labError.Visible = true;
                modClientes.Show();
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                if (ValidarCampos())
                {
                    oCliente = new ObjClientes();
                    oCliente.TipoDocumento = Convert.ToInt32(cboTipoDocumento.SelectedValue);
                    oCliente.Documento = Convert.ToInt64(txtDocumento.Text.Trim());
                    oCliente.Nombres = txtNombres.Text.Trim();
                    oCliente.Apellidos = txtApellidos.Text.Trim();
                    oCliente.Direccion = txtDireccion.Text.Trim();
                    oCliente.Telefono = Convert.ToInt64(txtTelefono.Text.Trim());
                    oCliente.Email = txtEmail.Text.Trim();
                    oCliente.Estado = cboEstado.SelectedValue == "1" ? true : false;
                    oCliente.UsuarioCreacion = Session["Usuario"].ToString();
                    oCliente.EquipoCreacion = System.Environment.MachineName;
                    oCliente.UsuarioModificacion = Session["Usuario"].ToString();
                    oCliente.EquipoModificacion = System.Environment.MachineName;

                    string strMensaje = labCrear.Text == "1" ? "creado" : "actualizado";

                    if (objClientes.Almacenar(oCliente))
                    {
                        string Titulo = "Información";
                        string Mensaje = "Cliente " + strMensaje + " correctamente.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmClientes.aspx'});";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                    else
                    {
                        labMensaje.Text = "El cliente no pudo ser " + strMensaje + ". Debe contactar al administrador del sistema.";
                        labError.Visible = true;
                        txtEmail.Focus();
                        modClientes.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                string Mensaje = labCrear.Text == "1" ? "crear" : "actualizar";

                labMensaje.Text = "Error tratando de " + Mensaje + " el cliente: " + ex.Message;
                labError.Visible = true;
                modClientes.Show();
            }
        }

        //Eliminar
        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ImageButton btnEliminar = (ImageButton)sender;
                long Documento = Convert.ToInt32(btnEliminar.CommandArgument.ToString().Trim());

                if (Documento > 0)
                {
                    oCliente = new ObjClientes();
                    oCliente.Documento = Documento;

                    if (objClientes.Eliminar(oCliente)) //Eliminar
                    {
                        DataTable dtCliente = objClientes.ListarCliente(Documento);

                        if (dtCliente.Rows.Count == 0) //Validar si el cliente fue eliminado
                        {
                            string Titulo = "Información";
                            string Mensaje = "Cliente eliminado correctamente.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmClientes.aspx'});";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        }
                        else
                        {
                            string Titulo = "Información";
                            string Mensaje = "El cliente no pudo ser eliminado. Debe contactar al administrador del sistema.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        }
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "El cliente no pudo ser eliminado. Debe contactar al administrador del sistema.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "El cliente no pudo ser eliminado. Debe contactar al administrador del sistema.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Eliminando Cliente";
                string Mensaje = "Error tratando de eliminar el cliente: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
    }
}