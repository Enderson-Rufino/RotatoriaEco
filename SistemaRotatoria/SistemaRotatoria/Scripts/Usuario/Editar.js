//JavaScript: para rodar quando a pagina abrir
$(document).ready(function () {
    //coloca aqui o que tiver que começar
    $.ajax({
        url: "/Usuarios/CarregaUsuario",
        dataType: 'JSON',
        type: "GET",
        async: false,
        data: {

        },
        success: function (data) {
            console.log(data);
            //$('#UsuCod').val(data.UsuCod);
            $('#txt-Nom').val(data.UsuNom);
            $('#txt-Sen').val(data.UsuSen);
            $('#txt-Mai').val(data.UsuMai);
            $('#txt-Emp').val(data.UsuEmp)
            $('#txt-Per').val(data.UsuPer);
            $('#txt-Sit').val(data.UsuSit)

        },
        error: function (erro) {
            console.log("error");
            console.log(erro);
        }
    });
});


$('#salva').click(function () {
    //coloca aqui o que tiver que começar
    alert("to aqui");
    var url = "/Usuarios/Editar";
    var nome = $("#txt-Nom").val();
    var senha = $("#txt-Sen").val();
    var email = $("#txt-Mai").val();
    var empresa = $("#txt-Emp").val();
    var perfil = $("#txt-Per").val();
    var situacao = $("#txt-Sit").val();
    $.post(url, { Nome: nome, Senha: senha, Email: email, Empresa: empresa, Perfil: perfil, Situacao: situacao }, function (data) {

    });
})