using System;
using System.IO;

namespace App.Web.Helper
{
    public class FileHelper
    {
        public bool TryDeleteFile(string filePath)
        {
            try
            {
                // Try opening the file with exclusive access
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // If we can open it, no other process is using it, so we can delete it
                    fs.Close(); // Close the file before deleting
                }

                // Delete the file
                File.Delete(filePath);
                return true; // Success
            }
            catch (IOException)
            {
                // The file is in use or cannot be accessed
                return false; // Deletion failed
            }
            catch (UnauthorizedAccessException)
            {
                // Handle permissions issues
                return false; // Deletion failed due to access issues
            }
        }
    }

}