using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System.Linq;

namespace DataAccessLayer.Services
{
    public class ProfileRepository : IProfileRepository
    {
        private Team2_GiellyGreenEntities objDataAccess;

        public ProfileRepository()
        {
            objDataAccess = new Team2_GiellyGreenEntities();
        }

        public dynamic GetProfileInfo()
        {
            return Enumerable.FirstOrDefault(objDataAccess.GetProfile());
        }

        public int InsertUpdateProfile(dynamic profileModel)
        {
            return objDataAccess.InsertUpdateProfile(profileModel.ProfileId,profileModel.CompanyName,profileModel.AddressLine,profileModel.City,profileModel.ZipCode,profileModel.Country,profileModel.DefaultVAT);
        }
    }
}
