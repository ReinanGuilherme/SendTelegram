﻿using SendTelegram.Services;
using SendTelegram.Utils.Methods;
using System.Reflection;

namespace SendTelegram.EndPoints.SendMessageDocument
{
    public class SendMessageDocumentPost
    {
        public static string Template => "/SendMessageDocument/";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        // Adicionando o serviço do telegramBot criado no diretorio Services.
        public static async Task<IResult> Action(HttpRequest httpRequest, TelegramBot tgBot)
        {
            try
            {
                //Recebe o arquivo da imagem enviado no corpo da requisição HTTP
                IFormFile file = httpRequest.Form.Files.FirstOrDefault();

                //Recebe o chatId enviado no corpo da requisição HTTP
                var chatId = httpRequest.Form["chatId"];

                //Verifica se o arquivo foi recebido corretamente e se é uma imagem válida
                if(file == null)
                {
                    return Results.ValidationProblem(("Arquivo não foi adicionado.").ConvertToProblemDetails());
                }
                else if (!file.ContentType.StartsWith("text") && !file.ContentType.StartsWith("application/pdf"))
                {
                    return Results.ValidationProblem(("Arquivo inválido.").ConvertToProblemDetails());
                }

                //Executando a operação de envio da mensagem.
                var result = await tgBot.SendMessageFileAsync(long.Parse(chatId), file);

                if (!result)
                {
                    return Results.ValidationProblem(("Erro ao tentar enviar arquivo.").ConvertToProblemDetails());
                }

                return Results.Json(new { message = "Arquivo enviado com sucesso." });
            }
            catch (Exception ex)
            {
                return Results.ValidationProblem(ex.Message.ConvertToProblemDetails());
            }

        }
    }
}
