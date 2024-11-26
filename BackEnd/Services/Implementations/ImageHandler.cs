using BackEnd.Services.Interfaces;
using System.Text.RegularExpressions;

namespace BackEnd.Services.Implementations
{
    public class ImageHandler : IImageHandler
    {
        private readonly long _maxFileSize = 2 * 1024 * 1024; // 2 MB
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png"};

        public byte[] ProcessImage(IFormFile image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "No image provided.");

            if (image.Length > _maxFileSize)
                throw new InvalidOperationException("File size exceeds the maximum allowed size.");

            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                throw new InvalidOperationException($"Unsupported file format: {extension}");

            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public bool IsValidFileName(string fileName)
        {
            return Regex.IsMatch(fileName, @"^[\w\-. ]+$");
        }

        public string GetSafeFileName(string originalFileName)
        {
            var safeName = Path.GetFileNameWithoutExtension(originalFileName)
                .Replace(" ", "_")
                .Replace("-", "_");

            safeName = Regex.Replace(safeName, @"[^a-zA-Z0-9_]", "");
            var extension = Path.GetExtension(originalFileName);

            return $"{safeName}{extension}";
        }
    }
}
