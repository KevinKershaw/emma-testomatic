module etlib
open System
open canopy
open canopy.configuration
open etconfig

let fileUploadSelectPdf (value1 : string) = 
    let fileName = (AppDomain.CurrentDomain.BaseDirectory  + @"sample.pdf")
    (element value1).SendKeys(fileName)

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

let comboSelect (value1 : string) (value2 : string) = 
    value1 << value2
    press enter

