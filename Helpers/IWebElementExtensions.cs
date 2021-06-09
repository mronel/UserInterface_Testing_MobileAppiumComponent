using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using OfficeOpenXml;
using System.Linq;

namespace RaizenTestFuncional.Helpers
{
    /// <summary>
    /// Métodos de ações com a interface web.
    /// </summary>
    public static class IWebElementExtensions
    {
        #region Variável

        private static readonly WebDriverWait wait = new WebDriverWait(DriverFactory.GetDriver(), DriverFactory.GetTimeOut());
        
        private static readonly IJavaScriptExecutor js = (IJavaScriptExecutor)DriverFactory.GetDriver();
        private static Actions action;
        private static readonly List<By> locator = new List<By>();

        #endregion

        #region Métodos         

        /// <summary>
        /// Permite navegar para uma página Web dentro do domínio da aplicação. Considera a URL definida em "specflow.json" e concatena com a página Web informada 
        /// </summary>
        /// <param name="urlpage"> Página Web que será acessada</param>
        public static void GoToPage(string urlpage)
        {
            DriverFactory.GetDriver().Navigate().GoToUrl(DriverFactory.GetUrl() + urlpage);
        }

        /// <summary>
        /// Permite navegar para uma página Web.
        /// </summary>
        /// <param name="urlpage"> Página Web que será acessada (URL Completa)</param>
        public static void Visit(string urlpage)
        {
            DriverFactory.GetDriver().Navigate().GoToUrl(urlpage);
        }

        /// <summary>
        /// Click: permite clicar em um WebElement que permite esta ação.
        /// </summary>
        /// <param name="element">WebElement que deverá sofrer a ação do clique.</param>
        private static void Click(this IWebElement element)
        {
            element.Click();
        }

        /// <summary>
        /// Verifica se o elemento é clicável.
        /// </summary>
        /// <param name="element">WebElement a ser verificado</param>
        public static void CheckClikable(this IWebElement element)
        {
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element);
        }

        /// <summary>
        /// Aguarda até que um elemento seja clicável.
        /// </summary>
        /// <param name="element">WebElement a ser aguardado.</param>
        public static void UntilClikable(this IWebElement element)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        /// <summary>
        /// Verifica se um WebElement esta sendo exibido na interface.
        /// </summary>
        /// <param name="element"> WebElement que deverá estar sendo exibido na interface</param>
        public static void CheckExist(this IWebElement element)
        {
            if (!element.Displayed)
            {
                throw new ElementNotInteractableException();
            }
        }

        /// <summary>
        /// Verifica se um WebElement esta sendo exibido na interface e retorna um valor booleano.
        /// </summary>
        /// <param name="element">WebElement que deverá estar sendo exibido na interface</param>
        /// <returns></returns>
        public static bool CheckExistWithValidate(this IWebElement element)
        {
            if (!element.Displayed)
            {
                return false;
            } else
            {
                return true;
            }
        }

        /// <summary>
        /// Verifica a existencia de um WebElement pelo seu CssSelector no DOM.
        /// </summary>
        /// <param name="CssSelector">CssSelector do elemento (string) </param>
        public static void ElementExistence(string CssSelector)
        {
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(CssSelector));
        }

        /// <summary>
        /// Aguarda até que o WebElement informado desapareça.
        /// </summary>
        /// <param name="CssSelector">CssSeletor do elemento (string)</param>
        public static void UntilElementInvisible(string CssSelector)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelector)));
        }

        /// <summary>
        /// Aguarda até que o WebElement esteja vísivel na interface.
        /// </summary>
        /// <param name="CssSelector">CssSeletor do elemento (string)</param>
        public static void UntilElementVisible(string CssSelector)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(CssSelector)));
        }

        /// <summary>
        /// Recupera o texto de um elemento WebElement.
        /// </summary>
        /// <param name="element">WebElement que deseja recuperar o valor exibido.</param>
        /// <returns></returns>
        public static string GetText(this IWebElement element)
        {
            return element.Text;
        }

        /// <summary>
        /// Recupera o valor de uma propridade do WebElement.
        /// </summary>
        /// <param name="element">WebElement que deseja-se recuperar um valor.</param>
        /// <param name="property"> Propriedade do WebElement que retornará o valor.</param>
        /// <returns></returns>
        public static string GetProperty(this IWebElement element, string property)
        {
            return element.GetProperty(property);
        }

        /// <summary>
        /// Setar um WebElement do tipo input "Text" com um valor.
        /// </summary>
        /// <param name="element">WebElement que receberá o valor.</param>
        /// <param name="text">Valor a ser setado.</param>
        public static void SetText(this IWebElement element, string text)
        {
            element.SendKeys(text);
        }

        /// <summary>
        /// Selecionar um valor em um Select (Combobox).
        /// </summary>
        /// <param name="dropdown">WebElement do tipo Select (Combobox).</param>
        /// <param name="value">Valor a se selecionado.</param>
        public static void SelectDropDownValue(this IWebElement dropdown, string value)
        {
            var selectElement = new SelectElement(dropdown);
            selectElement.SelectByValue(value);
        }

        /// <summary>
        /// Selecionar um valor pelo texto em um Select (Combobox).
        /// </summary>
        /// <param name="dropdown">WebElement do Select (Combobox) desejado.</param>
        /// <param name="text">Texto a ser selecionado.</param>
        public static void SelectDropDownText(this IWebElement dropdown, string text)
        {
            var selectElement = new SelectElement(dropdown);
            selectElement.SelectByText(text);
        }

        /// <summary>
        /// Selecionar um valor pelo indíce em um Select (Combobox).
        /// </summary>
        /// <param name="dropdown">WebElement do tipo Select (Combobox).</param>
        /// <param name="index">Indíce que será selecionado</param>
        public static void SelectDropDownIndex(this IWebElement dropdown, int index)
        {
            var selectElement = new SelectElement(dropdown);
            selectElement.SelectByIndex(index);
        }

        /// <summary>
        /// Recuperar um valor pelo option selecionado de um Select(Combobox).
        /// </summary>
        /// <param name="dropdown">WebElement do tipo Select(Combobox).</param>
        /// <returns></returns>
        public static string SelectDropDownGetValueFromSelectedOption(this IWebElement dropdown)
        {
            SelectElement select = new SelectElement(dropdown);
            return select.SelectedOption.Text;
        }

        /// <summary>
        /// Setar um Checkbox ou Radio Button.
        /// </summary>
        /// <param name="element">WebElement que deverá selecionado.</param>
        public static void SelectRadioOrCheckbox(this IWebElement element)
        {
            if (element.Displayed && element.Enabled)
            {
                element.Click();
            }
        }

        /// <summary>
        /// Selecionar vários Checkboxes ou Radios Buttons.
        /// </summary>
        /// <param name="elements"> ICollection de WebElements que deverão ser selecionados.</param>
        public static void SelectRadiosOrCheckboxes(ICollection<IWebElement> elements)
        {
            foreach (IWebElement element in elements)
            {
                if (element.Displayed && element.Enabled)
                {
                    element.Click();
                }
            }
        }

        /// <summary>
        /// Upload de arquivos.
        /// </summary>
        /// <param name="element"> WebElement do tipo input "File".</param>
        /// <param name="filePath"> Caminho do arquivo.</param>
        public static void FileUpload(this IWebElement element, string filePath)
        {
            element.SendKeys("@" + filePath);
        }

        /// <summary>
        /// Submeter um formulário
        /// </summary>
        /// <param name="element">WebElement do tipo "Form"</param>
        public static void Submit(this IWebElement element)
        {
            element.Submit();
        }

        /// <summary>
        /// Recupera os valores de uma tabela.
        /// </summary>
        /// <param name="table">WebElement do tipo "Table"</param>
        /// <returns></returns>
        public static ICollection<string> GetValuesOnTable(ICollection<IWebElement> table)
        {
            List<string> cells = new List<string>();

            foreach (var item in table)
            {
                cells.Add(item.Text);
            }

            return cells;
        }

        /// <summary>
        /// Selecionar um valor em uma tabela.
        /// </summary>
        /// <param name="table">WebElement do tipo "Table"</param>
        /// <param name="value">Valor a ser selecionado</param>
        public static void SelectValueOnTable(ICollection<IWebElement> table, string value)
        {
            foreach (var item in table)
            {
                if (item.Text == value)
                {
                    item.Click();
                }
            }

        }

        /// <summary>
        /// Realiza um tempo de espera na execução no teste.
        /// </summary>
        /// <param name="seconds">Aguarda em segundos</param>
        public static void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        /// <summary>
        /// Realiza o click em um WebElement via Javascript
        /// </summary>
        /// <param name="element"> WebElement que permite clique</param>
        public static void ClickJs(this IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverFactory.GetDriver();
            js.ExecuteScript("arguments[0].scrollIntoView()", element);
            js.ExecuteScript("arguments[0].click()", element);
        }

        /// <summary>
        /// Setar um texto em um input "Text" via Javascript
        /// </summary>
        /// <param name="element">WebElement do tipo input "Text"</param>
        /// <param name="value">Valor a ser setado</param>
        public static void SetTextJs(this IWebElement element, string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverFactory.GetDriver();
            js.ExecuteScript("arguments[0].scrollIntoView()", element);
            js.ExecuteScript("arguments[0].setAttribute('value','" + value + "')", element);
        }

        /// <summary>
        /// Executa um comando via Javascript
        /// </summary>
        /// <param name="element">WebElement que receberá a ação</param>
        /// <param name="commandJS">Comando em Javascript</param>
        public static void ExecuteJsCommand(this IWebElement element, string commandJS)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverFactory.GetDriver();
            js.ExecuteScript(commandJS, element);
        }

        /// <summary>
        /// Permite encontrar e mover até um WebElement
        /// </summary>
        /// <param name="element">WebElement a ser encontrado.</param>
        /// <returns></returns>
        public static Actions MoveToElement(this IWebElement element)
        {
            action = new Actions(DriverFactory.GetDriver());
            return action.MoveToElement(element);
        }

        /// <summary>
        /// Permite realiza o scroll na interface
        /// </summary>
        /// <param name="position">Sentido do Scroll: top ou bottom</param>
        public static void ScrollIntoView(string position)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverFactory.GetDriver();

            if (position.ToLower() == "top")
            {
                js.ExecuteScript("window.scrollTo(0, 0);");
            }

            if (position.ToLower() == "bottom")
            {
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            }
        }

        /// <summary>
        /// Seleciona um elemento em uma lista
        /// </summary>
        /// <param name="list">Lista de itens do tipo tag: UL (informar CssSelector do WebElement)</param>
        /// <param name="findValue">Valor a ser selecionado na lista</param>
        public static void SelectElementFromList(ICollection<IWebElement> list, string findValue)
        {
            foreach (IWebElement value in list)
            {
                if (value.Text == findValue)
                {
                    Click(value);
                    break;
                }

            }
        }

        /// <summary>
        /// Retorna um valor encontrado em uma lista
        /// </summary>
        /// <param name="list">Lista de itens do tipo tag: UL (informar CssSelector do WebElement)</param>
        /// <param name="findValue">Valor a ser selecionado na lista</param>
        /// <returns></returns>
        public static IWebElement FindElementFromList(ICollection<IWebElement> list, string findValue)
        {
            IWebElement elementFound = null;
            foreach (IWebElement value in list)
            {
                if (value.Text == findValue)
                {
                    elementFound = value;
                    break;
                }
            }
            return elementFound;
        }

        /// <summary>
        /// Retorna todos os valores do arquivo CSV informado
        /// </summary>
        /// <param name="file">Nome do arquivo (deverá estar na pasta DataSet)</param>
        /// <returns></returns>
        public static List<string> loadCsvFile(string file)
        {
            var reader = new StreamReader(File.OpenRead(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + @"\DataSets\" +file), Encoding.Default);
            List<string> searchList = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                searchList.Add(line);
            }
            return searchList;
        }

        /// <summary>
        /// Retorna todos os valores do arquivo (incluindo nomes de colunas)
        /// </summary>
        /// <param name="file"> Nome do arquivo (deverá estar na pasta DataSets) </param>
        /// <returns> Todos as informações do arquivo </returns>
        public static List<string> loadExcelFile(string file)
        {
            ExcelPackage xlPackage = new ExcelPackage(new FileInfo(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + @"\DataSets\" + file));
            List<string> d = new List<string>();

                var sheet = xlPackage.Workbook.Worksheets.First();
                var totalRows = sheet.Dimension.End.Row;
                var totalColumns = sheet.Dimension.End.Column;
            
                var data = new StringBuilder();
                
                for (int rowNum = 1; rowNum <= totalRows; rowNum++)
                {               
                    var row = sheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                    data.AppendLine(string.Join(",", row));
                    var value = data.ToString().Split(',');
                    
                    foreach(string v in value)
                    {
                        d.Add(v.Replace("\r\n",string.Empty));        
                    }
                    data.Clear();
            }
                return d;
        }

        /// <summary>
        /// Retorna os dados de um arquivo Excel ordenando por colunas
        /// </summary>
        /// <param name="file">Nome do arquivo</param>
        /// <returns> Matriz com os valores de colunas e linhas. Exemplo: [0,0],[1,0], [2,0] Valores de Colunas - [0,1], [1,1] , [2,1] Valores de linhas </returns>
        public static string[,] loadExcelFileByColumn(string file)
        {
            ExcelPackage xlPackage = new ExcelPackage(new FileInfo(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + @"\DataSets\" + file));

            var sheet = xlPackage.Workbook.Worksheets.First();
            var totalRows = sheet.Dimension.End.Row;
            var totalColumns = sheet.Dimension.End.Column;

            string[,] list = new string[totalColumns,totalRows];

            for (int colNum = 1; colNum <= totalColumns; colNum++)
            {
                for(int rowNum = 1; rowNum <= totalRows; rowNum++)
                {
                     list[colNum - 1, rowNum - 1] = sheet.Cells[rowNum, colNum, rowNum, colNum]?.Value?.ToString();   
                }
            }
            return list;
        }

        /// <summary>
        /// Criar um novo arquivo na pasta DataSet
        /// </summary>
        /// <param name="name">Nome do arquivo</param>
        /// <param name="extension">Extensão do arquivo(sem o ponto final). Exemplo: xlsx, txt, ...</param>
        public static void createFileOnDataSetFolder(string name, string extension)
        {
            File.Create(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + @"\DataSets\" + name + "." + extension);
        }

        /// <summary>
        /// Retorna o caminho do arquivo informado na pasta DataSets
        /// </summary>
        /// <param name="file">Nome do arquivo</param>
        /// <returns></returns>
        public static string returnDataSetFilePath(string file)
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent + @"\DataSets\" + file;
        }

        /// <summary>
        /// Setar um valor na linha e coluna em um arquivo Excel (arquivo não poderá estar aberto). 
        /// </summary>
        /// <param name="file"> Nome do arquivo na pasta DataSet</param>
        /// <param name="value">Valor a ser inserido na coluna/linha</param>
        /// <param name="row">Número da linha (Se não informado, será igual a primeira linha)</param>
        /// <param name="column">Número da Coluna (Se não informado, será igual a primeira coluna) (Exemplo: coluna A = 1, coluna B = 2, ...)</param>
        /// <param name="worksheet">Nome da planilha (Caso não informado, pegará a primeira planilha)</param>
        public static void setValueOnRowAndColumnOfExcelFile(string file, string value, int row=1, int column=1, string worksheet=null)
        {
            ExcelPackage xlPackage = new ExcelPackage(new FileInfo(returnDataSetFilePath(file)));
            if(worksheet != null)
            {
                xlPackage.Workbook.Worksheets[worksheet].SetValue(row,column,value);
            } else
            {
                xlPackage.Workbook.Worksheets[0].SetValue(row, column, value);
            }
            xlPackage.Save();
            xlPackage.Dispose();
        }

        /// <summary>
        /// Setar uma lista de valores, especificando a quantidade de linhas e colunas, em um arquivo Excel (arquivo não poderá estar aberto). 
        /// </summary>
        /// <param name="file">Nome do arquivo</param>
        /// <param name="values">Lista de valores</param>
        /// <param name="fromRow">Número da linha inicial (Se não informado, será igual a primeira linha)</param>
        /// <param name="toRow">Número da linha final (Se não informado, será igual a primeira linha) </param>
        /// <param name="fromColumn">Número da coluna inicial (Se não informado, será igual a primeira coluna) (Exemplo: coluna A = 1, coluna B = 2, ...)</param>
        /// <param name="toColumn">Número da coluna final (Se não informado, será igual a primeira coluna) (Exemplo: coluna A = 1, coluna B = 2, ...)</param>
        /// <param name="worksheet">Nome da planilha (Caso não informado, pegará a primeira planilha)</param>
        public static void setListValuesOnExcelFile(string file, List<string> values, int fromRow=1, int toRow=1, int fromColumn=1, int toColumn=1, string worksheet = null)
        {
            ExcelPackage xlPackage = new ExcelPackage(new FileInfo(returnDataSetFilePath(file)));
            if (worksheet != null)
            {
                var sheet = xlPackage.Workbook.Worksheets[worksheet];

                for (int i = 0; i <= values.Count - 1; i++){

                    sheet.Cells[fromRow, fromColumn, toRow, toColumn].Value = values[i].ToString();
                }
            }
            else
            {
                var sheet = xlPackage.Workbook.Worksheets[0];
                
                for (int i = 0; i <= values.Count - 1; i++)
                {

                    sheet.Cells[fromRow, fromColumn, toRow, toColumn].Value = values[i].ToString();
                }
            }
            xlPackage.Save();
            xlPackage.Dispose();
        }

        /// <summary>
        /// Limpa todo o conteúdo de um arquivo excel informado.
        /// </summary>
        /// <param name="file">Nome do arquivo excel na pasta de DataSets</param>
        public static void clearExcelFile(string file)
        {
            ExcelPackage xlPackage = new ExcelPackage(new FileInfo(returnDataSetFilePath(file)));
            for(int i=0; i <= xlPackage.Workbook.Worksheets.Count - 1; i++)
            {
                xlPackage.Workbook.Worksheets[i].Cells.Clear();
            }

            xlPackage.Save();
            xlPackage.Dispose();
        }

        #endregion
    }
}
