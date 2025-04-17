<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="CapaPresentacion.FormLogin" %>

<!DOCTYPE html>

<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!--Archivos para los mensajes tipo alerta-->
    <script src="Styles/scripts/alertify.js"></script>
    <link href="Styles/css/alertify.css" rel="stylesheet" />
    <link href="Styles/css/semantic.css" rel="stylesheet" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" rel="stylesheet" />

    <style>
        body {
            background: linear-gradient(135deg, #f8f8f8 0%, #f0f0f0 100%);
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .login-container {
            background: #ffffff;
            border-radius: 15px;
            box-shadow: 0 8px 30px rgba(0, 0, 0, 0.2);
            padding: 2.5rem;
            width: 100%;
            max-width: 450px;
            animation: fadeIn 0.5s ease-in-out;
            text-align: center;
        }

        .login-header {
            text-align: center;
            margin-bottom: 2rem;
        }

            .login-header h2 {
                font-weight: 700;
                color: #333;
            }

        .form-control {
            border-radius: 10px;
            padding: 0.75rem;
            border: 1px solid #ddd;
            transition: border-color 0.3s ease, box-shadow 0.3s ease;
        }

            .form-control:focus {
                border-color: #667eea;
                box-shadow: 0 0 8px rgba(102, 126, 234, 0.3);
            }

        .btn-primary {
            background: #0d6efd;
            border: none;
            border-radius: 10px;
            padding: 0.75rem;
            font-weight: 600;
        }

            .btn-primary:hover {
                background: #667eea;
            }

        .form-check-label {
            color: #555;
        }

        .forgot-password {
            text-align: right;
            margin-bottom: 1rem;
        }

            .forgot-password a {
                color: #667eea;
                text-decoration: none;
                font-size: 0.9rem;
            }

                .forgot-password a:hover {
                    text-decoration: underline;
                }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(-20px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    </style>
</head>

<body>
    <form id="form1" runat="server" style="width: 30%;">
        <div class="login-container">
            <div class="login-header">
                <h2>Bienvenido</h2>
                <p class="text-muted">Inicia sesión para continuar</p>
            </div>
            <br />
            <div class="mb-4">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-user"></i></span>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" autocomplete="off" placeholder="Nombre de usuario" />
                </div>
            </div>
            <div class="mb-4">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-lock"></i></span>
                    <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" autocomplete="off" placeholder="Contraseña" TextMode="Password" />
                </div>
            </div>
            <div class="d-flex justify-content-between align-items-center mb-3">
                <div class="forgot-password">
                    <a href="#">¿Olvidaste tu contraseña?</a>
                </div>
            </div>
            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary w-100" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
