using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.ViewModels;

namespace ComputerShopContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        void SaveReceivingsToWordFile(ReportBindingModel model);
        void SaveReceivingsToExcelFile(ReportBindingModel model);
    }
}