using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.POM
{
    public class Objects
    {

        private IWebDriver driver;
        By priceElement = By.XPath("//td[4]/span");
        By leastPriceProduct = By.XPath("//td[@class='product-remove']/a");
        public Objects(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);


        }


        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Add to cart')]")]
        private IList<IWebElement> products;

        [FindsBy(How = How.XPath, Using = "//table[@class='shop_table shop_table_responsive cart woocommerce-cart-form__contents']/tbody/tr[*]/td[4]/span")]
        private IList<IWebElement> cardProducts;

        [FindsBy(How = How.PartialLinkText, Using = "CART")]
        private IWebElement cart;

        [FindsBy(How = How.XPath, Using = "//table[@class='shop_table shop_table_responsive cart woocommerce-cart-form__contents']/tbody/tr[*]/td[4]/span")]
        private IList<IWebElement> removeProducts;

        public IList<IWebElement> getProducts()

        {
            return products;


        }
        public IList<IWebElement> waitPageForDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//table[@class='shop_table shop_table_responsive cart woocommerce-cart-form__contents']/tbody/tr[*]/td[4]/span")));
            return cardProducts;

        }

        public IWebElement getcart()

        {
            return cart;


        }
        public IList<IWebElement> removeElement()
        {

            return removeProducts;

        }

        public By getPrice()
        {
            return priceElement;
        }
        public By getLeastPrice()
        {
            return leastPriceProduct;
        }

    }
}

