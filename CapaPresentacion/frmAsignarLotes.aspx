<%@ Page Title="Asignar Lotes" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmAsignarLotes.aspx.cs" Inherits="CapaPresentacion.frmAsignarLotes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <h4 class="text-center font-weight-bold mt-2">Asignar Lotes</h4>

    <br />
    <div class="container">
        <div class="container">
            <div class="row">
                <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control w-25" ClientIDMode="Static" MaxLength="10" PlaceHolder="Documento Cliente" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)" TabIndex="2"></asp:TextBox>
                <asp:Button ID="btnConsultar" runat="server" CssClass="btn btn-secondary ml-2" ClientIDMode="Static" Text="Consultar" Width="10%" OnClick="btnConsultar_Click" />
                <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control ml-2" ClientIDMode="Static" Enabled="false" Width="63.5%"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="container mt-3">
        <div class="container">
            <div class="row">
                <asp:DropDownList ID="cboProyectos" runat="server" CssClass="form-control w-25" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="cboProyectos_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="cboManzanas" runat="server" CssClass="form-control ml-2 w-25" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="cboManzanas_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="cboLotes" runat="server" CssClass="form-control ml-2 w-25" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success ml-2" ClientIDMode="Static" Text="Agregar" Width="10%" OnClick="btnAgregar_Click" />
            </div>
        </div>
    </div>

    <div class="container mt-3" style="height: 45vh;">
        <asp:GridView ID="gvLotes" runat="server" CssClass="table-striped table-bordered float-left w-100" AutoGenerateColumns="false" ClientIDMode="Static">
            <Columns>
                <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Manzana" HeaderText="Manzana" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="IdLote" HeaderText="IdLote" Visible="false" />
                <asp:BoundField DataField="Lote" HeaderText="Lote" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Almacenado" HeaderText="Almacenado" Visible="false" />
                <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEliminarLote" runat="server" CommandArgument='<%# Eval("IdLote") + "," + Eval("Almacenado") %>' ToolTip="Eliminar" ImageUrl="~/Styles/img/Eliminar.png" Height="26px" Width="26px" OnClick="btnEliminarLote_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
        </asp:GridView>
    </div>

    <div class="container">
        <div id="divMensaje" runat="server" class="alert alert-danger alert-dismissible">
            <asp:Label ID="labMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
