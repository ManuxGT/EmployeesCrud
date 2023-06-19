namespace Core.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveImage(string base64ImageString, Guid employeeId);
        string GetImagePath(Guid employeeId);
        Task<string> GetImageBase64StringAsync(string filePath);
    }
}
