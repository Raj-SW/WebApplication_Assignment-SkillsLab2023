﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_Assignment_SkillsLab2023.Logger.Interface
{
    public interface ILogger
    {
        void LogError(Exception exception); 
        void InitializeLogFile(string message);
    }
}
