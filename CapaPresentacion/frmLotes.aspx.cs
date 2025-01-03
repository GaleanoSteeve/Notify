using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class frmLotes : System.Web.UI.Page
    {
        #region Variables

        ObjLotes oLote = new ObjLotes();
        NegLotes objLotes = new NegLotes();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            labError.Visible = false;
            txtFiltro.Focus();

            if (!IsPostBack)
            {
                ListarLotes();
            }
        }

        #endregion

        //Metodos
        private void ListarLotes()
        {
            try
            {
                DataTable dtLotes = objLotes.ListarLotes();

                if (dtLotes.Rows.Count > 0)
                {
                    gvLotes.DataSource = dtLotes;
                    gvLotes.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen Lotes creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Lotes";
                string Mensaje = "Error tratando de listar los Lotes: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        private void ListarComboEstados()
        {
            try
            {
                DataTable dtEstados = objLotes.ListarComboEstados();

                if (dtEstados.Rows.Count > 0)
                {
                    cboEstados.DataSource = dtEstados;
                    cboEstados.DataValueField = "IdEstado";
                    cboEstados.DataTextField = "Estado";
                    cboEstados.DataBind();
                }
                else
                {
                    labMensaje.Text = "No existen Estados creados en base de datos.";
                    labError.Visible = true;
                    LimpiarCombo("Estados");
                    modLotes.Show();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar el ComboBox de los Estados: " + ex.Message;
                labError.Visible = true;
                LimpiarCombo("Estados");
                modLotes.Show();
            }
        }
        private void LimpiarCombo(string Combo)
        {
            DataTable dtDatos = new DataTable();

            switch (Combo)
            {
                case "Proyectos":
                    cboProyectos.DataSource = dtDatos;
                    cboProyectos.DataBind();
                    break;
                case "Manzanas":
                    cboManzanas.DataSource = dtDatos;
                    cboManzanas.DataBind();
                    break;
                case "Estados":
                    cboEstados.DataSource = dtDatos;
                    cboEstados.DataBind();
                    break;
            }
        }

        //Lotes
        private void ListarComboProyectos()
        {
            try
            {
                DataTable dtEstados = objLotes.ListarComboProyectos();

                if (dtEstados.Rows.Count > 0)
                {
                    cboProyectos.DataSource = dtEstados;
                    cboProyectos.DataValueField = "IdProyecto";
                    cboProyectos.DataTextField = "Nombre";
                    cboProyectos.DataBind();
                }
                else
                {
                    labMensaje.Text = "No existen Proyectos creados en base de datos.";
                    LimpiarCombo("Proyectos");
                    labError.Visible = true;
                    modLotes.Show();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar el ComboBox de los Proyectos: " + ex.Message;
                LimpiarCombo("Proyectos");
                labError.Visible = true;
                modLotes.Show();
            }
        }
        protected void cboProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);

                if (IdProyecto > 0) //Listar manzanas
                {
                    oLote = new ObjLotes();
                    oLote.IdProyecto = IdProyecto;
                    DataTable dtManzanas = objLotes.ListarComboManzanas(oLote);

                    if (dtManzanas.Rows.Count > 0)
                    {
                        cboManzanas.DataSource = dtManzanas;
                        cboManzanas.DataValueField = "IdManzana";
                        cboManzanas.DataTextField = "Nombre";
                        cboManzanas.DataBind();
                        modLotes.Show();
                    }
                    else
                    {
                        labMensaje.Text = "No existen Manzanas creadas en base de datos para el proyecto seleccionado.";
                        LimpiarCombo("Manzanas");
                        labError.Visible = true;
                        modLotes.Show();
                    }
                }
                else
                {
                    labMensaje.Text = "Debe seleccionar un Proyecto.";
                    LimpiarCombo("Manzanas");
                    labError.Visible = true;
                    modLotes.Show();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar el ComboBox de las Manzanas: " + ex.Message;
                LimpiarCombo("Manzanas");
                labError.Visible = true;
                modLotes.Show();
            }
        }

        //Controles
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            LimpiarCombo("Manzanas");
            ListarComboProyectos();
            ListarComboEstados();
            cboProyectos.Focus();
            labCodigo.Text = "0";
            labCrear.Text = "1";
            modLotes.Show();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                labCrear.Text = "0";
                LinkButton btnEditar = (LinkButton)sender;
                int Codigo = Convert.ToInt32(btnEditar.Text);

                if (Codigo > 0)
                {
                    DataTable dtLote = objLotes.ListarLote(Codigo);

                    if (dtLote.Rows.Count > 0) //Lote existe
                    {
                        LimpiarCombo("Proyectos");
                        LimpiarCombo("Manzanas");
                        LimpiarCombo("Estados");
                        oLote = new ObjLotes();

                        int IdProyecto = Convert.ToInt32(dtLote.Rows[0]["IdProyecto"]);
                        int IdManzana = Convert.ToInt32(dtLote.Rows[0]["IdManzana"]);
                        int IdEstado = Convert.ToInt32(dtLote.Rows[0]["IdEstado"]);

                        ListarComboProyectos();
                        ListarComboEstados();

                        oLote.IdProyecto = IdProyecto;
                        DataTable dtManzanas = objLotes.ListarComboManzanas(oLote);

                        if (dtManzanas.Rows.Count > 0)
                        {
                            cboManzanas.DataSource = dtManzanas;
                            cboManzanas.DataValueField = "IdManzana";
                            cboManzanas.DataTextField = "Nombre";
                            cboManzanas.DataBind();
                        }
                        else
                        {
                            labMensaje.Text = "No existen Manzanas creadas en base de datos para el proyecto.";
                            LimpiarCombo("Manzanas");
                            labError.Visible = true;
                            modLotes.Show();
                        }

                        cboProyectos.SelectedValue = IdProyecto.ToString();
                        cboManzanas.SelectedValue = IdManzana.ToString();
                        cboEstados.SelectedValue = IdEstado.ToString();

                        labCodigo.Text = dtLote.Rows[0]["IdLote"].ToString();
                        txtNumero.Text = dtLote.Rows[0]["Numero"].ToString();
                        txtValor.Text = dtLote.Rows[0]["Valor"].ToString();
                        txtCuotaInicial.Text = dtLote.Rows[0]["CuotaInicial"].ToString();
                        txtCuotaMensual.Text = dtLote.Rows[0]["CuotaMensual"].ToString();
                        txtArea.Text = dtLote.Rows[0]["Area"].ToString();
                        cboProyectos.Focus();
                        modLotes.Show();
                    }
                    else
                    {
                        labCodigo.Text = "";
                        string Titulo = "Advertencia";
                        string Mensaje = "El Lote seleccionado no existe en base de datos.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "El Lote seleccionado no tiene código asignado.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Modificando Lote";
                string Mensaje = "Error tratando de modificar el Lote: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Parametro = txtFiltro.Text.Trim();

                if (Parametro != "")
                {
                    DataTable dtLotes = objLotes.ListarLotesParametros(Parametro);

                    if (dtLotes.Rows.Count > 0)
                    {
                        gvLotes.DataSource = dtLotes;
                        gvLotes.DataBind();
                        txtFiltro.Focus();
                    }
                    else
                    {
                        gvLotes.DataSource = null;
                        gvLotes.DataBind();
                        txtFiltro.Focus();

                        string Titulo = "Advertencia";
                        string Mensaje = "No existen Lotes creados en base de datos con los valores ingresados.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    ListarLotes();
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
        private bool ExisteLote()
        {
            try
            {
                bool Crear = labCrear.Text == "1" ? true : false;

                if (Crear) //Crear
                {
                    oLote = new ObjLotes();
                    oLote.IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);
                    oLote.IdManzana = Convert.ToInt32(cboManzanas.SelectedValue);
                    oLote.Numero = Convert.ToInt32(txtNumero.Text.Trim());

                    DataTable dtLote = objLotes.ExisteLote(oLote);

                    if (dtLote.Rows.Count > 0)
                    {
                        labMensaje.Text = "El Lote ingresado ya existe en base de datos.";
                        labError.Visible = true;
                        modLotes.Show();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar el Lote: " + ex.Message;
                labError.Visible = true;
                modLotes.Show();
                return false;
            }
        }
        private bool ValidarCampos()
        {
            try
            {
                if (cboProyectos.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar un Proyecto.";
                    labError.Visible = true;
                    cboProyectos.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (cboManzanas.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar una Manzana.";
                    labError.Visible = true;
                    cboManzanas.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (txtNumero.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Número es obligatorio.";
                    labError.Visible = true;
                    txtNumero.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (Convert.ToInt32(txtNumero.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Número debe ser mayor que cero.";
                    labError.Visible = true;
                    txtNumero.Text = "";
                    txtNumero.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (ExisteLote())
                {
                    return false;
                }
                else if (txtValor.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Valor es obligatorio.";
                    labError.Visible = true;
                    txtValor.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (Convert.ToInt64(txtValor.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Valor debe ser mayor que cero.";
                    labError.Visible = true;
                    txtValor.Text = "";
                    txtValor.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (txtCuotaInicial.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Cuota Inicial es obligatorio.";
                    labError.Visible = true;
                    txtCuotaInicial.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (Convert.ToInt64(txtCuotaInicial.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Cuota Inicial debe ser mayor que cero.";
                    txtCuotaInicial.Text = "";
                    txtCuotaInicial.Focus();
                    labError.Visible = true;
                    modLotes.Show();
                    return false;
                }
                else if (txtCuotaMensual.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Cuota Mensual es obligatorio.";
                    labError.Visible = true;
                    txtCuotaMensual.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (Convert.ToInt64(txtCuotaMensual.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Cuota Mensual debe ser mayor que cero.";
                    txtCuotaMensual.Text = "";
                    txtCuotaMensual.Focus();
                    labError.Visible = true;
                    modLotes.Show();
                    return false;
                }
                else if (txtArea.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Área es obligatorio.";
                    labError.Visible = true;
                    txtArea.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (Convert.ToInt32(txtArea.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Área debe ser mayor que cero.";
                    labError.Visible = true;
                    txtArea.Text = "";
                    txtArea.Focus();
                    modLotes.Show();
                    return false;
                }
                else if (cboEstados.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar una Estado.";
                    labError.Visible = true;
                    cboEstados.Focus();
                    modLotes.Show();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar los campos: " + ex.Message;
                labError.Visible = true;
                modLotes.Show();
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    oLote = new ObjLotes();
                    oLote.IdLote = Convert.ToInt32(labCodigo.Text);
                    oLote.IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);
                    oLote.Proyecto = cboProyectos.SelectedItem.Text;
                    oLote.IdManzana = Convert.ToInt32(cboManzanas.SelectedValue);
                    oLote.Manzana = cboManzanas.SelectedItem.Text;
                    oLote.Numero = Convert.ToInt32(txtNumero.Text.Trim());
                    oLote.Valor = Convert.ToDecimal(txtValor.Text.Trim());
                    oLote.CuotaInicial = Convert.ToDecimal(txtCuotaInicial.Text.Trim());
                    oLote.CuotaMensual = Convert.ToDecimal(txtCuotaMensual.Text.Trim());
                    oLote.Area = Convert.ToDecimal(txtArea.Text.Trim());
                    oLote.IdEstado = Convert.ToInt32(cboEstados.SelectedValue);
                    oLote.Estado = cboEstados.SelectedItem.Text;
                    oLote.UsuarioCreacion = Session["Usuario"].ToString();
                    oLote.UsuarioModificacion = Session["Usuario"].ToString();

                    string strMensaje = labCrear.Text == "1" ? "creado" : "actualizado";

                    if (objLotes.Almacenar(oLote))
                    {
                        string Titulo = "Información";
                        string Mensaje = "Lote " + strMensaje + " correctamente.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmLotes.aspx'});";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                    else
                    {
                        labMensaje.Text = "El Lote no pudo ser " + strMensaje + ". Debe contactar al administrador del sistema.";
                        labError.Visible = true;
                        modLotes.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                string Mensaje = labCrear.Text == "1" ? "crear" : "actualizar";

                labMensaje.Text = "Error tratando de " + Mensaje + " el Lote: " + ex.Message;
                labError.Visible = true;
                modLotes.Show();
            }
        }

        //Eliminar
        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btnEliminar = (ImageButton)sender;
                int Codigo = Convert.ToInt32(btnEliminar.CommandArgument.ToString().Trim());

                if (Codigo > 0)
                {
                    oLote = new ObjLotes();
                    oLote.IdLote = Codigo;

                    if (objLotes.Eliminar(oLote)) //Eliminar
                    {
                        DataTable dtLote = objLotes.ListarLote(Codigo);

                        if (dtLote.Rows.Count == 0) //Validar si el lote fue eliminado
                        {
                            string Titulo = "Información";
                            string Mensaje = "Lote eliminado correctamente.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmLotes.aspx'});";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        }
                        else
                        {
                            string Titulo = "Información";
                            string Mensaje = "El Lote no pudo ser eliminado. Debe contactar al administrador del sistema.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        }
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "El Lote no pudo ser eliminado. Debe contactar al administrador del sistema.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "El Lote no pudo ser eliminado. Debe contactar al administrador del sistema.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Eliminando Lote";
                string Mensaje = "Error tratando de eliminar el Lote: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
    }
}