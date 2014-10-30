module submissiontests
open System
open canopy
open runner
open etconfig
open lib

let all _ =
    context "submission tests"
    once (fun _ -> dataportLogin())
    lastly (fun _ -> dataportLogout())

    "CD - Ineligible for CUSIP" &&& fun _ ->
        url (baseDataportUrl + "Submission/SubmissionPortal.aspx")
        click "#tab1 > span"
        click "#ctl00_mainContentArea_createCDButton"
        click "#ctl00_mainContentArea_eventFilingRadio"
        click "#ctl00_mainContentArea_nextButton"
        click "#ctl00_mainContentArea_eventVoluntaryGridView_ctl04_categoryCheckBox"
        "#ctl00_mainContentArea_eventVoluntaryGridView_ctl04_consistingOfTextBox" << "test description"
        click "#ctl00_mainContentArea_nextButton"
        click "#ctl00_mainContentArea_securityTypeSelectControl_nonCUSIP9BasedList_1"
        sleep 0.01
        click "#ctl00_mainContentArea_nextButton"
        click "#ctl00_mainContentArea_nonCusipStateDropDownList"
        comboSelect "#ctl00_mainContentArea_nonCusipStateDropDownList" "State of Alabama"
        click "#ctl00_mainContentArea_nonCusipSearchImageButton"
        sleep 0.01
        click "#ctl00_mainContentArea_addNewIssueButton"
        "#ctl00_mainContentArea_issuerNameTextBox" << "te"
        click "#ctl00_mainContentArea_nextButton"
        displayed "#ctl00_mainContentArea_warningPopupContinueButton"
        waitFor(fun _ -> ((read "#ctl00_mainContentArea_warningMessageLabel").ToString().Length > 0))
        fieldContains "#ctl00_mainContentArea_warningMessageLabel" "You did not select securities to which the continuing disclosure document relates."
        click "#ctl00_mainContentArea_warningPopupContinueButton"
        "#ctl00_mainContentArea_issuerNameTextBox" << "test"
        "#ctl00_mainContentArea_issueNameTextBox" << "test"
        "#ctl00_mainContentArea_datedDateDataTextBox" << "12/12/2014"
        "#ctl00_mainContentArea_expectedClosingDateDataTextBox" << "12/12/2014"
        click "#ctl00_mainContentArea_addNewIssueSubmitImageButton"
        displayed "#ctl00_mainContentArea_nonCusipResultsGridView_ctl02_issueSelectCheckBox"
        click "#ctl00_mainContentArea_nextButton"
        waitFor(fun _ -> ((read "#ctl00_mainContentArea_warningMessageLabel").ToString().Length > 0))
        fieldContains "#ctl00_mainContentArea_warningMessageLabel" "You did not select securities to which the continuing disclosure document relates."
        click "#ctl00_mainContentArea_warningPopupContinueButton"
        check "#ctl00_mainContentArea_nonCusipResultsGridView_ctl02_issueSelectCheckBox"
        click "#ctl00_mainContentArea_nextButton"
        waitFor (fun _ -> (elements "#ctl00_mainContentArea_issuerInfoTitleLabel").Length > 0)
        click "#ctl00_mainContentArea_nextButton"
        fileUploadSelectPdf "#fileUpload1"
        click "#ctl00_mainContentArea_documentManagementControl_uploadSubmitButton"
        click "#ctl00_mainContentArea_savePublishExitControl_publishButton"
        click "#ctl00_mainContentArea_publishButton"
        click "#ctl00_mainContentArea_publishYesButton"
        fieldContains "#ctl00_mainContentArea_submissionConfirmationUserControl_confirmationInfoLabel" "PUBLISHED SUCCESSFULLY"

    "SHORT-ARS" &&& fun _ ->
        url (baseDataportUrl + "Submission/SubmissionPortal.aspx")
        click "#tab2" 
        click "#ctl00_mainContentArea_ARSCreateImageButton"
        "#ctl00_mainContentArea_bulkuploadSourceControl_cusipEntryTextBox" << "99999ab84"
        click "#ctl00_mainContentArea_bulkuploadSourceControl_processCusipsButton"
        waitFor (fun _ -> ((elements "#ctl00_mainContentArea_shortSecurityViewUserControl_cusip9ResultsListBox option").Length > 0))
        click "#ctl00_mainContentArea_nextButton"
        click "#ctl00_mainContentArea_documentManagementControl_documentSetupRadioButtonList_0"
        waitFor (fun _ -> (element "#ctl00_mainContentArea_documentManagementControl_fileUpload1DateReceivedTextBox").Enabled)
        "#ctl00_mainContentArea_documentManagementControl_fileUpload1DateReceivedTextBox" << DateTime.Today.ToShortDateString()
        fileUploadSelectPdf "#fileUpload1"
        click "#ctl00_mainContentArea_documentManagementControl_uploadSubmitButton" 
        click "#ctl00_mainContentArea_savePublishExitControl_publishButton"
        click "#ctl00_mainContentArea_publishButton"
        click "#ctl00_mainContentArea_publishYesButton"
        fieldContains "#ctl00_mainContentArea_submissionConfirmationUserControl_confirmationInfoLabel" "PUBLISHED SUCCESSFULLY"

