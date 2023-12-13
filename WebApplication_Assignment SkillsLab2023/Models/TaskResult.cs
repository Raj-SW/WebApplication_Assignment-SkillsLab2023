using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    public class TaskResult
    {
        public int IntegerResultValue { get; set; }
        public bool isSuccess { get; set; }
        public List<String> ResultMessageList { get; set; }
        public void AddResultMessage(string message)
        {
            ResultMessageList.Add(message);
        }
        public string GetAllResultMessageAsString()
        {
            string message = "";
            foreach (var item in ResultMessageList)
            {
                message+= item;
            }
            return message;
        }
        public TaskResult()
        {
            this.ResultMessageList = new List<String>();
            this.isSuccess = false;
            this.IntegerResultValue = 0;
        }
    }
}