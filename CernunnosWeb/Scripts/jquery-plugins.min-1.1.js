(function(b){function a(){}b.fn.extend({confirmModal:function(c){var d='<div id="dataConfirmModal" class="modal fade"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h4 class="modal-title">#Heading#</h4></div><div class="modal-body"><p>#Body#</p></div><div class="modal-footer"><a class="btn btn-default" id="dataConfirmCancelar">Cancelar</a><a class="btn btn-primary" id="dataConfirmOK">Confirmar</a></div></div></div></div>';c=b.extend({heading:"Confirma\u00e7\u00e3o",
body:"Deseja realmente excluir o registro selecionado?",href:"",confirmCallback:a},c);d=d.replace("#Heading#",c.heading).replace("#Body#",c.body);""==c.href&&(c.href=b(this).attr("href"));b("body").append(d);b("#dataConfirmOK").attr("href",c.href).click(function(){HideModalDialog("dataConfirmModal");setTimeout(function(){b("#dataConfirmModal").remove()},300)});b("#dataConfirmCancelar").click(function(){HideModalDialog("dataConfirmModal");c.confirmCallback();setTimeout(function(){b("#dataConfirmModal").remove()},
300)});ShowModalDialog("dataConfirmModal")}})})(jQuery);
(function(b){b.fn.extend({mostrarErro:function(a){var c="<span class='help-block error #class#'>#texto#</span>";a=b.extend({classe:"",texto:""},a);""!=a.texto&&(c=c.replace("#texto#",a.texto).replace("#class#",a.classe),b(this).nextAll("."+a.tag).remove(),b(c).insertAfter(this));0<b(this).parents(".form-element").length?b(this).parents(".form-element").addClass("has-error"):0<b(this).parents(".form-group").length?b(this).parents(".form-group").addClass("has-error"):(b(this).html(),AlertaValidacao(b(this).html()),
b(this).hide());b(this).val("")}})})(jQuery);(function(b){b.fn.extend({removerErro:function(a){a=b.extend({classe:""},a);0==b(this).parent().find(".help-block:visible").length&&(""==a.classe||0<b(this).nextAll("."+a.classe).length)&&(0<b(this).parents(".form-element").length?b(this).parents(".form-element").removeClass("has-error"):b(this).parents(".form-group").removeClass("has-error"));""!=a.classe&&b(this).nextAll("."+a.classe).remove()}})})(jQuery);
(function(b){b.fn.extend({setCursorPosition:function(a){b(this).each(function(b,d){if(d.setSelectionRange)d.focus(),d.setSelectionRange(a,a);else if(d.createTextRange){var e=d.createTextRange();e.collapse(!0);e.moveEnd("character",a);e.moveStart("character",a);e.select()}})}})})(jQuery);
function formataValor(b,a){a="undefined"!==typeof a?a:2;var c=-1<b.indexOf("-")?"-":"",d=b.replace(/[^0-9]/g,""),e=d.slice(0,d.length-a),e=e.replace(/^0*/g,""),e=e.replace(/\B(?=(\d{3})+(?!\d))/g,".");""===e&&(e="0");c+=e;d=d.slice(d.length-a);e=Array(2-d.length).join(0);0<a&&(c+=","+e+d);return c}
(function(b){b.fn.extend({maskValor:function(){b(this).css("text-align","right").keydown(function(a){a=a||window.event;a=a.which||a.charCode||a.keyCode;if(void 0===a)return!1;if(8===a||46===a||63272===a){if(""==b(this).val())return!1;a=b(this).val();var c=a.length-1,d=c+1;a=a.substring(0,c)+a.substring(d,a.length);b(this).val(formataValor(a));"0,00"==b(this).val()&&b(this).val("");return!1}return 0<b(this).attr("maxlength").length&&b(this).val().length>=b(this).attr("maxlength")&&"0"!=b(this).val().substring(0,
1)?!1:9==a||48<=a&&57>=a||96<=a&&105>=a}).keypress(function(a){a=a||window.event;a=a.which||a.charCode||a.keyCode;if(void 0===a)return!1;a=String.fromCharCode(a);a=b(this).val()+a;b(this).val(formataValor(a));return!1}).click(function(){b(this).setCursorPosition(b(this).val().length)})}})})(jQuery);
jQuery(function(b){b.datepicker.regional["pt-BR"]={closeText:"Fechar",prevText:"&#x3c;Anterior",nextText:"Pr&oacute;ximo&#x3e;",currentText:"Hoje",monthNames:"Janeiro Fevereiro Mar&ccedil;o Abril Maio Junho Julho Agosto Setembro Outubro Novembro Dezembro".split(" "),monthNamesShort:"Jan Fev Mar Abr Mai Jun Jul Ago Set Out Nov Dez".split(" "),dayNames:"Domingo Segunda-feira Ter&ccedil;a-feira Quarta-feira Quinta-feira Sexta-feira Sabado".split(" "),dayNamesShort:"Dom Seg Ter Qua Qui Sex Sab".split(" "),
dayNamesMin:"Dom Seg Ter Qua Qui Sex Sab".split(" "),weekHeader:"Sm",dateFormat:"dd/mm/yy",firstDay:0,isRTL:!1,showMonthAfterYear:!1,yearSuffix:""};b.datepicker.setDefaults(b.datepicker.regional["pt-BR"])});
(function(b){b.fn.extend({clickOnEnter:function(a){if(0<b(this).length){a=b.extend({element:""},a);var c=document.getElementById(b(this)[0].id);a=b("#"+a.element+" input:not([type=button],[type=submit],[type=hidden],[type=image]), #"+a.element+" select");0<a.length&&!a.first().hasClass("data")&&SetFocus(a.first()[0].id);a.each(function(){b(this).keypress(function(a){if(13==(event.keyCode?event.keyCode:event.which)&&c){if(c.click)return c.click(),!1;if(c.onclick)return c.onclick(),!1}})})}}})})(jQuery);
(function(){try{var b=console;Object.defineProperty(window,"console",{get:function(){if(b._commandLineAPI)throw"Sorry, for security reasons, the script console is deactivated";return b},set:function(a){b=a}})}catch(a){}})();