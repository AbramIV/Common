using System.IO.Compression;
using System.Text;

namespace CommonLib;

public static class FileHelper
{
    /// <summary>
    /// Create new filename if either already exist. Create directory.
    /// </summary>
    /// <param name="FullPath">Old filename.</param>
    /// <returns>New filename.</returns>
    public static string GetNextPath(string FullPath)
    {
        int count = 1;
        string nextPath;

        if (string.IsNullOrEmpty(Path.GetFileName(FullPath)) ||
            string.IsNullOrEmpty(Path.GetExtension(FullPath)))
        {
            if (!Directory.Exists(FullPath))
            {
                Directory.CreateDirectory(FullPath);
                return FullPath.EndsWith(@"\") ? FullPath : FullPath + @"\";
            }

            nextPath = $"{FullPath.Trim('\\')} ({count})\\";

            while (Directory.Exists(nextPath.Trim('\\')))
                nextPath = $"{FullPath.Trim('\\')} ({++count})\\";

            Directory.CreateDirectory(nextPath);
        }
        else
        {
            string extension = Path.GetExtension(FullPath);
            string directoryName = Path.GetDirectoryName(FullPath);
            string fileName = Path.GetFileNameWithoutExtension(FullPath);
            nextPath = $"{directoryName}\\{fileName} ({count}){extension}";

            if (!Directory.Exists(FullPath))
            {
                Directory.CreateDirectory(directoryName);
                return FullPath;
            }

            if (!File.Exists(FullPath)) return FullPath;

            while (File.Exists(nextPath))
                nextPath = $"{directoryName}\\{fileName} ({++count}){extension}";
        }

        return nextPath;
    }

    /// <summary>
    /// Extract files from archive and archives include.
    /// </summary>
    /// <param name="ArchivePath">Path to archive.</param>
    /// <param name="ExtractDirectory">Path to extract directory.</param>
    /// <param name="Recursive">It means about extraction including archives. </param>
    /// <returns>Paths of files.</returns>
    public static string[] Extract(string ArchivePath, string ExtractDirectory, bool Recursive)
    {
        ExtractDirectory = GetNextPath(ExtractDirectory.Trim('\\') + @"\" + Path.GetFileNameWithoutExtension(ArchivePath) + @"\");

        ZipFile.ExtractToDirectory(ArchivePath, ExtractDirectory, Encoding.GetEncoding(866));

        if (Recursive)
            foreach (string path in Directory.GetFiles(ExtractDirectory, "*.zip", SearchOption.AllDirectories))
                Extract(path, Path.GetDirectoryName(path), true);

        return Directory.GetFiles(ExtractDirectory, "*.*", SearchOption.AllDirectories);
    }
}