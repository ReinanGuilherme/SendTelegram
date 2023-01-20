namespace SendTelegram.Utils.Methods
{
    public static class ProblemDetailsExtensions
    {
        public static Dictionary<string, string[]> ConvertToProblemDetails(this List<string> msgError)
        {
            string[] Error = new string[msgError.Count];

            for (int count = 0; count < msgError.Count; count++)
            {
                Error[count] = msgError[count];
            }

            return Error
                    .GroupBy(g => "error")
                    .ToDictionary(g => "error", g => g.Select(x => x).ToArray());
        }

        public static Dictionary<string, string[]> ConvertToProblemDetails(this string msgError)
        {
            string[] Error = new string[1];

            Error[0] = msgError;

            return Error
                    .GroupBy(g => "error")
                    .ToDictionary(g => "error", g => g.Select(x => x).ToArray());
        }
    }
}
