$.ajax({
    dataType: "json",
    type: "GET",
    url: "/Rotatoria/GetAutorizacoesUsuario",
    success: function (dados) {
        var tabelaBody = document.getElementById("BodyAutorizacao");
        console.log(tabelaBody);
        console.log(dados);
        var html = "";
        dados.forEach(function (el) {
            console.log(el);
            //var re = /-?\d+/;
            //var m = re.exec(el.AutdatPro);
            //var AutdatPro = new Date(parseInt(m[0]));
            html = html + "<tr><td>" + el.AutCod + "</td><td>" + ((el.AutdatPro == null) ? "Sem Data" : el.AutdatPro) + "</td><td>" + el.VeiPla + "</td><td><input type='button' onclick='editar(" + el.AutCod + ")' value='Editar'/></td> </tr>";


        })
        tabelaBody.innerHTML = html;
        console.log(dados);
    }
});