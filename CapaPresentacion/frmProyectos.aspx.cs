using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class frmProyectos : System.Web.UI.Page
    {
        #region Variables

        private ObjProyectos oProyecto = new ObjProyectos();
        private NegProyectos objProyectos = new NegProyectos();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            labError.Visible = false;

            if (!IsPostBack)
            {
                ListarProyectos();
            }
        }

        #endregion

        //Metodos
        private void ListarProyectos()
        {
            try
            {
                DataTable dtProyectos = objProyectos.ListarProyectos();

                if (dtProyectos.Rows.Count > 0)
                {
                    gvProyectos.DataSource = dtProyectos;
                    gvProyectos.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen Proyectos creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Lotes";
                string Mensaje = "Error tratando de listar los Proyectos: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
        private bool ListarMaximoIdProyecto()
        {
            try
            {
                DataTable dtCodigo = objProyectos.ListarMaximoIdProyecto();

                if (dtCodigo.Rows.Count > 0)
                {
                    txtCodigo.Text = dtCodigo.Rows[0]["IdProyecto"].ToString();
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

        //Controles
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (ListarMaximoIdProyecto())
            {
                labProyectoSeleccionado.Text = "";
                txtNombre.Text = "";
                labCrear.Text = "1";
                modProyectos.Show();
                txtNombre.Focus();
            }
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                labCrear.Text = "0";
                oProyecto = new ObjProyectos();
                LinkButton btnEditar = (LinkButton)sender;
                int Codigo = Convert.ToInt32(btnEditar.Text);

                if (Codigo > 0)
                {
                    this.oProyecto.IdProyecto = Codigo;
                    DataTable dtProyecto = objProyectos.ListarProyecto(this.oProyecto);

                    if (dtProyecto.Rows.Count > 0) //Proyecto existe
                    {
                        txtCodigo.Text = dtProyecto.Rows[0]["IdProyecto"].ToString();
                        txtNombre.Text = dtProyecto.Rows[0]["Nombre"].ToString();
                        labProyectoSeleccionado.Text = txtNombre.Text;
                        modProyectos.Show();
                        txtNombre.Focus();
                    }
                    else
                    {
                        txtCodigo.Text = "";
                        labProyectoSeleccionado.Text = "";

                        string Titulo = "Advertencia";
                        string Mensaje = "El Proyecto seleccionado no existe en base de datos.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
                else
                {
                    txtCodigo.Text = "";
                    labProyectoSeleccionado.Text = "";

                    string Titulo = "Advertencia";
                    string Mensaje = "El Proyecto seleccionado no tiene código asignado.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                txtCodigo.Text = "";
                labProyectoSeleccionado.Text = "";

                string Titulo = "Error Modificando Proyecto";
                string Mensaje = "Error tratando de modificar el Proyecto: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
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
                    modProyectos.Show();
                    return false;
                }
                else if (txtNombre.Text.Trim() == "")
                {
                    labMensaje.Text = "El campo Nombre es obligatorio.";
                    labError.Visible = true;
                    modProyectos.Show();
                    txtNombre.Focus();
                    return false;
                }
                else if (ExisteProyecto())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar los campos: " + ex.Message;
                labError.Visible = true;
                modProyectos.Show();
                return false;
            }
        }
        private bool ExisteProyecto()
        {
            try
            {
                bool Crear = labCrear.Text == "1" ? true : false;

                this.oProyecto = new ObjProyectos();
                this.oProyecto.Nombre = txtNombre.Text.Trim();

                if (Crear) //Crear
                {
                    DataTable dtProyecto = objProyectos.ExisteProyecto(this.oProyecto);

                    if (dtProyecto.Rows.Count > 0)
                    {
                        labMensaje.Text = "El Proyecto ingresado ya existe en base de datos.";
                        labError.Visible = true;
                        modProyectos.Show();
                        return true;
                    }
                }
                else //Editar
                {
                    string ProyectoIngresado = txtNombre.Text.Trim();
                    string ProyectoSeleccionado = labProyectoSeleccionado.Text.Trim();

                    if (ProyectoIngresado != ProyectoSeleccionado) //Hubo cambio nombre proyecto: validar que no exista un proyecto con el nuevo nombre ingresado
                    {
                        DataTable dtProyecto = objProyectos.ExisteProyecto(this.oProyecto);

                        if (dtProyecto.Rows.Count > 0)
                        {
                            labMensaje.Text = "El Proyecto ingresado ya existe en base de datos.";
                            labError.Visible = true;
                            modProyectos.Show();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                labMensaje.Text = "Error tratando de validar el Proyecto: " + ex.Message;
                labError.Visible = true;
                modProyectos.Show();
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    this.oProyecto = new ObjProyectos();
                    this.oProyecto.IdProyecto = Convert.ToInt32(txtCodigo.Text.Trim());
                    this.oProyecto.Nombre = txtNombre.Text.Trim();

                    string strMensaje = labCrear.Text == "1" ? "creado" : "actualizado";

                    if (objProyectos.Almacenar(this.oProyecto))
                    {
                        string Titulo = "Información";
                        string Mensaje = "Proyecto " + strMensaje + " correctamente.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmProyectos.aspx'});";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                    else
                    {
                        labMensaje.Text = "El Proyecto no pudo ser " + strMensaje + ". Debe contactar al administrador del sistema.";
                        labError.Visible = true;
                        modProyectos.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                string Mensaje = labCrear.Text == "1" ? "crear" : "actualizar";

                labMensaje.Text = "Error tratando de " + Mensaje + " el Proyecto: " + ex.Message;
                labError.Visible = true;
                modProyectos.Show();
            }
        }

        //Eliminar
        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btnEliminar = (ImageButton)sender;
                int IdProyecto = Convert.ToInt32(btnEliminar.CommandArgument.ToString().Trim());

                if (IdProyecto > 0)
                {
                    this.oProyecto = new ObjProyectos();
                    this.oProyecto.IdProyecto = IdProyecto;

                    DataTable dtProyectoManzanas = objProyectos.ListarProyectoManzanas(this.oProyecto);

                    if (dtProyectoManzanas.Rows.Count == 0) //Proyecto no tiene manzanas asociadas
                    {
                        if (objProyectos.Eliminar(oProyecto)) //Eliminar
                        {
                            DataTable dtLote = objProyectos.ListarProyecto(oProyecto);

                            if (dtLote.Rows.Count == 0) //Validar si el proyecto fue eliminado
                            {
                                string Titulo = "Información";
                                string Mensaje = "Proyecto eliminado correctamente.";
                                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmProyectos.aspx'});";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                            }
                            else
                            {
                                string Titulo = "Información";
                                string Mensaje = "El Proyecto no pudo ser eliminado. Debe contactar al administrador del sistema.";
                                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                            }
                        }
                        else
                        {
                            string Titulo = "Advertencia";
                            string Mensaje = "El Proyecto no pudo ser eliminado. Debe contactar al administrador del sistema.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        }
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "El Proyecto no puede ser eliminado porque tiene Manzanas asociadas.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }                    
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "El Proyecto no pudo ser eliminado. Debe contactar al administrador del sistema.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Eliminando Proyecto";
                string Mensaje = "Error tratando de eliminar el Proyecto: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }
    }
}