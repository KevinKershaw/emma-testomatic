module devtests
open canopy
open runner
open etconfig
open lib
open System

let all _ = 
    context "dev tests"
    //once (fun _ -> dataportLogin())

    many 5 ( fun _ ->
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
    )
