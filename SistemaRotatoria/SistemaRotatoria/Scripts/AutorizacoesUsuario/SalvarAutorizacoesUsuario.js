/// <reference path="C:\Users\User\Documents\Visual Studio 2015\Projects\Rotatoria\Rotatoria\Views/Rotatoria/Calendario.cshtml" />
/// <reference path="C:\Users\User\Documents\Visual Studio 2015\Projects\Rotatoria\Rotatoria\Views/Rotatoria/Calendario.cshtml" />
//função que salva os dados do formulario
$(function () {

    $('#btn-salvar').click(function () {
        console.log('entrou');

        //pegando a data selecionada
        var dataSelecionada = document.getElementById("txt-data").value;
        //alert(dataSelecionada);

        //pegando data atual e convertendo para AAAA-MM-DD
        var dataAtual = new Date().toISOString().slice(0, 10);
        //alert(dataAtual);

        //validando a data
        if (dataSelecionada < dataAtual || dataSelecionada == "") {
            alert("A data não pode ser de um dia Anterior ou Vazia!");
            document.getElementById("txt-data").focus();
            return false;
        }
        else {
            var dataDia = $('#txt-data').val();
            //alert(dataDia);
        }

        //var dataDia = $('#txt-data').val();

        //validando a placa
        if (document.getElementById("txt-placa").value == "") {
            alert("A placa e Obrigatória!");
            document.getElementById("txt-placa").focus();
        }
        else {
            var placa = $('#txt-placa').val();
        }

        //validando o modelo
        if (document.getElementById("txt-modelo").value == "") {
            alert("O modelo e Obrigatório!");
            document.getElementById("txt-modelo").focus();
            return false;
        }
        else {
            var modelo = $('#txt-modelo').val();
        }

        //validando a marca
        if (document.getElementById("txt-marca").value == "") {
            alert("A marca e Obrigatória!");
            document.getElementById("txt-marca").focus();
            return false;
        }
        else {
            var marca = $('#txt-marca').val();
        }

        //validando a cor
        if (document.getElementById("txt-cor").value == "") {
            alert("A marca e Obrigatória!");
            document.getElementById("txt-cor").focus();
            return false;
        }
        else {
            var cor = $('#txt-cor').val();
        }

        //validando o eixo
        if (document.getElementById("txt-eixo").value == "") {
            alert("O eixo e Obrigatório!");
            document.getElementById("txt-eixo").focus();
            return false;
        }
        else {
            var eixo = $('#txt-eixo').val();
        }

        var dataInc = new Date().toLocaleString();
        alert(dataInc);

        $.ajax({
            url: generalSettings.salvarAutorizacao,
            type: "POST",
            async: true,
            data: {
                AutdatPro: dataDia,
                AutDatInc: dataInc,
                VeiPla: placa,
                VeiMar: marca,
                VeiMod: modelo,
                VeiCor: cor,
                VeiEix: eixo
            },
            success: function (data) {
                alert('Autorização salva com Sucesso!');
                var url = '@Url.Action("Calendario", "Rotatoria")';
                window.location.href = url;
                //$('#txt-modelo').val(data.veiculo.VeiMod);
            },
            error: function (erro) {
                alert('Esta placa ja esta Cadastrada... Pressione o Refresh!');
                console.log("error");
                console.log(erro);
            }
        });
    });
});

//função que busca a placa existente ao pressionar ENTER
function BuscaPlaca(event) {
    //codigo 13 é ENTER
    if (event.keyCode == 13) {

        var placa = $('#txt-placa').val();

        $.ajax({
            url: "/Rotatoria/VerificarPlaca",
            dataType: 'JSON',
            type: "GET",
            async: true,
            data: {
                placa: placa.toString()
            },
            success: function (data) {
                console.log(data);
                $('#txt-cor').val(data.VeiCor);
                $('#txt-marca').val(data.VeiMar);
                $('#txt-modelo').val(data.VeiMod);
                $('#txt-eixo').val(data.VeiEix);
            },
            error: function (erro) {
                alert("Esta Placa não esta Cadastrada!");
                console.log("error");
                console.log(erro);
            }
        });
    }

    console.log(event);
}