$('.input-number').on('input', function () {
    this.value = this.value.replace(/[^0-9]/g, '');
});

$('#btnlogin').click(function () {

    var autenticar = { 'Usuario': $('#txtUsuario').val(), 'Password': $('#txtPassword').val() };
    //if (autenticar.Usuario.toString() == null || autenticar.Usuario.toString() == "" ){
    //    shakeModal();
    //}
    $.ajax({
        url: 'Home/Login',
        type: 'POST',
        dataType: 'json',
        sync: false,
        data: autenticar,
        success: function (data) {
            if (data != null)
            {

                location.href = "Home/Principal";
            }
        },
        error: function (xhr,status,error) {
           // alertify.error("Usuario o Contraseña incorrecta - QuizGo");
            shakeModal();

        }
    });
});