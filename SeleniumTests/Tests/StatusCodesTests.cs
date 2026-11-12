using System.Net;

namespace SeleniumTests.Tests;

public class StatusCodesTests : BaseTest
{
    [TestCase("200")]
    [TestCase("301")]
    [TestCase("404")]
    [TestCase("500")]
    public void ClickStatusLink_PageShowsThatCode(string code)
    {
        Open("/status_codes");
        Driver.FindElement(By.LinkText(code)).Click();

        Assert.That(Driver.Url, Does.EndWith($"/status_codes/{code}"));
        Assert.That(Driver.FindElement(By.CssSelector(".example p")).Text,
            Does.Contain($"This page returned a {code} status code."));
    }

    [Test]
    public void BackLink_ReturnsToList()
    {
        Open("/status_codes/404");
        Driver.FindElement(By.LinkText("here")).Click();

        Assert.That(Driver.Url, Does.EndWith("/status_codes"));
    }
}

public class StatusCodesHttpTests
{
    private static readonly HttpClient Http =
        new(new HttpClientHandler { AllowAutoRedirect = false });

    [TestCase(200)]
    [TestCase(301)]
    [TestCase(404)]
    [TestCase(500)]
    public async Task Endpoint_ReturnsRealStatusCode(int code)
    {
        using var response = await Http.GetAsync($"{BaseTest.BaseUrl}/status_codes/{code}");
        Assert.That(response.StatusCode, Is.EqualTo((HttpStatusCode)code));
    }
}
