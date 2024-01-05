using System;
using System.Collections.Generic;
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
        public FileLogger()
        {
            this.filePath = "C:\\Users\\P12AC90\\Documents\\Log.txt";
            InitializeLogFile();
        }

        // Method to initialize the log file
        private void InitializeLogFile()
        {
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Log File Created: " + DateTime.Now);
                }
            }
        }

        // LogError method to append error to the log file
        public void LogError(Exception errorMessage)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine($"[Error - {DateTime.Now}] {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // If there's an issue logging the error, write to console or handle accordingly
                Console.WriteLine($"Error logging: {ex.Message}");
            }
        }
    }

}