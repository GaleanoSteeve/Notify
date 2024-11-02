<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="CapaPresentacion.FormLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Notify</title>
    <link href="Styles/css/assets/style.css" rel="stylesheet" />
    <link href="Styles/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/css/bootstrap-icons.css" rel="stylesheet" />
    <link href="Styles/css/boxicons.min.css" rel="stylesheet" />
    <link href="Styles/css/quill.snow.css" rel="stylesheet" />
    <link href="Styles/css/quill.bubble.css" rel="stylesheet" />
    <link href="Styles/css/remixicon.css" rel="stylesheet" />
    <link href="Styles/css/style.css" rel="stylesheet" />

    <!--Archivos para los menajes tipo alerta-->
    <script src="Styles/scripts/alertify.js"></script>
    <link href="Styles/css/alertify.css" rel="stylesheet" />
    <link href="Styles/css/semantic.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <section class="section register min-vh-50 d-flex flex-column align-items-center justify-content-center py-4">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">

                        <div class="d-flex justify-content-center py-4">
                            <a href="index.html" class="logo d-flex align-items-center w-auto">
                                <img src="Styles/img/logo.png" alt="" />
                                <span class="d-none d-lg-block">Notify</span>
                            </a>
                        </div>
                        <div class="card mb-3">
                            <div class="card-body">
                                <div class="pt-4 pb-2">
                                    <h5 class="card-title text-center pb-0 fs-10">Iniciar sesión en su cuenta</h5>
                                </div>
                                <br />
                                <br />
                                <form runat="server" class="row g-3 needs-validation">
                                    <div class="col-12">
                                        <asp:TextBox ID="txtUsuario" runat="server" class="form-control" placeholder="Usuario" autocomplete="off"></asp:TextBox>
                                        <div class="invalid-feedback">Por favor ingrese su usuario.</div>
                                    </div>
                                    <div class="col-12">
                                        <asp:TextBox ID="txtContrasena" runat="server" class="form-control mt-2" placeholder="Contraseña" autocomplete="off" TextMode="Password"></asp:TextBox>
                                        <div class="invalid-feedback">Por favor ingrese su contraseña.</div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="col-12">
                                        <asp:Button ID="btnIngresar" runat="server" class="btn btn-primary w-100" Text="Ingresar" OnClick="btnIngresar_Click"></asp:Button>
                                    </div>
                                </form>
                            </div>
                        </div>
                        <div class="credits">
                            Creado por JGaleano Software
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</body>
</html>
