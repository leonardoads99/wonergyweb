$(document).ready(function () {
    //remove letras
    $('.cpf, .rg, .numero, .telefone, .celular, .cep, .cnpj, .Potencia, .Tensao, .Corrente').keyup(function () {
        $(this).val(this.value.replace(/\D/g, ''));
    });

    // remove numero
    $('.nome, .nomeMae, .nomePai, .orgao,.complemento,.razaoSocial,.nomeFantasia, .NomeEquipamento, .Marca, .Modelo').keyup(function () {
        this.value = this.value.replace(/\d/g, '');
    });
    
    $(".telefone").inputmask("mask", { "mask": "(99) 99999-9999" });
    $(".cpf").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
    $(".cep").inputmask("mask", { "mask": "99.999-999" });
    $(".cnpj").inputmask("mask", { "mask": "99.999.999/9999-99" });
    $(".celular").inputmask("mask", { "mask": "(99) 99999-9999" });
    $(".rg").inputmask("mask", { "mask": "9.999.999" });

    desabilitarCamposCep();

        $(".cep").blur(function () {
            var cep = $(this).val().replace(/\D/g, '');

            if (cep != null) {
                var validaCep = /^[0-9]{8}$/;

                if (validaCep.test(cep)) {

                    $(".logradouro").val("...");
                    $(".bairro").val("...");
                    $(".cidade").val("...");
                    $(".uf").val("...");

                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {
                        if (!("erro" in dados)) {
                            $(".logradouro").val(dados.logradouro);
                            $(".bairro").val(dados.bairro);
                            $(".cidade").val(dados.localidade);
                            $(".uf").val(dados.uf);
                        } else {
                            limparFormularioCep()
                            alert("CEP não encontrado");
                        }
                    });
                }
            }
        });

    $('.cpf').blur(function () {

        var cpf = $(this).val();
        if (!validarCPF(cpf)) {
            $('#myModalCPF').modal('show')
        }
    });

    $('.cnpj').blur(function () {

        var cnpj = $(this).val();
        if (!validarCNPJ(cnpj)) {
            $('#myModalCNPJ').modal('show')
        }
    });
});

function limparFormularioCep() {
    $(".logradouro").val("");
    $(".bairro").val("");
    $(".cidade").val("");
    $(".uf").val("");
}

function desabilitarCamposCep() {
    $(".logradouro").attr("disabled", true);
    $(".bairro").attr("disabled", true);
    $(".cidade").attr("disabled", true);
    $(".uf").attr("disabled", true);
}

function validarCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');
    if (cpf == '') return false;
    if (cpf.length != 11 ||
        cpf == "00000000000" ||
        cpf == "11111111111" ||
        cpf == "22222222222" ||
        cpf == "33333333333" ||
        cpf == "44444444444" ||
        cpf == "55555555555" ||
        cpf == "66666666666" ||
        cpf == "77777777777" ||
        cpf == "88888888888" ||
        cpf == "99999999999")
        return false;
    add = 0;
    for (i = 0; i < 9; i++)
        add += parseInt(cpf.charAt(i)) * (10 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(9)))
        return false;
    add = 0;
    for (i = 0; i < 10; i++)
        add += parseInt(cpf.charAt(i)) * (11 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(10)))
        return false;
    return true;
}

function validarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;
    if (cnpj.length != 14)
        return false;

    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;
}





