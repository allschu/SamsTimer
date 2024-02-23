using SamsTimer.Services;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class SupportViewModel : ViewModelBase
    {
        private readonly ILogService _logService;
        public ICommand ShareCommand { get; }

        public SupportViewModel(ILogService logService)
        {
            _logService = logService;

            ShareCommand = new Command(async () => await ShareLogs());

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await CheckForUserPermission();
            });
        }

        public async Task ShareLogs()
        {
            string name = AppInfo.Name;
            string version = $"v{AppInfo.VersionString}.{AppInfo.BuildString}";

            try
            {
                var fileName = $"{name}_{version}_logs.zip";
                var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

                MemoryStream? compressedLogs = await _logService.GetCompressedLogs();

                if (compressedLogs == null)
                {
                    return;
                }

                await using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    await compressedLogs.CopyToAsync(fileStream);
                }

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "logbestanden",
                    File = new ShareFile(filePath)
                });
            }
            catch (Exception exception)
            {
                Application.Current.MainPage.DisplayAlert("Foutmelding", exception.Message, "OK");
            }
        }

        private async Task CheckForUserPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            if (status == PermissionStatus.Granted)
            {
                // Permission was granted
            }
            else
            {
                // Permission was denied
            }
        }
    }
}