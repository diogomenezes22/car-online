/// <reference path="jquery-1.8.3.js" />
/// <reference path="jquery-mascaras.js" />
/// <reference path="jquery.form.js" />
/// <reference path="jquery.blockUI.js" />
/// <reference path="jquery.livequery.js" />
/// <reference path="jquery.maskMoney.js" />
/// <reference path="funcoes.js" />

jQuery(document).ready(function () {
    function CriarJanelaModal(classePrincipal, textoCabecalho, textCorpo, classeBotaoSim, classeBotaoNao, classeBotaoFechar) {
        jQuery("body").append("" +
           "<div class='janela " + classePrincipal + "'>" +
           "<div class='cabecalhoJanela'>" + textoCabecalho + "<span class='botaoFechar " + classeBotaoFechar + "'>X</div> " +
           "<div class='corpoJanela textoMensagem'>" + textCorpo + "</div>" +
           "<div class='areaBotoes'><a href='#' class='botoesJanelaModal " + classeBotaoSim + "'>Sim</a><a href='#' class='botoesJanelaModal " + classeBotaoNao + "'>Não</a></div>");
    }
    CriarJanelaModal("janelaModalUsuario", "Aviso", "Deseja deletar o Usuario?", "botaoSimDeletarUsuario", "botaoNaoDeletarUsuario", "botaoFecharModalUsuario");
    //------------------------------MASCARAS----------------------------\\
    //Transforma todos os inputs do tipo text em calendários desde que usem a classe .CALENDARIO
    jQuery('.CALENDARIO').livequery(function () {
        jQuery(this).datepicker();
    });
    //Adiciona uma máscara de CNPJ em todos os inputs do tipo text que usam a classe .CNPJ
    jQuery('.CNPJ').livequery(function () {
        jQuery(this).mask("99.999.999-9999-99");
    });

    //Adiciona uma máscara de CNPJ em todos os inputs do tipo text que usam a classe .CNPJ
    jQuery('.NumeroPortas').livequery(function () {
        jQuery(this).mask("9");
    });
    //Adiciona uma máscara de CNPJ em todos os inputs do tipo text que usam a classe .CNPJ
    jQuery('.Preco').livequery(function () {
        jQuery(this).maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: ".", allowZero: true });
    });
    //Adiciona uma máscara de CNPJ em todos os inputs do tipo text que usam a classe .CNPJ
    jQuery('.Quilometragem').livequery(function () {
        jQuery(this).maskMoney({ decimal: ",", thousands: ".", allowZero: true });
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

    //Formulario de login
    jQuery(".formularioLogin").submit(function () {
        var campoEmail = $.trim(jQuery(".campoEmailLogin").val());
        var campoSenha = $.trim(jQuery(".campoSenhaLogin").val());

        if (campoEmail == "" || campoSenha == "") {
            alert("Informe o email e senha!");
            return false;
        }
    });


    //Formulario de cadastro e alteracao de Usuario
    jQuery(".formularioUsuario").submit(function () {
        var campoEmail = $.trim(jQuery(".campoEmailUsuario").val());
        var campoNome = $.trim(jQuery(".campoNomeUsuario").val());
        var campoSenha = $.trim(jQuery(".campoSenhaUsuario").val());

        if (campoEmail == "" || campoNome == "" || campoSenha == "") {
            alert("Campos obrigatórios!\nNome\nEmail\nSenha");
            return false;
        }
    });


    //Formulario de cadastro de alteracao de Veiculo
    jQuery(".formularioVeiculo").submit(function () {

        var campoTipoVeiculo = $('#TipoVeiculo option:selected').text();
        var campoMarcas = $('.ListaMarcas option:selected').text();
        var campoModelos = $('.ListaModelos option:selected').text();

        var campoQuilometragem = $.trim(jQuery(".campoQuilometragemVeiculo").val());
        var campoDescricao = $.trim(jQuery(".campoDescricaoVeiculo").val());
        var campoPreco = $.trim(jQuery(".campoPrecoVeiculo").val());

        if (campoTipoVeiculo == "---Selecione---" || campoQuilometragem == "" || campoDescricao == "" || campoPreco == "" || campoMarcas == "" || campoMarcas == "---Selecione---" || campoModelos == "" || campoModelos == "---Selecione---") {
            alert("Os seguintes campos são obrigatórios: \nTipo de Veículo\nMarca\nModelo\nQuilometragem\nDescricao\nPreço");
            return false;
        }
    });

    /*-------------------------AJAX DELETAR USUARIO-------------------------------*/

    
    var idUsuario = 0;
    jQuery(".linkDeletarUsuario").click(function () {
        idUsuario = jQuery(this).attr("id");
        MostrarMascara();
        MostrarJanelaModal("janelaModalUsuario");
    });
    jQuery(".botaoNaoDeletarUsuario").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalUsuario");
    });
    jQuery(".botaoFecharModalUsuario").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalUsuario");
    });
    jQuery(".botaoSimDeletarUsuario").livequery("click", function () {
        $.ajax({
            url: "/Usuario/DeletarUsuario/",
            dataType: "json",
            type: "post",
            data: { id: idUsuario },
            beforeSend: function (data) {

            },
            success: function (data) {
                alert("Usuario deletado com sucesso!");
                EsconderMascara();
                EsconderJanelaModal("janelaModalUsuario");
                location.reload();
            },
            error: function (data) {
                alert("Não foi possível deletar o Usuario!")
                EsconderMascara();
                EsconderJanelaModal("janelaModalUsuario");
            }
        });
    });







    /*-----------------------------------AJAX DO VEÍCULO----------------------*/

    //BUSCAR MARCAS
    jQuery("#TipoVeiculo").trigger("change");
    jQuery("#TipoVeiculo").change(function () {
        var idTipoVeiculo = jQuery(this).selected().val();
        if (idTipoVeiculo != 999) {
            var url = "/Veiculo/BuscarMarca/";
            $.post(url, { id: idTipoVeiculo }, function (data) {
                jQuery(".ListaMarcas option").remove();
                jQuery(".ListaModelos option").remove();
                jQuery(".ListaMarcas").append("<option value='999'>---Selecione---</option>");
                for (var i = 0; i < data.tamanho; i++) {
                    jQuery(".ListaMarcas").append("<option value='" + data.idMarca[i].toString() + "'>" + data.nomeMarca[i] + "</option>");
                }
            });
        }
        else {
            jQuery(".ListaMarcas option").remove();
            jQuery(".ListaModelos option").remove();
        }
    });

    //BUSCAR MODELOS
    jQuery(".ListaMarcas").trigger("change");
    jQuery(".ListaMarcas").change(function () {
        var idMarca = jQuery(this).selected().val();
        if (idMarca != 999) {
            var url = "/Veiculo/BuscarModelo/";
            $.post(url, { id: idMarca }, function (data) {
                jQuery(".ListaModelos option").remove();
                for (var i = 0; i < data.tamanho; i++) {
                    jQuery(".ListaModelos").append("<option value='" + data.idModelo[i].toString() + "'>" + data.nomeModelo[i] + "</option>");
                }
            });
        }
        else {
            jQuery(".ListaModelos option").remove();
        }
    });

    //BOTAO LIMPAR
    jQuery(".botaoLimparVeiculo").click(function () {
        jQuery(".ListaMarcas option").remove();
        jQuery(".ListaModelos option").remove();
    });

    /*-------------------------AJAX DELETAR VEICULO-------------------------------*/
    CriarJanelaModal("janelaModalVeiculo", "Aviso", "Deseja deletar o veículo?", "botaoSimDeletarVeiculo", "botaoNaoDeletarVeiculo", "botaoFecharModalVeiculo");

    var idVeiculo = 0;
    jQuery(".linkDeletarVeiculo").click(function () {
        idVeiculo = jQuery(this).attr("id");
        MostrarMascara();
        MostrarJanelaModal("janelaModalVeiculo");
    });
    jQuery(".botaoNaoDeletarVeiculo").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalVeiculo");
    });
    jQuery(".botaoFecharModalVeiculo").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalVeiculo");
    });
    jQuery(".botaoSimDeletarVeiculo").livequery("click", function () {
        $.ajax({
            url: "/Veiculo/DeletarVeiculo/",
            dataType: "json",
            type: "post",
            data: { id: idVeiculo },
            beforeSend: function (data) {

            },
            success: function (data) {
                alert("Veículo deletado com sucesso!");
                EsconderMascara();
                EsconderJanelaModal("janelaModalVeiculo");
                location.reload();
            },
            error: function (data) {
                alert("Não foi possível deletar o veículo!")
                EsconderMascara();
                EsconderJanelaModal("janelaModalVeiculo");
            }
        });
    });

    if (jQuery(".auxiliarUploadFoto").val() == "alteracaoVeiculo") {

        var idVeiculoUsuario = parseInt(jQuery(".idVeiculoUsuario").val());
        var urlTabelaFotosVeiculo = jQuery(".urlBuscarTabelaArquivo").val();
        BuscarTabelaArquivo(idVeiculoUsuario, urlTabelaFotosVeiculo);
    }


    //FUNÇÃO QUE TRAZ AS TABELAS DE ARQUIVOS DE TODOS OS COMPONENTES FILELIST DO BRIEFING
    function BuscarTabelaArquivo(idVeiculo, url) {
        jQuery.ajax({
            type: "POST",
            url: url,
            data: { idVeiculo: idVeiculo },
            success: function (data) {
                if (data != "") {
                    jQuery(".areaTabelaArquivos").each(function () {
                        jQuery(this).html("");
                        jQuery(this).html(data.tabelaArquivos.toString());

                    });
                }
                else {
                    alert("O tamanho do arquivo é inválido.");
                }
            },
            error: function (data) {
                alert("Não foi possível atualizar a tabela de arquivos.Contate o administrador ou tente mais tarde!");
            },
            dataType: "json"
        });
    }

    //FUNÇÃO AJAX PARA REMOVER O ARQUIVO
    function RemoverArquivo(idVeiculo, nomeArquivo, url) {
        jQuery.ajax({
            type: "POST",
            url: url,
            data: { idVeiculo: idVeiculo, nomeArquivo: nomeArquivo },
            success: function (data) {
                if (data.tabelaArquivos != "") {
                    location.reload();
                    //jQuery(".areaTabelaArquivos").each(function () {
                    // jQuery(this).html("");
                    // jQuery(this).html(data.tabelaArquivos.toString());
                    // });
                }
                alert(data.mensagem);
                EsconderFundoPreto();
            },
            error: function (data) {
                alert("Não foi possível deletar o arquivo.Contate o administrador ou tente mais tarde!");
                EsconderFundoPreto();
            },
            dataType: "json"
        });
    }

    //VERIFICA SE O USUÁRIO SELECIONOU ALGUM ARQUIVO
    function ArquivoSelecionado(idComponente) {
        var resultado = false;
        jQuery("input[type='file']").each(function () {
            if (jQuery(this).val() == "") {
                resultado = false;
            }
            else {
                resultado = true;
            }
        });
        return resultado;
    }
    function RetornaNomeArquivo(idComponente) {
        jQuery("input[type='file']").each(function () {
            return jQuery(this).val();
        });
    }


    //-------------EVENTOS DO FileList


    //BUSCA O GRUPO, COMPONENTE E O FILELIST QUE FOI SELECIONADO PARA SER USADO NO MOMENTO DO UPLOAD DE ARQUIVO
    jQuery(".botaoSalvarFileList").click(function () {
        idVeiculo = jQuery(this).attr("data-idVeiculo");
    });


    //---------------------------------------------------CÓDIGO PARA DELETAR OS ARQUIVOS DO BRIEFING--------------------------------------------------\\

    //CRIA A JANELA MODAL DE REMOVER O ARQUIVO
    CriarJanelaModal("janelaModalDeletarArquivo", "Aviso deletar arquivo", "Deseja deletar o arquivo selecionado?", "botaoSimDeletarArquivo", "botaoNaoDeletarArquivo", "botaoFechaJanelaModalDeletarArquivo");

    var idVeiculo = "";
    var nomeArquivo = "";
    var urlRemoverArquivo = "";
    //REMOVE UM ARQUIVO SELECIONADO
    jQuery(".botaoRemoverArquivoFileList").live("click", function () {
        idVeiculo = jQuery(this).attr("data-idVeiculo");
        nomeArquivo = jQuery(this).attr("data-nomeFinalArquivo");
        urlRemoverArquivo = jQuery(this).attr("data-Url");
        //FAZ O TRATAMENTO DA JANELA MODAL
        //EscurerTela($(this).attr('href'));
        MostrarJanelaModal("janelaModalDeletarArquivo", 200);
        jQuery(".textoCorpoJanelaModal").html("");
        jQuery(".textoCorpoJanelaModal").append("Deseja deletar o arquivo <span class='textoDestaque'>" + nomeArquivo + "</span>?");
    });

    jQuery(".botaoNaoDeletarArquivo").on("click", function () {
        EsconderJanelaModal("janelaModalDeletarArquivo", 500);
        //EsconderFundoPreto();
    });
    jQuery(".botaoSimDeletarArquivo").on("click", function () {
        EsconderJanelaModal("janelaModalDeletarArquivo", 500);
        RemoverArquivo(idVeiculo, nomeArquivo, urlRemoverArquivo);

    });

    //FUNÇÃO QUE FAZ O UPLOAD DO ARQUIVO
    $(function () {
        //CONFIGURA AS OPÇÕES DO FORMULÁRIO
        var options = {
            iframe: true,
            datatype: "json",
            //ANTES DE ENVIAR O FORMULÁRIO
            beforeSubmit: function (formData, jqForm, options) {
                alert("Enviando!");
                /*if (!(ArquivoSelecionado(idComponenteBotaoSalvarFileList))) {
                alert("Selecione um arquivo.");
                return false;
                }*/
                //DesabilitaBotaoGrupo(idGrupoBotaoSalvarFileList);
                //$(idFileList).block({ message: "<h1 class='loadingSalvarArquivos'><img src='/Content/Images/loadingSalvarArquivos.gif' class='loading' /> Salvando Arquivo...</h1>" });
            },
            //SE NÃO DER ERRO NO UPLOAD
            success: function (responseText, statusText, xhr, $form) {
                if (responseText == "Não foi possível gravar o arquivo.Contate o administrador ou tente mais tarde.") {
                    alert(responseText.toString());
                    //$(idFileList).unblock();
                    $(".ajaxUploadForm").resetForm();
                }
                else if (responseText.tipoErro=="TamanhoArquivo") {
                    alert(responseText.mensagem.toString());
                    $(idFileList).unblock();
                    $(".ajaxUploadForm").resetForm();
                }
                else if (responseText == "Já existe um arquivo com esse nome.") {
                    alert("Já existe um arquivo chamado " + RetornaNomeArquivo(idComponenteBotaoSalvarFileList));
                    //CriarJanelaModal("janelaModalSubstituirArquivo","Aviso",)
                }
                else if (responseText == "Esse arquivo não é uma imagem!") {
                    alert("Esse arquivo não é uma imagem!");
                    //CriarJanelaModal("janelaModalSubstituirArquivo","Aviso",)
                }
                else if (responseText != "") {
                    //HabilitarBotaoGrupo(idGrupoBotaoSalvarFileList);
                    //$(idFileList).unblock();
                    $(".ajaxUploadForm").resetForm();
                    location.reload();
                    /*jQuery(".areaTabelaArquivos").each(function () {
                    //if (jQuery(this).attr("data-idComponente") == idComponenteBotaoSalvarFileList && jQuery(this).attr("data-idGrupo") == idGrupoBotaoSalvarFileList) {
                    jQuery(this).html("");
                    jQuery(this).html(responseText.toString());
                    //}
                    });*/
                }
            },
            complete: function () {
                //alert("Completou!");
            },
            //SE DER ERRO NO ENVIO DO ARQUIVO
            error: function () {
                alert("Não foi possível fazer o upload do arquivo.Contate o administrador ou tente mais tarde.");
            }
        }

        //QUANDO O FORMULÁRIO DER SUBMIT
        $(".ajaxUploadForm").live("submit", function (e) {
            $(this).ajaxSubmit(options);
            return false;
        });
    });

    //---------FIM - eventos fileList

});