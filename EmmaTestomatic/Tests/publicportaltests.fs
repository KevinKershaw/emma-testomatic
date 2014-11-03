module publicportaltests
open canopy
open runner
open etconfig
open lib
open etlib

let all _ = 
    context "public portal tests"

    "529 plans" &&& fun _ ->
        url (baseEmmaUrl + "Home/Index")
        click "Find 529 Plans"
        on (baseEmmaUrl + "Search/Plan529.aspx")
        displayed "#statesMap"
        if isChrome() || isFirefox() then
            sleep 1
        let trans0 () = 
            if isIE() then
                js "$('#statesMap area[data-state-code=TX]').click();" |> ignore
            else
                click (xpath "//map[@id='statesMap']/area[21]")
            (elements "LONESTAR 529 PLAN").Length > 0
        waitFor trans0
        displayed "LONESTAR 529 PLAN"
        click "LONESTAR 529 PLAN"
        on (baseEmmaUrl + "IssueView/Plan529OrMFSDetails.aspx?id=MP1089&issueType=529")
        displayed "#ctl00_mainContentArea_osDetailsLink"
        displayed "#ctl00_mainContentArea_cdDetailsLink"
        displayed "#ctl00_mainContentArea_PDFImageControl_Image1"
        displayed (jquery "td.leafNode.ctl00_mainContentArea_documentTreeUserControl_documentTreeView_6")
        click "#ctl00_mainContentArea_cdDetailsLink"
        "#ctl00_mainContentArea_CDEventSubmissionFileListControl_noCDDocumentsLabel" == "The MSRB began collecting continuing disclosures on July 1, 2009. No event notices have been received for this issue."
        displayed "#ctl00_mainContentArea_CDAnnualSubmissionFileListControl_submissionAnnualDocumentList_ctl01_submissionFileListTable_ctl02_documentHyperLink"

    "contact us" &&& fun _ ->
        url (baseEmmaUrl + "Home/Index")
        click "Contact Us"
        on (baseEmmaUrl + "AboutEMMA/ContactUs.aspx")
        click "#ctl00_mainContentArea_submitButton"
        "#ctl00_mainContentArea_validationSummary" == "All fields are required."
        "#ctl00_mainContentArea_firstNameTextbox" << "Test"
        "#ctl00_mainContentArea_lastNameTextbox" << "Test"
        "#ctl00_mainContentArea_emailTextbox" << config.email
        "#ctl00_mainContentArea_confirmEmailTextbox" << "wrong@msrb.org"
        "#ctl00_mainContentArea_phoneTextBox" << "11111111111"
        comboSelect "#roleDropDownList" "ADVINV"
        comboSelect "#ctl00_mainContentArea_subjectDropDown" "Help/Educational Materials"
        "#ctl00_mainContentArea_messageBodyTextBox" << "Testing"
        check "#mayContactCheckBox"
        click "#ctl00_mainContentArea_submitButton"
        "#ctl00_mainContentArea_validationSummary > ul > li" == "Email Address entries do not match."
        "#ctl00_mainContentArea_confirmEmailTextbox" << config.email
        click "#ctl00_mainContentArea_submitButton"
        "#ctl00_mainContentArea_headerLabel" == "Thank you for contacting EMMA. We will contact you in the near future."

