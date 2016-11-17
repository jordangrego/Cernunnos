/*------EXIBE O MODAL DE CONFIRMAÇÃO DO CLICK--------*/
(function ($) {
    $.fn.extend({
        //pass the options variable to the function
        confirmModal: function (options) {
            var html = '<div id="dataConfirmModal" class="modal fade">' +
                              '<div class="modal-dialog">' +
                                '<div class="modal-content">' +
                                  '<div class="modal-header">' +
                                    '<h4 class="modal-title">#Heading#</h4>' +
                                  '</div>' +
                                  '<div class="modal-body">' +
                                    '<p>#Body#</p>' +
                                  '</div>' +
                                  '<div class="modal-footer">' +
                                    '<a class="btn btn-default" id="dataConfirmCancelar">Cancelar</a>' +
                                    '<a class="btn btn-primary" id="dataConfirmOK">Confirmar</a>' +
                                  '</div>' +
                                '</div>' +
                              '</div>' +
                            '</div>';

            var defaults = {
                heading: 'Confirmação',
                body: 'Deseja realmente excluir o registro selecionado?',
                href: '',
                confirmCallback: defaultCallback
            };

            var options = $.extend(defaults, options);
            html = html.replace('#Heading#', options.heading).replace('#Body#', options.body);

            if (options.href == '') {
                options.href = $(this).attr('href');
            }

            $('body').append(html);

            $('#dataConfirmOK').attr('href', options.href).click(function () {
                //$("#dataConfirmModal").modal('hide');

                HideModalDialog('dataConfirmModal');

                options.confirmCallback();
                setTimeout(function () {
                    $('#dataConfirmModal').remove();
                }, 300);
            });

            $('#dataConfirmCancelar').click(function () {
                //$("#dataConfirmModal").modal('hide');

                HideModalDialog('dataConfirmModal');

                options.confirmCallback();
                setTimeout(function () {
                    $('#dataConfirmModal').remove();
                }, 300);
            });

            ShowModalDialog('dataConfirmModal');
        }
    });

    function defaultCallback() {
        //
    }

})(jQuery);

/*------INCLUE O CSS DE ERRO--------*/
(function ($) {
    $.fn.extend({
        //pass the options variable to the function
        mostrarErro: function (options) {
            var html = "<span class='help-block error #class#'>#texto#</span>";

            var defaults = {
                classe: '',
                texto: ''
            };

            var options = $.extend(defaults, options);

            if (options.texto != "") {
                html = html.replace('#texto#', options.texto).replace('#class#', options.classe);

                //Evita duplicidade
                $(this).nextAll('.' + options.tag).remove();

                //Insere erro
                $(html).insertAfter(this);
            }

            if ($(this).parents('.form-element').length > 0) {
                $(this).parents('.form-element').addClass("has-error");
            }
            else if ($(this).parents('.form-group').length > 0) {
                $(this).parents('.form-group').addClass("has-error");
            }
            else {
                var texto = $(this).html();
                AlertaValidacao($(this).html());
                $(this).hide();
            }

            $(this).val("");
        }
    });
})(jQuery);

/*------REMOVE O CSS DE ERRO--------*/
(function ($) {
    $.fn.extend({
        //pass the options variable to the function
        removerErro: function (options) {

            var defaults = {
                classe: ''
            };

            var options = $.extend(defaults, options);

            if ($(this).parent().find('.help-block:visible').length == 0 && (options.classe == "" || $(this).nextAll('.' + options.classe).length > 0)) {
                if ($(this).parents('.form-element').length > 0) {
                    $(this).parents('.form-element').removeClass("has-error");
                }
                else {
                    $(this).parents('.form-group').removeClass("has-error");
                }
            }

            if (options.classe != "") {
                //Remove a descrição do erro
                $(this).nextAll('.' + options.classe).remove();
            }
        }
    });
})(jQuery);

/*----MUDA O CORSOR PARA A POSIÇÃO INDICADA-----*/
(function ($) {
    $.fn.extend({
        setCursorPosition: function (pos) {
            $(this).each(function (index, elem) {
                if (elem.setSelectionRange) {
                    elem.focus();
                    elem.setSelectionRange(pos, pos);
                } else if (elem.createTextRange) {
                    var range = elem.createTextRange();
                    range.collapse(true);
                    range.moveEnd("character", pos);
                    range.moveStart("character", pos);
                    range.select();
                }
            });
        }
    });
})(jQuery);

/*-----FORMARA O VALOR COM CASA DE MILHAR E DECIMAL------*/
function formataValor(value, dec) {

    dec = typeof dec !== 'undefined' ? dec : 2;

    var negative = (value.indexOf("-") > -1) ? "-" : "",
            onlyNumbers = value.replace(/[^0-9]/g, ""),
            integerPart = onlyNumbers.slice(0, onlyNumbers.length - dec),
            newValue,
            decimalPart,
            leadingZeros;

    // remove initial zeros
    integerPart = integerPart.replace(/^0*/g, "");
    // put settings.thousands every 3 chars
    integerPart = integerPart.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    if (integerPart === "") {
        integerPart = "0";
    }
    newValue = negative + integerPart;

    decimalPart = onlyNumbers.slice(onlyNumbers.length - dec);
    leadingZeros = new Array((2) - decimalPart.length).join(0);

    if (dec > 0) {
        newValue += "," + leadingZeros + decimalPart;
    }

    return newValue;
}

/*----APLICA A MARCARA PARA CAMPOS DE VALOR MONETÁRIO-----*/
(function ($) {
    $.fn.extend({
        maskValor: function (options) {

            var defaults = {
                decimais: 2
            };

            var options = $.extend(defaults, options);

            $(this).css("text-align", "right").keydown(function (e) {
                e = e || window.event;
                var key = e.which || e.charCode || e.keyCode;

                //needed to handle an IE "special" event
                if (key === undefined) {
                    return false;
                }

                //backspace, delete, 
                if (key === 8 || key === 46 || key === 63272) {

                    if ($(this).val() == "") {
                        return false;
                    }

                    var value = $(this).val();
                    var startPos = value.length - 1;
                    var endPos = startPos + 1;

                    value = value.substring(0, startPos) + value.substring(endPos, value.length);

                    $(this).val(formataValor(value, options.decimais));

                    if ($(this).val() == "0,00") {
                        $(this).val("");
                    }

                    return false;
                }
                else {
                    if ($(this).attr("maxlength").length > 0) {
                        if ($(this).val().length >= $(this).attr("maxlength") && $(this).val().substring(0, 1) != "0") {
                            return false;
                        }
                    }
                }

                // allow tab, numbers and keypad numbers ONLY
                return (key == 9 ||
                        (key >= 48 && key <= 57) ||
                        (key >= 96 && key <= 105));

            }).keypress(function (e) {
                e = e || window.event;
                var key = e.which || e.charCode || e.keyCode;

                //added to handle an IE "special" event
                if (key === undefined) {
                    return false;
                }

                var keyPressedChar = String.fromCharCode(key);
                var value = $(this).val() + keyPressedChar;

                $(this).val(formataValor(value, options.decimais));

                return false;

            }).click(function () {
                $(this).setCursorPosition($(this).val().length);
            });
        }

    });

})(jQuery);

/* Altera o datepicker para portugues brasil */
jQuery(function ($) {
    $.datepicker.regional['pt-BR'] = {
        closeText: 'Fechar',
        prevText: '&#x3c;Anterior',
        nextText: 'Pr&oacute;ximo&#x3e;',
        currentText: 'Hoje',
        monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
        dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 0,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['pt-BR']);
});

(function ($) {
    $.fn.extend({
        clickOnEnter: function (options) {
            if ($(this).length > 0) {
                var defaults = {
                    element: ''
                };

                var options = $.extend(defaults, options);
                var button = document.getElementById($(this)[0].id);
                var inputs = $("#" + options.element + " input:not([type=button],[type=submit],[type=hidden],[type=image], .not-enter), #" + options.element + " select");

                if (inputs.length > 0 && !inputs.first().hasClass("data"))
                {
                    SetFocus(inputs.first()[0].id);
                }
    
                inputs.each(function(){
                    $(this).keypress(function (e) {
                        var key = (event.keyCode ? event.keyCode : event.which);
                        if (key == 13) {
                            if (button) {
                                if (button.click) {
                                    button.click();
                                    return false;
                                }
                                else if (button.onclick) {
                                    button.onclick();
                                    return false;
                                }
                            }
                        }
                    });
                });

                var inputsNotEnter = $(".not-enter");
                inputsNotEnter.each(function(){
                    $(this).keypress(function (e) {
                        var key = (event.keyCode ? event.keyCode : event.which);
                        if (key == 13) {
                            return false;
                        }
                    });
                });
            }
        }
    });
})(jQuery);

(function () {
    try {
        var $_console$$ = console;
        Object.defineProperty(window, "console", {
            get: function () {
                if ($_console$$._commandLineAPI)
                    throw "Sorry, for security reasons, the script console is deactivated";
                return $_console$$
            },
            set: function ($val$$) {
                $_console$$ = $val$$
            }
        })
    } catch ($ignore$$) {
    }
})();
