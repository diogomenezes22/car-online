/// <reference path="jquery-1.8.3.js" />
/// <reference path="jquery-mascaras.js" />
/// <reference path="jquery.form.js" />
/// <reference path="jquery.blockUI.js" />
/// <reference path="jquery.livequery.js" />
/// <reference path="funcoes.js" />
/// <reference path="jquery-ui-1.8.11.js" />

jQuery(document).ready(function () {

    //------------------------------MASCARAS----------------------------\\
    //Transforma todos os inputs do tipo text em calendários desde que usem a classe .CALENDARIO
    jQuery('.CALENDARIO').livequery(function () {
        jQuery(this).datepicker(this, "dateFormat", "dd-mm-yy");
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

    //--------------------CONFIGURAÇÃO DO MENU DE OPÇÕES------------------------\\

    //-------------------------------FUNÇÕES DA ÁREA ADMINISTRATIVA-------------------------------\\

    function MostrarEsconderMenuAdministrativo(classeArea, classeAreaBotoes, efeitoMostrar) {
        if (efeitoMostrar) {
            jQuery("" + classeArea + " " + classeAreaBotoes + "").each(function () {
                jQuery(this).toggle(true);
            });
        }
        else {
            jQuery("" + classeArea + " " + classeAreaBotoes + "").each(function () {
                jQuery(this).toggle("slide", false);
            });
        }
    }

    //--------------------AO PASSAR O MOUSE POR CIMA DAS OPÇÕES MOSTRA O MENU DE CADA UMA --------------------\\

    jQuery(".areaConfiguracaoBotao").mouseover(function () {
        var referencia = "#" + jQuery(this).attr("id");
        MostrarEsconderMenuAdministrativo(referencia, ".botoesOcultos", true);
    });

    jQuery(".areaConfiguracaoBotao").mouseleave(function () {
        var referencia = "#" + jQuery(this).attr("id");
        MostrarEsconderMenuAdministrativo(referencia, ".botoesOcultos", false);
    });



    /*-------------------------AJAX DELETAR ADMINISTRADOR--------------------------------*/
    CriarJanelaModal("janelaModalAdministrador", "Aviso", "Deseja deletar o administrador?", "botaoSimDeletarAdministrador", "botaoNaoDeletarAdministrador", "botaoFecharModalAdministrador");
    var idAdministrador = 0;
    jQuery(".linkDeletarAdministrador").click(function () {
        idAdministrador = jQuery(this).attr("id");
        MostrarMascara();
        MostrarJanelaModal("janelaModalAdministrador");
    });
    jQuery(".botaoNaoDeletarAdministrador").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalAdministrador");
    });
    jQuery(".botaoFecharModalAdministrador").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalAdministrador");
    });
    jQuery(".botaoSimDeletarAdministrador").livequery("click", function () {
        $.ajax({
            url: "/Administrativo/Administrador/DeletarAdministrador/",
            dataType: "json",
            type: "post",
            data: { id: idAdministrador },
            beforeSend: function (data) {

            },
            success: function (data) {
                alert("Administrador deletado com sucesso!");
                EsconderMascara();
                EsconderJanelaModal("janelaModalAdministrador");
                location.reload();
            },
            error: function (data) {
                alert("Não foi possível deletar o administrador!")
                EsconderMascara();
                EsconderJanelaModal("janelaModalAdministrador");
            }
        });
    });

    /*-------------------------AJAX DELETAR FUNCIONARIO--------------------------------*/
    CriarJanelaModal("janelaModalFuncionario", "Aviso", "Deseja deletar o funcionário?", "botaoSimDeletarFuncionario", "botaoNaoDeletarFuncionario", "botaoFecharModalFuncionario");

    var idFuncionario = 0;
    jQuery(".linkDeletarFuncionario").click(function () {
        idFuncionario = jQuery(this).attr("id");
        MostrarMascara();
        MostrarJanelaModal("janelaModalFuncionario");
    });
    jQuery(".botaoNaoDeletarFuncionario").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalFuncionario");
    });
    jQuery(".botaoFecharModalFuncionario").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalFuncionario");
    });
    jQuery(".botaoSimDeletarFuncionario").livequery("click", function () {
        $.ajax({
            url: "/Administrativo/Funcionario/DeletarFuncionario/",
            dataType: "json",
            type: "post",
            data: { id: idFuncionario },
            beforeSend: function (data) {

            },
            success: function (data) {
                alert("Funcionário deletado com sucesso!");
                EsconderMascara();
                EsconderJanelaModal("janelaModalFuncionario");
                location.reload();
            },
            error: function (data) {
                alert("Não foi possível deletar o funcionário!")
                EsconderMascara();
                EsconderJanelaModal("janelaModalFuncionario");
            }
        });
    });


    /*-------------------------AJAX DELETAR SEGURADORA--------------------------------*/
    CriarJanelaModal("janelaModalSeguradora", "Aviso", "Deseja deletar o seguradora?", "botaoSimDeletarSeguradora", "botaoNaoDeletarSeguradora", "botaoFecharModalSeguradora");

    var idSeguradora = 0;
    jQuery(".linkDeletarSeguradora").click(function () {
        idSeguradora = jQuery(this).attr("id");
        MostrarMascara();
        MostrarJanelaModal("janelaModalSeguradora");
    });
    jQuery(".botaoNaoDeletarSeguradora").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalSeguradora");
    });
    jQuery(".botaoFecharModalSeguradora").livequery("click", function () {
        EsconderMascara();
        EsconderJanelaModal("janelaModalSeguradora");
    });
    jQuery(".botaoSimDeletarSeguradora").livequery("click", function () {
        $.ajax({
            url: "/Administrativo/Seguradora/DeletarSeguradora/",
            dataType: "json",
            type: "post",
            data: { id: idSeguradora },
            beforeSend: function (data) {

            },
            success: function (data) {
                alert("Seguradora deletada com sucesso!");
                EsconderMascara();
                EsconderJanelaModal("janelaModalSeguradora");
                location.reload();
            },
            error: function (data) {
                alert("Não foi possível deletar o seguradora!")
                EsconderMascara();
                EsconderJanelaModal("janelaModalSeguradora");
            }
        });
    });


    jQuery("#Estados").trigger("change");
    //BUSCAR CIDADES
    jQuery("#Estados").change(function () {
        var idCidadeFuncionario = jQuery("#idCidade").val();
        //alert("Alert id da cidade do funcionario " + idCidadeFuncionario);
        var idEstado = jQuery(this).selected().val();
        var url = jQuery(".urlBuscarCidade").val();
        $.post(url, { id: idEstado }, function (data) {
            jQuery(".listaCidades option").remove();
            for (var i = 0; i < data.tamanho; i++) {
                // if (data.idCidade[i].toString() == idCidade) {
                // alert("Achei a o cara");
                // jQuery(".listaCidades").append("<option selected='selected' value='" + data.idCidade[i].toString() + "'>" + data.nomeCidade[i] + "</option>");
                // }
                // else {
                jQuery(".listaCidades").append("<option value='" + data.idCidade[i].toString() + "'>" + data.nomeCidade[i] + "</option>");
                // }
            }
        });
    });
    //BOTAO LIMPAR CIDADES
    jQuery(".botaoLimparCidades").click(function () {
        jQuery(".listaCidades option").remove();
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
    //Formulario de cadastro e alteracao de administrador
    jQuery(".formularioCadastroAdministrador").submit(function () {
        var campoEmail = $.trim(jQuery(".campoEmailAdministrador").val());
        var campoSenha = $.trim(jQuery(".campoSenhaAdministrador").val());
        var campoNome = $.trim(jQuery(".campoNomeAdministrador").val());
        if (campoEmail == "" || campoSenha == "" || campoNome == "") {
            alert("Todos os campos são obrigatórios!");
            return false;
        }
    });


    //Validacao formulario de cadastro de parametro
    jQuery(".formularioParametro").submit(function () {
        var campoDescricao = $.trim(jQuery(".campoDescricaoParametro").val());
        var campoValor = $.trim(jQuery(".campoValorParametro").val());

        if (campoDescricao == "" || campoValor == "") {
            alert("Todos os campos são obrigatórios!");
            return false;
        }
    });


    //Formulario de cadastro de alteracao de funcionario
    jQuery(".formularioCadastroFuncionario").submit(function () {
        var campoEmail = $.trim(jQuery(".campoEmailFuncionario").val());
        var campoNome = $.trim(jQuery(".campoNomeFuncionario").val());
        var campoEndereco = $.trim(jQuery(".campoEnderecoFuncionario").val());
        var campoBairro = $.trim(jQuery(".campoBairroFuncionario").val());
        var campoCidade = $('.listaCidades option:selected').text();
        var campoSenha = $.trim(jQuery('.campoSenhaFuncionario').val());

        if (campoEmail == "" || campoNome == "" || campoEndereco == "" || campoBairro == "" || campoCidade == "" || campoSenha == "") {
            alert("Campos obrigatórios!\nNome\nEmail\nSenha\nEndereco\nBairro\nCidade");
            return false;
        }
    });

    //Formulario de cadastro de alteracao de Seguradora
    jQuery(".formularioCadastroSeguradora").submit(function () {
        var campoEmail = $.trim(jQuery(".campoEmailSeguradora").val());
        var campoNome = $.trim(jQuery(".campoNomeSeguradora").val());

        if (campoEmail == "" || campoNome == "") {
            alert("Campos obrigatórios!\nNome\nEmail");
            return false;
        }
    });

    function ValidarCheckBoxParametroCargo() {
        var marcados = 0;
        jQuery(".listaCargos").each(function () {
            if (jQuery(this).is(":checked")) {
                marcados++;
            }
        });
        return marcados;
    }

    //Configuração Parâmetros de Cargo
    var urlAlterarParametroCargo = jQuery(".urlAlterarParametroCargo").val()
    jQuery(".listaCargos").click(function () {
        var idCargo = jQuery(this).val();
        var ativo = "";

        var marcados = ValidarCheckBoxParametroCargo();

        if (marcados > 0) {
            if (jQuery(this).is(":checked")) {
                ativo = "S";
            }
            else {
                ativo = "N";
            }
            $.ajax({
                url: urlAlterarParametroCargo,
                dataType: "json",
                type: "post",
                data: { idCargo: idCargo, ativo: ativo },
                beforeSend: function (data) {
                    jQuery(".loadingParametros").toggle(400);
                },
                success: function (data) {
                    jQuery(".loadingParametros").toggle(400);
                },
                error: function (data) {
                    alert(data.mensagem.toString());
                }
            })
        }
        else {
            alert("Pelo menos um cargo deve estar marcado!");
            jQuery(this).attr("checked", "checked");
        }
    });


    //----------------------------------------AJAX PARA OS RELATORIOS-----------------------------\\

    jQuery(".porMarca").removeAttr("checked");
    jQuery(".porModelo").removeAttr("checked");

    jQuery(".porMarca").click(function () {
        if (jQuery(this).is(":checked")) {
            jQuery(".areaEscolhaMarca").toggle();
        }
        else {
            jQuery(".areaEscolhaMarca").toggle();
        }
    });


    jQuery(".porModelo").click(function () {
        if (jQuery(this).is(":checked")) {
            jQuery(".areaEscolhaModelo").toggle();
        }
        else {
            jQuery(".areaEscolhaModelo").toggle();
        }
    });





});