<%@ Page Title="Perfiles" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmPerfiles.aspx.cs" Inherits="CapaPresentacion.frmPerfiles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <h4 class="text-center font-weight-bold">Perfiles</h4>

    <div class="text-center">
        <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary mt-4" Width="10%" Text="Crear Perfil" />
        <div style="visibility: hidden">
            <asp:Button ID="btnAbrir" runat="server" />
        </div>
    </div>

    <div class="container">

        <div class="table-responsive" style="overflow-y: scroll;">
            <asp:GridView ID="gvPerfiles" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="Código" HeaderText="Código" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandArgument='<%#Eval("IdPerfil") + "," + Eval("Nombre") + "," + Eval("Activo") %>' Text='<%# Eval("IdPerfil") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Creación" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="Activo" HeaderText="Activo" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
