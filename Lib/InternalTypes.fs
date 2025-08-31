namespace Archer.Types.InternalTypes

open System
open System.ComponentModel
open Archer

/// <summary>
/// Represents the lifecycle events that can occur during test execution.
/// </summary>
type TestEventLifecycle =
    /// <summary>Event fired when test execution is about to start.</summary>
    /// <param name="CancelEventArgs">Arguments for cancellation.</param>
    | TestStartExecution of CancelEventArgs
    /// <summary>Event fired when test setup is about to start.</summary>
    /// <param name="CancelEventArgs">Arguments for cancellation.</param>
    | TestStartSetup of CancelEventArgs
    /// <summary>Event fired when test setup has ended.</summary>
    /// <param name="result">The result of the setup phase.</param>
    /// <param name="cancelEventArgs">Arguments for cancellation.</param>
    | TestEndSetup of result: SetupResult * cancelEventArgs: CancelEventArgs
    /// <summary>Event fired when the test itself is about to start.</summary>
    /// <param name="CancelEventArgs">Arguments for cancellation.</param>
    | TestStart of CancelEventArgs
    /// <summary>Event fired when the test itself has ended.</summary>
    /// <param name="TestResult">The result of the test execution.</param>
    | TestEnd of TestResult
    /// <summary>Event fired when test teardown is about to start.</summary>
    | TestStartTeardown
    /// <summary>Event fired when test execution has fully ended (including teardown).</summary>
    /// <param name="TestExecutionResult">The result of the full test execution.</param>
    | TestEndExecution of TestExecutionResult

/// <summary>
/// Delegate type for handling test execution lifecycle events.
/// </summary>
type TestExecutionDelegate = delegate of obj * TestEventLifecycle -> unit

/// <summary>
/// Interface for executing a test and handling its lifecycle events.
/// </summary>
type ITestExecutor =
    /// <summary>Event triggered for test lifecycle changes.</summary>
    [<CLIEvent>]
    abstract member TestLifecycleEvent: IEvent<TestExecutionDelegate, TestEventLifecycle>

    /// <summary>Executes the test in the given environment.</summary>
    /// <param name="environment">The runner environment for the test.</param>
    abstract member Execute: environment: RunnerEnvironment -> TestExecutionResult
    
    /// <summary>The parent test associated with this executor.</summary>
    abstract member Parent: ITest with get

/// <summary>
/// Interface representing a test, including its info and executor.
/// </summary>
and ITest =
    inherit ITestInfo
    /// <summary>Gets the executor for this test.</summary>
    abstract member GetExecutor: unit -> ITestExecutor

/// <summary>
/// Represents timing information for the different phases of a test.
/// </summary>
type TestTiming = {
    /// <summary>The duration of the setup phase.</summary>
    Setup: TimeSpan
    /// <summary>The duration of the test execution phase.</summary>
    Test: TimeSpan
    /// <summary>The duration of the teardown phase.</summary>
    Teardown: TimeSpan
    /// <summary>The total duration of the test (setup + test + teardown).</summary>
    Total: TimeSpan
}