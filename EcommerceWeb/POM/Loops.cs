using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.POM
{
    public class Loops
    {
        private readonly IWebDriver driver;
        private (IWebElement element, int index) elementWithLeastPrice;


        public Loops(IWebDriver driver)
        {
            this.driver = driver;
            //PageFactory.InitElements(driver, this);
        }

        public void AddFourRandomItemsToCart(List<IWebElement> productsOnPage)
        {
            Random random = new Random();
            int j = 0;
            List<IWebElement> target = new List<IWebElement>();

            while (j < 4)
            {
                // Generate a random number within the range of the productsOnPage list
                int length = random.Next(productsOnPage.Count);
                IWebElement element = productsOnPage[length];

                if (!target.Contains(element))
                {
                    // Add the selected element to the target list
                    target.Add(element);
                    Thread.Sleep(2000);
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                    // Click on the selected element using JavaScript executor
                    executor.ExecuteScript("arguments[0].click();", element);
                    j++;
                }
            }
        }

        public void SearchForLowestPriceItem(List<IWebElement> cartProducts, List<IWebElement> priceElements, Objects objects)
        {

            double minPrice = double.MaxValue;
            int indexWithLeastPrice = -1;
            if (cartProducts.Count > 0)
            {

                priceElements = new List<IWebElement>(driver.FindElements(objects.getPrice()));
                for (int i = 0; i < priceElements.Count; i++)
                {
                    var price = priceElements[i].Text;
                    string trimmedString = price.Trim('$');

                    if (double.TryParse(trimmedString, out double parsedPrice))
                    {
                        if (parsedPrice < minPrice)
                        {
                            // Update the minimum price and the index of the element with the least price
                            minPrice = parsedPrice;
                            indexWithLeastPrice = i;
                        }
                    }
                }
                if (indexWithLeastPrice >= 0)
                {
                    elementWithLeastPrice = (priceElements[indexWithLeastPrice], indexWithLeastPrice);

                }
            }
        }

        public void RemoveLowestPriceItem(List<IWebElement> cartProducts, Objects objects)
        {

            if (elementWithLeastPrice.element != null)
            {
                // Get the index and price from the tuple
                int index = elementWithLeastPrice.index;

                // Delete the element with the least price
                IList<IWebElement> deleteButtons = driver.FindElements(objects.getLeastPrice());
                if (index < deleteButtons.Count)
                {
                    IWebElement deleteButton = deleteButtons[index];
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                    executor.ExecuteScript("arguments[0].click();", deleteButton);
                    Thread.Sleep(2000);
                }
            }



        }
    }
}
