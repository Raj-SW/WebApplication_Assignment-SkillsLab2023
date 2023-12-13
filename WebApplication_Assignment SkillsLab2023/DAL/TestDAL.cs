using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.Common;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class TestDAL
    {
        //query string
        const string GET_ALL_CUSTOMERS = @"SELECT * FROM Customers";
        //query function
        public static List<CustomerModel> GetCustomerModels()
        {
            DBCommand cmd = new DBCommand();
            var dt = cmd.GetData(GET_ALL_CUSTOMERS);
            List<CustomerModel> customerModelList= new List<CustomerModel>();
            CustomerModel customerModel;

            foreach (DataRow row in dt.Rows)
            {
                customerModel = new CustomerModel();

                customerModel._customerId = (int)row["customer_id"];
                customerModel._customerFirstName = row["first_name"].ToString();
                customerModelList.Add(customerModel);
            }

            return customerModelList;
        }

    }
}