namespace Archer.CoreTypes.InternalTypes.FrameworkTypes

open System
open System.ComponentModel

open Archer
open Archer.CoreTypes.InternalTypes

type TestFailContainer =
    | EmptyFailures
    | FailedTests of (TestingFailure * ITest) list
    | FailContainer of name: string * (TestFailContainer list)
    
type TestSuccessContainer =
    | EmptySuccesses
    | SucceededTests of ITest list
    | SuccessContainer of name: string * (TestSuccessContainer list)
    
type TestIgnoreContainer =
    | EmptyIgnore
    | IgnoredTests of (string option * ITest) list
    | IgnoreContainer of name: string * (TestIgnoreContainer list)

type RunResults = {
    Failures: TestFailContainer list
    Successes: TestSuccessContainer list
    Ignored: TestIgnoreContainer list
    Seed: int
}

type FrameworkEventLifecycle =
    | FrameworkStartExecution of CancelEventArgs
    | TestLifeCycle of TestEventLifecycle
    | FrameworkEndExecution
    
type FrameworkExecutionDelegate = delegate of obj * FrameworkEventLifecycle -> unit

type FrameWorkTestArgs (test: ITest) =
    inherit EventArgs ()
    
    member _.Test with get () = test

type FrameWorkTestResultArgs (test: ITest, result: TestResult) =
    inherit EventArgs ()
    
    member _.Test with get () = test
    member _.Result with get () = result

type FrameworkTestCancelArgs (cancel: bool, test: ITest) =
    inherit CancelEventArgs (cancel)

    new (test) = FrameworkTestCancelArgs (false, test)
    
    member _.Test with get () = test
    
type FrameworkTestResultCancelArgs (cancel: bool, test: ITest, result: TestResult) =
    inherit CancelEventArgs (cancel)
    
    new (test: ITest) = FrameworkTestResultCancelArgs (false, test, TestSuccess)
    
    new (test: ITest, result: TestResult) = FrameworkTestResultCancelArgs (false, test, result)
    
    member _.Test with get () = test
    member _.TestResult with get () = result
    
type FrameworkDelegate = delegate of obj * EventArgs -> unit
type FrameworkCancelDelegate = delegate of obj * CancelEventArgs -> unit

type FrameworkTestDelegate = delegate of obj * FrameWorkTestArgs -> unit
type FrameworkTestResultDelegate = delegate of obj * FrameWorkTestResultArgs -> unit
type FrameworkTestCancelDelegate = delegate of obj * FrameworkTestCancelArgs -> unit
type FrameworkTestResultCancelDelegate = delegate of obj * FrameworkTestResultCancelArgs -> unit

type IFramework =
    abstract member Run: unit -> RunResults
    abstract member Run: getSeed: (unit -> int) -> RunResults
    abstract member AddTests: newTests: ITest seq -> IFramework
    [<CLIEvent>]
    abstract member FrameworkLifecycleEvent: IEvent<FrameworkExecutionDelegate, FrameworkEventLifecycle>