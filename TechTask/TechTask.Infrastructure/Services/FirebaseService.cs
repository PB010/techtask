using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace TechTask.Infrastructure.Services
{
    public class FirebaseService
    {
        public FirebaseService()
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.GetApplicationDefault()
            });
        }
    }
}
