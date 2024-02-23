namespace Mataeem.Lib
{
    public static class FileHelper
    {
        public static IFormFile ConvertBase64ToIFormFile(string base64String, string extension, string fileName)
        {
            try
            {
                // Convert Base64 string to byte array
                byte[] bytes = Convert.FromBase64String(base64String);

                // Create a MemoryStream from the byte array
                var ms = new MemoryStream(bytes);

                // Create an instance of IFormFile
                var file = new FormFile(ms, 0, ms.Length, null!, fileName +"."+ extension)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png" // Assuming the image is PNG, you can set appropriate content type
                };

                // Reset the position of the MemoryStream
                ms.Position = 0;

                return file;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error converting Base64 to IFormFile", ex);
            }
        }

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
