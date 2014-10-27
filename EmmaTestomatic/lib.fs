module lib
open System
open canopy
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
    //Console.WriteLine "dataportLogin"
    url (baseDataportUrl + "AboutDataport.aspx")
    click "#loginButton"
    "#UserName" << config.userId
    "#UPass" << config.password
    click "#LoginButton"
    on (baseDataportUrl + "Submission/SubmissionPortal.aspx")

let dataportLogout () =
    //Console.WriteLine "dataportLogout"
    url (baseDataportUrl + "Submission/SubmissionPortal.aspx")
    click "#ctl00_Masthead_logOutLinkButton"
    click "#ctl00_Masthead_logOutYesButton"
    on (baseDataportUrl + "AboutDataport.aspx")

let isIE () =
    (browser :? OpenQA.Selenium.IE.InternetExplorerDriver)

let isChrome () =
    (browser :? OpenQA.Selenium.Chrome.ChromeDriver)
