using System;
using System.Data;
using CapaNegocios;

namespace CapaPresentacion
{
    public partial class frmPerfiles : System.Web.UI.Page
    {
        #region Variables

        NegPerfiles objPerfiles = new NegPerfiles();

        #endregion

        #region Cargar formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}