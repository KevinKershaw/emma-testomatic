﻿module testmain
open canopy
open canopy.configuration
open canopy.reporters
open runner
open etconfig
open etreporter
open commandlineargs
open warmup
open System

let usage = "usage: EmmaTestomatic [args]\n" +
            "where args is optional and can be a combination of the following:\n" +
            "    -Firefox (use firefox browser*)\n" +
            "    -Chrome (use chrome browser)\n" +
            "    -IE (use internet explorer browser)\n" +
            "    -WarmUp (run prepare both emma and dataport for testing*)\n" +
            "    -NoWarmUp (do NOT run prepare both emma and dataport for testing)\n" +
            "    -Log (create log file of results, use logFileTemplate entry in config.yaml to specify file name)\n" +
            "    -Dev (run only the dev test suite)\n" +
            "    -PressEnter (require entering return key at end of tests)\n" +
            "    -Help or -? (display this usage text and run no tests)\n" +
            "parameters are not case sensitive\n" +
            "* default\n" +
            "running without specifying any args is equivalent to \"-Firefox -WarmUp\"\n" +
            "test case parameters are specified in the \"config.yaml\" file\n" +
            "this file can be edited with notepad or any other text editor"

let configReporters (cla : CommandLineArgs) =
    if cla.log then
        let logReporter = new LogFileReporter (config.logFileTemplate) :> IReporter
        let t = new TeeReporter (logReporter, new ConsoleReporter()) :> IReporter
        canopy.configuration.reporter <- t
        logReporter.describe (sprintf "EmmmaTestomatic version %s" versioninfo.versionString)
        logReporter.describe (sprintf "tests executed on %s %s" (DateTime.Now.ToShortDateString()) (DateTime.Now.ToShortTimeString()))
        let bstr = match cla.browser with
                   | Firefox -> "firefox"
                   | Chrome -> "chrome"
                   | IE -> "ie"
        logReporter.describe (sprintf "browser: %s" bstr)
        logReporter.describe "parameters:"
        logReporter.write ("baseEmmaUrl: " + baseEmmaUrl)
        logReporter.write ("baseDataportUrl: " + baseDataportUrl)

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
            configReporters cla
            chromeDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
            ieDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
            if cla.devMode then
                devtests.all ()
            else
                publicportaltests.all ()
                myemmatests.all ()
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
