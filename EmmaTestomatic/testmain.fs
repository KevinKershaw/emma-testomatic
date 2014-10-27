module testmain
open canopy
open canopy.configuration
open runner
open etconfig
open commandlineargs
open warmup
open System

let usage = "usage: EmmaTestomatic [args]\n" +
            "where args is optional and can be a combination of the following:\n" +
            "    -Firefox (use firefox browser*)\n" +
            "    -Chrome (use chrome browser)\n" +
            "    -IE (use internet explorer browser)\n" +
            "    -WarmUp (run prepare both emma and dataport for testing*)\n" +
            "    -Dev (run only the dev test suite)\n" +
            "    -PressEnter (require entering return key at end of tests)\n" +
            "    -Help or -? (display this usage text and run no tests)\n" +
            "parameters are not case sensitive\n" +
            "* default\n" +
            "running without specifying any args is equivalent to \"-Firefox -WarmUp\"\n" +
            "test case parameters are specified in the \"config.yaml\" file\n" +
            "this file can be edited with notepad or any other text editor"

[<EntryPoint>]
let main argv =
    let cla = parseCommandLine argv
    if cla.versionOnly then
        printfn "%s" versioninfo.versionString
        0
    else
        printfn "EmmmaTestomatic version %s" versioninfo.versionString
        if cla.showUsage || not cla.isValid then
            printfn "%s" usage
            -1
        else
            chromeDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
            ieDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
            if cla.devMode then
                devtests.all ()
            else
                publicportaltests.all ()
                submissiontests.all ()
            match cla.browser with
                | Firefox -> start firefox
                | Chrome -> start chrome
                | IE -> start ie
            if cla.warmUp then
                warmupEmma ()
                warmupDataport ()
            run()
            if cla.pressEnter then
                printfn "press [enter] to exit"
                System.Console.ReadLine() |> ignore
            quit()
            0
