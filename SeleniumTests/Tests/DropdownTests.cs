namespace SeleniumTests.Tests;

public class DropdownTests : BaseTest
{
    private SelectElement Dropdown => new(Driver.FindElement(By.Id("dropdown")));

    [SetUp]
    public void OpenPage() => Open("/dropdown");

    [Test]
    public void Default_PlaceholderIsSelectedAndDisabled()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(Dropdown.SelectedOption.Text, Is.EqualTo("Please select an option"));
            Assert.That(Dropdown.Options[0].Enabled, Is.False);
            Assert.That(Dropdown.Options.Select(o => o.Text),
                Is.EqualTo(new[] { "Please select an option", "Option 1", "Option 2" }));
        }
    }

    [Test]
    public void SelectByText_Option1()
    {
        Dropdown.SelectByText("Option 1");
        Assert.That(Dropdown.SelectedOption.Text, Is.EqualTo("Option 1"));
    }

    [Test]
    public void SelectByValue_Option2()
    {
        Dropdown.SelectByValue("2");
        Assert.That(Dropdown.SelectedOption.Text, Is.EqualTo("Option 2"));
    }
}
