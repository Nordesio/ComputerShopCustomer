﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.BindingModel
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }
        public string OrderName { get; set; }
        public int Price { get; set; }
        /// <summary>
        /// Дата получения
        /// </summary>
        public DateTime DateReceipt { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> OrderCustomers { get; set; }
    }
}
