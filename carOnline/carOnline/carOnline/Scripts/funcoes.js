/// <reference path="jquery-1.8.3.js" />
/// <reference path="jquery.livequery.js" />

    function CriarJanelaModal(classePrincipal,textoCabecalho, textCorpo, classeBotaoSim, classeBotaoNao,classeBotaoFechar) {
        jQuery("body").append("" +
           "<div class='janela " + classePrincipal + "'>" +
           "<div class='cabecalhoJanela'>" + textoCabecalho + "<span class='botaoFechar " + classeBotaoFechar + "'>X</div> " +
           "<div class='corpoJanela textoMensagem'>"+textCorpo+"</div>"+
           "<div class='areaBotoes'><a href='#' class='botoesJanelaModal "+classeBotaoSim+"'>Sim</a><a href='#' class='botoesJanelaModal "+classeBotaoNao+"'>Não</a></div>");
    }
    function MostrarJanelaModal(classePrincipal) {
        jQuery("." + classePrincipal + "").show();
    }
    function EsconderJanelaModal(classePrincipal) {
        jQuery("." + classePrincipal + "").hide();
    }
    function EsconderMascara() {
        $('.mascara').hide();
    }
    function MostrarMascara(){
        var id = $(".linkAuxiliarMascara").attr("href");
        var alturaTela = $(document).height();
        var larguraTela = $(window).width();

        //colocando o fundo preto
        $('.mascara').css({ 'width': larguraTela, 'height': alturaTela });
        //$('#mascara').fadeIn(600);
        $('.mascara').fadeTo("fast", 0.8);

        var left = ($(window).width() / 2) - ($(id).width() / 2);
        var top = ($(window).height() / 2) - ($(id).height() / 2);

        $(id).css({ 'top': top, 'left': left });
        $(id).show();
    }

    $(document).ready(function () {

        // Desabilita click de uma ancora
        $('.botao_link').click(function (e) {
            e.preventDefault();
            $('.formularioUsuario').submit();
        });
    });