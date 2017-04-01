using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.PhantomJS;

namespace FabrikamFiber.Web.UiTests_v14
{
    [TestClass]
    public class SeleniumTests
    {
        [TestMethod]
        public void Testar_Cadastro_Usando_Selenium()
        {
            ExecutarTesteCadastro(new ChromeDriver());
        }

        [TestMethod]
        public void Testar_Cadastro_Usando_PhantomJS()
        {
            ExecutarTesteCadastro(new PhantomJSDriver());
        }

        private static void ExecutarTesteCadastro(IWebDriver driver)
        {
            const string nome = "Fulano";
            const string endereco = "Rua ABC, 123";
            const string cidade = "São Paulo";
            const string estado = "SP";
            const string cep = "12345";

            var sobrenome = $"De Tal {DateTime.Now.ToString("hhmmss")}";

            using (driver)
            {
                // Inicia a navegação
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Navigate().GoToUrl("http://intranet.fabrikam.com");

                // Preenche formulário
                driver.FindElement(By.CssSelector("a[href='/Customers']")).Click();
                driver.FindElement(By.CssSelector("a.glossyBox.createNew")).Click();
                driver.FindElement(By.CssSelector("input#FirstName.text-box.single-line")).SendKeys(nome);
                driver.FindElement(By.CssSelector("input#LastName.text-box.single-line")).SendKeys(sobrenome);
                driver.FindElement(By.CssSelector("input#Address_Street.text-box.single-line")).SendKeys(endereco);
                driver.FindElement(By.CssSelector("input#Address_City.text-box.single-line")).SendKeys(cidade);
                driver.FindElement(By.CssSelector("input#Address_State.text-box.single-line")).SendKeys(estado);
                driver.FindElement(By.CssSelector("input#Address_Zip.text-box.single-line")).SendKeys(cep);

                // Clica em Salvar
                driver.FindElement(By.CssSelector("input.glossyBox")).Click();

                // Confere inserção
                var valorCelula = driver.FindElement(By.CssSelector("table.dataTable>tbody>tr:last-child>td:nth-child(3)")).Text.Trim();
                Assert.AreEqual(sobrenome, valorCelula, "Sobrenome não encontrado - possível erro na inserção");
            }
        }
    }
}
