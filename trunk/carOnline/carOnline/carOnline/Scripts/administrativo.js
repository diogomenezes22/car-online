/// <reference path="jquery-1.8.3.js" />
/// <reference path="jquery-mascaras.js" />
/// <reference path="jquery.form.js" />
/// <reference path="jquery.blockUI.js" />
/// <reference path="jquery.livequery.js" />
jQuery(document).ready(function () {
    //------------------------------MASCARAS----------------------------\\
    //Transforma todos os inputs do tipo text em calendários desde que usem a classe .CALENDARIO
    jQuery('.CALENDARIO').livequery(function () {
        jQuery(this).datepicker();
    });
    //Adiciona uma máscara de CNPJ em todos os inputs do tipo text que usam a classe .CNPJ
    jQuery('.CNPJ').livequery(function () {
        jQuery(this).mask("99.999.999-9999-99");
    });
    //Adiciona uma máscara de CPF em todos os inputs do tipo text que usam a classe .CPF
    jQuery('.CPF').livequery(function () {
        jQuery(this).mask("999.999.999-99");
    });
    //Adiciona uma máscara de CEP em todos os inputs do tipo text que usam a classe .CEP
    jQuery('.CEP').livequery(function () {
        jQuery(this).mask("99999-999");
    });
    //Adiciona uma máscara de TELEFONE em todos os inputs do tipo text que usam a classe .TELEFONE
    jQuery('.TELEFONE').livequery(function () {
        jQuery(this).mask("(99)9999-9999");
    });
    //Nova máscara com um dígito a mais válido para SP e outros estados que adotarem o modelo
    jQuery('.NOVO_FORMATO_TELEFONE').livequery(function () {
        jQuery(this).mask("(99)99999-9999");
    });


    //BUSCAR CIDADES
    jQuery("#Estados").change(function () {
        var idEstado = jQuery(this).selected().val();
        var url = jQuery(".urlBuscarCidade").val();
        $.post(url, { id: idEstado }, function (data) {
            jQuery(".listaCidades option").remove();
            for (var i = 0; i < data.tamanho; i++) {
                jQuery(".listaCidades").append("<option value='" + data.idCidade[i] + "'>" + data.nomeCidade[i] + "</option>");
            }
        });
    });
    //BOTAO LIMPAR CIDADES
    jQuery(".botaoLimparCidades").click(function () {
        jQuery(".listaCidades option").remove();
    });




});