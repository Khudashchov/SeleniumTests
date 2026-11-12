namespace SeleniumTests.Tests;

public class DragAndDropTests : BaseTest
{
    private const string Html5DragAndDropJs = @"
        const source = arguments[0], target = arguments[1];
        const dataTransfer = new DataTransfer();
        const fire = (el, type) => el.dispatchEvent(
            new DragEvent(type, { bubbles: true, cancelable: true, dataTransfer }));
        fire(source, 'dragstart');
        fire(target, 'dragenter');
        fire(target, 'dragover');
        fire(target, 'drop');
        fire(source, 'dragend');";

    private IWebElement ColumnA => Driver.FindElement(By.Id("column-a"));
    private IWebElement ColumnB => Driver.FindElement(By.Id("column-b"));
    private string HeaderA => ColumnA.FindElement(By.TagName("header")).Text;
    private string HeaderB => ColumnB.FindElement(By.TagName("header")).Text;

    private void DragAndDrop(IWebElement from, IWebElement to) =>
        Js.ExecuteScript(Html5DragAndDropJs, from, to);

    [SetUp]
    public void OpenPage() => Open("/drag_and_drop");

    [Test]
    public void Default_AIsLeftBIsRight()
    {
        Assert.That((HeaderA, HeaderB), Is.EqualTo(("A", "B")));
    }

    [Test]
    public void DragAToB_ColumnsSwap()
    {
        DragAndDrop(ColumnA, ColumnB);
        Assert.That((HeaderA, HeaderB), Is.EqualTo(("B", "A")));
    }

    [Test]
    public void DragTwice_ColumnsReturnBack()
    {
        DragAndDrop(ColumnA, ColumnB);
        DragAndDrop(ColumnA, ColumnB);
        Assert.That((HeaderA, HeaderB), Is.EqualTo(("A", "B")));
    }
}
