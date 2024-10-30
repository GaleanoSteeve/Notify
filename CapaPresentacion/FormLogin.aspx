<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormLogin.aspx.cs" Inherits="CapaPresentacion.FormLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Notify</title>
    <link href="Styles/assets/style.css" rel="stylesheet" />
    <link href="Styles/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/css/bootstrap-icons.css" rel="stylesheet" />
    <link href="Styles/css/boxicons.min.css" rel="stylesheet" />
    <link href="Styles/css/quill.snow.css" rel="stylesheet" />
    <link href="Styles/css/quill.bubble.css" rel="stylesheet" />
    <link href="Styles/css/remixicon.css" rel="stylesheet" />
    <link href="Styles/css/style.css" rel="stylesheet" />
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
                                    <h5 class="card-title text-center pb-0 fs-4">Iniciar sesión en su cuenta</h5>
                                    <p class="text-center small mt-4">Ingrese sus credenciales para iniciar sesión</p>
                                </div>
                                <form class="row g-3 needs-validation">
                                    <div class="col-12">
                                        <label for="yourUsername" class="form-label">Usuario</label>
                                        <input type="text" name="username" class="form-control" id="yourUsername" placeholder="Usuario" required="required" autocomplete="off" />
                                        <div class="invalid-feedback">Por favor ingrese su usuario.</div>
                                    </div>

                                    <div class="col-12">
                                        <label for="yourPassword" class="form-label">Contraseña</label>
                                        <input type="password" name="password" class="form-control" id="yourPassword" placeholder="Contraseña" required="required" autocomplete="off" />
                                        <div class="invalid-feedback">Por favor ingrese su contraseña.</div>
                                    </div>

                                    <div class="col-12">
                                        <button class="btn btn-primary w-100" type="submit">Login</button>
                                    </div>
                                </form>
                            </div>
                        </div>
                        <div class="credits">
                            Creado por Jeison S. Galeano M. 2024
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</body>
</html>
