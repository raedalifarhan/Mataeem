namespace Mataeem.Lib
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileToServer(IFormFile formFile, string folder, string? pictureUrl)
        {
            var fileName = formFile.FileName;

            var fileExtension = Path.GetExtension(fileName);

            var validImageExtensions = new string[] { ".jpeg", ".gif", ".png", ".jpg" };

            var isValidImage = validImageExtensions.Contains(fileExtension);

            if (!isValidImage) return null!;

            var fileDbUrl = $"{DateTime.Now:yyyyMMddhhmmss}{fileName}";

            var folderPath = Path.Combine("wwwroot", folder);

            var fileServerPath = Path.Combine(folderPath, fileDbUrl);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var stream = new FileStream(fileServerPath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            if (pictureUrl != null)
            {
                var oldFilePath = Path.Combine(folderPath, pictureUrl);
                File.Delete(oldFilePath);
            }

            return fileDbUrl;
        }
    }
}
