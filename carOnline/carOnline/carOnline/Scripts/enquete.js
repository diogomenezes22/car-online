/// <reference path="jquery-1.8.3.js" />
/// <reference path="jquery-mascaras.js" />
/// <reference path="jquery.form.js" />
/// <reference path="jquery.blockUI.js" />
/// <reference path="jquery.livequery.js" />
/// <reference path="funcoes.js" />
/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="data.format.js" />


jQuery(document).ready(function () {
    var identificador = 1;
    var quantidadeOpcoes = 1;
    jQuery(".adicionarOpcaoEnquete").click(function () {
        var opcaoRepetida = false;
        if ($.trim(jQuery("#textoOpcao").val()) != "") {

            jQuery("#areaOpcaoEnquete span").each(function () {
                if (jQuery(this).text() == jQuery("#textoOpcao").val()) {
                    alert("Essa opção já foi adicionada!");
                    opcaoRepetida = true;
                    return false;
                }
            });

            if (quantidadeOpcoes <= 5) {
                if (!opcaoRepetida) {
                    var tipoOpcao = jQuery("#tipoOpcaoEnquete").val();
                    var textoOpcao = jQuery("#textoOpcao").val();
                    if (tipoOpcao == "RadioButton") {
                        jQuery("#areaOpcaoEnquete").append("" +
                            "<p data-identificador=" + identificador + "><a href='#' data-identificador=" + identificador + " class='botaoRemoverOpcaoEnquete'>X</a><input type='radio' name='checkbox' data-identificador=" + identificador + "><span class='opcaoAdicionada'>" + textoOpcao + "</span></p>" +
                        "");
                        identificador++;
                        jQuery("#textoOpcao").html("");
                        jQuery("#textoOpcao").focus();
                    }
                    else {
                        jQuery("#areaOpcaoEnquete").append("" +
                            "<p data-identificador=" + identificador + "><a href='#' data-identificador=" + identificador + " class='botaoRemoverOpcaoEnquete'>X</a><input type='checkbox' data-identificador=" + identificador + "><span class='opcaoAdicionada'>" + textoOpcao + "</span></p>" +
                        "");
                        identificador++;
                        jQuery("#textoOpcao").html("");
                        jQuery("#textoOpcao").focus();
                    }
                    quantidadeOpcoes++;
                }
            }
            else {
                alert("Você não pode ultrapassar o máximo de 5 opções!");
                return false;
            }
        }
        else {
            alert("Informe um texto para opção!");
            return false;
        }
    });

    jQuery(".botaoSalvarEnquete").click(function () {
        var nomeEnquete = $.trim(jQuery("#nomeEnquete").val());
        var dataVigencia = $.trim(jQuery("#dataVigencia").val());


        var sDate = dateFormat(dataVigencia, "yyyy/mm/dd HH:MM:ss");

        var tipoOpcao = jQuery("#tipoOpcaoEnquete").val();
        var URL = jQuery(".urlSalvarEnquete").val();
        if (nomeEnquete == "") {
            alert("Informe um nome para a enquete!");
            jQuery("#nomeEnquete").focus();
            return false;
        }
        else if (quantidadeOpcoes < 2) {
            alert("A enquete deve ter pelo menos 2 opções!");
            return false;
        }
        else if (dataVigencia == "") {
            alert("Informe uma data de vigência!");
            return false;
        }
        else {
            var vetorOpcoesEnquete = {};
            jQuery("#areaOpcaoEnquete").each(function () {
                var opcoes = $.map($('span', this), function (e) { return e; });
                var elemento = "";
                for (var i = 0; i < opcoes.length; i++) {
                    elemento = opcoes[i];
                    vetorOpcoesEnquete[i] = $(elemento).text();
                }
                return false;
            });

            /*---------------------------------AJAX PARA SALVAR A ENQUETE----------------------*/
            // JSON.stringify(data)
            $.ajax({
                type: 'POST',
                url: URL,
                //cache: 'false',
                beforeSend: function () {
                    alert("Salvando enquete!");
                },
                data: { nomeEnquete: nomeEnquete, tipoOpcao: tipoOpcao, dataVigencia: sDate, opcoesEnquete: vetorOpcoesEnquete },
                //contentType: 'application/json',
                complete: function () {
                    //location.reload();
                },
                success: function (Resultado) {
                    alert("Enquete salva com sucesso!");
                },
                error: function (data) {
                    alert("Não foi possível salvar a enquete.Contate o administrador e tente mais tarde!");
                },
                dataType: 'json'
            });
        }
    });
    jQuery(".botaoRemoverOpcaoEnquete").live("click", function () {
        var identificador = jQuery(this).attr("data-identificador");
        jQuery("#areaOpcaoEnquete p").each(function () {
            if (jQuery(this).attr("data-identificador") == identificador) {
                jQuery(this).remove();
                quantidadeOpcoes--;
            }
        });
    });

    jQuery("#tipoOpcaoEnquete").change(function () {
        jQuery("#areaOpcaoEnquete").html("");
        quantidadeOpcoes = 1;
    });



});