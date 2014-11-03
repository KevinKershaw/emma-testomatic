module warmup
open canopy
open configuration
open etconfig
open lib
open etlib
open System

let warmupEmma () =
    reporter.describe "warmup emma"
    let origCompareTimeout = configuration.compareTimeout
    try
        try
            configuration.compareTimeout <- 120.0 
            url (baseEmmaUrl + "Search/Search.aspx")
            if isIE() then
                let jsResult = (js "return $('#ctl00_mainContentArea_disclaimerContent_yesButton').length;")
                if jsResult.ToString() <> "0" then
                    click "#ctl00_mainContentArea_disclaimerContent_yesButton"
            else
                if (elements "#ctl00_mainContentArea_disclaimerContent_yesButton").Length > 0 then
                    click "#ctl00_mainContentArea_disclaimerContent_yesButton"
            waitFor (fun _ -> (elements "#runSearchButton").Length > 0)
            js """$("#stateDropdownList").val("VA"); $("#stateDropdownList").multiselect("refresh");""" |> ignore
            "#issuerName" << "Richmond Virginia"
            comboSelect "#purposeDropdownList" "Education"
            sleep 1.0
            click "#runSearchButton"
            waitFor (fun _ -> (element "#lvSecurities").Displayed)
        with
        | _ -> reporter.write "warmup emma failed"
    finally
        configuration.compareTimeout <- origCompareTimeout

let warmupDataport () =
    reporter.describe "warmup dataport"
    let origCompareTimeout = configuration.compareTimeout
    try
        try
            configuration.compareTimeout <- 120.0 
            dataportLogin ()
            dataportLogout ()
        with
        | _ -> reporter.write "warmup dataport failed"
    finally
        configuration.compareTimeout <- origCompareTimeout 
    