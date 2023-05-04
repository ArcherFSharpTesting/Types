[<AutoOpen>]
module Archer.CoreTypes.InternalTypes.Helpers

open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.RunnerTypes

let addMany (tests: ITest list list) (runner: IRunner) =
    let tests = tests |> List.concat
    
    tests |> runner.AddTests
    
let add (tests: ITest list) (runner: IRunner) =
    runner.AddTests tests
    
let getTestName (test: ITest) =
    test.TestName
    
let getTags (test: ITest) =
    test.Tags
    
let getContainerName (test: ITest) =
    test.ContainerName
    
let getContainerPath (test: ITest) =
    test.ContainerPath
    
let getFilePath (test: ITest) =
    test.Location.FilePath
    
let getFileName (test: ITest) =
    test.Location.FileName
    
let getLineNumber (test: ITest) =
    test.Location.LineNumber
    
let getTestLocation (test: ITest) =
    test.Location