using GiellyGreen.Models;

namespace GiellyGreen.Helpers
{
    public class JsonResponseHelper
    {
        public static JsonResponse GetJsonResponse(int responseStatus, string message, dynamic result)
        {
            return new JsonResponse()
            {
                ResponseStatus = responseStatus,
                Message = message,
                Result = result,
            };
        }
    }
}