using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    public class DataModelResult<T>
    {
        public T ResultObject { get; set; }
        public TaskResult ResultTask { get; set;}
        public DataModelResult() 
        {
            this.ResultObject = Activator.CreateInstance<T>(); ;
            this.ResultTask = new TaskResult();  
        }
    }
}