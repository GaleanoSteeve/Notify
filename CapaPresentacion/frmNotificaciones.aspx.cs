using System;
using System.Data;
using CapaObjetos;
using CapaNegocios;
using System.Web.UI;
using System.Transactions;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class frmNotificaciones : System.Web.UI.Page
    {
        #region Variables

        private ObjNotificaciones oNotificaciones = new ObjNotificaciones();
        private NegNotificaciones objNotificaciones = new NegNotificaciones();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarRegistros();
            }
        }

        #endregion

        //Metodos
        private void ListarRegistros()
        {
            try
            {
                DataTable dtRegistros = objNotificaciones.ListarRegistros();

                if (dtRegistros.Rows.Count > 0)
                {
                    gvRegistros.DataSource = dtRegistros;
                    gvRegistros.DataBind();
                }
                else
                {
                    string Titulo = "Advertencia";
                    string Mensaje = "No existen Registros en base de datos.";
                    string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                }
            }
            catch (Exception ex)
            {
                string Titulo = "Error Cargando Registros";
                string Mensaje = "Error tratando de listar los Registros: " + ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
            }
        }

        //Enviar
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                bool Resultado = false;

                using (TransactionScope tsTransaction = new TransactionScope())
                {
                    foreach (GridViewRow gvRow in gvRegistros.Rows)
                    {
                        if (gvRow.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkNotificar = (gvRow.Cells[10].FindControl("chkNotificar") as CheckBox);

                            if (chkNotificar.Checked)
                            {
                                Resultado = true;
                                int IdClienteLote = Convert.ToInt32(gvRow.Cells[0].Text);
                                long Documento = Convert.ToInt64(gvRow.Cells[1].Text);
                                string Cliente = gvRow.Cells[2].Text.Trim();
                                DateTime FechaPagoCuota = Convert.ToDateTime(gvRow.Cells[9].Text.Trim());
                                string strFechaPago = FechaPagoCuota.ToString("dd 'de' MMMM 'del' yyyy");
                                string MedioNotificacion = "WEB";
                                string ValorCuota = gvRow.Cells[8].Text;
                                string strMensaje = "Estimado(a) " + Cliente + " le recordamos que la próxima cuota correspondiente a su Lote de terreno está programada para el " + strFechaPago + ", por un valor de $ " + ValorCuota;

                                oNotificaciones = new ObjNotificaciones();
                                oNotificaciones.IdClienteLote = IdClienteLote;
                                oNotificaciones.Documento = Documento;
                                oNotificaciones.Cliente = Cliente;
                                oNotificaciones.FechaPagoCuota = FechaPagoCuota;
                                oNotificaciones.MedioNotificacion = MedioNotificacion;
                                oNotificaciones.Mensaje = strMensaje;
                                oNotificaciones.UsuarioCreacion = Session["Usuario"].ToString();

                                if (!objNotificaciones.Enviar(oNotificaciones)) //Enviar notificaciones
                                {
                                    tsTransaction.Dispose();

                                }
                            }
                        }
                    }
                    tsTransaction.Complete();

                    if (Resultado)
                    {
                        string Titulo = "Información";
                        string Mensaje = "Notificaciones enviadas correctamente.";
                        string Tipo = "alertify.alert('" + Titulo + "', '" + Mensaje + "',function(){location.href='frmNotificaciones.aspx'});";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptId", Tipo, true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Controles
        protected void gvRegistros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header) //Ocultar la cabecera
            {
                e.Row.Cells[0].Attributes.Add("style", "display:none");
            }

            if (e.Row.RowType == DataControlRowType.DataRow) //Ocultar la celda 0
            {
                e.Row.Cells[0].Attributes.Add("style", "display:none");
            }
        }
    }
}