using Core.Services.Interfaces;
using Core.SettingsModels;
using Microsoft.Extensions.Options;

namespace Core.Services
{
    public class FileService : IFileService
    {
        private readonly FileServiceSettings _fileServiceSettings;
        private const string DefaultImageName = "image.jpg";

        public FileService(IOptions<FileServiceSettings> fileServiceSettings)
        {
            _fileServiceSettings = fileServiceSettings.Value;
        }

        public Task<string> SaveImage(string base64ImageString, Guid employeeId)
        {
            base64ImageString = base64ImageString.Substring(base64ImageString.IndexOf(",", StringComparison.InvariantCulture) + 1);

            byte[] bytes = Convert.FromBase64String(base64ImageString);
            CreateDirectoryIfNotExists(employeeId);

            string fullImagePath = GetImagePath(employeeId);

            using var fileStream = new FileStream(fullImagePath, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(bytes);

            return Task.FromResult(fullImagePath);
        }

        private void CreateDirectoryIfNotExists(Guid employeeId)
        {
            string imageDirectory = this.GetDirectory(employeeId);

            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
        }

        public async Task<string> GetImageBase64StringAsync(string filePath)
        {
            byte[] imageArray = await File.ReadAllBytesAsync(filePath);

            return Convert.ToBase64String(imageArray);
        }

        public string GetImagePath(Guid employeeId)
        {
            string imageDirectory = this.GetDirectory(employeeId);
            string fullImagePath = Path.Combine(imageDirectory, DefaultImageName);

            return fullImagePath;
        }

        private string GetDirectory(Guid employeeId) 
        {
            string imageDirectory = Path.Combine(_fileServiceSettings.ImagesRootFolder, employeeId.ToString());
            return imageDirectory;
        }
    }
}
