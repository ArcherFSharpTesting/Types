namespace Archer.CoreTypes.InternalTypes

open System
open System.ComponentModel
open Archer


type TestEventLifecycle =
    | TestStartExecution of CancelEventArgs
    | TestStartSetup of CancelEventArgs
    | TestEndSetup of result: SetupResult * cancelEventArgs: CancelEventArgs
    | TestStart of CancelEventArgs
    | TestEnd of TestResult
    | TestStartTeardown
    | TestEndExecution of TestExecutionResult
    
type TestExecutionDelegate = delegate of obj * TestEventLifecycle -> unit

type ITestExecutor =
    [<CLIEvent>]
    abstract member TestLifecycleEvent: IEvent<TestExecutionDelegate, TestEventLifecycle>

    abstract member Execute: environment: RunnerEnvironment -> TestExecutionResult
    
    abstract member Parent: ITest with get

and ITest =
    inherit ITestInfo
    abstract member GetExecutor: unit -> ITestExecutor
    
type TestTiming = {
    Setup: TimeSpan
    Test: TimeSpan
    Teardown: TimeSpan
    Total: TimeSpan
}