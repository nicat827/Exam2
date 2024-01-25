using Exam2.Utilities.Enums;

namespace Exam2.Utilities.Extencions
{
    public static class FileHelper
    {
        private const int ONE_KB = 1024;
        public static bool CheckFileType(this IFormFile file, FileType type)
        {
            switch (type)
            {
                case (FileType.Image):
                    if (file.ContentType.Contains("image/")) return true;
                    return false;
                case (FileType.Audio):
                    if (file.ContentType.Contains("audio/")) return true;
                    return false;
                case (FileType.Video):
                    if (file.ContentType.Contains("video/")) return true;
                    return false;
                default: return false;
            }
        }

        public static bool CheckFileSize(this IFormFile file, int maxSize, SizeType type = SizeType.Kb)
        {
            switch (type)
            {
                case SizeType.Kb:
                    if (file.Length <= maxSize * ONE_KB) return true;
                    return false;
                case SizeType.Mb:
                    if (file.Length <= maxSize * ONE_KB * ONE_KB) return true;
                    return false;
                case SizeType.Gb:
                    if (file.Length <= maxSize * ONE_KB * ONE_KB * ONE_KB) return true;
                    return false;
                default: return false;
            }
        }

        public static async Task<string> CreateFileAsync(this IFormFile file, string rootPath, params string[] folders)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
            string path = fileName._getPath(rootPath, folders);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        public static void DeleteFile(this string fileName, string rootpath, params string[] folders)
        {
            string path = fileName._getPath(rootpath, folders);
            if (File.Exists(path)) File.Delete(path);
        }

        //helper method for getting path
        private static string _getPath(this string fileName, string rootpath, params string[] folders)
        {
            string path = rootpath;
            foreach (string folder in folders)
            {
                path = Path.Combine(path, folder);
            }
            return Path.Combine(path, fileName);
        }

    }
}
