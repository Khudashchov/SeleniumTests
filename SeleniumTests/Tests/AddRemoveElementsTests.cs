namespace SeleniumTests.Tests;

public class AddRemoveElementsTests : BaseTest
{
    private static readonly By AddButton = By.XPath("//button[text()='Add Element']");
    private static readonly By DeleteButtons = By.CssSelector("#elements .added-manually");

    [SetUp]
    public void OpenPage() => Open("/add_remove_elements/");

    [Test]
    public void OnLoad_ThereAreNoDeleteButtons()
    {
        Assert.That(Driver.FindElements(DeleteButtons), Is.Empty);
    }

    [Test]
    public void ClickAddThreeTimes_ThreeDeleteButtonsAppear()
    {
        for (var i = 0; i < 3; i++)
            Driver.FindElement(AddButton).Click();

        var buttons = Driver.FindElements(DeleteButtons);
        Assert.That(buttons, Has.Count.EqualTo(3));
        Assert.That(buttons.Select(b => b.Text), Is.All.EqualTo("Delete"));
    }

    [Test]
    public void ClickDelete_RemovesOneButton()
    {
        Driver.FindElement(AddButton).Click();
        Driver.FindElement(AddButton).Click();

        Driver.FindElements(DeleteButtons).First().Click();

        Assert.That(Driver.FindElements(DeleteButtons), Has.Count.EqualTo(1));
    }
}
