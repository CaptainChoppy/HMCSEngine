using System.Text;

namespace HMCSEngine
{
    internal sealed class Debug
    {
        private const string BaseLogFileName = "Log";

        private static string CurrentLogFileName => $"{BaseLogFileName}_{DateTime.Now.ToString(HMCS.DateTimeFormat)}{Files.TextFileExtention}";
        private static string CurrentLogFilePath => Path.Combine(Files.LogsDirectory, CurrentLogFileName);

        private const string InfoHeader = "INFO";
        private const string WarningHeader = "WARNING";
        private const string ErrorHeader = "ERROR";
        private const string FatalHeader = "FATAL";
        private const string CommandHeader = "COMMAND";

        public static bool LogFiling = false;

        public static LogLevel LogLevel = LogLevel.Info;

        private static List<Log> Logs = new List<Log>();

        public static void Log(object? message, LogLevel level, bool filelog)
        {
            if ((int)(LogLevel) > (int)(level))
            {
                return;
            }

            if(filelog == true)
            {
                Logs.Add(new Log(level, message));
            }

            Console.WriteLine($"{LogLevelToString(level)}: {message}");
        }
        public static void RawLog(object? message)
        {
            Console.WriteLine(message);
        }
    
        public static void InfoLog(object? message)
        {
            Log(message, LogLevel.Info, true);
        }
        public static void CommandLog(object? message)
        {
            Log(message, LogLevel.Command, true);
        }
        public static void WarningLog(object? message)
        {
            Log(message, LogLevel.Warning, true);
        }
        public static void ErrorLog(object? message)
        {
            Log(message, LogLevel.Error, true);
        }
        public static void FatalLog(object? message)
        {
            Log(message, LogLevel.Fatal, true);
        }

        public static void CreateLogFile(bool ignoreconditions)
        {
            if(LogFiling == false && ignoreconditions != true)
            {
                return;
            }

            int filecreationattempts = 0;

            if (Directory.Exists(Files.LogsDirectory) == false)
            {
                Directory.CreateDirectory(Files.LogsDirectory);
            }

            do
            {
                filecreationattempts++;

                FileStream filestream;

                try
                {
                    filestream = new FileStream(CurrentLogFilePath, FileMode.Create, FileAccess.Write);

                    foreach (Log log in Logs)
                    {
                        byte[] buffer = Encoding.ASCII.GetBytes(log.ToString());

                        filestream.Write(buffer, 0, buffer.Length);
                    }
                    
                    Logs.Clear();
                    filestream.Close();
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                    continue;
                }

                return;
            } while (filecreationattempts < 7);

            throw new LogFileCreationException("Could not write a log file.");
        }

        public static string LogLevelToString(LogLevel level)
        {
            string leveltext;

            switch (level)
            {
                case LogLevel.Info:
                    leveltext = InfoHeader;
                    break;

                case LogLevel.Warning:
                    leveltext = WarningHeader;
                    break;

                case LogLevel.Error:
                    leveltext = ErrorHeader;
                    break;

                case LogLevel.Fatal:
                    leveltext = FatalHeader;
                    break;

                case LogLevel.Command:
                    leveltext = CommandHeader;
                    break;
                       
                default:
                    leveltext = "invalid log level";
                    break;
            }

            return leveltext;
        }

        public static string? GetConsoleInput()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }
    }

    internal struct Log
    {
        public readonly DateTime Time;
        public readonly LogLevel Level;
        public readonly object? Message;

        public Log(LogLevel level, object? message)
        {
            Level = level;
            Message = message;

            Time = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Time}] {Debug.LogLevelToString(Level)} - {Message}\r\n";
        }
    }

    internal enum LogLevel : int
    {
        Info = 0,
        Warning,
        Error,
        Fatal,

        Command

    }
}

public class LogFileCreationException : IOException
{
    public LogFileCreationException() : base()
    {

    }

    public LogFileCreationException(string message) : base(message)
    {

    }
}
