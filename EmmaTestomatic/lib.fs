module lib
open System
open canopy
open canopy.configuration
open etconfig

let comboSelect (value1 : string) (value2 : string) = 
    value1 << value2
    press enter

let fileUploadSelectPdf (value1 : string) = 
    let fileName = (AppDomain.CurrentDomain.BaseDirectory  + @"sample.pdf")
    (element value1).SendKeys(fileName)

let fieldContains (value1 : string) (value2 : string) = 
    contains value2 (read value1)

let dataportLogin () =
    try
        url (baseDataportUrl + "AboutDataport.aspx")
        click "#loginButton"
        "#UserName" << config.userId
        "#UPass" << config.password
        click "#LoginButton"
        on (baseDataportUrl + "Submission/SubmissionPortal.aspx")
    with
    | _ ->
         reporter.write "dataportLogin failed"

let dataportLogout () =
    try
        url (baseDataportUrl + "Submission/SubmissionPortal.aspx")
        click "#ctl00_Masthead_logOutLinkButton"
        click "#ctl00_Masthead_logOutYesButton"
        on (baseDataportUrl + "AboutDataport.aspx")
    with
    | _ ->
        reporter.write "dataportLogout failed"

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
