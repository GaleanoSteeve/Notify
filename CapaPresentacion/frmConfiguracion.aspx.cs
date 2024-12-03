using System;
using System.Data;
using CapaNegocios;
using System.Web.UI;

namespace CapaPresentacion
{
    public partial class frmConfiguracion : System.Web.UI.Page
    {
        #region Variables

        NegInformacionRegional objInformacionRegional = new NegInformacionRegional();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarComboDepartamentos();
                ListarComboCiudades();
                //ListarConfiguracion();
            }
        }

        #endregion

        //Metodos
        private void ListarComboCiudades()
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
                string Titulo = "Error";
                string Mensaje = "Error tratando listar las ciudades: " + ex.Message.ToString().Replace("'", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
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
                    string Mensaje = "No existen departamentos creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error";
                string Mensaje = "Error tratando listar los departamentos: " + ex.Message.ToString().Replace("'", "");
                string str = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", str, true);
            }
        }

        //Eventos
        protected void cboDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string IdDepartamento = cboDepartamentos.SelectedValue;

                if (Convert.ToInt16(IdDepartamento) > 0)
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
                        string Mensaje = "No existen ciudades para el departamento seleccionado.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "Debe seleccionar un departamento.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);

                    ListarComboCiudades();
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error";
                string Mensaje = "Error tratando listar las ciudades: " + ex.Message.ToString().Replace("'", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
            }
        }

        //Guardar
        private bool ValidarEmail()
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    return false;
                }
                else if (txtRazonSocial.Text.Trim() == "")
                {
                    txtRazonSocial.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Razón Social es obligatorio.";
                    string script = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", script, true);
                    return false;
                }
                else if (txtNombreComercial.Text.Trim() == "")
                {
                    txtNombreComercial.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Nombre Comercial es obligatorio.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    return false;
                }
                else if (txtDireccion.Text.Trim() == "")
                {
                    txtDireccion.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Dirección es obligatorio.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    return false;
                }
                else if (txtTelefono.Text.Trim() == "")
                {
                    txtTelefono.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Teléfono es obligatorio.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    return false;
                }
                else if (txtTelefono.Text.Trim().Length < 7)
                {
                    txtTelefono.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Teléfono no tiene el formato correcto.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    return false;
                }
                else if (txtEmail.Text.Trim() == "")
                {
                    txtEmail.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = " El campo Email es obligatorio.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    return false;
                }
                else if (!ValidarEmail())
                {
                    txtEmail.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "El campo Email no tiene el formato correcto.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    return false;
                }
                else if (Convert.ToInt16(cboDepartamentos.SelectedValue) <= 0)
                {
                    cboDepartamentos.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "Debe seleccionar un Departamento.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    return false;
                }
                else if (Convert.ToInt16(cboCiudades.SelectedValue) <= 0)
                {
                    cboCiudades.Focus();
                    string Titulo = "Advertencia";
                    string Mensaje = "Debe seleccionar una Ciudad.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string Titulo = "Error";
                string Mensaje = "Error tratando validar los campos: " + ex.Message.ToString().Replace("'", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {

                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error";
                string Mensaje = "Error tratando guardar los datos: " + ex.Message.ToString().Replace("'", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scriptID", Tipo, true);
            }
        }
    }
}