<%@ Page Title="Configuración" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmConfiguracion.aspx.cs" Inherits="CapaPresentacion.frmConfiguracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <h4 class="text-center font-weight-bold">Configuración</h4>

    <br />
    <div class="container mt-3">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">

                <div class="form-group">
                    <label class="float-left" for="txtNit">Nit</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:TextBox ID="txtNit" runat="server" CssClass="form-control" MaxLength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label class="float-left" for="txtRazonSocial">Razón Social</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label class="float-left" for="txtNombreComercial">Nombre Comercial</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:TextBox ID="txtNombreComercial" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label class="float-left" for="txtDireccion">Dirección</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">

                <div class="form-group">
                    <label class="float-left" for="txtTelefono">Teléfono</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)"></asp:TextBox>
                </div>

                <label class="float-left" for="txtEmail">Email</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                <div class="input-group">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">@</span>
                    </div>
                </div>

                <div class="form-group mt-3">
                    <label class="float-left" for="cboDepartamentos">Departamento</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:DropDownList ID="cboDepartamentos" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboDepartamentos_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <label class="float-left" for="cboMunicipios">Municipio</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:DropDownList ID="cboMunicipios" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="container mt-3 text-center">
            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary w-25" Text="Guardar" OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>
