using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopBusinessLogic.OfficePackage.HelperEnums;
using ComputerShopBusinessLogic.OfficePackage.HelperModels;
namespace ComputerShopBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcelCustomer
    {
        /// Создание отчета
        public void CreateReport(ExcelInfoCustomer info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;

            foreach (var procedure in info.CustomerOrder)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = procedure.CustomerName,
                    StyleInfo = ExcelStyleInfoType.TextWithBroder
                });
                rowIndex++;

                foreach (var procedures in info.CustomerOrder)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = procedures.OrderName.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = procedures.DateCreate.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });

                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = procedures.Price.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    rowIndex++;
                }
            }
            SaveExcel(info);
        }

        // Создание excel-файла
        protected abstract void CreateExcel(ExcelInfoCustomer info);

        // Добавляем новую ячейку в лист
        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

        // Объединение ячеек
        protected abstract void MergeCells(ExcelMergeParameters excelParams);

        // Сохранение файла
        protected abstract void SaveExcel(ExcelInfoCustomer info);
    }
}