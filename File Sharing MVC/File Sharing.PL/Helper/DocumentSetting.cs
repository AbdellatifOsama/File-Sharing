using Microsoft.AspNetCore.Http;

namespace File_Sharing.PL.Helper
{
    public static class DocumentSetting
    {
        public static string Upload(IFormFile formFile)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files");
            var FileName = $"{Guid.NewGuid()}{formFile.FileName}";
            var FilePath = Path.Combine(FolderPath, FileName);
            using var FileStream = new FileStream(FilePath,FileMode.Create);
            formFile.CopyTo(FileStream);
            return FileName;
        }
        public static void Delete(string _fileName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files");
            var FilePath = Path.Combine(FolderPath, _fileName);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
