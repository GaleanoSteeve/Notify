<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmUsuarios.aspx.cs" Inherits="CapaPresentacion.frmUsuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function LimpiarControles() {
            document.getElementById('').value = "";
            document.getElementById('').value = "";
            document.getElementById('').value = "0";
            document.getElementById('').value = "";
            document.getElementById('').value = "";
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <h4 class="text-center font-weight-bold">Usuarios</h4>

    <div class="text-center">
        <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary mt-3" Text="Crear Usuario" OnClick="btnCrear_Click" />
        <div style="visibility: hidden">
            <asp:Button ID="btnAbrir" runat="server" />
        </div>
    </div>

    <div class="container" style="height: 65vh; overflow-y: scroll; padding: 0;">

        <div class="table-responsive">
            <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="Código" HeaderText="Código" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandArgument='<%#Eval("Codigo") + "," + Eval("Nombres") + "," + Eval("Activo") %>' Text='<%# Eval("Codigo") %>' OnClick="btnEditar_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" HeaderStyle-Width="35%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Perfil" HeaderText="Perfil" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="PuedeEliminar" HeaderText="Puede Eliminar" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Activo" HeaderText="Activo" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Creación" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-Width="15%" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>

    <div class="container-fluid">

        <ajaxToolkit:ModalPopupExtender ID="modUsuarios" runat="server" TargetControlID="btnAbrir" PopupControlID="popUsuarios" BackgroundCssClass="modalBackgroundPerfiles" CancelControlID="btnCancelar">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="popUsuarios" runat="server" CssClass="modalpopupPerfiles w-75" BorderColor="White" HorizontalAlign="Center" Style="height: 92vh; overflow-y: scroll;">

            <div class="mt-4">
                <h5 style="color: steelblue; font-weight: 600;">Registro de Usuarios</h5>
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

            <div class="row mt-3 w-100">

                <div id="divIzquierda" class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">

                    <div class="form-group">
                        <label class="float-left" for="txtCodigo">Código</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtNombres">Nombres</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtUsuario">Usuario</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtContrasena">Contraseña</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="txtConfirmacionContrasena">Confirmación Contraseña</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:TextBox ID="txtConfirmacionContrasena" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div id="divDerecha" class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6" style="height: 72vh;">

                    <div class="form-group">
                        <label class="float-left" for="cboPerfiles">Perfil</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:DropDownList ID="cboPerfiles" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="cboPuedeEliminar">Puede Eliminar</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:DropDownList ID="cboPuedeEliminar" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label class="float-left" for="cboEstado">Activo</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                        <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>

                    <div>
                        <asp:Label ID="labCrear" runat="server" Text="0" ClientIDMode="Static" Visible="true"></asp:Label>
                        <asp:Label ID="labUsuario" runat="server" Text="user" ClientIDMode="Static" Visible="true"></asp:Label>
                    </div>
                </div>
            </div>

            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click" />
            <button id="btnCancelar" class="btn btn-danger" onclick="LimpiarControles()">Cancelar</button>
        </asp:Panel>
    </div>
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
</asp:Content>
