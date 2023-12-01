using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    public class CustomerModel
    {
        public int _customerId { get; set; }
        public string _customerFirstName { get; set; }
        public string _customerLastName { get; set;}
        public string _customerEmail { get; set;}
        public string _customerPhone { get; set;}
    }
}