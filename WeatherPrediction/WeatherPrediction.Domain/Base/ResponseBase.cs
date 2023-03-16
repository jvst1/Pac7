using WeatherPrediction.Infrastructure.Extensions;

namespace WeatherPrediction.Domain.Base
{
    public class ResponseBase
    {
        public ResponseBase()
        {
            Sucesso = true;
            Mensagem = string.Empty;
        }

        public ResponseBase(Exception ex)
        {
            Sucesso = false;


            if (ex is ValidationException validation)
            {
                BrokenRules = validation.BrokenRules;
                Mensagem = ex.Message;
            }
            else
            {
                Mensagem = ex.GetInnermostExceptionMessage();
            }
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string CodErro { get; set; }
        public List<BusinessRule> BrokenRules { get; set; }
    }
}