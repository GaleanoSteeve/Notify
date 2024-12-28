﻿using System;
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
        NegRegionales objInformacionRegional = new NegRegionales();

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
                    string Mensaje = "No existen tipos de documentos creados en base de datos.";
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

        //Administrar
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            cboTipoDocumento.Enabled = true;
            txtDocumento.Enabled = true;
            cboTipoDocumento.Focus();
            labDocumento.Text = "";
            labDomicilio.Text = "";
            labCrear.Text = "1";
            ListarComboPaises();
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
                        LimpiarCombo("Corregimientos");
                        LimpiarCombo("Departamentos");
                        LimpiarCombo("Municipios");
                        LimpiarCombo("Veredas");
                        LimpiarCombo("Paises");

                        cboTipoDocumento.SelectedValue = dtCliente.Rows[0]["TipoDocumento"].ToString();
                        txtDocumento.Text = dtCliente.Rows[0]["Documento"].ToString();
                        labDocumento.Text = dtCliente.Rows[0]["Documento"].ToString();
                        txtNombres.Text = dtCliente.Rows[0]["Nombres"].ToString();
                        txtApellidos.Text = dtCliente.Rows[0]["Apellidos"].ToString();

                        //Domicilio
                        int IdPais = Convert.ToInt32(dtCliente.Rows[0]["IdPais"].ToString());
                        int IdDepartamento = Convert.ToInt32(dtCliente.Rows[0]["IdDepartamento"].ToString());
                        int IdMunicipio = Convert.ToInt32(dtCliente.Rows[0]["IdMunicipio"].ToString());
                        int IdCorregimiento = Convert.ToInt32(dtCliente.Rows[0]["IdCorregimiento"].ToString());
                        int IdVereda = Convert.ToInt32(dtCliente.Rows[0]["IdVereda"].ToString());

                        DataTable dtPaises = objInformacionRegional.ListarComboPaises(); //Paises

                        if (dtPaises.Rows.Count > 0)
                        {
                            cboPaises.DataSource = dtPaises;
                            cboPaises.DataValueField = "IdPais";
                            cboPaises.DataTextField = "Nombre";
                            cboPaises.DataBind();
                            cboPaises.SelectedValue = IdPais.ToString();
                        }

                        DataTable dtDepartamentos = objInformacionRegional.ListarComboDepartamentosPais(IdPais); //Departamentos

                        if (dtDepartamentos.Rows.Count > 0)
                        {
                            cboDepartamentos.DataSource = dtDepartamentos;
                            cboDepartamentos.DataValueField = "IdDepartamento";
                            cboDepartamentos.DataTextField = "Nombre";
                            cboDepartamentos.DataBind();
                            cboDepartamentos.SelectedValue = IdDepartamento.ToString();
                        }

                        DataTable dtMunicipios = objInformacionRegional.ListarComboMunicipiosDepartamento(IdDepartamento); //Municipios

                        if (dtMunicipios.Rows.Count > 0)
                        {
                            cboMunicipios.DataSource = dtMunicipios;
                            cboMunicipios.DataValueField = "IdMunicipio";
                            cboMunicipios.DataTextField = "Nombre";
                            cboMunicipios.DataBind();
                            cboMunicipios.SelectedValue = IdMunicipio.ToString();
                        }

                        DataTable dtCorregimientos = objInformacionRegional.ListarComboCorregimientosMunicipio(IdMunicipio); //Corregimientos

                        if (dtCorregimientos.Rows.Count > 0)
                        {
                            cboCorregimientos.DataSource = dtCorregimientos;
                            cboCorregimientos.DataValueField = "IdCorregimiento";
                            cboCorregimientos.DataTextField = "Nombre";
                            cboCorregimientos.DataBind();
                            cboCorregimientos.SelectedValue = IdCorregimiento.ToString();
                        }

                        DataTable dtVeredas = objInformacionRegional.ListarComboVeredasCorregimientos(IdCorregimiento); //Veredas

                        if (dtVeredas.Rows.Count > 0)
                        {
                            cboVeredas.DataSource = dtVeredas;
                            cboVeredas.DataValueField = "IdVereda";
                            cboVeredas.DataTextField = "Nombre";
                            cboVeredas.DataBind();
                            cboVeredas.SelectedValue = IdVereda.ToString();
                        }

                        txtBarrio.Text = dtCliente.Rows[0]["Barrio"].ToString();
                        txtDireccion.Text = dtCliente.Rows[0]["Direccion"].ToString();
                        txtWhatsApp.Text = dtCliente.Rows[0]["WhatsApp"].ToString();
                        txtTelefono1.Text = dtCliente.Rows[0]["Telefono1"].ToString();
                        txtTelefono2.Text = Convert.ToInt64(dtCliente.Rows[0]["Telefono2"]) > 0 ? dtCliente.Rows[0]["Telefono2"].ToString() : "";
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

        //Domicilio
        private void ListarComboPaises()
        {
            try
            {
                DataTable dtPaises = objInformacionRegional.ListarComboPaises();

                if (dtPaises.Rows.Count > 0)
                {
                    cboPaises.DataSource = dtPaises;
                    cboPaises.DataValueField = "IdPais";
                    cboPaises.DataTextField = "Nombre";
                    cboPaises.DataBind();

                    ListarComboDepartamentosPais(170);
                    cboPaises.SelectedValue = "170";
                }
                else
                {
                    labMensaje.Text = "No existen Países creados en base de datos.";
                    LimpiarCombo("Departamentos");
                    labError.Visible = true;
                    modClientes.Show();
                    cboPaises.Focus();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar los Países: " + ex.Message;
                LimpiarCombo("Departamentos");
                labError.Visible = true;
                modClientes.Show();
            }
        }
        private void LimpiarCombo(string Combo)
        {
            DataTable dtDatos = new DataTable();

            switch(Combo)
            {
                case "Paises":
                    cboPaises.DataSource = dtDatos;
                    cboPaises.DataBind();
                    break;
                case "Departamentos":
                    cboDepartamentos.DataSource = dtDatos;
                    cboDepartamentos.DataBind();
                    break;
                case "Municipios":
                    cboMunicipios.DataSource = dtDatos;
                    cboMunicipios.DataBind();
                    break;
                case "Corregimientos":
                    cboCorregimientos.DataSource = dtDatos;
                    cboCorregimientos.DataBind();
                    break;
                case "Veredas":
                    cboVeredas.DataSource = dtDatos;
                    cboVeredas.DataBind();
                    break;
            }
        }
        private void ListarComboDepartamentosPais(int IdPais)
        {
            try
            {
                DataTable dtDepartamentos = objInformacionRegional.ListarComboDepartamentosPais(IdPais);

                if (dtDepartamentos.Rows.Count > 1)
                {
                    cboDepartamentos.DataSource = dtDepartamentos;
                    cboDepartamentos.DataValueField = "IdDepartamento";
                    cboDepartamentos.DataTextField = "Nombre";
                    cboDepartamentos.DataBind();

                    LimpiarCombo("Corregimientos");
                    LimpiarCombo("Municipios");
                    LimpiarCombo("Veredas");
                    modClientes.Show();
                }
                else
                {
                    labMensaje.Text = "No existen Departamentos creados en base de datos para el país seleccionado.";
                    LimpiarCombo("Corregimientos");
                    LimpiarCombo("Departamentos");
                    LimpiarCombo("Municipios");
                    LimpiarCombo("Veredas");
                    labDomicilio.Text = "";
                    labError.Visible = true;
                    modClientes.Show();
                    cboPaises.Focus();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar los Departamentos: " + ex.Message;
                LimpiarCombo("Corregimientos");
                LimpiarCombo("Departamentos");
                LimpiarCombo("Municipios");
                LimpiarCombo("Veredas");
                labDomicilio.Text = "";
                labError.Visible = true;
                modClientes.Show();
                cboPaises.Focus();
            }
        }
        protected void cboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdPais = Convert.ToInt32(cboPaises.SelectedValue);

                if (IdPais > 0)
                {
                    ListarComboDepartamentosPais(IdPais);

                    string Pais = cboPaises.SelectedItem.Text;
                    labDomicilio.Text = Pais + " - ";
                }
                else
                {
                    labMensaje.Text = "Debe seleccionar un País.";
                    LimpiarCombo("Corregimientos");
                    LimpiarCombo("Departamentos");
                    LimpiarCombo("Municipios");
                    LimpiarCombo("Veredas");
                    labError.Visible = true;
                    modClientes.Show();
                    cboPaises.Focus();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar los Departamentos: " + ex.Message;
                LimpiarCombo("Corregimientos");
                LimpiarCombo("Departamentos");
                LimpiarCombo("Municipios");
                LimpiarCombo("Veredas");
                labError.Visible = true;
                modClientes.Show();
            }
        }
        protected void cboVeredas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Vereda = cboVeredas.SelectedItem.Text;
            labDomicilio.Text = cboVeredas + " - ";
            modClientes.Show();
        }
        protected void cboMunicipios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdMunicipio = Convert.ToInt32(cboMunicipios.SelectedValue);

                if (IdMunicipio > 0)
                {
                    DataTable dtCorregimientos = objInformacionRegional.ListarComboCorregimientosMunicipio(IdMunicipio);

                    if (dtCorregimientos.Rows.Count > 0)
                    {
                        cboCorregimientos.DataSource = dtCorregimientos;
                        cboCorregimientos.DataValueField = "IdCorregimiento";
                        cboCorregimientos.DataTextField = "Nombre";
                        cboCorregimientos.DataBind();

                        string Municipio = cboMunicipios.SelectedItem.Text;
                        labDomicilio.Text = labDomicilio.Text + " - " + Municipio + " - ";

                        LimpiarCombo("Veredas");
                        modClientes.Show();
                    }
                    else
                    {
                        labMensaje.Text = "No existen Corregimientos creados en base de datos para el municipio seleccionado.";
                        LimpiarCombo("Corregimientos");
                        LimpiarCombo("Veredas");
                        labError.Visible = true;
                        cboMunicipios.Focus();
                        modClientes.Show();
                    }
                }
                else
                {
                    labMensaje.Text = "Debe seleccionar un Municipio.";
                    LimpiarCombo("Corregimientos");
                    LimpiarCombo("Veredas");
                    labError.Visible = true;
                    cboMunicipios.Focus();
                    modClientes.Show();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar los Corregimientos: " + ex.Message;
                LimpiarCombo("Corregimientos");
                LimpiarCombo("Veredas");
                labError.Visible = true;
                cboMunicipios.Focus();
                modClientes.Show();
            }
        }
        protected void cboDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdDepartamento = Convert.ToInt32(cboDepartamentos.SelectedValue);

                if (IdDepartamento > 0)
                {
                    DataTable dtMunicipios = objInformacionRegional.ListarComboMunicipiosDepartamento(IdDepartamento);

                    if (dtMunicipios.Rows.Count > 0)
                    {
                        cboMunicipios.DataSource = dtMunicipios;
                        cboMunicipios.DataValueField = "IdMunicipio";
                        cboMunicipios.DataTextField = "Nombre";
                        cboMunicipios.DataBind();

                        string Departamento = cboDepartamentos.SelectedItem.Text;
                        labDomicilio.Text = labDomicilio.Text + " - " + Departamento + " - ";

                        LimpiarCombo("Corregimientos");
                        LimpiarCombo("Veredas");
                        modClientes.Show();
                    }
                    else
                    {
                        labMensaje.Text = "No existen Municipios creados en base de datos para el departamento seleccionado.";
                        LimpiarCombo("Corregimientos");
                        LimpiarCombo("Municipios");
                        cboDepartamentos.Focus();
                        LimpiarCombo("Veredas");
                        labError.Visible = true;
                        modClientes.Show();
                    }
                }
                else
                {
                    labMensaje.Text = "Debe seleccionar un Departamento.";
                    LimpiarCombo("Corregimientos");
                    LimpiarCombo("Municipios");
                    cboDepartamentos.Focus();
                    LimpiarCombo("Veredas");
                    labError.Visible = true;
                    modClientes.Show();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar los Municipios: " + ex.Message;
                LimpiarCombo("Corregimientos");
                LimpiarCombo("Municipios");
                cboDepartamentos.Focus();
                LimpiarCombo("Veredas");
                labError.Visible = true;
                modClientes.Show();
            }
        }
        protected void cboCorregimientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdCorregimiento = Convert.ToInt32(cboCorregimientos.SelectedValue);

                if (IdCorregimiento > 0)
                {
                    DataTable dtVeredas = objInformacionRegional.ListarComboVeredasCorregimientos(IdCorregimiento);

                    if (dtVeredas.Rows.Count > 0)
                    {
                        string Corregimiento = cboCorregimientos.SelectedItem.Text;
                        labDomicilio.Text = Corregimiento + " - ";

                        cboVeredas.DataSource = dtVeredas;
                        cboVeredas.DataValueField = "IdVereda";
                        cboVeredas.DataTextField = "Nombre";
                        cboVeredas.DataBind();
                        modClientes.Show();
                    }
                    else
                    {
                        labMensaje.Text = "No existen Veredas creados en base de datos para el corregimiento seleccionado.";
                        cboCorregimientos.Focus();
                        LimpiarCombo("Veredas");
                        labError.Visible = true;
                        modClientes.Show();
                    }
                }
                else
                {
                    labMensaje.Text = "Debe seleccionar un Corregimiento.";
                    cboCorregimientos.Focus();
                    LimpiarCombo("Veredas");
                    labError.Visible = true;
                    modClientes.Show();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar las Veredas: " + ex.Message;
                cboCorregimientos.Focus();
                LimpiarCombo("Veredas");
                labError.Visible = true;
                modClientes.Show();
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
                else if (cboPaises.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar un País.";
                    labError.Visible = true;
                    cboPaises.Focus();
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
                else if (cboCorregimientos.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar un Corregimiento.";
                    cboCorregimientos.Focus();
                    labError.Visible = true;
                    modClientes.Show();
                    return false;
                }
                else if (cboVeredas.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar una Vereda.";
                    labError.Visible = true;
                    cboVeredas.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtBarrio.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Barrio es obligatorio.";
                    labError.Visible = true;
                    txtBarrio.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtDireccion.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Domicilio es obligatorio.";
                    labError.Visible = true;
                    txtDireccion.Focus();
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
                else if (txtTelefono1.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Teléfono 1 es obligatorio.";
                    labError.Visible = true;
                    txtTelefono1.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (Convert.ToInt64(txtTelefono1.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Teléfono 1 debe ser mayor que cero.";
                    labError.Visible = true;
                    txtTelefono1.Text = "";
                    txtTelefono1.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtTelefono1.Text.Trim().Length < 7)
                {
                    labMensaje.Text = "El campo Teléfono 1 no tiene el formato correcto.";
                    labError.Visible = true;
                    txtTelefono1.Focus();
                    modClientes.Show();
                    return false;
                }
                else if (txtTelefono2.Text.Trim() != "")
                {
                    if (Convert.ToInt64(txtTelefono2.Text.Trim()) <= 0)
                    {
                        labMensaje.Text = "El campo Teléfono 2 debe ser mayor que cero.";
                        labError.Visible = true;
                        txtTelefono2.Text = "";
                        txtTelefono2.Focus();
                        modClientes.Show();
                        return false;
                    }
                    else if (txtTelefono2.Text.Trim().Length < 7)
                    {
                        labMensaje.Text = "El campo Teléfono 2 no tiene el formato correcto.";
                        labError.Visible = true;
                        txtTelefono2.Focus();
                        modClientes.Show();
                        return false;
                    }
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
                    oCliente.IdPais = Convert.ToInt32(cboPaises.SelectedValue);
                    oCliente.Pais = cboPaises.SelectedItem.Text;
                    oCliente.IdDepartamento = Convert.ToInt32(cboDepartamentos.SelectedValue);
                    oCliente.Departamento = cboDepartamentos.SelectedItem.Text;
                    oCliente.IdMunicipio = Convert.ToInt32(cboMunicipios.SelectedValue);
                    oCliente.Municipio = cboMunicipios.SelectedItem.Text;
                    oCliente.IdCorregimiento = Convert.ToInt32(cboCorregimientos.SelectedValue);
                    oCliente.Corregimiento = cboCorregimientos.SelectedItem.Text;
                    oCliente.IdVereda = Convert.ToInt32(cboVeredas.SelectedValue);
                    oCliente.Vereda = cboVeredas.SelectedItem.Text;
                    oCliente.Barrio = txtBarrio.Text.Trim();
                    oCliente.Direccion = txtDireccion.Text.Trim();
                    oCliente.WhatsApp = Convert.ToInt64(txtWhatsApp.Text.Trim());
                    oCliente.Telefono1 = Convert.ToInt64(txtTelefono1.Text.Trim());
                    oCliente.Email = txtEmail.Text.Trim();
                    oCliente.Estado = cboEstado.SelectedValue == "1" ? true : false;
                    oCliente.UsuarioCreacion = Session["Usuario"].ToString();
                    oCliente.EquipoCreacion = System.Environment.MachineName;
                    oCliente.UsuarioModificacion = Session["Usuario"].ToString();
                    oCliente.EquipoModificacion = System.Environment.MachineName;

                    if (txtTelefono2.Text.Trim() != "")
                    {
                        oCliente.Telefono2 = Convert.ToInt64(txtTelefono2.Text.Trim());
                    }

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