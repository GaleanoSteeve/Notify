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
                <asp:Button ID="btnConsultar" runat="server" CssClass="btn btn-secondary ml-2" ClientIDMode="Static" Text="Consultar" Width="10%" />
                <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control ml-2" ClientIDMode="Static" Enabled="false" Width="63.5%"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="container mt-3">
        <div class="container">
            <div class="row">
                <asp:DropDownList ID="cboProyectos" runat="server" CssClass="form-control w-25" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="cboProyectos_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="cboManzanas" runat="server" CssClass="form-control ml-2 w-25" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                <asp:DropDownList ID="cboLotes" runat="server" CssClass="form-control ml-2 w-25" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success ml-2" ClientIDMode="Static" Text="Agregar" Width="10%"/>
            </div>
        </div>
    </div>

    <br />
    <div class="container">
    </div>
</asp:Content>
