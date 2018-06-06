
$(document).ready(function () {
    $('#modalTrocarSenha').modal('show')

    $('#myModal').modal('show')

    $('#informacaoSenha').modal('show')


    $("#fecharBotãoModalCadastro").click(function () {
        limparCampos();
    });  
});

function limparCampos() {
    //empresa
    $(".razaoSocial").val('');
    $(".nomeFantasia").val('');
    $(".cnpj").val('');
    $(".cep").val('');
    $(".logradouro").val('');
    $(".numero").val('');
    $(".bairro").val('');
    $(".cidade").val('');
    $(".uf").val('');
    $(".complemento").val('');
    $(".email").val('');
    $(".telefone").val('');
    $(".celular").val('');
    $(".site").val('');

    //funcionario
    $(".nome").val('');
    $(".cpf").val('');
    $(".rg").val('');
    $(".nomeMae").val('');
    $(".nomePai").val('');
    $(".email").val('');
    $(".dataNascimento").val('');
    $(".orgao").val('');
    $(".sexo").val('');
    $(".estadoCivil").val('');
    
   




}
