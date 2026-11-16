using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace ConsoleSelenium.Tests
{
    [TestClass]
    public class WebFormularioDesafioTest
    {
        [TestMethod]
        public void Parte1()
        {
            using IWebDriver driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://duckduckgo.com");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var campoTexto = wait.Until(d => d.FindElement(By.Name("q")));

            campoTexto.SendKeys("Selenium WebDriver C#");
            campoTexto.SendKeys(Keys.Enter);

            wait.Until(d => d.Title.Contains("Selenium WebDriver C#"));
            Assert.AreEqual("Selenium WebDriver C# at DuckDuckGo", driver.Title);
        }

        [TestMethod]
        public void Parte2()
        {
            using IWebDriver driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://duckduckgo.com");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var campoTexto = wait.Until(d => d.FindElement(By.Name("q")));
            campoTexto.SendKeys("C# unit testing");

            By suggestionsLocator = By.CssSelector("[role='option'], .search-suggestions-list li, .sbox-suggestions li");

            ReadOnlyCollection<IWebElement> suggestions = wait.Until(d =>
            {
                var elements = d.FindElements(suggestionsLocator);
                if (elements.Count >= 3 && elements[2].Displayed)
                {
                    return elements;
                }
                return null;
            });

            IWebElement terceiraOpcao = suggestions[2];
            string textoEsperado = terceiraOpcao.Text.Trim();

            terceiraOpcao.Click();

            wait.Until(d => d.Title.Contains(textoEsperado, StringComparison.OrdinalIgnoreCase));

            Assert.IsTrue(
                driver.Title.Equals($"{textoEsperado} at DuckDuckGo", StringComparison.OrdinalIgnoreCase),
                $"Título esperado: '{textoEsperado} at DuckDuckGo', mas foi: '{driver.Title}'"
            );
        }
        [TestMethod]
        public void Parte3()
        {
            using IWebDriver driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("https://duckduckgo.com");

            var campoTexto = wait.Until(d => d.FindElement(By.Name("q")));
            campoTexto.SendKeys("Automated Testing");
            campoTexto.SendKeys(Keys.Enter);

            wait.Until(d => d.Title.Contains("Automated Testing", StringComparison.OrdinalIgnoreCase));
            string urlPaginaResultados = driver.Url;

            IWebElement primeiroResultado = wait.Until(d =>
            {
                var links = d.FindElements(By.CssSelector("a[data-testid='result-title-a']"));
                return links.Count > 0 && links[0].Displayed ? links[0] : null;
            });

            string hrefEsperado = primeiroResultado.GetAttribute("href");
            primeiroResultado.Click();

            wait.Until(d => !d.Url.Contains("duckduckgo.com", StringComparison.OrdinalIgnoreCase));

            Assert.IsFalse(
                driver.Url.Contains("duckduckgo.com", StringComparison.OrdinalIgnoreCase),
                $"O navegador deveria ter saído do DuckDuckGo, mas permaneceu em: {driver.Url}"
            );
            Assert.IsFalse(string.IsNullOrEmpty(hrefEsperado), "O link orgânico não possuía um atributo href válido.");

            driver.Navigate().Back();

            wait.Until(d => d.Url.Contains("duckduckgo.com", StringComparison.OrdinalIgnoreCase));

            Assert.AreEqual(urlPaginaResultados, driver.Url, "A URL da página de resultados não foi restaurada.");
            Assert.IsTrue(
                driver.Title.Contains("Automated Testing", StringComparison.OrdinalIgnoreCase),
                $"O título restaurado não corresponde à busca original. Título atual: {driver.Title}"
            );

            IWebElement campoBuscaRestaurado = wait.Until(d => d.FindElement(By.Name("q")));
            Assert.AreEqual("Automated Testing", campoBuscaRestaurado.GetAttribute("value"));
        }
    }
}
