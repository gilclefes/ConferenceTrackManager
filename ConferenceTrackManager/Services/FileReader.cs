using System;
using System.IO;
using System.Text;

namespace ConferenceTrackManager.Services
{
    public static class FileReader
    {
        public static string[] ReadFile(string fileSource)
        {
            try
            {
                //return the content of the file in the form of string array
                //if filesource is wrong an exception will be thrown
                if (File.Exists(fileSource))
                {
                    return File.ReadAllLines(fileSource, Encoding.UTF8);
                }
                else
                {
                   // Console.WriteLine("File Source Given is wrong");
                    throw new FileNotFoundException("File Source Given is wrong");
                }

            }
            catch (Exception)
            {
               // Console.WriteLine(ex);
                throw new FileNotFoundException("File Source Given is wrong");
            }
        }
    }
}
