using SendTelegram.Utils.EntityAux;

namespace SendTelegram.EndPoints.SendMessageText
{
    public class SendMessageTextRequest
    {
        public long ChatId { get; set; }
        public string Message { get; set; }

        public ValidRequest ValidRequest;

        public SendMessageTextRequest(long chatId, string message)
        {
            ValidRequest = ValidateFields(chatId, message);
            ChatId = chatId;
            Message = message;
        }

        private ValidRequest ValidateFields(long chatId, string message)
        {            

            ValidRequest Valid = new ValidRequest();

            if(chatId < 1)
            {
                Valid.MsgError.Add("ChatId inválido.");
            }

            if (string.IsNullOrEmpty(message))
            {
                Valid.MsgError.Add("Message não pode está null nem vazio.");
            }

            return Valid;
        }
    }
}