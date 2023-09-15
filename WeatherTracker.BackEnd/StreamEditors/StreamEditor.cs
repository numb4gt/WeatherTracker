using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.BackEnd.StreamEditors
{
    public class StreamEditor
    {
        public string filePath;

        public StreamEditor(string filePath)
        {
            this.filePath = filePath;
        }

        public void AddToFile(string data)
        {
            if (IsFileEmpty(filePath))
            {
                AppendDataToFile(filePath, "[\n\t" + data + "\n]");
            }
            else
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    fileStream.Seek(-1, SeekOrigin.End);
                    fileStream.SetLength(fileStream.Position-1);
                    fileStream.Flush();
                }

                AppendDataToFile(filePath, ",\n\t" + data + "\n]");
            }
        }

        static bool IsFileEmpty(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return fs.Length <= 3; // after creating an empty json file, its length is equal to three invisible characters. 
            }
        }

        static void AppendDataToFile(string filePath, string data)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                sw.Write(data);
            }
        }
    }
}
