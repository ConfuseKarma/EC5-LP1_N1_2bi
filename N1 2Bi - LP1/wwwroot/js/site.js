function buscaCEP()
{
    var cep = document.getElementById("Cep").value;
    cep = cep.replace('-', ''); // removemos o traço do CEP
    if (cep.length > 0)
    {
        var linkAPI = 'https://viacep.com.br/ws/' + cep + '/json/';
        $.ajax({
            url: linkAPI,
            beforeSend: function ()
            {
                document.getElementById("Endereco").value = '...';
                document.getElementById("Cidade").value = '...';
                document.getElementById("Estado").value = '...';
            },
            success: function (dados)
            {
                if (dados.erro != undefined) // quando o CEP não existe...
                {
                    alert('CEP não localizado...');
                    document.getElementById("Endereco").value = '';
                    document.getElementById("Cidade").value = '';
                    document.getElementById("Estado").value = '';
                }
                else // quando o CEP existe
                {
                    document.getElementById("Endereco").value = dados.logradouro;
                    document.getElementById("Cidade").value = dados.localidade;
                    document.getElementById("Estado").value = dados.uf;
                    document.getElementById("Numero").focus();

                }
            }
        });
    }
}

function aplicaFiltroConsultaAvancada() {
    var vNome = document.getElementById('nome').value;
    var vAnalises = document.getElementById('analises').value;
    var vPrecoMenor = document.getElementById('precoMenor').value;
    var vPrecoMaior = document.getElementById('precoMaior').value;
    
    $.ajax({
        url: "/Produto/ObtemDadosConsultaAvancada",
        data: { nome: vNome, analises: vAnalises, precoMenor: vPrecoMenor, precoMaior: vPrecoMaior },
        success: function (dados) {
            if (dados.erro != undefined) {
                alert(dados.msg);
            }
            else {
                document.getElementById('resultadoConsulta').innerHTML = dados;
            }
        },
    });

}

function aplicaFiltroConsultaAvancadaReviews() {
    var vProdutoId = document.getElementById('produtoId').value;
    var vNomeUsuario = document.getElementById('nomeUsuario').value;
    var vPontuacao = document.getElementById('pontuacao').value;
    var vPeriodo = document.getElementById('periodo').value;

    $.ajax({
        url: "/Reviews/ObtemDadosConsultaAvancada",
        data: { produtoId: vProdutoId, nomeUsuario: vNomeUsuario, pontuacao: vPontuacao, periodo: vPeriodo },
        success: function (dados) {
            if (dados.erro != undefined) {
                alert(dados.msg);
            }
            else {
                document.getElementById('resultadoConsulta').innerHTML = dados;
            }
        },
    });

}