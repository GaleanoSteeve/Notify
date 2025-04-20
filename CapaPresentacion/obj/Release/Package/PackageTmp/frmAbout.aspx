<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="frmAbout.aspx.cs" Inherits="CapaPresentacion.frmAbout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .about-container {
            text-align: center;
            padding: 20px;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            max-width: 90%;
            width: 90%;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }

        .icon {
            font-size: 50px;
            color: #0066cc;
        }

        h2 {
            color: #333;
            margin: 15px 0;
        }

        p {
            color: #666;
            margin: 10px 0;
        }

        .contact-link {
            color: #0066cc;
            text-decoration: none;
        }

            .contact-link:hover {
                text-decoration: underline;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="about-container">
        <div class="icon">🌟</div>

        <h2>Acerca de Nosotros</h2>

        <p>Notify es una aplicación para la gestión eficiente de datos de clientes y sus lotes. Permite organizar de forma centralizada la información de los clientes, los detalles de los lotes y el estadode las cuotas, facilitando un control claro y preciso. Su funcionalidad clave incluye el envío de notificaciones por WhatsApp, recordando a los clientes las fechas de pago de manera oportuna, lo que mejora la comunicación y reduce pagos atrasados.</p>
        
        <p>Con una interfaz intuitiva y un diseño seguro, la aplicación es ideal para empresas de cualquier tamaño, ofreciendo flexibilidad y escalabilidad. En Profesional Software, nos dedicamos a brindar herramientas innovadoras que optimizan procesos y fortalecen la relación con los clientes.</p>
        
        <p>Contacta con nosotros en: galeanosteeve@gmail.com</p>
        <%--<p>Contacta con nosotros en: <a href="galeanosteeve@gmail.com" class="contact-link">galeanosteeve@gmail.com</a></p>--%>
    </div>
</asp:Content>
