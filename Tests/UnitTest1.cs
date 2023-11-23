using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Tests
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            EdgeOptions edgeOptions = new EdgeOptions();

            edgeOptions.AcceptInsecureCertificates = true;

            driver = new EdgeDriver(edgeOptions);

            driver.Navigate().GoToUrl("https://localhost:7145/Identity/Account/Login");

            var emailInput = driver.FindElement(By.Name("Input.Email"));
            emailInput.SendKeys("admin@admin.com");

            var passwordInput = driver.FindElement(By.Name("Input.Password"));
            passwordInput.SendKeys("Admin!23");

            var loginButton = driver.FindElement(By.Id("login-submit"));
            loginButton.Click();
        }

        [Test]
        public void TestAddVehicle()
        {
            driver.Navigate().GoToUrl("https://localhost:7145/Vehicle/Create");

            var nameInput = driver.FindElement(By.Id("name"));
            nameInput.SendKeys("Test Vehicle");

            var colorInput = driver.FindElement(By.Id("color"));
            colorInput.SendKeys("Red");

            var rangeInput = driver.FindElement(By.Id("range"));
            rangeInput.SendKeys("300");

            var createButton = driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Assert.That(driver.Url, Is.EqualTo("https://localhost:7145/Vehicle"));

            var vehicleList = driver.FindElements(By.Id("name"));
            var isVehicleAdded = false;
            foreach (var vehicle in vehicleList)
            {
                if (vehicle.Text.Contains("Test Vehicle"))
                {
                    isVehicleAdded = true;
                    break;
                }
            }

            Assert.That(isVehicleAdded, Is.True, "Test Vehicle not found in the vehicle list");
        }


        [Test]
        public void TestReservation()
        {
            driver.Navigate().GoToUrl("https://localhost:7145/user/reservation");

            var reserveLink = driver.FindElement(By.CssSelector("a[href='/user/reservation/reserve?id=1']"));
            reserveLink.Click();

            Assert.That(driver.Url, Is.EqualTo("https://localhost:7145/user/reservation/reserve?id=1"));

            var createReservation = driver.FindElement(By.CssSelector("a[href='/user/reservation/create?id=1']"));
            createReservation.Click();

            Assert.That(driver.Url, Is.EqualTo("https://localhost:7145/user/reservation/create?id=1")); 

            var reservationStartInput = driver.FindElement(By.Id("reservationStart"));

            reservationStartInput.Clear();

            var script = "arguments[0].value = arguments[1]";
            var startDate = "2023-06-01"; 
            var startTime = "10:00"; 
            ((IJavaScriptExecutor)driver).ExecuteScript(script, reservationStartInput, startDate + "T" + startTime);

            var reservationEndInput = driver.FindElement(By.Id("reservationEnd"));

            reservationEndInput.Clear();

            var endDate = "2023-06-05";
            var endTime = "18:00";
            ((IJavaScriptExecutor)driver).ExecuteScript(script, reservationEndInput, endDate + "T" + endTime);

            var createButton = driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Assert.That(driver.Url, Is.EqualTo("https://localhost:7145/user/reservation")); 
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}