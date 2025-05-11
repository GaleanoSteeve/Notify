<%@ Page Title="Notificaciones" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmNotificaciones.aspx.cs" Inherits="CapaPresentacion.frmNotificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function SeleccionarTodos(chkTodos) {
            $('#<%=gvRegistros.ClientID %>').find("input:CheckBox").each(function () {
                if (this != chkTodos) {
                    this.checked = chkTodos.checked;
                }
            });
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h4 class="text-center font-weight-bold mt-4" style="color: #007bff;">Notificaciones</h4>

    <div class="container-fluid mt-3" style="height: 66vh; width: 98%; overflow-y: scroll; padding: 0;">

        <div class="table-responsive">
            <asp:GridView ID="gvRegistros" runat="server" CssClass="table table-striped table-hover table-bordered" AutoGenerateColumns="False" OnRowDataBound="gvRegistros_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="IdClienteLote" HeaderText="IdClienteLote" />
                    <asp:BoundField DataField="Documento" HeaderText="Documento" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="WhatsApp" HeaderText="WhatsApp" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Manzana" HeaderText="Manzana" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="NumeroLote" HeaderText="Lote" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="CuotaMensual" HeaderText="Cuota" HeaderStyle-Width="8%" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="FechaPagoCuota" HeaderText="Fecha Pago" HeaderStyle-Width="10%" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Notificado" HeaderText="Notificado" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Notificar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkNotificarTodos" runat="server" OnClick="javascript:SeleccionarTodos(this)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkNotificar" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#007bff" Font-Bold="False" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>
    <div class="container mt-3 text-center">
        <asp:Button ID="btnEnviar" runat="server" CssClass="btn btn-primary w-25" Text="Enviar" OnClick="btnEnviar_Click" />
    </div>
</asp:Content>
