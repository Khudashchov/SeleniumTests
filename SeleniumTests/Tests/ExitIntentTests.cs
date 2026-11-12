namespace SeleniumTests.Tests;

public class ExitIntentTests : BaseTest
{
    private IWebElement Modal => Driver.FindElement(By.Id("ouibounce-modal"));

    private void SimulateMouseLeavingViewport() =>
        Js.ExecuteScript(
            "document.documentElement.dispatchEvent(" +
            "new MouseEvent('mouseleave', { clientY: -1, bubbles: true }));");

    [SetUp]
    public void OpenPage() => Open("/exit_intent");

    [Test]
    public void OnLoad_ModalIsHidden()
    {
        Assert.That(Modal.Displayed, Is.False);
    }

    [Test]
    public void MouseLeavesViewport_ModalAppears()
    {
        SimulateMouseLeavingViewport();

        Wait.Until(_ => Modal.Displayed);
        Assert.That(Modal.FindElement(By.CssSelector(".modal-title h3")).Text,
            Is.EqualTo("This is a modal window").IgnoreCase);
    }

    [Test]
    public void ClickClose_ModalDisappears()
    {
        SimulateMouseLeavingViewport();
        Wait.Until(_ => Modal.Displayed);

        Modal.FindElement(By.CssSelector(".modal-footer p")).Click();

        Wait.Until(_ => !Modal.Displayed);
        Assert.That(Modal.Displayed, Is.False);
    }
}
