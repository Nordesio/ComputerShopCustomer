﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.ViewModels;
namespace ComputerShopBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfoCustomer
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportCustomerOrderViewModel> CustomerOrder { get; set; }
    }
}
