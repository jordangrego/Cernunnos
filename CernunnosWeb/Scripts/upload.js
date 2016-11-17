// Executa as funções após o carregamento da página
$(document).ready(function () {
    carregaUpload();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(carregaUpload);
});

function uploadComplete(sender, args) {
    try {
        /*Validation for file extension*/
        var fileExtension = args.get_fileName();

        if (fileExtension.indexOf(".") > -1) {
            fileExtension = fileExtension.substring(fileExtension.indexOf(".") + 1);
        }

        /*Validation for file size*/
        if (parseInt(args.get_length()) > (1024 * 1024 * 5)) {
            HideUpdateProgress();
            AlertaValidacao("O limite de tamanho do arquivo para anexação é de 5MB.");
            return;
        }

        /*Validation for file size*/
        if (fileExtension != "png") {
            HideUpdateProgress();
            AlertaValidacao("Somente são permitidos iamgens em formato PNG.");
            return;
        }

        $('#' + sender._inputFile.id).parent().parent().parent().parent().find(".texto-anexo").val(args.get_fileName());

        HideUpdateProgress();
    }
    catch (e) {
        AlertaErro(e.message);
    }
}

function uploadError(sender, args) {
    AlertaErro("Arquivos não carregado: " + args.get_errorMessage());
}

function uploadStarted(sender, args) {
    ShowUpdateProgress();
}

function carregaUpload() {
    $(".file-upload").mousemove(function (e) {
        var offL, offR, inpStart
        offL = $(this).offset().left;
        offT = $(this).offset().top;
        aaa = $(this).find("input[type=file]").width();
        $(this).find("input[type=file]").css({
            left: e.pageX - offL - 110,
            top: e.pageY - offT - 0
        })
    });
    /*
    $(".file-upload").find("input[type=file]").click(function () {
        if ($(".upload-habilitado").val() == "0") {
            $(".texto-anexo").removerErro({
                classe: "erroupload"
            });
            $(".texto-anexo").mostrarErro({
                classe: "erroupload",
                texto: "O registro deve ser salvo antes de anexar um documento."
            });
            return false;
        }
    });
    */
}