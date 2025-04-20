<%@ Page Title="Notificaciones" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmNotificaciones.aspx.cs" Inherits="CapaPresentacion.frmNotificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .construction-container {
            text-align: center;
            padding: 20px;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            max-width: 500px;
            width: 90%;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }
        .icon {
            font-size: 50px;
            color: #ff9900;
        }
        h2 {
            color: #333;
        }
        p {
            color: #666;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="construction-container">
        <div class="icon">🚧</div>
        <h2>Página en Construcción</h2>
        <p>Trabajamos en un proyecto transformador.</p>
    </div>
</asp:Content>
