using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public static class TestBL
    {
        public static List<CustomerModel> getCustomers()
        {
            List<CustomerModel> customersList = new List<CustomerModel>();
            
            return TestDAL.GetCustomerModels();
        }
    }
}