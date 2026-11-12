namespace SeleniumTests.Tests;

public class GeolocationTests : BaseTest
{
    private const double KyivLatitude = 50.4501;
    private const double KyivLongitude = 30.5234;

    [Test]
    public void WhereAmI_ShowsEmulatedCoordinates()
    {
        Driver.ExecuteCdpCommand("Browser.grantPermissions", new Dictionary<string, object>
        {
            ["origin"] = BaseUrl,
            ["permissions"] = new[] { "geolocation" }
        });
        Driver.ExecuteCdpCommand("Emulation.setGeolocationOverride", new Dictionary<string, object>
        {
            ["latitude"] = KyivLatitude,
            ["longitude"] = KyivLongitude,
            ["accuracy"] = 100
        });

        Open("/geolocation");
        Driver.FindElement(By.XPath("//button[text()='Where am I?']")).Click();

        var latitude = Wait.Until(d => d.FindElement(By.Id("lat-value")));
        var longitude = Driver.FindElement(By.Id("long-value"));
        var mapLink = Driver.FindElement(By.CssSelector("#map-link a"));

        using (Assert.EnterMultipleScope())
        {
            Assert.That(latitude.Text, Is.EqualTo("50.4501"));
            Assert.That(longitude.Text, Is.EqualTo("30.5234"));
            Assert.That(mapLink.GetDomAttribute("href"), Does.EndWith("?q=50.4501,30.5234"));
        }
    }
}
