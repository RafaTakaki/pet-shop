namespace Library.Aplication.Services;

public static class EmailTemplateService
{
    public static string GerarTemplateConfirmacaoServico(string nomePet, string tipoServico, DateTime dataServico)
    {
        var dataFormatada = dataServico.ToString("dd/MM/yyyy HH:mm");
        
        return $@"
            <html>
                <body style='font-family: Arial, sans-serif; margin: 0; padding: 0;'>
                    <div style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 40px 20px;'>
                        <div style='max-width: 600px; margin: 0 auto;'>
                            <h1 style='color: white; margin: 0 0 10px 0;'>✓ Serviço Cadastrado</h1>
                            <p style='color: rgba(255,255,255,0.8); margin: 0;'>Confirmação de novo agendamento</p>
                        </div>
                    </div>
                    
                    <div style='background-color: #f5f5f5; padding: 40px 20px;'>
                        <div style='max-width: 600px; margin: 0 auto;'>
                            <p style='color: #666; font-size: 16px;'>Olá,</p>
                            <p style='color: #666; font-size: 14px; line-height: 1.6;'>
                                Confirmamos o cadastro de um novo serviço para seu pet. Aqui estão os detalhes:
                            </p>
                            
                            <div style='background-color: white; padding: 20px; border-radius: 5px; border-left: 5px solid #667eea; margin: 30px 0;'>
                                <table style='width: 100%; border-collapse: collapse;'>
                                    <tr>
                                        <td style='padding: 10px 0; border-bottom: 1px solid #eee;'>
                                            <span style='color: #999; font-weight: bold;'>Pet:</span>
                                        </td>
                                        <td style='padding: 10px 0; border-bottom: 1px solid #eee; text-align: right;'>
                                            <span style='color: #333; font-size: 16px;'>{nomePet}</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 10px 0; border-bottom: 1px solid #eee;'>
                                            <span style='color: #999; font-weight: bold;'>Tipo de Serviço:</span>
                                        </td>
                                        <td style='padding: 10px 0; border-bottom: 1px solid #eee; text-align: right;'>
                                            <span style='color: #333; font-size: 16px;'>{tipoServico}</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 10px 0;'>
                                            <span style='color: #999; font-weight: bold;'>Data/Hora:</span>
                                        </td>
                                        <td style='padding: 10px 0; text-align: right;'>
                                            <span style='color: #667eea; font-size: 16px; font-weight: bold;'>{dataFormatada}</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            
                            <p style='color: #666; font-size: 14px; line-height: 1.6;'>
                                Se você não realizou esta ação ou deseja fazer alterações, entre em contato conosco o quanto antes.
                            </p>
                            
                            <hr style='border: none; border-top: 1px solid #ddd; margin: 30px 0;'/>
                            
                            <p style='color: #999; font-size: 12px; text-align: center; margin: 0;'>
                                <strong>Sistema Pet Shop</strong><br/>
                                © 2026 - Todos os direitos reservados
                            </p>
                        </div>
                    </div>
                </body>
            </html>";
    }

    public static string GerarTemplateRecuperacaoSenha(string nomeUsuario, string linkRecuperacao)
    {
        return $@"
            <html>
                <body style='font-family: Arial, sans-serif; margin: 0; padding: 0;'>
                    <div style='background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%); padding: 40px 20px;'>
                        <div style='max-width: 600px; margin: 0 auto;'>
                            <h1 style='color: white; margin: 0 0 10px 0;'>🔒 Recuperação de Senha</h1>
                            <p style='color: rgba(255,255,255,0.8); margin: 0;'>Solicitação de alteração de senha</p>
                        </div>
                    </div>
                    
                    <div style='background-color: #f5f5f5; padding: 40px 20px;'>
                        <div style='max-width: 600px; margin: 0 auto;'>
                            <p style='color: #666; font-size: 16px;'>Olá {nomeUsuario},</p>
                            <p style='color: #666; font-size: 14px; line-height: 1.6;'>
                                Recebemos uma solicitação para recuperação de senha da sua conta. Se você não fez essa solicitação, ignore este email.
                            </p>
                            
                            <p style='color: #666; font-size: 14px; line-height: 1.6;'>
                                Para reset sua senha, clique no botão abaixo. <strong>Este link é válido por 24 horas.</strong>
                            </p>
                            
                            <div style='text-align: center; margin: 30px 0;'>
                                <a href='{linkRecuperacao}' style='background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%); color: white; padding: 15px 40px; text-decoration: none; border-radius: 5px; font-weight: bold; display: inline-block;'>
                                    Recuperar Senha
                                </a>
                            </div>
                            
                            <p style='color: #999; font-size: 12px; line-height: 1.6;'>
                                Ou copie e cole este link no seu navegador:<br/>
                                <span style='color: #666; font-size: 11px; word-break: break-all;'>{linkRecuperacao}</span>
                            </p>
                            
                            <hr style='border: none; border-top: 1px solid #ddd; margin: 30px 0;'/>
                            
                            <div style='background-color: #fff3cd; padding: 15px; border-radius: 5px; margin-bottom: 20px;'>
                                <p style='color: #856404; font-size: 12px; margin: 0;'>
                                    <strong>⚠️ Aviso Importante:</strong> Nunca compartilhe este link com ninguém. O sistema Pet Shop nunca pedirá sua senha por email.
                                </p>
                            </div>
                            
                            <p style='color: #999; font-size: 12px; text-align: center; margin: 0;'>
                                <strong>Sistema Pet Shop</strong><br/>
                                © 2026 - Todos os direitos reservados
                            </p>
                        </div>
                    </div>
                </body>
            </html>";
    }
}
