module lib
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

let getSubmissionId (value1 : string) =
    try
        let s = (read value1)
        let b = s.IndexOf("(")
        let e = s.IndexOf(")")
        s.Substring((b+1), (e-b-1))
    with
    | _ -> ""

let setFieldValue (f : string) (v : string) =
    f << v

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
