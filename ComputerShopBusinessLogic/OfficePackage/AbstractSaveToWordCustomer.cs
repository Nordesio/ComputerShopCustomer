using ComputerShopBusinessLogic.OfficePackage.HelperEnums;
using ComputerShopBusinessLogic.OfficePackage.HelperModels;
using System.Collections.Generic;
using System.Linq;

namespace ComputerShopBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWordCustomer
    {
        // Создание документа
        public void CreateDoc(WordInfoCustomer info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var procedure in info.CustomerOrder)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { ("Клиент: " + procedure.CustomerName.ToString(), new WordTextProperties {Bold = true, Size = "24"}),
                        ("Название заказа: " + procedure.OrderName, new WordTextProperties {Bold = false, Size = "24"}),
                        ("дата создания: " + procedure.DateCreate.ToString(), new WordTextProperties {Bold = false, Size = "24"}),
                        ("Стоимость " + procedure.Price.ToString(), new WordTextProperties {Bold = false, Size = "24"})},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);
        }

        // Создание doc-файла
        protected abstract void CreateWord(WordInfoCustomer info);

        // Создание абзаца с текстом
        protected abstract void CreateParagraph(WordParagraph paragraph);

        // Сохранение файла
        protected abstract void SaveWord(WordInfoCustomer info);
    }
}
