<%@ Page Title="Lotes" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmLotes.aspx.cs" Inherits="CapaPresentacion.frmLotes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h4 class="text-center font-weight-bold">Lotes</h4>

    <div class="text-center mt-3">
        <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary" Text="Crear" Width="10%" OnClick="btnCrear_Click" />
        <div class="mt-2" style="height: 1px; visibility: hidden;">
            <asp:Button ID="btnAbrir" runat="server" />
        </div>
    </div>

    <div class="container justify-content-center align-items-center col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6 w-25">
        <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" AutoPostBack="true" PlaceHolder="Digite un valor y presione la tecla Enter" ToolTip="Para quitar los filtros, borre todo y presione Enter" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
    </div>

    <div class="container-fluid mt-3" style="height: 60vh; overflow-y: scroll; padding: 0; width: 90%;">

        <div class="table-responsive">
            <asp:GridView ID="gvLotes" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="IdLote" HeaderText="Código" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandArgument='<%#Eval("IdLote")%>' Text='<%#Eval("IdLote")%>' OnClick="btnEditar_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Manzana" HeaderText="Manzana" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Numero" HeaderText="Número" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Valor" HeaderText="Valor" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="CuotaInicial" HeaderText="Cuota Inicial" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="CuotaMensual" HeaderText="Cuota Mensual" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField AccessibleHeaderText="Eliminar" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%#Eval("IdLote")%>' ToolTip="Eliminar" ImageUrl="~/Styles/img/Eliminar.png" Width="26px" Height="26px" OnClientClick='javascript:return confirm("¿Está seguro que desea eliminar el lote?")' OnClick="btnEliminar_Click" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>

    <div class="container-fluid">
        <ajaxToolkit:ModalPopupExtender ID="modLotes" runat="server" TargetControlID="btnAbrir" PopupControlID="popLotes" BackgroundCssClass="modalBackgroundPerfiles" CancelControlID="btnCancelar">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="popLotes" runat="server" CssClass="modalpopupPerfiles w-75" BorderColor="White" HorizontalAlign="Center" Style="height: 84vh; overflow-y: scroll;">

            <div class="mt-4 mr-3">
                <h5 style="color: steelblue; font-weight: 600;">Administrar Lotes</h5>
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
                        <label class="float-left" for="cboProyectos">Proyecto</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:DropDownList ID="cboProyectos" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="1"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtNumero">Número</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="4" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)" TabIndex="3"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtCuotaInicial">Cuota Inicial</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtCuotaInicial" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)" TabIndex="5"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtArea">Aréa</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="4" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)" TabIndex="7"></asp:TextBox>
                    </div>
                </div>

                <div id="divDerecha" class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">

                    <div class="form-group">
                        <label class="float-left" for="cboManzanas">Manzana</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:DropDownList ID="cboManzanas" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="2"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtValor">Valor</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtValor" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)" TabIndex="4"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtCuotaMensual">Cuota Mensual</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtCuotaMensual" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)" TabIndex="6"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="cboEstados">Estado</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:DropDownList ID="cboEstados" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="8"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div style="visibility: hidden;">
                <asp:Label ID="labCrear" runat="server" ClientIDMode="Static" Font-Size="2px"></asp:Label>
                <asp:Label ID="labCodigo" runat="server" ClientIDMode="Static" Font-Size="2px"></asp:Label>
            </div>

            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary mt-2" Text="Guardar" TabIndex="9" OnClick="btnGuardar_Click" />
            <button id="btnCancelar" class="btn btn-danger mr-4 mt-2" tabindex="10" onclick="LimpiarControles()">Cancelar</button>
        </asp:Panel>
    </div>
</asp:Content>
