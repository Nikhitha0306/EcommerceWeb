using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace EcommerceWeb.Hooks
{
    [Binding]
    public sealed class HooksInitialization
    {
        private readonly IObjectContainer _container;

        public HooksInitialization(IObjectContainer container)
        {
            _container = container;
        }


        [BeforeScenario("@tag1")]
        public void BeforeScenarioWithTag()
        {

        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            _container.RegisterInstanceAs<IWebDriver>(driver);
            driver.Url = "https://cms.demo.katalon.com/";

        }

        [AfterScenario]
        public void AfterScenario()
        {
           var driver = _container.Resolve<IWebDriver>();
            if (driver != null)
            {
                driver.Quit();
            }

        }
    }
}