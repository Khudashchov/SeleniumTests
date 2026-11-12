namespace SeleniumTests.Tests;

public class InputsTests : BaseTest
{
    private IWebElement NumberInput => Driver.FindElement(By.CssSelector("input[type='number']"));

    private string? Value => NumberInput.GetDomProperty("value");

    [SetUp]
    public void OpenPage() => Open("/inputs");

    [Test]
    public void TypeDigits_ValueIsSaved()
    {
        NumberInput.SendKeys("12345");
        Assert.That(Value, Is.EqualTo("12345"));
    }

    [Test]
    public void TypeLetters_AreIgnored()
    {
        NumberInput.SendKeys("abc");
        Assert.That(Value, Is.Empty);
    }

    [Test]
    public void ArrowUpAndDown_ChangeValue()
    {
        NumberInput.SendKeys("5");

        NumberInput.SendKeys(Keys.ArrowUp);
        Assert.That(Value, Is.EqualTo("6"));

        NumberInput.SendKeys(Keys.ArrowDown + Keys.ArrowDown);
        Assert.That(Value, Is.EqualTo("4"));
    }

    [Test]
    public void Clear_EmptiesField()
    {
        NumberInput.SendKeys("42");
        NumberInput.Clear();
        Assert.That(Value, Is.Empty);
    }
}
