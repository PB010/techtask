using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using TechTask.Application.Interfaces;

namespace TechTask.Infrastructure.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly IHttpContextAccessor _accessor;

        public FirebaseService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async void Check()
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.GetApplicationDefault(),
                ServiceAccountId = "firebase-adminsdk-orb2o@tech-task-d0a63.iam.gserviceaccount.com"
            });

            var uId = _accessor.HttpContext.User.Identity.ToString();
            var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uId);
        }
    }
}
