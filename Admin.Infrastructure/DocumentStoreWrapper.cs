
using Admin.Shared;
using Raven.Client.Documents;

namespace Admin.Infrastructure
{
    /// <summary>
    /// The IDocumentStore is a singleton and thread-safe, 
    /// which means it's designed to be instantiated once and then used throughout the lifetime of the application.
    /// </summary>
    public class DocumentStoreWrapper
    {
        private static readonly Lazy<IDocumentStore> LazyStore =
          new Lazy<IDocumentStore>(() =>
          {
              var store = new DocumentStore
              {
                  Urls = new[] { AppConstants.RavenDbUrl },
                  Database = AppConstants.RavenDb
              };

              store.Initialize();

              return store;
          });

        public static IDocumentStore Store => LazyStore.Value;
    }
}
