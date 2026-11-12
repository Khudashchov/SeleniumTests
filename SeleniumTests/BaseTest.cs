using OpenQA.Selenium.Chrome;

namespace SeleniumTests;

public abstract class BaseTest
{
    public const string BaseUrl = "https://the-internet.herokuapp.com";

    protected ChromeDriver Driver = null!;
    protected WebDriverWait Wait = null!;

    protected IJavaScriptExecutor Js => Driver;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("--window-size=1280,900");

        if (Environment.GetEnvironmentVariable("HEADLESS") == "1")
            options.AddArgument("--headless=new");

        Driver = new ChromeDriver(options);
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
    }

    [TearDown]
    public void TearDown()
    {
        Driver.Quit();
        Driver.Dispose();
    }

    protected void Open(string path) => Driver.Navigate().GoToUrl(BaseUrl + path);
}
