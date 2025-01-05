using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class frmAsignarLotes : System.Web.UI.Page
    {
        #region Variables

        private DataTable dtLotes = new DataTable();

        private ObjLotes oLote = new ObjLotes();
        private ObjClientes oCliente = new ObjClientes();
        private ObjClientesLotes oClienteLote = new ObjClientesLotes();

        private NegLotes objLotes = new NegLotes();
        private NegClientes objClientes = new NegClientes();
        private NegClientesLotes objClientesLotes = new NegClientesLotes();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            divMensaje.Visible = false;

            if (!IsPostBack)
            {
                Session["dtLotes"] = null;
                ListarComboProyectos();
                txtDocumento.Focus();
                CrearLista();
            }
        }

        #endregion

        //Metodos
        private void CrearLista()
        {
            try
            {
                if (Session["dtLotes"] == null) //Crear DataTable para mostrar GridView vacio
                {
                    DataTable dtDatos = new DataTable();

                    if (dtDatos.Rows.Count == 0)
                    {
                        dtDatos.Columns.Add("Documento", typeof(string));
                        dtDatos.Columns.Add("IdProyecto", typeof(string));
                        dtDatos.Columns.Add("Proyecto", typeof(string));
                        dtDatos.Columns.Add("IdManzana", typeof(string));
                        dtDatos.Columns.Add("Manzana", typeof(string));
                        dtDatos.Columns.Add("IdLote", typeof(string));
                        dtDatos.Columns.Add("Lote", typeof(string));
                        dtDatos.Columns.Add("Almacenado", typeof(string));

                        DataRow drFila = dtDatos.NewRow();
                        drFila["Documento"] = "";
                        drFila["IdProyecto"] = "";
                        drFila["Proyecto"] = "";
                        drFila["IdManzana"] = "";
                        drFila["Manzana"] = "";
                        drFila["IdLote"] = "";
                        drFila["Lote"] = "";
                        drFila["Almacenado"] = "";
                        dtDatos.Rows.Add(drFila);
                    }
                    dtDatos.AcceptChanges();
                    gvLotes.DataSource = dtDatos;
                    gvLotes.DataBind(); 
                }
                else if (Session["dtLotes"] != null)
                {
                    if (string.IsNullOrEmpty(Session["dtLotes"].ToString()) == true)
                    {
                        this.dtLotes = (DataTable)Session["dtLotes"];
                        gvLotes.DataSource = this.dtLotes;
                        gvLotes.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de crear lista vacía: " + ex.Message;
                labMensaje.Visible = true;
            }
        }

        //Cliente
        private void LimpiarCampos()
        {
            cboProyectos.SelectedValue = "0";
            Session["dtLotes"] = null;
            LimpiarCombo("Manzanas");
            LimpiarCombo("Lotes");
            txtCliente.Text = "";
            txtDocumento.Focus();
        }
        private bool ValidarCampos()
        {
            try
            {
                if (txtDocumento.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Documento es obligatorio.";
                    divMensaje.Visible = true;
                    txtDocumento.Focus();
                    return false;
                }
                else if (Convert.ToInt64(txtDocumento.Text.Trim()) <= 0)
                {
                    labMensaje.Text = "El campo Documento debe ser mayor que cero.";
                    divMensaje.Visible = true;
                    txtDocumento.Text = "";
                    txtDocumento.Focus();
                    return false;
                }
                else if (txtDocumento.Text.Trim().Length < 7)
                {
                    labMensaje.Text = "El campo Documento no tiene el formato correcto.";
                    divMensaje.Visible = true;
                    txtDocumento.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar los campos: " + ex.Message;
                divMensaje.Visible = true;
                return false;
            }
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    long Documento = Convert.ToInt64(txtDocumento.Text.Trim());

                    if (Documento > 0)
                    {
                        oClienteLote = new ObjClientesLotes();
                        oClienteLote.Documento = Documento;
                        DataSet dsCliente = objClientesLotes.ListarClienteLote(oClienteLote);

                        if (dsCliente.Tables.Count > 0 && dsCliente.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtLotes = dsCliente.Tables[1];
                            DataTable dtCliente = dsCliente.Tables[0];

                            if (dtCliente.Rows.Count > 0) //Cliente existe
                            {
                                LimpiarCampos();
                                labMensaje.Text = "";
                                divMensaje.Visible = false;

                                string Nombres = dtCliente.Rows[0]["Nombres"].ToString();
                                string Apellidos = dtCliente.Rows[0]["Apellidos"].ToString();
                                long WhatsApp = Convert.ToInt64(dtCliente.Rows[0]["WhatsApp"]);
                                string NombreCompleto = Nombres + " " + Apellidos + "  -  WhatsApp: " + " " + WhatsApp;

                                txtCliente.Text = NombreCompleto;

                                if (dtLotes.Rows.Count > 0) //Cliente tiene lotes
                                {
                                    Session["dtLotes"] = dtLotes;
                                    gvLotes.DataSource = dtLotes;
                                    gvLotes.DataBind();
                                }
                                else
                                {
                                    labMensaje.Text = "Cliente no tiene lotes asignados en base de datos.";
                                    divMensaje.Visible = true;
                                    Session["dtLotes"] = null;
                                    cboProyectos.Focus();
                                }
                            }
                            else
                            {
                                labMensaje.Text = "Cliente no existe en base de datos.";
                                divMensaje.Visible = true;
                                LimpiarCampos();
                            }
                        }
                        else
                        {
                            labMensaje.Text = "Cliente no existe en base de datos.";
                            divMensaje.Visible = true;
                            LimpiarCampos();
                        }
                    }
                    else
                    {
                        labMensaje.Text = "El campo Documento es obligatorio.";
                        divMensaje.Visible = true;
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de consultar el cliente: " + ex.Message;
                divMensaje.Visible = true;
                txtDocumento.Focus();
            }
        }

        //Lotes
        private void ListarComboProyectos()
        {
            try
            {
                DataTable dtProyectos = objLotes.ListarComboProyectos();

                if (dtProyectos.Rows.Count > 0)
                {
                    cboProyectos.DataSource = dtProyectos;
                    cboProyectos.DataValueField = "IdProyecto";
                    cboProyectos.DataTextField = "Nombre";
                    cboProyectos.DataBind();
                }
                else
                {
                    //labMensajeLotes.Text = "No existen Proyectos creados en base de datos.";
                    //LimpiarCombo("Proyectos");
                }
            }
            catch (Exception ex)
            {
                //labMensajeLotes.Text = "Error tratando de listar los Proyectos: " + ex.Message;
                //LimpiarCombo("Proyectos");
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
                case "Lotes":
                    cboLotes.DataSource = dtDatos;
                    cboLotes.DataBind();
                    break;
            }
        }
        protected void cboManzanas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdManzana = Convert.ToInt32(cboManzanas.SelectedValue);
                int IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);

                if (IdProyecto > 0 && IdManzana > 0) //Listar lotes
                {
                    oLote = new ObjLotes();
                    oLote.IdManzana = IdManzana;
                    oLote.IdProyecto = IdProyecto;
                    DataTable dtLotes = objLotes.ListarComboLotes(oLote);

                    if (dtLotes.Rows.Count > 0) //Existen lotes
                    {
                        LimpiarCombo("Lotes");

                        cboLotes.DataSource = dtLotes;
                        cboLotes.DataValueField = "IdLote";
                        cboLotes.DataTextField = "Numero";
                        cboLotes.DataBind();

                        divMensaje.Visible = false;
                        labMensaje.Text = "";
                        cboLotes.Focus();
                    }
                    else
                    {
                        labMensaje.Text = "No existen Lotes creados en base de datos para la manzana seleccionada.";
                        divMensaje.Visible = true;
                        LimpiarCombo("Lotes");
                        cboManzanas.Focus();
                    }
                }
                else
                {
                    labMensaje.Text = "Debe seleccionar una Manzana.";
                    divMensaje.Visible = true;
                    LimpiarCombo("Lotes");
                    cboManzanas.Focus();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar los Lotes: " + ex.Message;
                divMensaje.Visible = true;
                LimpiarCombo("Lotes");
            }
        }
        protected void cboProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);

                if (IdProyecto > 0)
                {
                    oLote = new ObjLotes();
                    oLote.Operacion = "LCMP";
                    oLote.IdProyecto = IdProyecto;
                    DataTable dtManzanas = objLotes.ListarComboManzanas(oLote);

                    if (dtManzanas.Rows.Count > 0) //Existen manzanas
                    {
                        LimpiarCombo("Manzanas");
                        LimpiarCombo("Lotes");

                        cboManzanas.DataSource = dtManzanas;
                        cboManzanas.DataValueField = "IdManzana";
                        cboManzanas.DataTextField = "Nombre";
                        cboManzanas.DataBind();

                        divMensaje.Visible = false;
                        labMensaje.Text = "";
                        cboManzanas.Focus();
                    }
                    else
                    {
                        labMensaje.Text = "No existen Manzanas creadas en base de datos para el proyecto seleccionado.";
                        divMensaje.Visible = true;
                        LimpiarCombo("Manzanas");
                        LimpiarCombo("Lotes");
                        cboProyectos.Focus();
                    }
                }
                else
                {
                    labMensaje.Text = "Debe seleccionar un Proyecto.";
                    divMensaje.Visible = true;
                    LimpiarCombo("Manzanas");
                    LimpiarCombo("Lotes");
                    cboProyectos.Focus();
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de listar las Manzanas: " + ex.Message;
                divMensaje.Visible = true;
                LimpiarCombo("Manzanas");
                LimpiarCombo("Lotes");
            }
        }

        //Agregar
        private void AgregarLote()
        {
            try
            {
                long Documento = Convert.ToInt64(txtDocumento.Text.Trim());

                int IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);
                string Proyecto = cboProyectos.SelectedItem.Text;

                int IdManzana = Convert.ToInt32(cboManzanas.SelectedValue);
                string Manzana = cboManzanas.SelectedItem.Text;

                int IdLote = Convert.ToInt32(cboLotes.SelectedValue);
                string Lote = cboLotes.SelectedItem.Text;

                if (this.dtLotes.Rows.Count == 0)
                {
                    this.dtLotes = new DataTable();
                    this.dtLotes.Columns.Add("Documento", typeof(long));
                    this.dtLotes.Columns.Add("IdProyecto", typeof(int));
                    this.dtLotes.Columns.Add("Proyecto", typeof(string));
                    this.dtLotes.Columns.Add("IdManzana", typeof(int));
                    this.dtLotes.Columns.Add("Manzana", typeof(string));
                    this.dtLotes.Columns.Add("IdLote", typeof(int));
                    this.dtLotes.Columns.Add("Lote", typeof(string));
                    this.dtLotes.Columns.Add("Almacenado", typeof(int));

                    DataRow drFila = this.dtLotes.NewRow();
                    drFila["Documento"] = Documento;
                    drFila["IdProyecto"] = IdProyecto;
                    drFila["Proyecto"] = Proyecto;
                    drFila["IdManzana"] = IdManzana;
                    drFila["Manzana"] = Manzana;
                    drFila["IdLote"] = IdLote;
                    drFila["Lote"] = Lote;
                    drFila["Almacenado"] = 0;
                    this.dtLotes.Rows.Add(drFila);
                }
                else
                {
                    DataRow drFila = this.dtLotes.NewRow();
                    drFila["Documento"] = Documento;
                    drFila["IdProyecto"] = IdProyecto;
                    drFila["Proyecto"] = Proyecto;
                    drFila["IdManzana"] = IdManzana;
                    drFila["Manzana"] = Manzana;
                    drFila["IdLote"] = IdLote;
                    drFila["Lote"] = Lote;
                    drFila["Almacenado"] = 0;
                    this.dtLotes.Rows.Add(drFila);
                }
                this.dtLotes.AcceptChanges();

                Session["dtLotes"] = this.dtLotes;
                gvLotes.DataSource = this.dtLotes;
                gvLotes.DataBind();
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de agregar el Lote: " + ex.Message;
                divMensaje.Visible = true;
            }
        }
        private bool ValidarCamposLotes()
        {
            try
            {
                if (!ValidarCampos())
                {
                    return false;
                }
                else if (txtCliente.Text.Trim() == "")
                {
                    labMensaje.Text = "Debe consultar un Cliente.";
                    divMensaje.Visible = true;
                    txtDocumento.Text = "";
                    txtDocumento.Focus();
                    return false;
                }
                else if (cboProyectos.SelectedValue == "" || cboProyectos.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar un Proyecto.";
                    divMensaje.Visible = true;
                    cboProyectos.Focus();
                    return false;
                }
                else if (cboManzanas.SelectedValue == "" || cboManzanas.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar una Manzana.";
                    divMensaje.Visible = true;
                    cboManzanas.Focus();
                    return false;
                }
                else if (cboLotes.SelectedValue == "" || cboLotes.SelectedValue == "0")
                {
                    labMensaje.Text = "Debe seleccionar un Lote.";
                    divMensaje.Visible = true;
                    cboLotes.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar los campos: " + ex.Message;
                divMensaje.Visible = true;
                return false;
            }
        }
        public bool ValidarLoteRepetido()
        {
            try
            {
                if (Session["dtLotes"] == null)
                {
                    this.dtLotes = new DataTable();
                }
                else if (Session["dtLotes"] != null)
                {
                    if (string.IsNullOrEmpty(Session["dtLotes"].ToString()) == true)
                    {
                        this.dtLotes = (DataTable)Session["dtLotes"];
                    }
                }

                for (int i = 0; i < this.dtLotes.Rows.Count; i++)
                {
                    int IdLoteCombo = Convert.ToInt32(cboLotes.SelectedValue);
                    int IdLoteLista = Convert.ToInt32(this.dtLotes.Rows[i]["IdLote"]);

                    if (IdLoteCombo == IdLoteLista)
                    {
                        labMensaje.Text = "El Lote seleccionado ya se encuentra agregado en la lista.";
                        divMensaje.Visible = true;
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de recorrer la lista: " + ex.Message;
                divMensaje.Visible = true;
                return false;
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposLotes())
                {
                    int IdLote = Convert.ToInt32(cboLotes.SelectedValue);
                    long Documento = Convert.ToInt64(txtDocumento.Text.Trim());
                    int IdManzana = Convert.ToInt32(cboManzanas.SelectedValue);
                    int IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);

                    if (Documento > 0)
                    {
                        if (IdProyecto > 0 && IdManzana > 0 && IdLote > 0)
                        {
                            if (ValidarLoteRepetido())
                            {
                                AgregarLote();
                            }
                        }
                        else
                        {
                            labMensaje.Text = "Debe seleccionar un Proyecto, una Manzana y un Lote.";
                            divMensaje.Visible = true;
                        }
                    }
                    else
                    {
                        labMensaje.Text = "Debe consultar un Cliente.";
                        divMensaje.Visible = true;
                        txtDocumento.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de agregar el Lote: " + ex.Message;
            }
        }

        //Eliminar
        protected void btnEliminarLote_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (Session["dtLotes"] == null)
                {
                    this.dtLotes = new DataTable();
                }
                else if (Session["dtLotes"] != null)
                {
                    if (string.IsNullOrEmpty(Session["dtLotes"].ToString()) == true)
                    {
                        this.dtLotes = (DataTable)Session["dtLotes"];
                    }
                }

                if (this.dtLotes.Rows.Count > 0)
                {
                    ImageButton btnEliminar = (ImageButton)sender;
                    string strDatos = btnEliminar.CommandArgument;
                    string[] Datos = strDatos.Split(',');

                    int IdLote = Convert.ToInt32(Datos[0]);
                    int Almacenado = Convert.ToInt32(Datos[1]);

                    if (IdLote > 0)
                    {
                        if (Almacenado == 1) //Lote esta almacenado en base de datos
                        {
                            oClienteLote = new ObjClientesLotes();
                            oClienteLote.IdClienteLote = IdLote;

                            if (!objClientesLotes.Eliminar(oClienteLote)) //Eliminar asociacion lote con el cliente
                            {
                                labMensaje.Text = "No se pudo eliminar el Lote. Debe comunicarse con el administrador del sistema.";
                                divMensaje.Visible = true;
                                return;
                            }
                        }

                        foreach (DataRow drFila in this.dtLotes.Rows) //Eliminar lote de gvLotes
                        {
                            int IdLoteFila = Convert.ToInt32(drFila["IdLote"]);

                            if (IdLote == IdLoteFila)
                            {
                                drFila.Delete();
                                this.dtLotes.AcceptChanges();
                                break;
                            }
                        }
                        
                        if (this.dtLotes.Rows.Count > 0)
                        {
                            Session["dtLotes"] = this.dtLotes;
                            gvLotes.DataSource = this.dtLotes;
                            gvLotes.DataBind();
                        }
                        else
                        {
                            Session["dtLotes"] = null;
                            CrearLista();
                        }
                    }
                    else
                    {
                        labMensaje.Text = "Debe seleccionar el Lote a eliminar.";
                        divMensaje.Visible = true;
                    }
                }
                else
                {
                    labMensaje.Text = "No existen Lotes para eliminar.";
                    divMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de eliminar el lote: " + ex.Message;
                labMensaje.Visible = true;
            }
        }
    }
}