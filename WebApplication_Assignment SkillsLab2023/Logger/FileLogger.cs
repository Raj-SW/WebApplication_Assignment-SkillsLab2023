using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Logger.Interface;

namespace WebApplication_Assignment_SkillsLab2023.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string filePath;

        // Constructor to set the file path
        public FileLogger(string loggerFilePath = "C:\\Users\\P12AC90\\source\\repos\\WebApplication_Assignment SkillsLab2023\\WebApplication_Assignment SkillsLab2023\\Logger\\Logs\\Log.txt")
        {
            filePath = loggerFilePath;
        }

        // Method to initialize the log file
        public void InitializeLogFile(string message)
        {

            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        // LogError method to append error to the log file
        public void LogError(Exception exception)
        {
            string fullMessage = "---------------------------------------------------------";
            fullMessage += Environment.NewLine + $"Timestamp: {DateTime.Now}";
            fullMessage += Environment.NewLine + $"Exception Type: {this.GetType().FullName}";
            fullMessage += Environment.NewLine + $"Message: {exception.Message}";
            fullMessage += Environment.NewLine + $"Inner Exception: {exception.InnerException}";
            fullMessage += Environment.NewLine + $"Stack Trace: {exception.StackTrace}";
            fullMessage += Environment.NewLine + "\"---------------------------------------------------------\";";

            InitializeLogFile(fullMessage);
        }
    }

}