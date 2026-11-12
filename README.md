# Selenium + C# + NUnit: тести для the-internet.herokuapp.com

## Запуск у Visual Studio
1. Потрібні Visual Studio 2022/2026 з робочим навантаженням **.NET desktop development** (або ASP.NET) і встановлений Google Chrome.
2. Відкрий `SeleniumTests.sln`. NuGet-пакети відновляться автоматично.
3. **Test → Test Explorer** (`Ctrl+E, T`), далі **Run All** (`Ctrl+R, A`).
   Окремий тест можна запустити правим кліком по ньому в Test Explorer або по методу в коді.
4. Якщо VS скаже, що немає .NET 8: ПКМ по проєкту → Properties → Target framework і вибери встановлену версію (наприклад .NET 10).

## Запуск з консолі
   ```
   dotnet test
   ```
   Без вікна браузера: `HEADLESS=1 dotnet test` (у PowerShell: `$env:HEADLESS=1; dotnet test`).
   Лише один клас: `dotnet test --filter "FullyQualifiedName~DropdownTests"`.

ChromeDriver окремо ставити не треба: Selenium Manager завантажить його сам.

## Структура
- `BaseTest.cs`: запуск і закриття Chrome, `Wait`, `Js`, `Open()`.
- `Tests/`: окремий файл для кожної сторінки сайту.

| Файл | Що тренує |
|---|---|
| AddRemoveElementsTests | FindElements, підрахунок елементів |
| CheckboxesTests | `.Selected`, кліки |
| DropdownTests | `SelectElement` |
| InputsTests | `SendKeys`, `Keys.ArrowUp`, читання `value` |
| StatusCodesTests | `By.LinkText`, URL + HttpClient для справжнього HTTP-коду |
| DragAndDropTests | HTML5 drag-and-drop через JS (Actions тут не працює) |
| ShiftingContentTests | стабільні локатори, `Location`, `[Repeat]` |
| GeolocationTests | CDP: підміна геолокації |
| JavaScriptErrorTests | CDP: перехоплення JS-помилок під час завантаження |
| ExitIntentTests | модальне вікно, JS-подія `mouseleave` |
