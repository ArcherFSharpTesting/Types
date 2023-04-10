namespace Archer.CoreTypes.InternalTypes

open System
open System.ComponentModel
open Archer

type TestCancelEventArgsWithResults (cancel: bool, result: TestResult) =
    inherit CancelEventArgs (cancel)
    
    new (result: TestResult) = TestCancelEventArgsWithResults (false, result)
    
    member _.TestResult with get () = result 
  
type TestEventArgs (result: TestResult) =
    inherit EventArgs()
    
    member _.TestResult with get () = result
    static member Success with get () = TestEventArgs TestSuccess
    static member Failure failure = TestFailure failure

type CancelTestDelegate = delegate of obj * TestCancelEventArgsWithResults -> unit
type CancelDelegate = delegate of obj * CancelEventArgs -> unit
type TestResultDelegate = delegate of obj * TestEventArgs -> unit
type TestDelegate = delegate of obj * EventArgs -> unit

type TestTag =
    | Category of string

type ITestExecutor =
    [<CLIEvent>]
    abstract member StartExecution: IEvent<CancelDelegate, CancelEventArgs> with get
    
    [<CLIEvent>]
    abstract member StartSetup: IEvent<CancelDelegate, CancelEventArgs> with get
    [<CLIEvent>]
    abstract member EndSetup: IEvent<CancelTestDelegate, TestCancelEventArgsWithResults> with get
    
    [<CLIEvent>]
    abstract member StartTest: IEvent<CancelDelegate, CancelEventArgs> with get
    [<CLIEvent>]
    abstract member EndTest: IEvent<TestResultDelegate, TestEventArgs> with get
    
    [<CLIEvent>]
    abstract member StartTearDown: IEvent<TestDelegate, EventArgs> with get
    
    [<CLIEvent>]
    abstract member EndExecution: IEvent<TestResultDelegate, TestEventArgs> with get

    abstract member Execute: unit -> TestResult
    
    abstract member Parent: ITest with get

and ITest =
    abstract member ContainerPath: string with get
    abstract member ContainerName: string with get
    abstract member TestName: string with get
    abstract member FilePath: string with get
    abstract member FileName: string with get
    abstract member LineNumber: int with get
    abstract member Tags: TestTag seq
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