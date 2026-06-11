using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;

namespace AssetManagement.Tests
{
    public class AssetE2ETests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl = "http://127.0.0.1:5500/Frontend/index.html";

        public AssetE2ETests()
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless"); 

            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
        }

        // Test 1: Search functionality
        [Fact]
        public void Test_LoadAndSearchFunctionality()
        {
            // page opening
            _driver.Navigate().GoToUrl(_baseUrl);

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//tbody[@id='table-body']/tr")));

            // search field by id
            var searchInput = _driver.FindElement(By.Id("search-input"));

            // simulation
            searchInput.SendKeys("laptop");

            // all rows
            var rows = _driver.FindElements(By.XPath("//tbody[@id='table-body']/tr"));

            foreach (var row in rows)
            {
                string rowText = row.Text.ToLower();
                string displayStyle = row.GetAttribute("style");

                if (rowText.Contains("laptop"))
                {
                    // if it contains "laptop", it should be visible (!display: none)
                    Assert.True(string.IsNullOrEmpty(displayStyle) || !displayStyle.Contains("display: none"));
                }
                else
                {
                    Assert.Contains("display: none", displayStyle.ToLower() ?? "");
                }
            }
        }

        // Test 2: Create Asset with Validation
        [Fact]
        public void Test_CreateAsset_WithValidation()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));

            // --- A: negative test (empty fields validation) ---
            var addButton = _driver.FindElement(By.Id("btn-add"));
            addButton.Click(); // empty form submit

            wait.Until(ExpectedConditions.AlertIsPresent());
            var alert = _driver.SwitchTo().Alert();

            Assert.Equal("Please provide asset name and serial number.", alert.Text);
            alert.Accept(); // close alert box

            // --- B: positive test ---
            var nameInput = _driver.FindElement(By.Id("asset-name"));
            var serialInput = _driver.FindElement(By.Id("serial-number"));

            // fill the form with valid data
            nameInput.SendKeys("Selenium Test PC");
            serialInput.SendKeys("SN-SEL-100");
            addButton.Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            var successAlert = _driver.SwitchTo().Alert();
            Assert.Contains("successfully", successAlert.Text.ToLower());
            successAlert.Accept();
        }

        // Test 3: Open and close Edit Modal
        [Fact]
        public void Test_OpenAndCloseEditModal()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            // first row loading
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//tbody[@id='table-body']/tr")));

            // searching for the Edit button of the first row
            // XPath expression: find the first row in the table body, then look for a button with text "Edit" inside that row
            var editButton = _driver.FindElement(By.XPath("//tbody[@id='table-body']/tr[1]//button[text()='Edit']"));
            editButton.Click();

            // searching Modal window 
            var modal = _driver.FindElement(By.Id("edit-modal"));

            // check if it's visible
            Assert.Contains("display: flex", modal.GetAttribute("style").ToLower() ?? "");

            // search for delete button --> DELETE
            var closeButton = _driver.FindElement(By.Id("btn-close-modal"));
            closeButton.Click();

            // style must be none
            Assert.Contains("display: none", modal.GetAttribute("style").ToLower() ?? "");
        }

        // Test 4: delete confirmation simulation (canceling the delete action)
        [Fact]
        public void Test_DeleteAssetFunctionality()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//tbody[@id='table-body']/tr")));

            // actual row count
            int initialRowCount = _driver.FindElements(By.XPath("//tbody[@id='table-body']/tr")).Count;

            // first row's delete button
            var deleteButton = _driver.FindElement(By.XPath("//tbody[@id='table-body']/tr[1]//button[text()='Delete']"));
            deleteButton.Click();

            // confirm window: "❌ Are you sure about deleting this asset?"
            wait.Until(ExpectedConditions.AlertIsPresent());
            var confirmBox = _driver.SwitchTo().Alert();

            // simulation: Discard button
            confirmBox.Dismiss();

            // number of rows is unchanged
            int rowCountAfterCancel = _driver.FindElements(By.XPath("//tbody[@id='table-body']/tr")).Count;
            Assert.Equal(initialRowCount, rowCountAfterCancel);
        }

        // Test 5: actual delete functionality
        [Fact]
        public void Test_ActualDeleteAsset()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//tbody[@id='table-body']/tr")));

            int initialCount = _driver.FindElements(By.XPath("//tbody[@id='table-body']/tr")).Count;

            _driver.FindElement(By.XPath("//tbody[@id='table-body']/tr[1]//button[text()='Delete']")).Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            _driver.SwitchTo().Alert().Accept();

            // refreshing the page to see the changes
            Thread.Sleep(1000);

            int afterDeleteCount = _driver.FindElements(By.XPath("//tbody[@id='table-body']/tr")).Count;
            Assert.Equal(initialCount - 1, afterDeleteCount); // one less after deletion
        }

        // stop Chrome
        public void Dispose()
        {
            //_driver.Quit();
        }
    }
}