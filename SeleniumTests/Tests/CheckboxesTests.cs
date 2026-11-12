namespace SeleniumTests.Tests;

public class CheckboxesTests : BaseTest
{
    private IReadOnlyList<IWebElement> Boxes =>
        Driver.FindElements(By.CssSelector("#checkboxes input[type='checkbox']"));

    [SetUp]
    public void OpenPage() => Open("/checkboxes");

    [Test]
    public void DefaultState_FirstUncheckedSecondChecked()
    {
        Assert.That(Boxes, Has.Count.EqualTo(2));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(Boxes[0].Selected, Is.False, "checkbox 1");
            Assert.That(Boxes[1].Selected, Is.True, "checkbox 2");
        }
    }

    [Test]
    public void ClickEachCheckbox_StateIsInverted()
    {
        Boxes[0].Click();
        Boxes[1].Click();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(Boxes[0].Selected, Is.True);
            Assert.That(Boxes[1].Selected, Is.False);
        }
    }

    [Test]
    public void CheckAll_AllAreSelected()
    {
        foreach (var box in Boxes.Where(b => !b.Selected))
            box.Click();

        Assert.That(Boxes.Select(b => b.Selected), Is.All.True);
    }
}
