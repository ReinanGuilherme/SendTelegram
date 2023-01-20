using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using static System.Net.WebRequestMethods;

namespace SendTelegram.Services
{
    public class TelegramBot
    {

        private readonly IConfiguration configuration;

        public TelegramBot(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //Envia mensagens de texto para um chat específico, conforme o ID do chat e a mensagem são fornecidos como parâmetros.
        public async Task<bool> SendMessageTextAsync(long chatId, string message)
        {
            try
            {
                //Validando se o ChatId existe.
                if (!await GetChatId(chatId))
                {
                    return false;
                }

                //Realiza a verificação da validade dos campos de entrada e, caso não sejam válidos, entra na condição de campos inválidos.
                if (string.IsNullOrEmpty(message))
                {
                    return false;
                }

                //Recupera o token do bot a partir do arquivo appsettings.json e configura o objeto TelegramBotClient com essas informações.
                var bot = new TelegramBotClient(configuration["TelegramSettings:Token"]);

                //Realizando o envio da mensagem.
                await bot.SendTextMessageAsync(chatId, message);

                return true;
            }
            catch (Exception ex)
            {
                //Em caso de ocorrência de algum erro, é retornado o valor booleano 'false'.
                return false;
            }
        }

        //Envia imagem, pdf ou txt para um chat específico.
        public async Task<bool> SendMessageFileAsync(long chatId, IFormFile file)
        {
            try
            {
                //Validando se o ChatId existe.
                if (!await GetChatId(chatId))
                {
                    return false;
                }

                InputOnlineFile fileToSend = "";                

                string typeFile = "";

                
                using (var stream = file.OpenReadStream())
                {
                    //Converte um objeto do tipo IFormFile para InputOnlineFile
                    fileToSend = new InputOnlineFile(stream, file.FileName);

                    //Verificando o tipo do arquivo.
                    switch (file.ContentType)
                    {
                        case var contentType when contentType.StartsWith("image"):
                            typeFile = "image";

                            break;
                        case var contentType when contentType.StartsWith("application/pdf"):
                            typeFile = "pdf";

                            break;
                        case var contentType when contentType.StartsWith("text"):
                            typeFile = "txt";

                            break;
                        default:
                            return false;
                            break;
                    }

                    //Recupera o token do bot a partir do arquivo appsettings.json e configura o objeto TelegramBotClient com essas informações.
                    var bot = new TelegramBotClient(configuration["TelegramSettings:Token"]);

                    //Consulta o tipo do arquivo e realizando o envio da mensagem.
                    switch (typeFile)
                    {
                        case "image":
                            await bot.SendPhotoAsync(chatId, fileToSend);
                            break;
                        case "pdf":
                        case "txt":
                            await bot.SendDocumentAsync(chatId, fileToSend);
                            break;
                        default:
                            break;
                    }

                    return true;
                }                
            }
            catch (Exception ex)
            {
                //Em caso de ocorrência de algum erro, é retornado o valor booleano 'false'.
                return false;
            }
        }

        //Validando se o ChatId existe.
        public async Task<bool> GetChatId(long chatId)
        {
            var bot = new TelegramBotClient(configuration["TelegramSettings:Token"]);

            var updates = await bot.GetUpdatesAsync();

            foreach (var chat in updates)
            {
                if(chat.Message.Chat.Id == chatId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
