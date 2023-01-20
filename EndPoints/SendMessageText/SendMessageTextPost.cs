using Telegram.Bot.Types;
using Telegram.Bot;
using Newtonsoft.Json.Linq;
using SendTelegram.Services;
using SendTelegram.Utils.Methods;

namespace SendTelegram.EndPoints.SendMessageText
{
    public class SendMessageTextPost
    {
        public static string Template => "/SendMessageText/";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        // Adicionando o serviço do telegramBot criado no diretorio Services.
        public static async Task<IResult> Action(SendMessageTextRequest request, TelegramBot tgBot)
        {
            //Realiza a verificação dos campos de entrada e, caso não sejam válidos, entra na condição de campos inválidos.
            if (!request.ValidRequest.ValidFields())
            {
                //Retornando os erros de validação.
                return Results.ValidationProblem(request.ValidRequest.ReturnErrorsDetails());
            }

            //Executando a operação de envio da mensagem.
            var result = await tgBot.SendMessageTextAsync(request.ChatId, request.Message);

            //Verifica a ocorrência de erros durante o processo de envio da mensagem.
            if (!result)
            {
                return Results.ValidationProblem(("Falha na operação de envio da mensagem.").ConvertToProblemDetails());
            }

            return Results.Json(new { success = "Operação de envio da mensagem concluída com sucesso." });
        }
    }
}
