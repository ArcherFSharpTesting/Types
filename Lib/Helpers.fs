
/// <summary>
/// Provides helper functions for working with Archer test types and runners.
/// </summary>
[<AutoOpen>]
module Archer.CoreTypes.InternalTypes.Helpers

open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.RunnerTypes

/// <summary>
/// Adds multiple lists of tests to a runner.
/// </summary>
/// <param name="tests">A list of test lists to add.</param>
/// <param name="runner">The test runner to add tests to.</param>
let addMany (tests: ITest list list) (runner: IRunner) =
    let tests = tests |> List.concat
    tests |> runner.AddTests
    
/// <summary>
/// Adds a list of tests to a runner.
/// </summary>
/// <param name="tests">The list of tests to add.</param>
/// <param name="runner">The test runner to add tests to.</param>
let add (tests: ITest list) (runner: IRunner) =
    runner.AddTests tests
    
/// <summary>
/// Gets the name of a test.
/// </summary>
/// <param name="test">The test to get the name from.</param>
let getTestName (test: ITest) =
    test.TestName
    
/// <summary>
/// Gets the tags associated with a test.
/// </summary>
/// <param name="test">The test to get tags from.</param>
let getTags (test: ITest) =
    test.Tags
    
/// <summary>
/// Gets the container name of a test.
/// </summary>
/// <param name="test">The test to get the container name from.</param>
let getContainerName (test: ITest) =
    test.ContainerName
    
/// <summary>
/// Gets the container path (namespace) of a test.
/// </summary>
/// <param name="test">The test to get the container path from.</param>
let getContainerPath (test: ITest) =
    test.ContainerPath
    
/// <summary>
/// Gets the file path where the test is located.
/// </summary>
/// <param name="test">The test to get the file path from.</param>
let getFilePath (test: ITest) =
    test.Location.FilePath
    
/// <summary>
/// Gets the file name where the test is located.
/// </summary>
/// <param name="test">The test to get the file name from.</param>
let getFileName (test: ITest) =
    test.Location.FileName
    
/// <summary>
/// Gets the line number where the test is located.
/// </summary>
/// <param name="test">The test to get the line number from.</param>
let getLineNumber (test: ITest) =
    test.Location.LineNumber
    
/// <summary>
/// Gets the code location information for a test.
/// </summary>
/// <param name="test">The test to get the location from.</param>
let getTestLocation (test: ITest) =
    test.Location
    
/// <summary>
/// Gets the executor function for a test.
/// </summary>
/// <param name="test">The test to get the executor from.</param>
let getTestExecutor (test: ITest) =
    test.GetExecutor ()