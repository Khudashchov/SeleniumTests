namespace SeleniumTests.Tests;

public class ShiftingContentTests : BaseTest
{
    private static readonly By MenuLinks = By.CssSelector(".example ul li a");
    private static readonly By Gallery = By.LinkText("Gallery");

    [Test]
    public void Menu_HasFiveItems()
    {
        Open("/shifting_content/menu");

        var names = Driver.FindElements(MenuLinks).Select(a => a.Text);
        Assert.That(names, Is.EqualTo(new[] { "Home", "About", "Contact Us", "Portfolio", "Gallery" }));
    }

    [Test]
    public void Menu_PixelShift100_MovesOnlyGalleryBy100px()
    {
        Open("/shifting_content/menu");
        var galleryX = Driver.FindElement(Gallery).Location.X;
        var portfolioX = Driver.FindElement(By.LinkText("Portfolio")).Location.X;

        Open("/shifting_content/menu?pixel_shift=100");
        var shiftedGalleryX = Driver.FindElement(Gallery).Location.X;
        var shiftedPortfolioX = Driver.FindElement(By.LinkText("Portfolio")).Location.X;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(galleryX - shiftedGalleryX, Is.EqualTo(100).Within(1));
            Assert.That(shiftedPortfolioX, Is.EqualTo(portfolioX), "Інші пункти не мають зсуватися");
        }
    }

    [Test, Repeat(3)]
    public void Menu_RandomMode_GalleryIsStillFoundByText()
    {
        Open("/shifting_content/menu?mode=random");
        Assert.That(Driver.FindElement(Gallery).Displayed, Is.True);
    }

    [Test]
    public void Image_IsDisplayedAndLoaded()
    {
        Open("/shifting_content/image");
        var image = Driver.FindElement(By.CssSelector(".example img.shift"));

        var naturalWidth = Convert.ToInt64(Js.ExecuteScript("return arguments[0].naturalWidth;", image));

        using (Assert.EnterMultipleScope())
        {
            Assert.That(image.Displayed, Is.True);
            Assert.That(naturalWidth, Is.GreaterThan(0));
        }
    }

    [Test, Repeat(3)]
    public void List_ImportantRecordIsAlwaysPresent()
    {
        Open("/shifting_content/list");
        var listText = Driver.FindElement(By.CssSelector(".example .row")).Text;

        Assert.That(listText, Does.Contain("Important Information You're Looking For"));
    }
}
