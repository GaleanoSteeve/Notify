<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmClientes.aspx.cs" Inherits="CapaPresentacion.frmClientes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <h4 class="text-center font-weight-bold">Clientes</h4>

    <div class="text-center mt-3">
        <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary" Text="Crear" Width="10%" OnClick="btnCrear_Click" />
        <div class="mt-2" style="height: 1px; visibility: hidden;">
            <asp:Button ID="btnAbrir" runat="server" />
        </div>
    </div>

    <div class="container justify-content-center align-items-center col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6 w-25">
        <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" AutoPostBack="true" onfocus="this.selectionStart = this.selectionEnd = this.value.length" PlaceHolder="Digite un valor y presione la tecla Enter" ToolTip="Para quitar los filtros, borre todo y presione Enter" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
    </div>

    <div class="container-fluid mt-3" style="height: 60vh; width: 90%; overflow-y: scroll; padding: 0;">

        <div class="table-responsive">
            <asp:GridView ID="gvClientes" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="Documento" HeaderText="Documento" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandArgument='<%#Eval("Documento")%>' Text='<%#Eval("Documento")%>' OnClick="btnEditar_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="WhatsApp" HeaderText="WhatsApp" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Municipio" HeaderText="Municipio" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Activo" HeaderText="Activo" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField AccessibleHeaderText="Eliminar" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%#Eval("Documento")%>' ToolTip="Eliminar" ImageUrl="~/Styles/img/Eliminar.png" Width="26px" Height="26px" OnClientClick='javascript:return confirm("¿Está seguro que desea eliminar el cliente?")' OnClick="btnEliminar_Click" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>

    <div class="container-fluid">

        <ajaxToolkit:ModalPopupExtender ID="modClientes" PopupControlID="pClientes" runat="server" BackgroundCssClass="modalBackgroundPerfiles" TargetControlID="btnAbrir" CancelControlID="btnCancelar">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pClientes" runat="server" CssClass="modalpopupPerfiles" BorderColor="White" HorizontalAlign="Center" Style="width: 80%; height: 92vh; overflow-y: scroll;">

            <div class="container">

                <div class="mt-4 mr-3">
                    <h5 style="color: steelblue; font-weight: 600;">Administrar Clientes</h5>
                </div>

                <div class="container">
                    <div id="labError" runat="server" class="alert alert-danger alert-dismissible">
                        <button class="close" type="button" data-dismiss="alert">
                            <span>&times;</span>
                        </button>
                        <asp:Label ID="labMensaje" runat="server"></asp:Label>
                    </div>
                </div>

                <ajaxToolkit:TabContainer ID="tabClientesLocales" runat="server">

                    <ajaxToolkit:TabPanel ID="tabClientes" runat="server" HeaderText="Clientes">

                        <ContentTemplate>

                            <div class="row ml-1 mt-2 w-100">

                                <div id="divIzquierda" class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">
                                    
                                    <div class="form-group">
                                        <label class="float-left" for="cboTipoDocumento">Tipo Documento</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:DropDownList ID="cboTipoDocumento" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="1"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label class="float-left" for="txtNombres">Nombres</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="100" TabIndex="3"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label class="float-left" for="cboDepartamentos">Departamento</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:DropDownList ID="cboDepartamentos" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" TabIndex="6" OnSelectedIndexChanged="cboDepartamentos_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label class="float-left" for="txtWhatsApp">WhatsApp</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:TextBox ID="txtWhatsApp" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" autocomplete="off" onkeypress="return Numeros(event)" TabIndex="12"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label class="float-left" for="txtEmail">Email</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="100" TabIndex="15"></asp:TextBox>
                                    </div>
                                </div>

                                <div id="divDerecha" class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">                                    

                                    <div class="form-group">
                                        <label class="float-left" for="txtDocumento">Documento</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)" TabIndex="2"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label class="float-left" for="txtApellidos">Apellidos</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="100" TabIndex="4"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label class="float-left" for="cboMunicipios">Municipio</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:DropDownList ID="cboMunicipios" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="7"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label class="float-left" for="txtTelefono">Teléfono</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" autocomplete="off" onkeypress="return Numeros(event)" TabIndex="13"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label class="float-left" for="cboEstado">Activo</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                                        <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="16"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel ID="tabLotes" runat="server" HeaderText="Lotes">
                        <ContentTemplate>
                            <div class="mt-4 mr-3">
                                <h5 style="color: black; font-size: 18px; font-weight: 600;">Lotes</h5>
                            </div>

                            <asp:Label ID="labMensajeLotes" runat="server" ForeColor="Red" Visible="true" Text="Mensaje"></asp:Label>

                            <div class="table-responsive">
                                <asp:GridView ID="gvLotes" runat="server" CssClass="table-striped table-bordered float-left mt-3 w-100" AutoGenerateColumns="false" ClientIDMode="Static">
                                    <Columns>
                                        <asp:BoundField DataField="IdProyecto" Visible="false" />
                                        <asp:BoundField DataField="NombreProyecto" HeaderText="Nombre Proyecto" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="IdManzana" Visible="false" />
                                        <asp:BoundField DataField="NombreManzana" HeaderText="Nombre Manzana" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="IdLote" Visible="false" />
                                        <asp:BoundField DataField="NumeroLote" HeaderText="Número Lote" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Area" HeaderText="Área (m²)" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Valor" HeaderText="Valor" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                                    </Columns>
                                    <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>

                <div style="visibility: hidden;">
                    <asp:Label ID="labCrear" runat="server" ClientIDMode="Static" Font-Size="2px"></asp:Label>
                    <asp:Label ID="labDocumento" runat="server" ClientIDMode="Static" Font-Size="2px"></asp:Label>
                </div>

                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary mb-3" Text="Guardar" TabIndex="17" OnClick="btnGuardar_Click" />
                <button id="btnCancelar" class="btn btn-danger mb-3 mr-3" tabindex="18" onclick="LimpiarControles()">Cancelar</button>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
