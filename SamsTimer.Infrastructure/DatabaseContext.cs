using Admin.Shared;
using Google.Cloud.Firestore;

namespace SamsTimer.Infrastructure
{
    public class DatabaseContext
    {
        public FirestoreDb Database { get; }

        public DatabaseContext()
        {
           
        }
    }
}