$.ajax({
    dataType: "json",
    type: "GET",
    url: "/Usuarios/GetUsuario",
    success: function (dados) {
        var tabelaBody = document.getElementById("BodyUsuario");
        console.log(tabelaBody);
        console.log(dados);
        var html = "";
        dados.forEach(function (el) {
            console.log(el);
            //var re = /-?\d+/;
            //var m = re.exec(el.AutdatPro);
            //var AutdatPro = new Date(parseInt(m[0]));
            html = html + "<tr><td>" + el.UsuNom + "</td><td>" + el.UsuMai +
                          "</td><td>" + el.UsuEmp + "</td><td>" + ((el.UsuPer == 'A') ? "Administrador" : (el.UsuPer == 'C') ? "Cliente" : (el.UsuPer == 'G') ? "Guarita" : el.UsuPer ) +
                          "</td><td>" + ((el.UsuSit == 'A') ? "Ativo" : (el.UsuSit == 'I') ? "Inativo" : el.UsuSit) + "</td><td><input type='button' onclick='editar(" + el.UsuCod + ")' value='Editar'/></td></tr>";
               


        })
        tabelaBody.innerHTML = html;
        console.log(dados);
    }
});


function editar(id) {
    alert(id);
    $.ajax({
        dataType: "text",
        url: "/Usuarios/Editar",
        type: "GET",
        success: function (data) {
            id: id
            console.log(data);
            console.log(id);
        },
        error: function (erro) {
            console.log("error");
            console.log(erro);
        }

    });
}
