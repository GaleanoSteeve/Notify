using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;

namespace CapaPresentacion
{
    public partial class frmAsignarLotes : System.Web.UI.Page
    {
        #region Variables

        private ObjLotes oLote = new ObjLotes();
        private NegLotes objLotes = new NegLotes();
        private NegClientesLotes objClientesLotes = new NegClientesLotes();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDocumento.Focus();

            if (!IsPostBack)
            {
                ListarComboProyectos();
            }
        }

        #endregion

        //Controles
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
                        //LimpiarCombo("Manzanas");
                        //LimpiarCombo("Lotes");

                        cboManzanas.DataSource = dtManzanas;
                        cboManzanas.DataValueField = "IdManzana";
                        cboManzanas.DataTextField = "Nombre";
                        cboManzanas.DataBind();
                        cboManzanas.Focus();
                    }
                    else
                    {
                        //labMensajeLotes.Text = "No existen Manzanas creadas en base de datos para el proyecto seleccionado.";
                        //LimpiarCombo("Manzanas");
                        //LimpiarCombo("Lotes");
                    }
                }
                else
                {
                    //labMensajeLotes.Text = "Debe seleccionar un Proyecto.";
                    //LimpiarCombo("Manzanas");
                    //LimpiarCombo("Lotes");
                }
            }
            catch (Exception ex)
            {
                //labMensajeLotes.Text = "Error tratando de listar las Manzanas: " + ex.Message;
                //LimpiarCombo("Manzanas");
                //LimpiarCombo("Lotes");
            }
        }

        /*
        public bool ValidarLoteRepetido()
        {
            try
            {
                if (Session["dtLotes"] != null) //Validar que la variable no sea null
                {
                    if (string.IsNullOrEmpty(Session["dtLotes"].ToString()) == true)
                    {
                        this.dtLotes = (DataTable)Session["dtLotes"];
                    }
                }
                else
                {
                    this.dtLotes = new DataTable();
                }

                for (int i = 0; i < this.dtLotes.Rows.Count; i++)
                {
                    int IdLote = Convert.ToInt32(cboLotes.SelectedValue);
                    int Codigo = Convert.ToInt32(this.dtLotes.Rows[i]["IdLote"]);

                    if (Codigo == IdLote) //Lote esta agregado
                    {
                        labMensajeLotes.Text = "El Lote seleccionado ya se encuentra agregado en la lista.";
                        labMensajeLotes.Visible = true;
                        modClientes.Show();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                labMensajeLotes.Text = "Error tratando de validar el Lote en GridView: " + ex.Message;
                labMensajeLotes.Visible = true;
                modClientes.Show();
                return false;
            }
        }

        protected void btnAgregarLote_Click(object sender, EventArgs e)
        {
            try
            {
                int IdLote = Convert.ToInt32(cboLotes.SelectedValue);
                int IdManzana = Convert.ToInt32(cboManzanas.SelectedValue);
                int IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);
                long Documento = Convert.ToInt64(txtDocumento.Text.Trim());

                if (Documento > 0)
                {
                    if (IdProyecto > 0 && IdManzana > 0 && IdLote > 0)
                    {
                        bool Crear = labCrear.Text == "1" ? true : false;

                        if (!Crear) //Recorrer dtLotes para no agregar el mismo lote dos veces o mas
                        {
                            if (ValidarLoteRepetido())
                            {
                                AgregarLote();
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        labMensajeLotes.Text = "Debe seleccionar un Proyecto, una Manzana y un Lote.";
                        labMensajeLotes.Visible = true;
                        modClientes.Show();
                    }
                }
                else
                {
                    labMensaje.Text = "El campo Documento es obligatorio.";
                    labMensajeLotes.Visible = true;
                    modClientes.Show();
                }
            }
            catch (Exception ex)
            {
                labMensajeLotes.Text = "Error tratando de agregar Lote: " + ex.Message;
                labMensajeLotes.Visible = true;
                modClientes.Show();
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

                    if (dtLotes.Rows.Count > 0)
                    {
                        labMensajeLotes.Visible = false;
                        labMensajeLotes.Text = "";
                        LimpiarCombo("Lotes");

                        cboLotes.DataSource = dtLotes;
                        cboLotes.DataValueField = "IdLote";
                        cboLotes.DataTextField = "Numero";
                        cboLotes.DataBind();
                        modClientes.Show();
                    }
                    else
                    {
                        labMensajeLotes.Text = "No existen Lotes creados en base de datos para la manzana seleccionada.";
                        labMensajeLotes.Visible = true;
                        LimpiarCombo("Lotes");
                        modClientes.Show();
                    }
                }
                else
                {
                    labMensajeLotes.Text = "Debe seleccionar una Manzana.";
                    labMensajeLotes.Visible = true;
                    LimpiarCombo("Lotes");
                    modClientes.Show();
                }
            }
            catch (Exception ex)
            {
                labMensajeLotes.Text = "Error tratando de listar los Lotes: " + ex.Message;
                labMensajeLotes.Visible = true;
                LimpiarCombo("Lotes");
                modClientes.Show();
            }
        }*/

        //private void AgregarLote()
        //{
        //    try
        //    {
        //        string Lote = cboLotes.SelectedItem.Text;
        //        int IdLote = Convert.ToInt32(cboLotes.SelectedValue);
        //        int IdManzana = Convert.ToInt32(cboManzanas.SelectedValue);
        //        long Documento = Convert.ToInt64(txtDocumento.Text.Trim());
        //        int IdProyecto = Convert.ToInt32(cboProyectos.SelectedValue);

        //        if (this.dtLotes.Rows.Count == 0)
        //        {
        //            this.dtLotes = new DataTable();
        //            this.dtLotes.Columns.Add("Documento", typeof(long));
        //            this.dtLotes.Columns.Add("IdProyecto", typeof(int));
        //            this.dtLotes.Columns.Add("IdManzana", typeof(int));
        //            this.dtLotes.Columns.Add("IdLote", typeof(int));
        //            this.dtLotes.Columns.Add("Lote", typeof(string));

        //            DataRow drFila = this.dtLotes.NewRow();
        //            drFila["Documento"] = Documento;
        //            drFila["IdProyecto"] = IdProyecto;
        //            drFila["IdManzana"] = IdManzana;
        //            drFila["IdLote"] = IdLote;
        //            drFila["Lote"] = Lote;
        //            this.dtLotes.Rows.Add(drFila);
        //        }
        //        else
        //        {
        //            DataRow drFila = this.dtLotes.NewRow();
        //            drFila["Documento"] = Documento;
        //            drFila["IdProyecto"] = IdProyecto;
        //            drFila["IdManzana"] = IdManzana;
        //            drFila["IdLote"] = IdLote;
        //            drFila["Lote"] = Lote;
        //            this.dtLotes.Rows.Add(drFila);
        //        }
        //        this.dtLotes.AcceptChanges();

        //        Session["dtLotes"] = this.dtLotes;
        //        gvLotes.DataSource = this.dtLotes;
        //        gvLotes.DataBind();
        //        modClientes.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        labMensajeLotes.Text = "Error tratando de agregar Lote: " + ex.Message;
        //        labMensajeLotes.Visible = true;
        //        modClientes.Show();
        //    }
        //}
    }
}