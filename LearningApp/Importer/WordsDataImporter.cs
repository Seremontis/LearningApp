using HtmlAgilityPack;
using Importer.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Importer
{
    public class WordsDataImporter
    {
        private const string _excelLocalization = @"C:\temp\test.xlsx";
        private Dictionary<string, string> _sourceWords = new Dictionary<string, string>()
        {
            { "https://www.ang.pl/slownictwo/slownictwo-angielskie-poziom-a1", "A1"},
            { "https://www.ang.pl/slownictwo/slownictwo-angielskie-poziom-a2", "A2"},
            { "https://www.ang.pl/slownictwo/slownictwo-angielskie-poziom-b1", "B1"},
            { "https://www.ang.pl/slownictwo/slownictwo-angielskie-poziom-b2", "B2"},
            { "https://www.ang.pl/slownictwo/czlowiek-czynnosci-rzeczowniki-po-angielsku","Człowiek-czynności(rzeczowniki)" },
            { "https://www.ang.pl/slownictwo/czlowiek-czynnosci-czasowniki-po-angielsku", "Człowiek-czynności(czasowniki)"},
            { "https://www.ang.pl/slownictwo/czlowiek-czynnosci-czasowniki-b1-po-angielsku","Człowiek-czynności(czasowniki)" },
        };
        private HtmlWeb _web = new HtmlWeb();
        private List<WordsModel> data = new List<WordsModel>();

        public void ImportData()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage document = new ExcelPackage())
            {
                document.Workbook.Worksheets.Add("English Words");
                var excelWorksheet = document.Workbook.Worksheets["English Words"];
                SetupWorksheet(excelWorksheet);
                GetData();
                excelWorksheet.Cells[2, 1].LoadFromCollection<WordsModel>(data);
                document.SaveAs(new FileInfo(_excelLocalization));
            }
        }

        private void SetupWorksheet(ExcelWorksheet worksheet)
        {
            var model = typeof(WordsModel)
                .GetProperties()
                .Select(pi => pi.Name)
                .ToArray();
            string headerRange = string.Format("A1:{0}1", GetMaxColumnIndex(model));
            worksheet.Cells[headerRange].LoadFromArrays(new List<string[]>() { model });
        }

        private string GetMaxColumnIndex(string[] headerRow)
        {
            return Char.ConvertFromUtf32(headerRow.Length + 64);
        }

        private void GetData()
        {
            foreach (var item in _sourceWords)
            {
                HtmlNode node = LoadHtml(item.Key);
                var countPage = node.SelectSingleNode(@"//div[contains(@class,'my_pagination')]/ul[@class='pagination']").
                    ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element)
                    .Select(x =>
                    {
                        int res;
                        int.TryParse(x.InnerText, out res);
                        return res;
                    }).Max();
                AddRecords(node, item.Value);
                for (int i = 2; i <= countPage; i++)
                    AddRecords(LoadHtml(item.Key + $"/page/{i}"), item.Value);
            }
        }

        private HtmlNode LoadHtml(string url) => _web.Load(url).DocumentNode;

        private void AddRecords(HtmlNode node, string type)
        {
            foreach (HtmlNode item in node.SelectNodes(@"//div[@class='dictionary']/div[@class='ditem']"))
            {
                WordsModel model = new WordsModel()
                {
                    EnglishMeaning = item.SelectSingleNode(@".//p[@class='word']")
                                    .ChildNodes.Where(x => x.NodeType == HtmlNodeType.Text).FirstOrDefault()?.InnerText,
                    PolishMeaning = item.SelectSingleNode(@".//p[@class='tr']")?.InnerText,
                    Type = type.Length > 2 ? type : string.Empty,
                    Level = item.SelectSingleNode(@".//p[@class='word']/span")?.GetClasses().FirstOrDefault(),
                };

                if (!string.IsNullOrEmpty(model.PolishMeaning))
                {
                    WordsModel inList = data.FirstOrDefault(x => x.EnglishMeaning == model.EnglishMeaning);
                    if (inList != null)
                    {
                        inList.PolishMeaning += ", " + model.PolishMeaning;
                        inList.Type += " " + model.Type;
                    }
                    else
                        data.Add(model);
                }

            }
        }

    }
}