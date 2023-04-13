namespace Archer.CoreTypes.InternalTypes

open System
open System.ComponentModel
open Archer


type TestEventLifecycle =
    | TestExecutionStarted of CancelEventArgs
    | TestSetupStarted of CancelEventArgs
    | TestEndSetup of testResult: TestResult * cancelEventArgs: CancelEventArgs
    | TestStart of CancelEventArgs
    | TestEnd of TestResult
    | TestStartTearDown
    | TestEndExecution of TestResult
    
type TestExecutionDelegate = delegate of obj * TestEventLifecycle -> unit

type ITestExecutor =
    [<CLIEvent>]
    abstract member TestLifecycleEvent: IEvent<TestExecutionDelegate, TestEventLifecycle>

    abstract member Execute: environment: FrameworkEnvironment -> TestResult
    
    abstract member Parent: ITest with get

and ITest =
    inherit ITestInfo
    abstract member GetExecutor: unit -> ITestExecutor
    
type TestTiming = {
    Setup: TimeSpan
    Test: TimeSpan
    TearDown: TimeSpan
    Total: TimeSpan
}
    
type TestFailureReport = {
    Result: TestResult
    Time: TestTiming
    Test: ITest
}

type TestSuccessReport = {
    Time: TestTiming
    Test: ITest
}
    
type TestContainerReport = {
    ContainerFullName: string
    ContainerName: string
    Failures: TestFailureReport list
    Successes: TestSuccessReport list
}

type TestReport = {
    Seed: int
    TestContainers: TestContainerReport list
}