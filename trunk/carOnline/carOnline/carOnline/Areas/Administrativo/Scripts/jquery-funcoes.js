$(document).ready(function() {	
  if($(".center").width()<=990 && $(".center").width()>800){
	   	$("#projeto").width($(".center").width()-450);
		$("#grupo_quest").width($(".center").width()-450);
		$("#grafico").width(416);
		$(".bloco_branco").width(400);
		$("#grafico").css("float","right");
		$(".question_white").width($(".center").width()-20);
		$(".question_cinza").width($(".center").width()-20);
		$(".result1").height($(".resposta1").height()-20);
		$(".result2").height($(".resposta2").height()-20);
		$(".result3").height($(".resposta3").height()-20);
		$(".result4").height($(".resposta4").height()-20);
			
		
   }else if($(".center").width()<=800){   
		$("#projeto").width($(".center").width()-20);
		$("#grupo_quest").width($(".center").width()-20);
		$("#grafico").width($(".center").width()-20);
		$(".bloco_branco").width("100%");
		$("#grafico").css("float","left");
		$(".result1").height($(".resposta1").height()-20);
		$(".result2").height($(".resposta2").height()-20);
		$(".result3").height($(".resposta3").height()-20);
		$(".result4").height($(".resposta4").height()-20);
		
	}else{
		$("#projeto").width(550);
		$("#grupo_quest").width(550);
		$("#grafico").width(416);
		$(".bloco_branco").width(400);
		$("#grafico").css("float","right");
		$(".question_white").width($(".center").width());
		$(".question_cinza").width($(".center").width());
		$(".result1").height($(".resposta1").height()-20);
		$(".result2").height($(".resposta2").height()-20);
		$(".result3").height($(".resposta3").height()-20);
		$(".result4").height($(".resposta4").height()-20);	
	}
	
	
});

$(window).resize(function(){

  if($(".center").width()<=990 && $(".center").width()>800){
	   	$("#projeto").width($(".center").width()-450);
		$("#grupo_quest").width($(".center").width()-450);
		$("#grafico").width(416);
		$(".bloco_branco").width(400);
		$("#grafico").css("float","right");
		$(".question_white").width($(".center").width()-20);
		$(".question_cinza").width($(".center").width()-20);
		$(".result1").height($(".resposta1").height()-20);
		$(".result2").height($(".resposta2").height()-20);
		$(".result3").height($(".resposta3").height()-20);
		$(".result4").height($(".resposta4").height()-20);
			
		
   }else if($(".center").width()<=800){   
		$("#projeto").width($(".center").width()-20);
		$("#grupo_quest").width($(".center").width()-20);
		$("#grafico").width($(".center").width()-20);
		$(".bloco_branco").width("100%");
		$("#grafico").css("float","left");
		$(".result1").height($(".resposta1").height()-20);
		$(".result2").height($(".resposta2").height()-20);
		$(".result3").height($(".resposta3").height()-20);
		$(".result4").height($(".resposta4").height()-20);
		
	}else{
		$("#projeto").width(550);
		$("#grupo_quest").width(550);
		$("#grafico").width(416);
		$(".bloco_branco").width(400);
		$("#grafico").css("float","right");
		$(".question_white").width($(".center").width());
		$(".question_cinza").width($(".center").width());
		$(".result1").height($(".resposta1").height()-20);
		$(".result2").height($(".resposta2").height()-20);
		$(".result3").height($(".resposta3").height()-20);
		$(".result4").height($(".resposta4").height()-20);	
	}

});








