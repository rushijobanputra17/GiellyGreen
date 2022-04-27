using DataAccessLayer.Services;
using GiellyGreen.Helpers;
using GiellyGreen.Models;
using System;
using System.Web.Http;

namespace GiellyGreen.Controllers
{
    [Authorize]
    public class ProfileController : ApiController
    {
        public JsonResponse objResponse;
        public static ProfileRepository profileRepository = new ProfileRepository();

        public JsonResponse Get()
        {
            try
            {
                var profile = profileRepository.GetProfileInfo();
                objResponse = JsonResponseHelper.GetJsonResponse(1, "Records found : ", profile);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.InnerException.Message);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.Message);
                }
            }

            return objResponse;
        }

        public JsonResponse Post(ProfileViewModel model)
        {
            try
            {
                if (profileRepository.InsertUpdateProfile(model) == 1)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(1, "Record added successfully", model.ProfileId);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(0, "There was an error while adding record", null);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.InnerException.Message);
                }
                else
                {
                    objResponse = JsonResponseHelper.GetJsonResponse(2, "Exception", ex.Message);
                }
            }

            return objResponse;
        }
    }
}
