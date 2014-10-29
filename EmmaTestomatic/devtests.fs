module devtests
open canopy
open runner
open etconfig
open lib
open System

let all _ = 
    context "dev tests"
    once (fun _ -> warmup.warmupEmma())

    wip ( fun _ ->
        url (baseEmmaUrl + "Home/Index")
        click "Advanced Search"
        on (baseEmmaUrl + "Search/Search.aspx")
        js """$("#stateDropdownList").val("VA"); $("#stateDropdownList").multiselect("refresh");""" |> ignore
        "#issuerName" << "Richmond"
        "#fitchOperatorDropDown" << "EQGT"
        "#fitchDropDown" << "1050"
        "#snpOperatorDropDown" << "EQGT"
        "#snpDropDown" << "1500"
        click "#runSearchButton"
        displayed "#counterLabel"
        displayed "#securitiesTab"
        "#searchName" << "Testing 2"
        click "Save Search"
    )
