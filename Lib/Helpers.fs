[<AutoOpen>]
module Archer.CoreTypes.InternalTypes.Helpers

open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.FrameworkTypes

let addMany (tests: ITest list list) (framework: IFramework) =
    let tests = tests |> List.concat
    
    tests |> framework.AddTests
    
let add (tests: ITest list) (framework: IFramework) =
    framework.AddTests tests
    
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