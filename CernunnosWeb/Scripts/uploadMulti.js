// Executa as funções após o carregamento da página
$(document).ready(function () {
    carregaUpload();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(carregaUpload);
});

function uploadComplete(sender, args) {
    try {

        /*Validation for file size*/
        if (parseInt(args.get_fileSize()) > (1024 * 1024 * 20)) {
            AlertaErro("Arquivos devem ser menor que 20.00MB");
            return;
        }

        HideUpdateProgress();
    }
    catch (e) {
        AlertaErro(e.message);
    }
}

function uploadError(sender, args) {
    AlertaErro("Ocorre um erro ao fazer o upload do arquivo.");
}

function uploadStarted(sender, args) {
    ShowUpdateProgress();
}

function startUpload() {

    setTimeout(function () { $('.ajax__fileupload_uploadbutton').trigger('click') }, 300);

}

function carregaUpload() {

    //Formata os botões utilizando o css do bootstrap
    $('.ajax__fileupload_selectFileButton').addClass("btn btn-default btn-sm ajax_custom_selectFileButton");
    $('.ajax_custom_selectFileButton').removeClass("ajax__fileupload_selectFileButton");
    $('.ajax__fileupload_uploadbutton').addClass("btn btn-default btn-sm");
    $('.ajax__fileupload_uploadbutton').css("display", "none");

    //Altera os labels do componente de upload para pt-BR
    Sys.Extended.UI.Resources.AjaxFileUpload_SelectFile = "Selecione"; // Select Files text change
    Sys.Extended.UI.Resources.AjaxFileUpload_DropFiles = "Arraste e solte arquivos"; //Drop files here text change
    Sys.Extended.UI.Resources.AjaxFileUpload_Pending = "pendente"; //pending
    Sys.Extended.UI.Resources.AjaxFileUpload_Remove = "Remover"; //Remove
    Sys.Extended.UI.Resources.AjaxFileUpload_Upload = "Carregar"; //Upload
    Sys.Extended.UI.Resources.AjaxFileUpload_Uploaded = "Uploaded"; //Uploaded
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadedPercentage = "{0} %"; //uploaded {0} %
    Sys.Extended.UI.Resources.AjaxFileUpload_Uploading = "Carregando"; //Uploading
    Sys.Extended.UI.Resources.AjaxFileUpload_FileInQueue = "{0} arquivo(s)."; //{0} file(s) in queue.
    Sys.Extended.UI.Resources.AjaxFileUpload_AllFilesUploaded = ""; //All Files Uploaded.
    Sys.Extended.UI.Resources.AjaxFileUpload_FileList = "Lista dos arquivos carregados:"; //List of Uploaded files:
    Sys.Extended.UI.Resources.AjaxFileUpload_SelectFileToUpload = "Por favor, selecione o(s) arquivo(s) para fazer o upload."; //Please select file(s) to upload.
    Sys.Extended.UI.Resources.AjaxFileUpload_Cancelling = "Cancelando..."; //Cancelling...
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadError = "Um erro ocorreu durante o carregamento do arquivo."; //An Error occured during file upload.
    Sys.Extended.UI.Resources.AjaxFileUpload_CancellingUpload = "Cancelando o carregamento..."; //Cancelling upload...
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadingInputFile = "Carregando arquivo: {0}."; //Uploading file: {0}.
    Sys.Extended.UI.Resources.AjaxFileUpload_Cancel = "Cancelar"; //Cancel
    Sys.Extended.UI.Resources.AjaxFileUpload_Canceled = "cancelado"; //cancelled
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadCanceled = "Carregamento do arquivo cancelado"; //File upload cancelled
    Sys.Extended.UI.Resources.AjaxFileUpload_DefaultError = "Erro ao carregar arquivo"; //File upload error
    Sys.Extended.UI.Resources.AjaxFileUpload_UploadingHtml5File = "Carregando arquivo: {0} de {1} bytes."; //Uploading file: {0} of size {1} bytes.
    Sys.Extended.UI.Resources.AjaxFileUpload_error = "erro"; //error

    //verifica se o upload está habilidado
    $(".ajax__fileupload_selectFileContainer").find("input[type=file]").click(function () {
        if ($(".upload-habilitado").val() == "0") {
            $(".file-upload").removerErro({
                classe: "erroupload"
            });
            $(".file-upload").mostrarErro({
                classe: "erroupload",
                texto: "O registro deve ser salvo antes de anexar um documento."
            });
            return false;
        }
    });
}
