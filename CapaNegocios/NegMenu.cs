using CapaDatos;
using System;
using System.Data;

namespace CapaNegocios
{
    public class NegMenu
    {
        #region Variables

        private string Menu = "";
        private char Comillas = '\u0022';
        private DatMenu objMenu = new DatMenu();
        private DataTable dtModulos = new DataTable();

        #endregion

        public string CrearMenu(int IdPerfil, string Usuario)
        {
            try
            {
                this.Menu = "<nav class=" + this.Comillas + "navbar navbar-expand-lg navbar-light bg-light" + this.Comillas + ">";
                this.Menu += "<a class=" + this.Comillas + "navbar-brand" + this.Comillas + " href=" + this.Comillas + "frmInicio.aspx" + this.Comillas + ">Notify</a>";
                this.Menu += "<button class=" + this.Comillas + "navbar-toggler" + this.Comillas + " type=" + this.Comillas + "button" + this.Comillas + " data-toggle=" + this.Comillas + "collapse" + this.Comillas + " data-target=" + this.Comillas + "#navbarSupportedContent" + this.Comillas + " aria-controls=" + this.Comillas + "navbarSupportedContent" + this.Comillas + " aria-expanded=" + this.Comillas + "false" + this.Comillas + " aria-label=" + this.Comillas + "Toggle navigation" + this.Comillas + ">";
                this.Menu += "<span class=" + this.Comillas + "navbar-toggler-icon" + this.Comillas + "></span></button>";
                this.Menu += "<div class=" + this.Comillas + "collapse navbar-collapse" + this.Comillas + " id=" + this.Comillas + "navbarSupportedContent" + this.Comillas + ">";
                this.Menu += "<ul class=" + this.Comillas + "navbar-nav mr-auto" + this.Comillas + ">";

                this.dtModulos = objMenu.ListarPermisos(IdPerfil); //Consultar modulos perfil

                for (int i = 0; i < dtModulos.Rows.Count; i++) //Recorrer DataTable para agregar los modulos padre al menu
                {
                    string Modulo = dtModulos.Rows[i]["Modulo"].ToString();
                    int IdPadre = Convert.ToInt16(dtModulos.Rows[i]["IdPadre"]);
                    int IdModulo = Convert.ToInt32(this.dtModulos.Rows[i]["IdModulo"]);
                    bool TienePermiso = Convert.ToBoolean(dtModulos.Rows[i]["TienePermiso"]);

                    if (TienePermiso && IdPadre == 0) //Modulo es padre
                    {
                        this.Menu += "<li class =" + this.Comillas + "nav-item dropdown" + this.Comillas + ">";
                        this.Menu += "<a class=" + this.Comillas + "nav-link dropdown-toggle" + this.Comillas + " href=" + this.Comillas + "#" + this.Comillas + " id=" + this.Comillas + "navbarDropdown" + this.Comillas + " role=" + this.Comillas + "button" + this.Comillas + " data-toggle=" + this.Comillas + "dropdown" + this.Comillas + " aria-haspopup=" + this.Comillas + "true" + this.Comillas + " aria-expanded=" + this.Comillas + "false" + this.Comillas + ">" + Modulo + "</a>";

                        this.Menu += this.PintarHijo(IdModulo, this.dtModulos); //Pintar modulos hijos
                    }
                }

                this.Menu += "</li>";
                this.Menu += "<li class=" + this.Comillas + "nav-item" + this.Comillas + "><a class=" + this.Comillas + "nav-link" + this.Comillas + " href=" + this.Comillas + "frmLogin.aspx?CerrarSesion=-1" + this.Comillas + ">Cerrar sesion</a></li>";
                this.Menu += "&nbsp;&nbsp;&nbsp;&nbsp;<li class=" + this.Comillas + "nav-item active" + this.Comillas + "><a class=" + this.Comillas + "nav-link" + this.Comillas + "href=" + this.Comillas + "frmAbout.aspx" + this.Comillas + ">Acerca de</a></li>";
                this.Menu += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                             "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                             "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                             "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span>" + "Usuario: " + Usuario + "</span></p>";
                this.Menu += "</ul></div></nav>";
                return this.Menu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string PintarHijo(int IdModulo, DataTable dtModulos)
        {
            try
            {
                int Contador = 0;
                string Hijos = "<div class =" + this.Comillas + "dropdown-menu" + this.Comillas + " aria-labelledby=" + this.Comillas + "navbarDropdown" + this.Comillas + ">"; //Recorrer modulos a los que tiene acceso el usuario

                for (int i = 0; i < dtModulos.Rows.Count; i++)
                {
                    string Modulo = dtModulos.Rows[i]["Modulo"].ToString();
                    int IdPadre = Convert.ToInt32(dtModulos.Rows[i]["IdPadre"]);
                    string Formulario = dtModulos.Rows[i]["Formulario"].ToString();
                    bool TienePermiso = Convert.ToBoolean(dtModulos.Rows[i]["TienePermiso"]);

                    if (TienePermiso && (IdModulo == IdPadre)) //Modulo padre
                    {
                        Contador++;
                        Hijos += "<a class=" + this.Comillas + "dropdown-item" + this.Comillas + " href=" + this.Comillas + Formulario + this.Comillas + ">" + Modulo + "</a>";
                    }
                }

                if (Contador > 0)
                {
                    Hijos += "</div></li>";
                }
                else
                {
                    Hijos += "</div>";
                }
                return Hijos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
