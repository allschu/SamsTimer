using Google.Cloud.Firestore;
using SamsTimer.Shared;

namespace SamsTimer.Infrastructure
{
    public class DatabaseContext
    {
        public FirestoreDb Database { get; }

        public DatabaseContext()
        {
            //Only creates a new instance not a new Db
            Database = FirestoreDb.Create(AppConstants.FirebaseProjectName);
        }
    }
}