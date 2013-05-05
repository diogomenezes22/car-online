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



    function convertDate(stringdate) {
        // Internet Explorer does not like dashes in dates when converting, 
        // so lets use a regular expression to get the year, month, and day 
        var DateRegex = /([^-]*)-([^-]*)-([^-]*)/;
        var DateRegexResult = stringdate.match(DateRegex);
        var DateResult;
        var StringDateResult = "";

        // try creating a new date in a format that both Firefox and Internet Explorer understand
        try {
            DateResult = new Date(DateRegexResult[2] + "/" + DateRegexResult[3] + "/" + DateRegexResult[1]);
        }
        // if there is an error, catch it and try to set the date result using a simple conversion
        catch (err) {
            DateResult = new Date(stringdate);
        }

        // format the date properly for viewing
        StringDateResult = (DateResult.getMonth() + 1) + "/" + (DateResult.getDate() + 1) + "/" + (DateResult.getFullYear());

        return StringDateResult;
    }


    jQuery(".botaoSalvarEnquete").click(function () {

        var qtdOpcoes = 0;
        jQuery("#areaOpcaoEnquete p").each(function () {

            qtdOpcoes++;
        });


        var nomeEnquete = $.trim(jQuery("#nomeEnquete").val());
        var dataVigenciaInicial = $.trim(jQuery("#dataVigencia").val());

        var erros = 0;
        var tipoOpcao = jQuery("#tipoOpcaoEnquete").val();
        var URL = jQuery(".urlSalvarEnquete").val();

        var sDate = "";

        if (dataVigenciaInicial != "") {
            sDate = dataVigenciaInicial;
        }
        if (nomeEnquete == "") {
            alert("Informe um nome para a enquete!");
            erros++;
        }
        if (sDate == "") {
            alert("Informe uma data de vigência!");
            erros++;
        }
        if (qtdOpcoes < 2) {
            alert("A enquete deve ter pelo menos 2 opções!");
            erros++;
            alert(erros);
        }



        if (erros == 0) {
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
                    location.reload();
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