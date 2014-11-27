module lib
open OpenQA.Selenium
open OpenQA.Selenium.Interactions
open System
open canopy
open canopy.configuration
open etconfig

let isIE () =
    (browser :? OpenQA.Selenium.IE.InternetExplorerDriver)

let isChrome () =
    (browser :? OpenQA.Selenium.Chrome.ChromeDriver)

let isFirefox () =
    (browser :? OpenQA.Selenium.Firefox.FirefoxDriver)

let setFieldValue (f : string) (v : string) =
    f << v

let getFieldValue (f : string) =
    read f

let assertFieldValue (f : string) (v : string) =
    f == v

let assertFieldContains (f : string) (v : string) =
    contains v (read f)

let assertUrl (u : string) =
    on u

let assertDisplayed (f : string) =
    displayed f

let assertNotDisplayed (f : string) =
    notDisplayed f

let assertEqual expected actual =
    is expected actual

let getRandomNumberString (size : int) =
    let rnd = System.Random()
    let max = pown 10 size
    (rnd.Next max).ToString("00")

let fileUploadSelectPdf (value1 : string) = 
    let fileName = (AppDomain.CurrentDomain.BaseDirectory  + @"sample.pdf")
    (element value1).SendKeys(fileName)

let switchToWindow window =
    browser.SwitchTo().Window(window) |> ignore

let getOtherWindow currentWindow =
    browser.WindowHandles |> Seq.find (fun w -> w <> currentWindow)

let switchToOtherWindow currentWindow =
    switchToWindow (getOtherWindow currentWindow) |> ignore

let closeOtherWindow currentWindow =
    switchToOtherWindow currentWindow
    browser.Close()

let mouseOver (f : string) =
    new Actions(browser)
        |> fun a -> a.MoveToElement(element f)
        |> fun a -> a.Build()
        |> fun a -> a.Perform()

let extendTimeout (fn : unit -> unit) =
    let origCompareTimeout = configuration.compareTimeout
    try
        configuration.compareTimeout <- 120.0 
        fn ()
    finally
        configuration.compareTimeout <- origCompareTimeout

let waitForAjax () =
    let fn () =
        sleep 0.01
        let ret = js "return $.active;" |> Convert.ToInt32
        ret = 0
    waitFor fn

let waitForPostback () =
    let fn () = 
        sleep 0.01
        let ret = js "return Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();" |> Convert.ToBoolean
        ret = false
    waitFor fn
