using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace CapaPresentacion
{
    public partial class frmClientes : System.Web.UI.Page
    {
        #region Variables

        private ObjClientes oCliente = new ObjClientes();
        private NegClientes objClientes = new NegClientes();
        private NegUsuarios objUsuarios = new NegUsuarios();
        private NegRegionales objRegionales = new NegRegionales();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            labError.Visible = false;
            txtFiltro.Focus();

            if (!IsPostBack)
            {
                ListarComboDepartamentos();
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
                    string Mensaje = "No existen Clientes creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Clientes";
                string Mensaje = "Error tratando de listar los Clientes: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
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
                string Titulo = "Error Cargando Estados";
                string Mensaje = "Error tratando de listar el ComboBox de los Estados: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
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
                    string Mensaje = "No existen Tipos de Documentos creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Tipo Documentos";
                string Mensaje = "Error tratando de listar el ComboBox de los Tipos de Documentos: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }

        //Controles
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            labMensajeLotes.Visible = true;
            labMensajeLotes.Text = "";

            DataTable dtDatos = new DataTable();
            gvLotes.DataSource = dtDatos;
            gvLotes.DataBind();

            cboTipoDocumento.Enabled = true;
            txtDocumento.Enabled = true;
            LimpiarCombo("Municipios");
            Session["dtLotes"] = null;
            tabLotes.Visible = false;
            labDocumento.Text = "";
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
                    DataSet dsCliente = objClientes.ListarCliente(Documento);

                    if (dsCliente.Tables[0].Rows.Count > 0) //Cliente existe
                    {
                        LimpiarCombo("Municipios");
                        DataTable dtCliente = dsCliente.Tables[0];

                        cboTipoDocumento.SelectedValue = dtCliente.Rows[0]["TipoDocumento"].ToString();
                        txtDocumento.Text = dtCliente.Rows[0]["Documento"].ToString();
                        labDocumento.Text = dtCliente.Rows[0]["Documento"].ToString();
                        txtNombres.Text = dtCliente.Rows[0]["Nombres"].ToString();
                        txtApellidos.Text = dtCliente.Rows[0]["Apellidos"].ToString();

                        //Domicilio
                        int IdDepartamento = Convert.ToInt32(dtCliente.Rows[0]["IdDepartamento"]);
                        int IdMunicipio = Convert.ToInt32(dtCliente.Rows[0]["IdMunicipio"]);

                        DataTable dtDepartamentos = objRegionales.ListarComboDepartamentos(); //Departamentos

                        if (dtDepartamentos.Rows.Count > 0)
                        {
                            cboDepartamentos.DataSource = dtDepartamentos;
                            cboDepartamentos.DataValueField = "IdDepartamento";
                            cboDepartamentos.DataTextField = "Nombre";
                            cboDepartamentos.DataBind();
                            cboDepartamentos.SelectedValue = IdDepartamento.ToString();
                        }

                        DataTable dtMunicipios = objRegionales.ListarComboMunicipiosDepartamento(IdDepartamento); //Municipios

                        if (dtMunicipios.Rows.Count > 0)
                        {
                            cboMunicipios.DataSource = dtMunicipios;
                            cboMunicipios.DataValueField = "IdMunicipio";
                            cboMunicipios.DataTextField = "Nombre";
                            cboMunicipios.DataBind();
                            cboMunicipios.SelectedValue = IdMunicipio.ToString();
                        }

                        txtWhatsApp.Text = dtCliente.Rows[0]["WhatsApp"].ToString();
                        txtTelefono.Text = dtCliente.Rows[0]["Telefono"].ToString();
                        txtEmail.Text = dtCliente.Rows[0]["Email"].ToString();
                        cboEstado.SelectedValue = Convert.ToBoolean(dtCliente.Rows[0]["Estado"]) ? "1" : "0";
                        cboTipoDocumento.Enabled = false;
                        txtDocumento.Enabled = false;

                        DataTable dtLotes = dsCliente.Tables[1]; //Lotes
                        tabLotes.Visible = true;

                        if (dtLotes.Rows.Count > 0)
                        {
                            gvLotes.DataSource = dtLotes;
                            gvLotes.DataBind();

                            labMensajeLotes.Visible = false;
                            labMensajeLotes.Text = "";
                        }
                        else
                        {
                            labMensajeLotes.Text = "El Cliente no tiene lotes asociados.";
                            labMensajeLotes.Visible = true;

                            DataTable dtDatos = new DataTable();
                            gvLotes.DataSource = dtDatos;
                            gvLotes.DataBind();
                        }

                        txtNombres.Focus();
                        modClientes.Show();
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "El Cliente seleccionado no existe en base de datos.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "El Cliente seleccionado no tiene documento asignado.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Modificando Cliente";
                string Mensaje = "Error tratando de modificar el Cliente: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
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
                    DataTable dtClientes = objClientes.ListarClientesParametros(Filtro);

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

        //Domicilio
        private void LimpiarCombo(string Combo)
        {
            DataTable dtDatos = new DataTable();

            switch (Combo)
            {
                case "Departamentos":
                    cboDepartamentos.DataSource = dtDatos;
                    cboDepartamentos.DataBind();
                    break;
                case "Municipios":
                    cboMunicipios.DataSource = dtDatos;
                    cboMunicipios.DataBind();
                    break;
            }
        }
        private void ListarComboDepartamentos()
        {
            try
            {
                DataTable dtDepartamentos = objRegionales.ListarComboDepartamentos();

                if (dtDepartamentos.Rows.Count > 1)
                {
                    cboDepartamentos.DataSource = dtDepartamentos;
                    cboDepartamentos.DataValueField = "IdDepartamento";
                    cboDepartamentos.DataTextField = "Nombre";
                    cboDepartamentos.DataBind();
                    LimpiarCombo("Municipios");
                }
                else
                {
                    labMensaje.Text = "No existen Departamentos creados en base de datos para el país seleccionado.";
                    LimpiarCombo("Municipios");
                    labError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar los Departamentos: " + ex.Message;
                LimpiarCombo("Municipios");
                labError.Visible = true;
            }
        }
        protected void cboDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdDepartamento = Convert.ToInt32(cboDepartamentos.SelectedValue);

                if (IdDepartamento > 0)
                {
                    DataTable dtMunicipios = objRegionales.ListarComboMunicipiosDepartamento(IdDepartamento);

                    if (dtMunicipios.Rows.Count > 0)
                    {
                        cboMunicipios.DataSource = dtMunicipios;
                        cboMunicipios.DataValueField = "IdMunicipio";
                        cboMunicipios.DataTextField = "Nombre";
                        cboMunicipios.DataBind();
                        modClientes.Show();
                    }
                    else
                    {
                        labMensaje.Text = "No existen Municipios creados en base de datos para el departamento seleccionado.";
                        LimpiarCombo("Municipios");
                        cboDepartamentos.Focus();
                        labError.Visible = true;
                        modClientes.Show();
                    }
                }
                else
                {
                    labMensaje.Text = "Debe seleccionar un Departamento.";
                    LimpiarCombo("Municipios");
                    cboDepartamentos.Focus();
                    labError.Visible = true;
                    modClientes.Show();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar los Municipios: " + ex.Message;
                LimpiarCombo("Municipios");
                cboDepartamentos.Focus();
                labError.Visible = true;
                modClientes.Show();
            }
        }

        //Guardar
        private bool ValidarEmail()
        {
            try
            {
                Regex objRegex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
                var objMatch = objRegex.Match(txtEmail.Text.Trim());
                bool Resultado = objMatch.Success;

                if (Resultado)
                {
                    try
                    {
                        var objMailAddress = new MailAddress(txtEmail.Text.Trim());
                        string Host = objMailAddress.Host;
                        string[] Dominio = Host.Split('.');
                        int Cantidad = Dominio.Length;

                        if (Cantidad > 1)
                        {
                            string Smtp = "";
                            string Tipo = "";
                            string Proveedor = "";

                            if (Cantidad == 2)
                            {
                                Smtp = Dominio[1].ToString();
                                Proveedor = Dominio[0].ToString();

                                if (Smtp == "com" || Smtp == "edu" || Smtp == "org")
                                {
                                    Resultado = true;
                                }
                                else
                                {
                                    Resultado = false;
                                }
                            }
                            else if (Cantidad == 3)
                            {
                                Smtp = Dominio[1].ToString();
                                Tipo = Dominio[2].ToString();
                                Proveedor = Dominio[0].ToString();

                                if (Smtp == "com" || Smtp == "edu" || Smtp == "org")
                                {
                                    if (Tipo == "co")
                                    {
                                        Resultado = true;
                                    }
                                    else
                                    {
                                        Resultado = false;
                                    }
                                }
                                else
                                {
                                    Resultado = false;
                                }
                            }
                            else
                            {
                                Resultado = false;
                            }
                        }
                        else
                        {
                            Resultado = false;
                        }
                    }
                    catch (FormatException)
                    {
                        Resultado = false;
                    }
                }

                if (Resultado)
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
                else if (cboDepartamentos.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar un Departamento.";
                    cboDepartamentos.Focus();
                    labError.Visible = true;
                    modClientes.Show();
                    return false;
                }
                else if (cboMunicipios.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar un Municipio.";
                    labError.Visible = true;
                    cboMunicipios.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtWhatsApp.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo WhatsApp es obligatorio.";
                    labError.Visible = true;
                    txtWhatsApp.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (Convert.ToInt64(txtWhatsApp.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo WhatsApp debe ser mayor que cero.";
                    labError.Visible = true;
                    txtWhatsApp.Text = "";
                    txtWhatsApp.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtWhatsApp.Text.Trim().Length < 10)
                {
                    labMensaje.Text = "El campo WhatsApp no tiene el formato correcto.";
                    labError.Visible = true;
                    txtWhatsApp.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtTelefono.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Teléfono 1 es obligatorio.";
                    labError.Visible = true;
                    txtTelefono.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (Convert.ToInt64(txtTelefono.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Teléfono 1 debe ser mayor que cero.";
                    labError.Visible = true;
                    txtTelefono.Text = "";
                    txtTelefono.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtTelefono.Text.Trim().Length < 7)
                {
                    labMensaje.Text = "El campo Teléfono 1 no tiene el formato correcto.";
                    labError.Visible = true;
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
                    oCliente.IdDepartamento = Convert.ToInt32(cboDepartamentos.SelectedValue);
                    oCliente.Departamento = cboDepartamentos.SelectedItem.Text;
                    oCliente.IdMunicipio = Convert.ToInt32(cboMunicipios.SelectedValue);
                    oCliente.Municipio = cboMunicipios.SelectedItem.Text;
                    oCliente.WhatsApp = Convert.ToInt64(txtWhatsApp.Text.Trim());
                    oCliente.Telefono = Convert.ToInt64(txtTelefono.Text.Trim());
                    oCliente.Email = txtEmail.Text.Trim();
                    oCliente.Estado = cboEstado.SelectedValue == "1" ? true : false;
                    oCliente.UsuarioCreacion = Session["Usuario"].ToString();
                    oCliente.UsuarioModificacion = Session["Usuario"].ToString();

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
                        labMensaje.Text = "El Cliente no pudo ser " + strMensaje + ". Debe contactar al administrador del sistema.";
                        labError.Visible = true;
                        modClientes.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                string Mensaje = labCrear.Text == "1" ? "crear" : "actualizar";

                labMensaje.Text = "Error tratando de " + Mensaje + " el Cliente: " + ex.Message;
                labError.Visible = true;
                modClientes.Show();
            }
        }

        //Eliminar
        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btnEliminar = (ImageButton)sender;
                long Documento = Convert.ToInt64(btnEliminar.CommandArgument.ToString().Trim());

                if (Documento > 0)
                {
                    oCliente = new ObjClientes();
                    oCliente.Documento = Documento;

                    if (objClientes.Eliminar(oCliente)) //Eliminar
                    {
                        DataSet dsCliente = objClientes.ListarCliente(Documento);

                        if (dsCliente.Tables[0].Rows.Count == 0) //Validar si el cliente fue eliminado
                        {
                            string Titulo = "Información";
                            string Mensaje = "Cliente eliminado correctamente.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmClientes.aspx'});";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        }
                        else
                        {
                            string Titulo = "Información";
                            string Mensaje = "El Cliente no pudo ser eliminado. Debe contactar al administrador del sistema.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        }
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "El Cliente no pudo ser eliminado. Debe contactar al administrador del sistema.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "El Cliente no pudo ser eliminado. Debe contactar al administrador del sistema.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Eliminando Cliente";
                string Mensaje = "Error tratando de eliminar el Cliente: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
    }
}