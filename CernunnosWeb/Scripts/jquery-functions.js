var btn = $.fn.button.noConflict() // reverts $.fn.button to jqueryui btn
$.fn.btn = btn // assigns bootstrap button functionality to $.fn.btn

var inputsChanged = false;
var inputfocus = '';

// Executa as funções após o carregamento da página
$(document).ready(function () {
    startUpFunctions();
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(startUpFunctions);
});

function beginRequest(sender, args) {
    ShowUpdateProgress();
}

// Remove o popup modal enquando a página é carregada (Ajax)
function endRequest(sender, args) {

    HideUpdateProgress();

    if (inputfocus != '') {
        SetFocus(inputfocus, true);
    }

    var error = args.get_error();
    if (error != null) {
        args.set_errorHandled(true);
        if (error.message.indexOf("VALIDACAO") == -1) {
            //AlertaErro(error.message);
            AlertaErro("<strong>Ocorreu um erro no sistema!</strong> Favor entrar em contato com a área responsável.");
        }
        else {
            args.set_errorHandled(true);
            AlertaValidacao("" + error.message.replace("Sys.WebForms.PageRequestManagerServerErrorException: VALIDACAO:", ""));
        }
    }
    else {
        $("#div-alerta-erro").remove();
    }
}

function ShowUpdateProgress() {
    // Exibe o popup modal enquando carrega a página (Ajax)
    var NewDialog = $('<div id="UpdateProgress" style="text-align:center;"><img src="/Cernunnos/Imagens/ajax-loader.gif" /><br />Carregando</div>');
    NewDialog.dialog({
        height: 78,
        width: 100,
        modal: true,
        resizable: false,
        zIndex: 1090,
        dialogClass: "no-close progress"
    });
}

function HideUpdateProgress() {
    $('#UpdateProgress').remove();
}

function startUpFunctions() {

    inputsChanged = false;

    $(window).scroll(function () {
        if ($(".progress").length > 0) {
            var set = ($(document).scrollTop() + ($(document).height() / 2) - $(".progress").width()) + 'px'
            $(".progress").animate({ top: set }, { duration: 500, queue: false });
        }
    });

    // Registra alteração no formulário
    $('form input, textarea, select').change(function () {
        inputsChanged = true;
    });

    $('form input').attr('autocomplete', 'off');

    $('.textarea[maxlength]').unbind("keydown");

    $("textarea[maxlength]").keydown(function (event) {
        return !$(this).attr("maxlength") || this.value.length < $(this).attr("maxlength") || event.keyCode == 8 || event.keyCode == 46;
    }).keyup(function (event) {
        var limit = $(this).attr("maxlength");

        if (!limit) return;

        if (this.value.length <= limit) return true;
        else {
            this.value = this.value.substr(0, limit);
            return false;
        }
    });

    $(".cancel").click(function () {
        if (inputsChanged) {
            $(this).confirmModal({
                heading: 'Confirmação',
                body: 'Tem certeza que deseja cancelar a operação?'
            });
            return false;
        }
    });

    $('.excluir').click(function () {
        $(this).confirmModal({
            heading: 'Atenção!',
            body: 'Após a confirmação essa operação não poderá ser desfeita. Deseja realmente excluir o registro?'
        });
        return false;
    });

    // Adciona um listener no Validator para incluir a classe de erro do Bootstrap
    if (typeof Page_Validators != 'undefined') {
        $.each(Page_Validators, function (index, validator) {
            $('#' + validator.id).attrchange({
                trackValues: true,
                callback: function (event) {
                    if (event.newValue.indexOf("inline") >= 0) {
                        $(this).mostrarErro();
                    }
                    else {
                        $(this).removerErro();
                    }
                    //event    	          - event object
                    //event.attributeName - Name of the attribute modified
                    //event.oldValue      - Previous value of the modified attribute
                    //event.newValue      - New value of the modified attribute
                    //Triggered when the selected elements attribute is added/updated/removed
                }
            });
        });
    }

    $(".accordion").accordion({
        collapsible: true
    });

    $('.data').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy"
    });

    $('.data-icon').datepicker({
        showOn: "button",
        buttonImage: "/PMS/Imagens/calendar.png",
        buttonImageOnly: true,
        buttonText: "Selecione uma data",
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy"
    }).css({
        float: 'left',
        marginRight: '5px',
        width: '80px'
    });

    $("img[class='ui-datepicker-trigger']").css({
        marginTop: '3px',
        cursor: 'pointer'
    });

    $('.data, .data-icon').unbind("blur");

    $('.data, .data-icon').mask("99/99/9999");
    $('.data, .data-icon').blur(function () {
        $(this).removerErro({
            classe: 'datainvalida'
        });

        if ($(this).val() != "") {
            if (!isData($(this).val())) {
                $(this).mostrarErro({
                    classe: 'datainvalida',
                    texto: 'Data inválida.'
                });
                $(this).focus();
            }
        }
    });

    $('.email').unbind("blur");

    $('.email').change(function () {
        $(this).removerErro({
            classe: 'emailInvalido'
        });

        if ($(this).val() != "") {
            if (!isEmail($(this).val())) {
                $(this).mostrarErro({
                    classe: 'emailInvalido',
                    texto: 'Email inválido.'
                });
                $(this).focus();
            }
        }
    });

    $('.hora').unbind("blur");

    $('.hora').mask("99:99");
    $('.hora').blur(function () {
        $(this).removerErro({
            classe: 'horainvalida'
        });
        if ($(this).val() != "") {
            if (!isHora($(this).val())) {
                $(this).mostrarErro({
                    classe: 'horainvalida',
                    texto: 'Hora inválida.'
                });
                $(this).focus();
            }
        }
    });

    $('.cpf').unbind("change");

    $('.cpf').mask("?999.999.999-99");
    $('.cpf').bind("change", function () {
        $(this).parents('.form-group').removeClass("has-error");
        $(this).parents('.form-element').removeClass("has-error");
        $(".cfpinvalido").remove();

        if (!isCPF($(this).val())) {
            $(this).parents('.form-group').addClass("has-error");
            $(this).parents('.form-element').addClass("has-error");
            $("<span class='help-block cfpinvalido'>CPF inválido.</span>").insertAfter(this);
            $(this).val("");
            //Trata bug dos caracteres aparecerem novamente.
            $('.cpf').mask("?999.999.999-99");
            $(this).focus();
        }
    });

    $('.cnpj').unbind("change");

    $('.cnpj').mask("?99.999.999/9999-99");
    $('.cnpj').bind("change", function () {
        $(this).parents('.form-group').removeClass("has-error");
        $(this).parents('.form-element').removeClass("has-error");
        $(".cnjpinvalido").remove();

        if (!isCNPJ($(this).val())) {
            $(this).parents('.form-group').addClass("has-error");
            $(this).parents('.form-element').addClass("has-error");
            $("<span class='help-block cnjpinvalido'>CNPJ inválido.</span>").insertAfter(this);
            $(this).val("");
            //Trata bug dos caracteres aparecerem novamente.
            $('.cnpj').mask("?99.999.999/9999-99");
            $(this).focus();
        }
    });

    $('.cep').mask("99.999-999");

    $('.telefone').mask("(99) 99999999?99", { placeholder: " " });

    $('.numero').unbind("paste");

    $(".numero").keypress(function (event) {
        var key = (event.keyCode ? event.keyCode : event.which);
        var controlKeys = [8, 9, 13];
        var isControlKey = controlKeys.join(",").match(new RegExp(key));
        if ((48 <= key && key <= 57) || isControlKey) {
            return;
        }
        else {
            event.preventDefault();
        }
    }).bind({
        paste: function () {
            setTimeout(jQuery.proxy(function () {
                this.val(this.val().replace(/[^0-9]/g, ''));
            }, $(this)), 0);
        }
    });

    $('.quantidade').unbind("paste");

    $('.quantidade').css("text-align", "right").keypress(function (event) {
        var key = (event.keyCode ? event.keyCode : event.which);
        var controlKeys = [8, 9, 13];
        var isControlKey = controlKeys.join(",").match(new RegExp(key));
        if ((48 <= key && key <= 57) || isControlKey) {
            return;
        }
        else {
            event.preventDefault();
        }
    }).bind({
        paste: function () {
            setTimeout(jQuery.proxy(function () {
                this.val(this.val().replace(/[^0-9]/g, ''));
            }, $(this)), 0);
        }
    });

    $('.valor').maskValor();

    $('.inteiro').maskValor({
        decimais: 0
    });

    $('.caixa').unbind("keyup");
    $('.caixa').unbind("paste");

    $('.caixa').keyup(function (e) {
        var key = (event.keyCode ? event.keyCode : event.which);
        var max = $(this).attr("maxlength");
        if ((key >= 48 && key <= 57) && $(this).val().length >= max) {
            var inputs = $(this).closest('form').find(':input:visible');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    }).bind({
        paste: function () {
            setTimeout(jQuery.proxy(function () {
                var max = this.attr("maxlength");
                if (this.val().length >= max) {
                    var inputs = $(".caixa"); //this.closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                }
            }, $(this)), 0);
        }
    });

    $(".metadado").keypress(function (event) {
        var key = (event.keyCode ? event.keyCode : event.which);
        var controlKeys = [8, 9, 13, 95];
        var isControlKey = controlKeys.join(",").match(new RegExp(key));

        if ((key < 65 || key > 90) && (key < 97 || key > 123) && key != 32 && !isControlKey) {
            event.preventDefault();
        }
        else {
            return;
        }
    }).bind({
        paste: function () {
            setTimeout(jQuery.proxy(function () {
                this.val(this.val().replace(/[_\W]+/g, ''));
            }, $(this)), 0);
        }
    });

    $("input[data-toggle='tooltip']").tooltip();
    $("a[data-toggle='tooltip']").tooltip();
    $("textarea[data-toggle='tooltip']").tooltip();

    //Configura o enter para executar a açao do botão promário.
    $(".modal:visible").each(function () {
        var div = $(this)[0].id;
        $(this).find(".btn-primary").clickOnEnter({
            element: div
        });
    });
}

function SetFocus(elemento, executar) {
    inputfocus = elemento;

    if (executar) {
        setTimeout(function () {
            $("#" + elemento).focus();
        }, 300);
    }
}

function isCPF(cpf) {
    //var cpf = Objcpf.value;
    exp = /\.|\-/g
    cpf = cpf.toString().replace(exp, "");
    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0, soma2 = 0;
    var vlr = 11;

    for (i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);

    var digitoGerado = (soma1 * 10) + soma2;
    if (digitoGerado != digitoDigitado) {
        return false;
    }
    else {
        return true;
    }
}

//valida o CNPJ digitado
function isCNPJ(cnpj) {
    //var cnpj = ObjCnpj.value;
    var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    var dig1 = new Number;
    var dig2 = new Number;

    exp = /\.|\-|\//g
    cnpj = cnpj.toString().replace(exp, "");
    var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

    for (i = 0; i < valida.length; i++) {
        dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
        dig2 += cnpj.charAt(i) * valida[i];
    }
    dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
    dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

    if (((dig1 * 10) + dig2) != digito) {
        return false;
    }
    else {
        return true;
    }
}

function isData(dateString) {
    var dt = parseInt(dateString.substring(0, 2));
    var mon = parseInt(dateString.substring(3, 5)) - 1;
    var yr = parseInt(dateString.substring(6, 10));
    var hor = 0;
    var min = 0;

    if (dateString.length == 16) {
        hor = parseInt(dateString.substring(11, 13));
        min = parseInt(dateString.substring(14, 16));
    }

    var d = new Date(yr, mon, dt, hor, min)

    return (d.getFullYear() == yr && d.getMonth() == mon && d.getDate() == Number(dt) && d.getHours() == hor && d.getMinutes() == min);
}

function isHora(timeString) {

    var hor = parseInt(timeString.substring(0, 2));
    var min = parseInt(timeString.substring(3, 5));

    var d = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDay(), hor, min);

    return (d.getHours() == hor && d.getMinutes() == min);
}

function isEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function ToDate(dateStr) {
    if (dateStr != "" && typeof dateStr != 'undefined') {
        var dateSplit = dateStr.split('/');
        return new Date(dateSplit[2], dateSplit[1] - 1, dateSplit[0]);
    }
    else {
        return new Date();
    }

}

function StringToDate(dateString) {
    var dt = parseInt(dateString.substring(0, 2));
    var mon = parseInt(dateString.substring(3, 5)) - 1;
    var yr = parseInt(dateString.substring(6, 10));
    var hor = 0;
    var min = 0;

    if (dateString.length == 16) {
        hor = parseInt(dateString.substring(11, 13));
        min = parseInt(dateString.substring(14, 16));
    }

    return new Date(yr, mon, dt, hor, min)
}

function DateToMonthYear(date) {

    var dd = date.getDate();
    var mm = date.getMonth() + 1; //January is 0!
    var yyyy = date.getFullYear();

    return mm + '/' + yyyy;
}

function GetMonth(monthYear) {
    if (monthYear.indexOf('/') > -1) {
        var dateSplit = monthYear.split('/');
        var month = dateSplit[0];

        //Tratar bug IE
        if (month.indexOf('0') == 0)
            month = month.replace("0", "");

        var intMonth = parseInt(month);
        return (intMonth - 1);
    }
    return 0;
}

function GetYear(monthYear) {
    if (monthYear.indexOf('/') > -1) {
        var dateSplit = monthYear.split('/');
        var year = dateSplit[1];
        var intYear = parseInt(year);
        return intYear;
    }
    return 0;
}

var current_zindex = 1050;

function ShowModalDialog(div) {
    $('#' + div).modal({ show: true, backdrop: 'static', keyboard: true });

    $('body').css('overflow', 'hidden');

    //Faz o tratamento de modal sobre modal
    $('.modal-backdrop.fade.in:last-child').css('z-index', current_zindex);
    current_zindex += 10;
    $('#' + div).css('z-index', current_zindex);

    $("#" + div).find(".btn-primary").clickOnEnter({
        element: div
    });
}

function ShowInnerModalDialog(div) {
    $('#' + div).modal({ show: true, backdrop: 'static', keyboard: true });

    $('body').css('overflow', 'hidden');

    //Faz o tratamento de modal sobre modal
    $('#' + div).parent().prepend($('.modal-backdrop.fade.in:last-child').detach());
    $('.modal-backdrop.fade.in:last-child').css('z-index', current_zindex);
    current_zindex += 10;
    $('#' + div).css('z-index', current_zindex);
}

function AppendModalDialogOnBody(div) {
    $(document.body).append($('#' + div).detach());
}

function HideModalDialog(div) {
    var zindex = $('#' + div).css('z-index') - 10;

    $('#' + div).modal('hide');

    setTimeout(function () {
        //Remove o modal-backdrop se o popup for fechado erradamente pelo post do ajax.
        if ($(".modal:visible").length == 0 && $('.modal-backdrop').length > 0) {
            $('.modal-backdrop').remove();
        }

        $('.modal:visible').css('overflow', 'auto');

        /*
        $('.modal-backdrop').each(function () {
            if ($(this).css('z-index') == zindex) {
                $(this).remove();
            }
        });
        */
    }, 400);

    //Mostra a barra de rolagem se não tiver mais nenhum modal.
    if ($('.modal-backdrop.fade.in').length <= 1) {
        $('body').css('overflow', 'auto');
    }
}

function ShowAlertaModal(mensagem, url, css) {

    var idModal = "dataAlertModal" + (current_zindex += 10);
    var idOkModal = "dataAlertOK" + (current_zindex += 10);

    var html = '<div id="' + idModal + '" class="modal fade">' +
                '<div class="modal-dialog alert #css#">' +
                    '<h4>#Heading#</h4>' +
                    '<p>#Body#</p>' +
                    '<div class="modal-footer">' +
                        '<a class="btn btn-default" id="' + idOkModal + '">Fechar</a>' +
                    '</div>' +
                '</div>' +
            '</div>';

    html = html.replace('#Heading#', '').replace('#Body#', mensagem).replace('#css#', css);

    $('body').append(html);

    ShowModalDialog(idModal);

    $('#' + idOkModal).click(function () {

        HideModalDialog(idModal);

        setTimeout(function () {
            if (typeof url != 'undefined') {
                window.location = url;
            }
        }, 300);
    });
}

function AlertaSucesso(mensagem, url) {
    ShowAlertaModal(mensagem, url, 'alert-success');
}

function AlertaErro(mensagem, url) {
    ShowAlertaModal(mensagem, url, 'alert-danger');
}

function AlertaValidacao(mensagem, url) {
    ShowAlertaModal(mensagem, url, 'alert-warning');
}

function ShowReport(title) {
    $('body').css('overflow', 'hidden');

    if ($("body").find("#dataReportModal").length == 0) {

        var reportModal = '<div id="dataReportModal" class="modal fade">' +
                            '<div class="modal-dialog modal-lg">' +
                                '<div class="modal-content">' +
                                    '<div class="modal-header">' +
                                        '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>' +
                                        '<h4 class="modal-title">#Heading#</h4>' +
                                    '</div>' +
                                    '<div id="reportBody" class="modal-body">' +
                                    '</div>' +
                                '</div>' +
                            '</div>' +
                        '</div>';

        reportModal = reportModal.replace('#Heading#', title);

        $("body").append(reportModal);
        $("#reportBody").append($("#report"));

        $("#dataReportModal .modal-dialog").width("80%");
        $("#dataReportModal .modal-dialog").css("min-width", "844px");
        $("#dataReportModal .modal-body").height($(window).height() - 110);
        $("#dataReportModal .modal-dialog").css("margin-top", "5px");
        $("#dataReportModal").css('overflow', 'hidden');
    }

    $('#dataReportModal .modal-title').text(title);

    $('#dataReportModal').modal({
        show: true,
        backdrop: 'static',
        outline: 'none',
        top: '1'
    });

    $('#report').show();

    $('#dataReportModal .close').click(function () {
        $("#dataReportModal").modal('hide');
        //Mostra a barra de rolagem se não tiver mais nenhum modal.
        if ($('.modal-backdrop.fade.in').length <= 1) {
            $('body').css('overflow', 'auto');
        }
    });
}

function NotificacaoSucesso(text) {
    var n = noty({
        text: text,
        type: 'success',        //alert  success  error  warning  information  confirm 
        dismissQueue: true,
        layout: 'topCenter',
        theme: 'defaultTheme',  //bootstrapTheme; defaultTheme
        timeout: 1000
    });
    return n;
}

function CriaBreadCrumbItemComposto() {
    var html = "<li><a href='/Protocolo/Default.aspx'>Página Inicial</a></li><li class='active'>Home</li><li class='active'><a href='/Protocolo/Itens/ItensPesquisa.aspx'>Itens</a></li><li class='active'>Item Componentizado</li>"
    $('.breadcrumb').html(html);
}
