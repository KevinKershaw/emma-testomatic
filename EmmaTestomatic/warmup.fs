module warmup
open canopy
open configuration
open etconfig
open lib

let warmupEmma () =
    let origCompareTimeout = configuration.compareTimeout
    configuration.compareTimeout <- 120.0 
    url (baseEmmaUrl + "Search/Search.aspx")
    if (elements "#ctl00_mainContentArea_disclaimerContent_yesButton").Length > 0 then
        click "#ctl00_mainContentArea_disclaimerContent_yesButton"
    waitFor (fun _ -> (elements "#runSearchButton").Length > 0)
    js """$("#stateDropdownList").val("VA"); $("#stateDropdownList").multiselect("refresh");""" |> ignore
    "#issuerName" << "Richmond Virginia"
    comboSelect "#purposeDropdownList" "Education"
    sleep 1.0
    click "#runSearchButton"
    waitFor (fun _ -> (element "#lvSecurities").Displayed)
    configuration.compareTimeout <- origCompareTimeout 

let warmupDataport () =
    let origCompareTimeout = configuration.compareTimeout
    configuration.compareTimeout <- 120.0 
    dataportLogin ()
    dataportLogout ()
    configuration.compareTimeout <- origCompareTimeout 
    