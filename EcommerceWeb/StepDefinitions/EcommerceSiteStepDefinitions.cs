using EcommerceWeb.POM;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using WebDriverManager.DriverConfigs.Impl;

namespace EcommerceWeb.StepDefinitions
{
    [Binding]
    public class EcommerceSiteStepDefinitions
    {
        private IWebDriver driver;
        private Objects objects;
        private Loops loops;

        public EcommerceSiteStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            objects = new Objects(driver);
            loops = new Loops(driver);
        }

        IList<IWebElement> cartProducts;
        IList<IWebElement> productsOnPage;
        IList<IWebElement> removeProducts;
        IList<IWebElement> priceElements;
        IWebElement elementWithLeastPrice = null;

        [Given(@"I add four random items to my cart")]
        public void GivenIAddFourRandomItemsToMyCart()
        {
            //Add four random products to the cart 
            productsOnPage = objects.getProducts();
            loops.AddFourRandomItemsToCart(productsOnPage.ToList());
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            // Click on the cart button to view the cart
            objects.getcart().Click();
        }

        [Then(@"I find total four items listed in my cart")]
        public void ThenIFindTotalFourItemsListedInMyCart()
        {
            // Wait for the cart page to display and get the list of products in the cart
            cartProducts = objects.waitPageForDisplay();
            Assert.AreEqual(4, cartProducts.Count, "Cart should have 4 products.");
        }

        [When(@"I search for lowest price item")]
        public void WhenISearchForLowestPriceItem()
        {

            priceElements = new List<IWebElement>();
            // Search for the item with the lowest price among the products in the cart
            loops.SearchForLowestPriceItem(cartProducts.ToList(), priceElements.ToList(), objects);

        }
        

        [When(@"I am able to remove the lowest price item")]
        public void WhenIAmAbleToRemoveTheLowestPriceItem()
        {
            // Remove the item with the lowest price from the cart
            loops.RemoveLowestPriceItem(cartProducts.ToList(), objects);

        }


        [Then(@"I am able to verify three items in my cart")]
        public void ThenIAmAbleToVerifyThreeItemsInMyCart()
        {
            removeProducts = objects.removeElement();
            Assert.AreEqual(3, removeProducts.Count, "Cart should have 3 products left after deleting the item with the least price.");
        }
    }
}
