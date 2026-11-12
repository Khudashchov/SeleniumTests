namespace SeleniumTests.Tests;

public class JavaScriptErrorTests : BaseTest
{
    private const string ErrorCatcherJs =
        "window.__jsErrors = [];" +
        "window.addEventListener('error', e => window.__jsErrors.push(e.message));";

    private List<string> CollectedErrors()
    {
        var raw = Js.ExecuteScript("return window.__jsErrors || [];") as IEnumerable<object>;
        return raw?.Select(e => e.ToString() ?? "").ToList() ?? new List<string>();
    }

    [SetUp]
    public void InstallErrorCatcher()
    {
        Driver.ExecuteCdpCommand("Page.addScriptToEvaluateOnNewDocument",
            new Dictionary<string, object> { ["source"] = ErrorCatcherJs });
    }

    [Test]
    public void PageLoad_ThrowsTypeError()
    {
        Open("/javascript_error");

        var errors = CollectedErrors();

        Assert.That(errors, Has.Count.EqualTo(1));
        Assert.That(errors[0], Does.Contain("Cannot read properties of undefined (reading 'xyz')"));
    }

    [Test]
    public void NormalPage_HasNoJsErrors()
    {
        Open("/checkboxes");
        Assert.That(CollectedErrors(), Is.Empty);
    }
}
