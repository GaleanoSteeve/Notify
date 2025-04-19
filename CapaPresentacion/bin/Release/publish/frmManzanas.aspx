<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmManzanas.aspx.cs" Inherits="CapaPresentacion.frmManzanas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h4 class="text-center font-weight-bold">Manzanas</h4>

    <div class="text-center mt-3">
        <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary" Text="Crear" Width="10%" OnClick="btnCrear_Click" />
        <div class="mt-2" style="height: 1px; visibility: hidden;">
            <asp:Button ID="btnAbrir" runat="server" />
        </div>
    </div>

    <div class="container-fluid mt-3" style="height: 56vh; overflow-y: scroll; padding: 0; width: 90%;">

        <div class="table-responsive">
            <asp:GridView ID="gvManzanas" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="IdManzana" HeaderText="Código" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandArgument='<%#Eval("IdManzana")%>' Text='<%#Eval("IdManzana")%>' OnClick="btnEditar_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NombreManzana" HeaderText="Nombre Manzana" HeaderStyle-Width="45%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="NombreProyecto" HeaderText="Nombre Proyecto" HeaderStyle-Width="35%" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField AccessibleHeaderText="Eliminar" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%#Eval("IdManzana")%>' ToolTip="Eliminar" ImageUrl="~/Styles/img/Eliminar.png" Width="26px" Height="26px" OnClientClick='javascript:return confirm("¿Está seguro que desea eliminar la Manzana?")' OnClick="btnEliminar_Click" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>

    <div class="container-fluid">
        <ajaxToolkit:ModalPopupExtender ID="modManzanas" runat="server" TargetControlID="btnAbrir" PopupControlID="popManzanas" BackgroundCssClass="modalBackgroundPerfiles" CancelControlID="btnCancelar">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="popManzanas" runat="server" CssClass="modalpopupPerfiles w-75" BorderColor="White" HorizontalAlign="Center" Style="height: 84vh;">

            <div class="mt-4 mr-3">
                <h5 style="color: steelblue; font-weight: 600;">Administrar Manzanas</h5>
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

                    <div class="form-group">
                        <label class="float-left" for="cboProyectos">Proyecto</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:DropDownList ID="cboProyectos" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="3"></asp:DropDownList>
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
            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" TabIndex="4" OnClick="btnGuardar_Click" />
            <button id="btnCancelar" class="btn btn-danger mr-4" tabindex="5" onclick="LimpiarControles()">Cancelar</button>

            <div style="visibility: hidden;">
                <asp:Label ID="labManzanaSeleccionada" runat="server" ClientIDMode="Static" Font-Size="2px"></asp:Label>
                <asp:Label ID="labCrear" runat="server" ClientIDMode="Static" Font-Size="2px"></asp:Label>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
