using SendTelegram.Utils.Methods;

namespace SendTelegram.Utils.EntityAux
{
    public class ValidRequest
    {
        public List<string> MsgError { get; set; }
        public ValidRequest()
        {

            MsgError = new List<string>();
        }

        //Valida se a requisição possue campos invalidos.
        public bool ValidFields()
        {
            if (MsgError.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Formata as mensagens de erros para o padrão web,utilizando o metodo da pasta Utils
        public Dictionary<string, string[]> ReturnErrorsDetails()
        {
            return MsgError.ConvertToProblemDetails();
        }
    }
}
