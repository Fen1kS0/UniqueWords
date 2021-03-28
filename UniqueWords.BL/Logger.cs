using System.IO;

namespace UniqueWords.BL
{
    public class Logger
    {
        private readonly string _path;

        public Logger()
        {
            _path = Directory.GetCurrentDirectory() + @"\logs.txt";
        }

        public void AddLog(string log)
        {
            using (StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.WriteLine(log);
            }
        }

        public void Clear()
        {
            File.Delete(_path);

            File.Create(_path);
        }
    }
}