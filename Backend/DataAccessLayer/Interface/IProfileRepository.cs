namespace DataAccessLayer.Interface
{
    public interface IProfileRepository
    {
        dynamic GetProfileInfo();

        int InsertUpdateProfile(dynamic profileModel);
    }

   
}
