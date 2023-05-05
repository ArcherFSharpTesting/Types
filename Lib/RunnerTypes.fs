namespace Archer.CoreTypes.InternalTypes.RunnerTypes

open System
open System.ComponentModel

open Archer
open Archer.CoreTypes.InternalTypes

type TestFailureType =
    | SetupFailureType of SetupTeardownFailure
    | TestRunFailureType of TestFailure
    | TeardownFailureType of SetupTeardownFailure
    | GeneralFailureType of GeneralTestingFailure

type TestFailContainer =
    | EmptyFailures
    | FailedTests of (TestFailureType * ITest) list
    | FailContainer of name: string * (TestFailContainer list)
    
type TestSuccessContainer =
    | EmptySuccesses
    | SucceededTests of ITest list
    | SuccessContainer of name: string * (TestSuccessContainer list)
    
type TestIgnoreContainer =
    | EmptyIgnore
    | IgnoredTests of (string option * CodeLocation * ITest) list
    | IgnoreContainer of name: string * (TestIgnoreContainer list)

type RunResults = {
    Failures: TestFailContainer list
    Successes: TestSuccessContainer list
    Ignored: TestIgnoreContainer list
    Seed: int
}

type RunnerEventLifecycle =
    | RunnerStartExecution of CancelEventArgs
    | RunnerTestLifeCycle of ITest * TestEventLifecycle * CancelEventArgs
    | RunnerEndExecution
    
type RunnerExecutionDelegate = delegate of obj * RunnerEventLifecycle -> unit

type RunnerTestArgs (test: ITest) =
    inherit EventArgs ()
    
    member _.Test with get () = test

type RunnerTestResultArgs (test: ITest, result: TestResult) =
    inherit EventArgs ()
    
    member _.Test with get () = test
    member _.Result with get () = result

type RunnerTestCancelArgs (cancel: bool, test: ITest) =
    inherit CancelEventArgs (cancel)

    new (test) = RunnerTestCancelArgs (false, test)
    
    member _.Test with get () = test
    
type RunnerTestResultCancelArgs (cancel: bool, test: ITest, result: TestResult) =
    inherit CancelEventArgs (cancel)
    
    new (test: ITest) = RunnerTestResultCancelArgs (false, test, TestSuccess)
    
    new (test: ITest, result: TestResult) = RunnerTestResultCancelArgs (false, test, result)
    
    member _.Test with get () = test
    member _.TestResult with get () = result
    
type RunnerDelegate = delegate of obj * EventArgs -> unit
type RunnerCancelDelegate = delegate of obj * CancelEventArgs -> unit

type RunnerTestDelegate = delegate of obj * RunnerTestArgs -> unit
type RunnerTestResultDelegate = delegate of obj * RunnerTestResultArgs -> unit
type RunnerTestCancelDelegate = delegate of obj * RunnerTestCancelArgs -> unit
type RunnerTestResultCancelDelegate = delegate of obj * RunnerTestResultCancelArgs -> unit

type IRunner =
    abstract member Run: unit -> RunResults
    abstract member Run: getSeed: (unit -> int) -> RunResults
    abstract member Run: filter: (ITest list -> ITest list) -> RunResults
    abstract member Run: filter: (ITest list -> ITest list) * getSeed: (unit -> int) -> RunResults
    abstract member TestTags: TestTag list with get
    abstract member AddTests: newTests: ITest seq -> IRunner
    [<CLIEvent>]
    abstract member RunnerLifecycleEvent: IEvent<RunnerExecutionDelegate, RunnerEventLifecycle>