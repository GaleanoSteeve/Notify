<%@ Page Title="Perfiles" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmPerfiles.aspx.cs" Inherits="CapaPresentacion.frmPerfiles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function LimpiarControles() {
            document.getElementById('txtIdPerfil').value = "";
            document.getElementById('txtNombre').value = "";
            document.getElementById('cboEstado').value = "0";
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <h4 class="text-center font-weight-bold">Perfiles</h4>

    <div class="text-center">
        <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary mt-3" Text="Crear Perfil" OnClick="btnCrear_Click" />
        <div style="visibility: hidden">
            <asp:Button ID="btnAbrir" runat="server" />
        </div>
    </div>

    <div class="container" style="height: 65vh; overflow-y: scroll; padding: 0;">

        <div class="table-responsive">
            <asp:GridView ID="gvPerfiles" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="Código" HeaderText="Código" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandArgument='<%#Eval("IdPerfil") + "," + Eval("Nombre") + "," + Eval("Activo") %>' Text='<%# Eval("IdPerfil") %>' OnClick="btnEditar_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Creación" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Activo" HeaderText="Activo" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>

    <div class="container-fluid">

        <ajaxToolkit:ModalPopupExtender ID="modPerfiles" runat="server" TargetControlID="btnAbrir" PopupControlID="popPerfiles" BackgroundCssClass="modalBackgroundPerfiles" CancelControlID="btnCancelar">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="popPerfiles" runat="server" CssClass="modalpopupPerfiles w-75" BorderColor="White" HorizontalAlign="Center" Style="height: 92vh;">

            <div class="mt-4">
                <h5 style="color: steelblue; font-weight: 600;">Registro de Perfiles</h5>
            </div>

            <div class="row mt-3 w-100">

                <div id="divIzquierda" class="col-12 col-sm-12 col-md-4 col-lg-4 col-xl-4">

                    <div class="form-group">
                        <label class="float-left" for="txtIdPerfil">Código</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtIdPerfil" runat="server" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtNombre">Nombre</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="cboCiudades">Activo</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>

                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click"/>
                    <button id="btnCancelar" class="btn btn-danger" onclick="LimpiarControles()">Cancelar</button>

                    <div>
                        <asp:Label ID="labCrear" runat="server" Text="0" Visible="true"></asp:Label>
                        <asp:Label ID="labPerfil" runat="server" Text="Default" Visible="true"></asp:Label>
                    </div>
                </div>

                <div id="divDerecha" class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8" style="height: 72vh; overflow-y: scroll; padding: 0;">

                    <asp:GridView ID="gvModulos" runat="server" CssClass="table table-striped table-hover w-100" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="IdModulo" HeaderText="Código" HeaderStyle-Width="15%" />
                            <asp:BoundField DataField="Modulo" HeaderText="Módulo" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Padre" HeaderText="Categoría" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderText="Permiso" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAgregar" runat="server" Checked='<%# Eval("TienePermiso").Equals(0) ? false : true%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>

            <div class="container">
                <div id="labError" runat="server" class="alert alert-danger alert-dismissible">
                    <button class="close" type="button" data-dismiss="alert">
                        <span>&times;</span>
                    </button>
                    <strong>Error: </strong>
                    <asp:Label ID="labMensaje" runat="server"></asp:Label>
                </div>
            </div>
            <asp:HiddenField ID="HiddenField" runat="server" />
        </asp:Panel>
    </div>
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
</asp:Content>
