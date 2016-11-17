<%@ Page Title="" Language="C#" MasterPageFile="~/Unauthorized.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CernunnosWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updAutenticacao" runat="server">
    <ContentTemplate>
        <asp:Login ID="lgnAutenticaUsuario" runat="server" DisplayRememberMe="False" Width="100%" onauthenticate="AutenticaUsuario_Authenticate" FailureText="Usuário ou senha inválido!">
            <LayoutTemplate>

                <div class="panel panel-default top40">
                    <div class="panel-heading text-center"><strong>Autenticação</strong></div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group form-element has-error">
                                <span class="help-block text-center text-info">
                                    <asp:Literal id="FailureText" runat="server"></asp:Literal>
                                </span>
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                                <div class="form-group form-element">
                                    <label for="input-usuario" class="control-label">Usuário <span class="obrigatorio">*</span></label>
                                    <asp:TextBox id="UserName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:requiredfieldvalidator id="UserNameRequired" runat="server" ForeColor="" CssClass="help-block" ControlToValidate="UserName" Text="Este campo é obrigatório." Display="Dynamic"></asp:requiredfieldvalidator>
                                </div>
                                <div class=" form-group form-element">
                                    <label for="input-senha" class="control-label">Senha <span class="obrigatorio">*</span></label>
                                    <asp:TextBox id="Password" runat="server" textMode="Password" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:requiredfieldvalidator id="PasswordRequired" runat="server" ForeColor="" CssClass="help-block" ControlToValidate="Password" Text="Este campo é obrigatório." Display="Dynamic"></asp:requiredfieldvalidator>
                                </div>
                                <div class="form-group">
                                    <asp:button id="Login" CommandName="Login" runat="server" Text="Entrar" CssClass="btn btn-primary col-lg-12 col-md-12 col-sm-12 col-xs-12"></asp:button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <p class="text-center text-info">
                        <small>Recomendado o uso do navegador <a href="http://www.google.com/intl/pt-BR/chrome/browser/" target="_blank">Chrome</a></small>.
                    </p>
                </div>

            </LayoutTemplate>
        </asp:Login>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
