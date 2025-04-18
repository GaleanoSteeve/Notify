using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class frmManzanas : System.Web.UI.Page
    {
        #region Variables

        private ObjManzanas oManzana = new ObjManzanas();
        private NegManzanas objManzanas = new NegManzanas();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            labError.Visible = false;

            if (!IsPostBack)
            {
                ListarManzanas();
                ListarComboProyectos();
            }
        }

        #endregion

        //Metodos
        private void LimpiarCombo()
        {
            DataTable dtDatos = new DataTable();
            cboProyectos.DataSource = dtDatos;
            cboProyectos.DataBind();
        }
        private void ListarManzanas()
        {
            try
            {
                DataTable dtManzanas = objManzanas.ListarManzanas();

                if (dtManzanas.Rows.Count > 0)
                {
                    gvManzanas.DataSource = dtManzanas;
                    gvManzanas.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen Manzanas creadas en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Manzanas";
                string Mensaje = "Error tratando de listar las Manzanas: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        private bool ListarMaximoCodigo()
        {
            try
            {
                DataTable dtCodigo = objManzanas.ListarMaximoIdManzana();

                if (dtCodigo.Rows.Count > 0)
                {
                    txtCodigo.Text = dtCodigo.Rows[0]["IdManzana"].ToString();
                    return true;
                }
                else
                {
                    txtCodigo.Text = "";
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen Proyectos creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    return false;
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Código";
                string Mensaje = "Error tratando de listar Código: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                return false;
            }
        }
        private void ListarComboProyectos()
        {
            try
            {
                DataTable dtProyectos = objManzanas.ListarComboProyectos();

                if (dtProyectos.Rows.Count > 0)
                {
                    cboProyectos.DataSource = dtProyectos;
                    cboProyectos.DataValueField = "IdProyecto";
                    cboProyectos.DataTextField = "Nombre";
                    cboProyectos.DataBind();
                }
                else
                {
                    labMensaje.Text = "No existen Proyectos creados en base de datos.";
                    labError.Visible = true;
                    modManzanas.Show();
                    LimpiarCombo();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar el ComboBox de los Proyectos: " + ex.Message;
                labError.Visible = true;
                modManzanas.Show();
                LimpiarCombo();
            }
        }

        //Controles
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (ListarMaximoCodigo())
            {
                labManzanaSeleccionada.Text = "";
                txtNombre.Text = "";
                labCrear.Text = "1";
                modManzanas.Show();
                txtNombre.Focus();
            }
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                labCrear.Text = "0";
                this.oManzana = new ObjManzanas();
                LinkButton btnEditar = (LinkButton)sender;
                int Codigo = Convert.ToInt32(btnEditar.Text);

                if (Codigo > 0)
                {
                    this.oManzana.IdManzana = Codigo;
                    DataTable dtManzana = objManzanas.ListarManzana(this.oManzana);

                    if (dtManzana.Rows.Count > 0) //Manzana existe
                    {
                        txtCodigo.Text = dtManzana.Rows[0]["IdManzana"].ToString();
                        txtNombre.Text = dtManzana.Rows[0]["Nombre"].ToString();
                        cboProyectos.SelectedValue = dtManzana.Rows[0]["IdProyecto"].ToString();
                        labManzanaSeleccionada.Text = txtNombre.Text;
                        modManzanas.Show();
                        txtNombre.Focus();
                    }
                    else
                    {
                        txtCodigo.Text = "";
                        labManzanaSeleccionada.Text = "";

                        string Titulo = "Advertencia";
                        string Mensaje = "La Manzana seleccionada no existe en base de datos.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    txtCodigo.Text = "";
                    labManzanaSeleccionada.Text = "";

                    string Titulo = "Advertencia";
                    string Mensaje = "La Manzana seleccionada no tiene código asignado.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                txtCodigo.Text = "";
                labManzanaSeleccionada.Text = "";

                string Titulo = "Error Modificando Manzana";
                string Mensaje = "Error tratando de modificar la Manzana: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }

        //Guardar
        private bool ValidarCampos()
        {
            try
            {
                if (txtCodigo.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Código es obligatorio.";
                    labError.Visible = true;
                    modManzanas.Show();
                    return false;
                }
                else if (txtNombre.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Nombre es obligatorio.";
                    labError.Visible = true;
                    modManzanas.Show();
                    txtNombre.Focus();
                    return false;
                }
                else if (Convert.ToInt32(cboProyectos.SelectedValue) == 0)
                {
                    labMensaje.Text = "Debe seleccionar un Proyecto.";
                    labError.Visible = true;
                    cboProyectos.Focus();
                    modManzanas.Show();
                    return false;
                }
                else if (ExisteManzana())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar los campos: " + ex.Message;
                labError.Visible = true;
                modManzanas.Show();
                return false;
            }
        }
        private bool ExisteManzana()
        {
            try
            {
                bool Crear = labCrear.Text == "1" ? true : false;

                this.oManzana = new ObjManzanas();
                this.oManzana.Nombre = txtNombre.Text.Trim();
                this.oManzana.IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);

                if (Crear) //Crear
                {
                    DataTable dtManzana = objManzanas.ExisteManzana(this.oManzana);

                    if (dtManzana.Rows.Count > 0)
                    {
                        labMensaje.Text = "La manzana ingresada ya está existe en base de datos asociada al proyecto seleccionado.";
                        labError.Visible = true;
                        modManzanas.Show();
                        return true;
                    }
                }
                else //Editar
                {
                    string ManzanaIngresada = txtNombre.Text.Trim();
                    string ManzanaSeleccionada = labManzanaSeleccionada.Text.Trim();

                    if (ManzanaIngresada != ManzanaSeleccionada) //Hubo cambio nombre: validar que no exista una manzana con el nuevo nombre ingresado
                    {
                        DataTable dtManzana = objManzanas.ExisteManzana(this.oManzana);

                        if (dtManzana.Rows.Count > 0)
                        {
                            labMensaje.Text = "La manzana ingresada ya está existe en base de datos asociada al proyecto seleccionado.";
                            labError.Visible = true;
                            modManzanas.Show();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar la Manzana: " + ex.Message;
                labError.Visible = true;
                modManzanas.Show();
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    this.oManzana = new ObjManzanas();
                    this.oManzana.IdManzana = Convert.ToInt32(txtCodigo.Text.Trim());
                    this.oManzana.Nombre = txtNombre.Text.Trim();
                    this.oManzana.IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);

                    string strMensaje = labCrear.Text == "1" ? "creada" : "actualizada";

                    if (objManzanas.Almacenar(this.oManzana))
                    {
                        string Titulo = "Información";
                        string Mensaje = "Manzana " + strMensaje + " correctamente.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmManzanas.aspx'});";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                    else
                    {
                        labMensaje.Text = "La Manzana no pudo ser " + strMensaje + ". Debe contactar al administrador del sistema.";
                        labError.Visible = true;
                        modManzanas.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                string Mensaje = labCrear.Text == "1" ? "crear" : "actualizar";

                labMensaje.Text = "Error tratando de " + Mensaje + " la Manzana: " + ex.Message;
                labError.Visible = true;
                modManzanas.Show();
            }
        }

        //Eliminar
        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btnEliminar = (ImageButton)sender;
                int IdManzana = Convert.ToInt32(btnEliminar.CommandArgument.ToString().Trim());

                if (IdManzana > 0)
                {
                    this.oManzana = new ObjManzanas();
                    this.oManzana.IdManzana = IdManzana;

                    DataTable dtManzanaLotes = objManzanas.ListarManzanaLotes(this.oManzana);

                    if (dtManzanaLotes.Rows.Count == 0) //Manzana no tiene lotes asociados
                    {
                        if (objManzanas.Eliminar(this.oManzana)) //Eliminar
                        {
                            DataTable dtLote = objManzanas.ListarManzana(this.oManzana);

                            if (dtLote.Rows.Count == 0) //Validar si la manzana fue eliminada
                            {
                                string Titulo = "Información";
                                string Mensaje = "Manzana eliminada correctamente.";
                                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmManzanas.aspx'});";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                            }
                            else
                            {
                                string Titulo = "Información";
                                string Mensaje = "La Manzana no pudo ser eliminada. Debe contactar al administrador del sistema.";
                                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                            }
                        }
                        else
                        {
                            string Titulo = "Advertencia";
                            string Mensaje = "La Manzana no pudo ser eliminada. Debe contactar al administrador del sistema.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        }
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "La Manzana no puede ser eliminada porque tiene lotes asociados.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "La Manzana no pudo ser eliminada. Debe contactar al administrador del sistema.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Eliminando Manzana";
                string Mensaje = "Error tratando de eliminar la Manzana: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
    }
}