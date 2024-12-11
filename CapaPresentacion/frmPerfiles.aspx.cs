using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;
using System.Transactions;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class frmPerfiles : System.Web.UI.Page
    {
        #region Variables

        ObjModulos oModulo = new ObjModulos();
        ObjPerfiles oPerfil = new ObjPerfiles();

        NegPerfiles objPerfiles = new NegPerfiles();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            labError.Visible = false;

            if (!IsPostBack)
            {
                ListarComboEstados();
                ListarPerfiles();
            }
        }

        #endregion

        //Metodos
        private void ListarPerfiles()
        {
            try
            {
                DataTable dtPerfiles = objPerfiles.ListarPerfiles();

                if (dtPerfiles.Rows.Count > 0)
                {
                    gvPerfiles.DataSource = dtPerfiles;
                    gvPerfiles.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen perfiles creados en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    HiddenField.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                HiddenField.Value = string.Empty;
                string Titulo = "Error Cargando Perfiles";
                string Mensaje = "Error tratando de listar los perfiles: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmPerfiles.aspx'});";
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
                txtIdPerfil.Text = "";
                labError.Visible = true;
                labMensaje.Text = "Error tratando de listar los estados: " + ex.Message;
                modPerfiles.Show();
            }
        }
        private void ListarMaximoPerfil()
        {
            try
            {
                DataTable dtDatos = objPerfiles.ListarMaximoPerfil();

                if (dtDatos.Rows.Count > 0)
                {
                    int Perfil = Convert.ToInt16(dtDatos.Rows[0]["IdPerfil"]);
                    txtIdPerfil.Text = Perfil.ToString();
                }
                else
                {
                    txtIdPerfil.Text = "";
                    labError.Visible = true;
                    labMensaje.Text = "No se pudo listar el máximo código de perfil. Por favor intente de nuevo.";
                    modPerfiles.Show();
                }
            }
            catch (Exception ex)
            {
                txtIdPerfil.Text = "";
                labError.Visible = true;
                labMensaje.Text = "Error tratando de listar el código del perfil: " + ex.Message;
                modPerfiles.Show();
            }
        }
        private void ListarModulosPerfil(int IdPerfil)
        {
            try
            {
                DataTable dtModulos = objPerfiles.ListarModulosPerfil(IdPerfil);

                if (dtModulos.Rows.Count > 0)
                {
                    Session["dtModulos"] = dtModulos;
                    gvModulos.DataSource = dtModulos;
                    gvModulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                txtIdPerfil.Text = "";
                labError.Visible = true;
                labMensaje.Text = "Error tratando de listar los módulos: " + ex.Message;
                modPerfiles.Show();
            }
        }

        //Administrar
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            labCrear.Text = "1";
            labPerfil.Text = "";
            cboEstado.SelectedValue = "1";
            ListarModulosPerfil(0);
            ListarMaximoPerfil();
            modPerfiles.Show();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                labCrear.Text = "0";
                LinkButton btnEditar = (LinkButton)sender;
                string strDatos = btnEditar.CommandArgument;
                string[] Datos = strDatos.Split(',');

                int IdPerfil = Convert.ToInt32(Datos[0]);

                if (IdPerfil > 0)
                {
                    DataTable dtPerfil = objPerfiles.ListarPerfil(IdPerfil);

                    if (dtPerfil.Rows.Count > 0) //Perfil existe
                    {
                        labPerfil.Text = dtPerfil.Rows[0]["Nombre"].ToString();
                        txtIdPerfil.Text = dtPerfil.Rows[0]["IdPerfil"].ToString();
                        txtNombre.Text = dtPerfil.Rows[0]["Nombre"].ToString();
                        cboEstado.SelectedValue = dtPerfil.Rows[0]["Activo"].ToString() == "SI" ? "1" : "0";

                        DataTable dtModulos = objPerfiles.ListarModulosPerfil(IdPerfil);

                        if (dtModulos.Rows.Count > 0)
                        {
                            DataView dvPermisos = new DataView(dtModulos);
                            dvPermisos.RowFilter = "TienePermiso > 0";
                            DataTable dtPermisos = dvPermisos.ToTable();

                            for (int i = 0; i < dtPermisos.Rows.Count; i++)
                            {
                                if (HiddenField.Value == "") //Agregar primer elemento
                                {
                                    HiddenField.Value += dtPermisos.Rows[i]["IdModulo"].ToString();
                                }
                                else
                                {
                                    HiddenField.Value += "," + dtPermisos.Rows[i]["IdModulo"].ToString();
                                }
                            }

                            gvModulos.DataSource = dtModulos;
                            gvModulos.DataBind();
                            modPerfiles.Show();
                        }
                        else
                        {
                            string Titulo = "Advertencia";
                            string Mensaje = "El perfil seleccionado no tiene permisos configurados.";
                            string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                            HiddenField.Value = string.Empty;
                        }
                    }
                    else
                    {
                        string Titulo = "Advertencia";
                        string Mensaje = "El perfil seleccionado no existe en base de datos.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                        HiddenField.Value = string.Empty;
                    }
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "El perfil seleccionado no tiene código asignado.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    HiddenField.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                HiddenField.Value = string.Empty;
                string Titulo = "Error Editando Perfil";
                string Mensaje = "Error tratando de editar el perfil: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmPerfiles.aspx'});";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }

        //Guardar
        private bool ValidarCampos()
        {
            try
            {
                if (txtIdPerfil.Text.Trim() == "")
                {
                    txtIdPerfil.Focus();
                    labError.Visible = true;
                    labMensaje.Text = "El campo Código es obligatorio.";
                    modPerfiles.Show();
                    return false;
                }
                else if (txtNombre.Text.Trim() == "")
                {
                    txtNombre.Focus();
                    labError.Visible = true;
                    labMensaje.Text = "El campo Nombre es obligatorio.";
                    modPerfiles.Show();
                    return false;
                }
                else if (ValidarNombrePerfil())
                {
                    txtNombre.Focus();
                    labError.Visible = true;
                    labMensaje.Text = "El Nombre del perfil ingresado ya existe en base de datos.";
                    modPerfiles.Show();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                labError.Visible = true;
                labMensaje.Text = "Error tratando de validar los campos: " + ex.Message;
                modPerfiles.Show();
                return false;
            }
        }
        private bool ValidarNombrePerfil()
        {
            try
            {
                bool Crear = labCrear.Text == "1" ? true : false;

                if (Crear) //Crear
                {
                    DataTable dtPerfil = objPerfiles.ListarPerfilPorNombre(txtNombre.Text.Trim());

                    if (dtPerfil.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else //Actualizar
                {
                    string PerfilIngresado = txtNombre.Text.Trim();
                    string PerfilSeleccionado = labPerfil.Text.Trim();

                    if (PerfilIngresado != PerfilSeleccionado) //Hubo cambio de perfil
                    {
                        DataTable dtPerfil = objPerfiles.ListarPerfilPorNombre(txtNombre.Text.Trim());

                        if (dtPerfil.Rows.Count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else //No hubo cambio de perfil
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                labError.Visible = true;
                labMensaje.Text = "Error tratando de validar el nombre del perfil: " + ex.Message;
                modPerfiles.Show();
                return false;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    oPerfil = new ObjPerfiles();
                    oPerfil.IdPerfil = Convert.ToInt32(txtIdPerfil.Text);
                    oPerfil.Nombre = txtNombre.Text.Trim();
                    oPerfil.Estado = cboEstado.SelectedValue == "1" ? true : false;
                    oPerfil.UsuarioCreacion = Session["Usuario"].ToString();
                    oPerfil.EquipoCreacion = System.Environment.MachineName;
                    oPerfil.UsuarioModificacion = Session["Usuario"].ToString();
                    oPerfil.EquipoModificacion = System.Environment.MachineName;

                    using (TransactionScope tsTransaction = new TransactionScope())
                    {
                        if (objPerfiles.Guardar(oPerfil)) //Guardar perfil
                        {
                            if (objPerfiles.EliminarModulosPerfil(oPerfil)) //Eliminar modulos perfil
                            {
                                foreach (GridViewRow gvRow in gvModulos.Rows)
                                {
                                    if (gvRow.RowType == DataControlRowType.DataRow)
                                    {
                                        int IdModulo = Convert.ToInt32(gvRow.Cells[0].Text);
                                        CheckBox chkAplica = (gvRow.Cells[3].FindControl("chkAgregar") as CheckBox);

                                        oModulo = new ObjModulos();
                                        oModulo.IdModulo = IdModulo;
                                        oModulo.TienePermiso = false;
                                        oModulo.IdPerfil = oPerfil.IdPerfil;

                                        if (chkAplica.Checked)
                                        {
                                            oModulo.TienePermiso = true;
                                        }
                                    }
                                    objPerfiles.GuardarModulosPerfil(oModulo);
                                }
                            }
                            else
                            {
                                tsTransaction.Dispose();
                            }
                        }
                        else
                        {
                            tsTransaction.Dispose();
                        }
                        tsTransaction.Complete();
                    }

                    HiddenField.Value = string.Empty;
                    string strMensaje = labCrear.Text == "1" ? "creado" : "actualizado";

                    string Titulo = "Información";
                    string Mensaje = "Perfil " + strMensaje + " correctamente.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmPerfiles.aspx'});";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Mensaje = labCrear.Text == "1" ? "crear" : "modificar";

                labPerfil.Text = "";
                txtIdPerfil.Text = "";
                HiddenField.Value = string.Empty;

                labError.Visible = true;
                labMensaje.Text = "Error tratando de " + Mensaje + " el perfil: " + ex.Message;
                modPerfiles.Show();
            }
        }
    }
}