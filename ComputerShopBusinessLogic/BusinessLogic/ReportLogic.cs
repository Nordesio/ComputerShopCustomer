using ComputerShopBusinessLogic.OfficePackage;
using ComputerShopBusinessLogic.OfficePackage.HelperModels;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.BusinessLogicsContracts;
using ComputerShopContracts.StoragesContracts;
using ComputerShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ComputerShopBusinessLogic.BusinessLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly IReceivingStorage _receivingStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        public ReportLogic(IReceivingStorage receivingStorage, AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord)
        {
            _receivingStorage = receivingStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
        }
        public void SaveReceivingsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список получения техники",
                Receivings = _receivingStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveReceivingsToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список получения техники",
                Receivings = _receivingStorage.GetFullList()
            });
        }
    }
}