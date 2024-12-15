using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;

namespace CapaPresentacion
{
    public partial class frmConfiguracion : System.Web.UI.Page
    {
        #region Variables

        private ObjConfiguracion oConfiguracion = new ObjConfiguracion();

        private NegConfiguracion objConfiguracion = new NegConfiguracion();
        private NegInformacionRegional objInformacionRegional = new NegInformacionRegional();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarComboDepartamentos();
                ListarConfiguracion();
            }         
        }

        #endregion

        //Metodos
        private void ListarConfiguracion()
        {
            try
            {
                DataTable dtConfiguracion = objConfiguracion.ListarConfiguracion();

                if (dtConfiguracion.Rows.Count > 0)
                {
                    string IdDepartamento = dtConfiguracion.Rows[0]["IdDepartamento"].ToString();

                    txtNit.Text = dtConfiguracion.Rows[0]["Documento"].ToString();
                    txtRazonSocial.Text = dtConfiguracion.Rows[0]["RazonSocial"].ToString();
                    txtNombreComercial.Text = dtConfiguracion.Rows[0]["NombreComercial"].ToString();
                    txtDireccion.Text = dtConfiguracion.Rows[0]["Direccion"].ToString();
                    txtTelefono.Text = dtConfiguracion.Rows[0]["Telefono"].ToString();
                    txtEmail.Text = dtConfiguracion.Rows[0]["Email"].ToString();
                    cboDepartamentos.SelectedValue = IdDepartamento;
                    ListarComboCiudadesPorDepartamento(IdDepartamento); //Listar combo ciudades
                    cboCiudades.SelectedValue = dtConfiguracion.Rows[0]["IdCiudad"].ToString();
                    txtNit.Enabled = false;
                }
                else
                {
                    txtNit.Enabled = true;
                    ListarComboCiudadesNull();
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen datos de la Configuración del sistema creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Configuración";
                string Mensaje = "Error tratando de listar la Configuración del sistema: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        private void ListarComboCiudadesNull()
        {
            try
            {
                DataTable dtCiudades = new DataTable();

                //Agregar columnas
                dtCiudades.Columns.Add("IdCiudad", typeof(string));
                dtCiudades.Columns.Add("Nombre", typeof(string));

                //Agregar filas
                dtCiudades.Rows.Add("0", "Seleccione...");

                //Aceptar cambios
                dtCiudades.AcceptChanges();

                cboCiudades.DataSource = dtCiudades;
                cboCiudades.DataValueField = "IdCiudad";
                cboCiudades.DataTextField = "Nombre";
                cboCiudades.DataBind();
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Ciudades";
                string Mensaje = "Error tratando de listar las Ciudades Null: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        private void ListarComboDepartamentos()
        {
            try
            {
                DataTable dtDepartamentos = objInformacionRegional.ListarComboDepartamentos();

                if (dtDepartamentos.Rows.Count > 0)
                {
                    cboDepartamentos.DataSource = dtDepartamentos;
                    cboDepartamentos.DataValueField = "IdDepartamento";
                    cboDepartamentos.DataTextField = "Nombre";
                    cboDepartamentos.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen Departamentos creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Departamentos";
                string Mensaje = "Error tratando de listar los Departamentos: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        private void ListarComboCiudadesPorDepartamento(string IdDepartamento)
        {
            try
            {
                DataTable dtCiudades = objInformacionRegional.ListarCiudadPorDepartamento(IdDepartamento);

                if (dtCiudades.Rows.Count > 0)
                {
                    cboCiudades.DataSource = dtCiudades;
                    cboCiudades.DataValueField = "IdCiudad";
                    cboCiudades.DataTextField = "Nombre";
                    cboCiudades.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen ciudades creadas en base de datos para el departamento seleccionado.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Ciudades";
                string Mensaje = "Error tratando de listar las Ciudades: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }

        //Controles
        protected void cboDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string IdDepartamento = cboDepartamentos.SelectedValue;

                if (Convert.ToInt16(IdDepartamento) > 0)
                {
                    ListarComboCiudadesPorDepartamento(IdDepartamento);
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "Debe seleccionar un departamento.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Ciudades";
                string Mensaje = "Error tratando de listar las Ciudades: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Validando Email";
                string Mensaje = "Error tratando de validar el Email: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                return false;
            }            
        }
        private bool ValidarCampos()
        {
            try
            {
                if (txtNit.Text.Trim() == "")
                {
                    txtNit.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Nit es obligatorio.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
                else if (txtRazonSocial.Text.Trim() == "")
                {
                    txtRazonSocial.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Razón Social es obligatorio.";
                    string script = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", script, true);
                    return false;
                }
                else if (txtNombreComercial.Text.Trim() == "")
                {
                    txtNombreComercial.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Nombre Comercial es obligatorio.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
                else if (txtDireccion.Text.Trim() == "")
                {
                    txtDireccion.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Dirección es obligatorio.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
                else if (txtTelefono.Text.Trim() == "")
                {
                    txtTelefono.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Teléfono es obligatorio.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
                else if (txtTelefono.Text.Trim().Length < 7)
                {
                    txtTelefono.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Teléfono no tiene el formato correcto.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
                else if (txtEmail.Text.Trim() == "")
                {
                    txtEmail.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = " El campo Email es obligatorio.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
                else if (!ValidarEmail())
                {
                    txtEmail.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Email no tiene el formato correcto.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
                else if (Convert.ToInt32(cboDepartamentos.SelectedValue) <= 0)
                {
                    cboDepartamentos.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "Debe seleccionar un Departamento.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
                else if (Convert.ToInt32(cboCiudades.SelectedValue) <= 0)
                {
                    cboCiudades.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "Debe seleccionar una Ciudad.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string Titulo = "Error Validando Campos";
                string Mensaje = "Error tratando de validar los campos: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    oConfiguracion.Documento = Convert.ToInt64(txtNit.Text.Trim());
                    oConfiguracion.RazonSocial = txtRazonSocial.Text.Trim();
                    oConfiguracion.NombreComercial = txtNombreComercial.Text.Trim();
                    oConfiguracion.IdDepartamento = cboDepartamentos.SelectedValue;
                    oConfiguracion.Departamento = cboDepartamentos.SelectedItem.Text;
                    oConfiguracion.IdCiudad = cboCiudades.SelectedValue;
                    oConfiguracion.Ciudad = cboCiudades.SelectedItem.Text;
                    oConfiguracion.Direccion = txtDireccion.Text.Trim();
                    oConfiguracion.Telefono = Convert.ToInt64(txtTelefono.Text.Trim());
                    oConfiguracion.Email = txtEmail.Text.Trim();
                    oConfiguracion.UsuarioCreacion = Session["Usuario"].ToString();
                    oConfiguracion.EquipoCreacion = System.Environment.MachineName;

                    string Resultado = objConfiguracion.Guardar(oConfiguracion);

                    if (Resultado == "C") //Crear
                    {
                        string Titulo = "Información";
                        string Mensaje = "La configuración del sistema fue creada correctamente.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                    else if (Resultado == "A") //Actualizar
                    {
                        string Titulo = "Información";
                        string Mensaje = "La configuración del sistema fue actualizada correctamente.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "Los datos de la configuración no pudieron se almacenados. Contacte al administrador del sistema.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Almacenando Configuración";
                string Mensaje = "Error tratando guardar los datos de la Configuración: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
    }
}