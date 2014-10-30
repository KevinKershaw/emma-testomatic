module myemmatests
open canopy
open runner
open etconfig
open lib

let meIsLoggedIn () =
    let isVisible = (js """return $("#userSignedInPanel").is(":visible");""")
    isVisible.ToString() = "true"

let meLogin () =
    click "#myEmma"
    displayed "#signInPanel"
    click "#Email"
    sleep 0.01
    "#Email" << config.myEmma.userId
    "#Pin" << config.myEmma.password
    click "#signInButton"
    "#userSignedInPanel" == ("Welcome, " + config.myEmma.userId)

let meLogout () =
    click "#myEmma"
    click ".menuSignOut"
    notDisplayed "#userSignedInPanel"


let all _ = 
    context "myemma tests basic"
    once(fun _ ->
        url (baseEmmaUrl + "Home/Index")
        if meIsLoggedIn () then
            meLogout ()
    )

    "myemma login" &&& fun _ ->
        meLogin ()

    "myemma logout" &&& fun _ ->
        meLogout ()

(*    
    context "myemma tests extended"
    once(fun _ ->
        url (baseEmmaUrl + "Home/Index")
        if meIsLoggedIn () = false then
            meLogin ()
    )

    "CreateSavedSearch" &&& fun _->
        url (baseEmmaUrl + "Home/Index")
        click "#ctl00_Masthead_search"
        js """$("#stateDropdownList").val("VA"); $("#stateDropdownList").multiselect("refresh");""" |> ignore
*)