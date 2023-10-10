namespace FlyCheapBot
{
    public class Logger
    {
        private enum Destination
        {
            File,
            Console,
            TgChannel
        }

        // константы задающие место записи лога для каждого из трёх уровней лога
        private static readonly List<Destination> DebugLogDestination = new List<Destination>() { Destination.Console };

        private static readonly List<Destination> InfoLogDestination = new List<Destination>()
            { Destination.Console, Destination.File };

        private static readonly List<Destination> ErrorLogDestination = new List<Destination>()
            { Destination.Console, Destination.File };

        private static string LogFileFolder => Path.Combine($@"~/Users/kyoto/Desktop/content.txt");

        private static string LogFilePath
        {
            get
            {
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd"); // Текущая дата 
                string fileName = $"LOG_{currentDate}.log";
                return Path.Combine(LogFileFolder, fileName);
            }
        }

        static Logger()
        {
            if (!Directory.Exists(LogFileFolder))
            {
                Directory.CreateDirectory(LogFileFolder);
            }
        }

        private static void Log(string messagePrefix, List<Destination> destinations, string message)
        {
            if (destinations.Contains((Destination.Console)))
            {
                LogToConsole(messagePrefix, message);
            }

            if (destinations.Contains((Destination.File)))
            {
                LogToFile(messagePrefix, message);
            }

            if (destinations.Contains((Destination.TgChannel)))
            {
                LogToTgChannel(messagePrefix, message);
            }
        }

        public static void Debug(string message)
        {
            Log("dbg", DebugLogDestination, message);
        }

        public static void Info(string message)
        {
            Log("INFO", InfoLogDestination, message);
        }

        public static void Error(string message)
        {
            Log("ERR", ErrorLogDestination, message);
        }

        private static string FormatLogMessage(string messagPrefix, string message)
        {
            return $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{messagPrefix}] : {message}";
        }

        private static void CreateLogFile()
        {
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Close();
            }
        }

        private static void LogToConsole(string mePrefix, string message)
        {
            Console.WriteLine(FormatLogMessage(mePrefix, message));
        }

        private static void LogToFile(string messagePrefix, string message)
        {
            CreateLogFile();

            using (StreamWriter writer = new StreamWriter(LogFilePath, true))
            {
                writer.WriteLine(FormatLogMessage(messagePrefix, message));
            }
        }

        private static void LogToTgChannel(string messagePrefix, string message)
        {
        }
    }
}