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
                    <asp:textbox id="txtNit" runat="server" cssclass="form-control" maxlength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)"></asp:textbox>
                </div>

                <div class="form-group">
                    <label class="float-left" for="txtDireccion">Dirección</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:textbox id="txtDireccion" runat="server" cssclass="form-control"></asp:textbox>
                </div>

                <label class="float-left" for="txtEmail">Email</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                <div class="input-group">
                    <asp:textbox id="txtEmail" runat="server" cssclass="form-control" textmode="Email"></asp:textbox>
                    <div class="input-group-append">
                        <span class="input-group-text">@</span>
                    </div>
                </div>

                <div class="form-group mt-3">
                    <label class="float-left" for="cboDepartamentos">Departamento</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:dropdownlist id="cboDepartamentos" runat="server" cssclass="form-control" autopostback="true" onselectedindexchanged="cboDepartamentos_SelectedIndexChanged"></asp:dropdownlist>
                </div>

                <div class="form-group">
                    <label class="float-left" for="cboTipoCuenta">Tipo Cuenta</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:dropdownlist id="cboTipoCuenta" runat="server" cssclass="form-control"></asp:dropdownlist>
                </div>
            </div>

            <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">

                <div class="form-group">
                    <label class="float-left" for="txtRazonSocial">Razón Social</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:textbox id="txtRazonSocial" runat="server" cssclass="form-control"></asp:textbox>
                </div>

                <div class="form-group">
                    <label class="float-left" for="txtTelefono">Teléfono</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:textbox id="txtTelefono" runat="server" cssclass="form-control" maxlength="10" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)"></asp:textbox>
                </div>

                <div class="form-group">
                    <label class="float-left" for="txtDiasNotificacion">Días Previos Notificación</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:textbox id="txtDiasNotificacion" runat="server" cssclass="form-control" maxlength="2" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)"></asp:textbox>
                </div>

                <div class="form-group">
                    <label class="float-left" for="cboMunicipios">Municipio</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:dropdownlist id="cboMunicipios" runat="server" cssclass="form-control"></asp:dropdownlist>
                </div>

                <div class="form-group mt-3">
                    <label class="float-left" for="txtNumeroCuenta">Número Cuenta</label><span class="float-left font-weight-bold ml-1 text-danger">*</span>
                    <asp:textbox id="txtNumeroCuenta" runat="server" cssclass="form-control" maxlength="12" oncopy="return false;" oncut="return false;" onpaste="return false;" onkeypress="return Numeros(event)"></asp:textbox>
                </div>
            </div>
        </div>

        <div class="container mt-3 text-center">
            <asp:button id="btnGuardar" runat="server" cssclass="btn btn-primary w-25" text="Guardar" onclick="btnGuardar_Click" />
        </div>
        <br />
    </div>
</asp:Content>
