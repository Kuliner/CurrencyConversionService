using System.Runtime.Serialization;
using System.ServiceModel;

namespace CurrencyConversionService
{
    [ServiceContract]
    public interface ICurrencyConversionService
    {
        [OperationContract]
        string ConvertCurrencyToText(decimal value);
    }
}
