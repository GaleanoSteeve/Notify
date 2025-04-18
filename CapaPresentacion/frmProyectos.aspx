<%@ Page Title="Proyectos" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmProyectos.aspx.cs" Inherits="CapaPresentacion.frmProyectos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h4 class="text-center font-weight-bold">Proyectos</h4>

    <div class="text-center mt-3">
        <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary" Text="Crear" Width="10%" OnClick="btnCrear_Click" />
        <div class="mt-2" style="height: 1px; visibility: hidden;">
            <asp:Button ID="btnAbrir" runat="server" />
        </div>
    </div>

    <div class="container-fluid mt-3" style="height: 56vh; overflow-y: scroll; padding: 0; width: 90%;">

        <div class="table-responsive">
            <asp:GridView ID="gvProyectos" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="IdProyecto" HeaderText="Código" HeaderStyle-Width="30%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandArgument='<%#Eval("IdProyecto")%>' Text='<%#Eval("IdProyecto")%>' OnClick="btnEditar_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField AccessibleHeaderText="Eliminar" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%#Eval("IdProyecto")%>' ToolTip="Eliminar" ImageUrl="~/Styles/img/Eliminar.png" Width="26px" Height="26px" OnClientClick='javascript:return confirm("¿Está seguro que desea eliminar el Proyecto?")' OnClick="btnEliminar_Click"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>

    <div class="container-fluid">
        <ajaxToolkit:ModalPopupExtender ID="modProyectos" runat="server" TargetControlID="btnAbrir" PopupControlID="popProyectos" BackgroundCssClass="modalBackgroundPerfiles" CancelControlID="btnCancelar">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="popProyectos" runat="server" CssClass="modalpopupPerfiles w-75" BorderColor="White" HorizontalAlign="Center" Style="height: 84vh;">

            <div class="mt-4 mr-3">
                <h5 style="color: steelblue; font-weight: 600;">Administrar Proyectos</h5>
            </div>

            <div class="container">
                <div id="labError" runat="server" class="alert alert-danger alert-dismissible">
                    <button class="close" type="button" data-dismiss="alert">
                        <span>&times;</span>
                    </button>
                    <asp:Label ID="labMensaje" runat="server"></asp:Label>
                </div>
            </div>

            <br />
            <div class="row mt-3 w-100">

                <div id="divIzquierda" class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">

                    <div class="form-group">
                        <label class="float-left" for="txtNumero">Código</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" ClientIDMode="Static" Enabled="false" TabIndex="1"></asp:TextBox>
                    </div>
                </div>

                <div id="divDerecha" class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">

                    <div class="form-group">
                        <label class="float-left" for="txtValor">Nombre</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="100" TabIndex="2"></asp:TextBox>
                    </div>
                </div>
            </div>

            <br />
            <br />
            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" TabIndex="3" OnClick="btnGuardar_Click"/>
            <button id="btnCancelar" class="btn btn-danger mr-4" tabindex="4" onclick="LimpiarControles()">Cancelar</button>

            <div style="visibility: hidden;">
                <asp:Label ID="labProyectoSeleccionado" runat="server" ClientIDMode="Static" Font-Size="2px"></asp:Label>
                <asp:Label ID="labCrear" runat="server" ClientIDMode="Static" Font-Size="2px"></asp:Label>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
