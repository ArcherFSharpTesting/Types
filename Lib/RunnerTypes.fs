namespace Archer.Types.InternalTypes.RunnerTypes

open System
open System.ComponentModel

open Archer
open Archer.Types.InternalTypes

/// <summary>
/// Represents the type of failure that can occur during a test run.
/// </summary>
type TestFailureType =
    /// <summary>Failure during setup.</summary>
    /// <param name="SetupTeardownFailure">The setup failure details.</param>
    | SetupFailureType of SetupTeardownFailure
    /// <summary>Failure during test execution.</summary>
    /// <param name="TestFailure">The test failure details.</param>
    | TestRunFailureType of TestFailure
    /// <summary>Failure during teardown.</summary>
    /// <param name="SetupTeardownFailure">The teardown failure details.</param>
    | TeardownFailureType of SetupTeardownFailure
    /// <summary>General failure not specific to setup, test, or teardown.</summary>
    /// <param name="GeneralTestingFailure">The general failure details.</param>
    | GeneralFailureType of GeneralTestingFailure

/// <summary>
/// Container for failed test results.
/// </summary>
type TestFailContainer =
    /// <summary>No failures.</summary>
    | EmptyFailures
    /// <summary>List of failed tests and their failure types.</summary>
    /// <param name="(TestFailureType * ITest) list">The failed tests and their failure types.</param>
    | FailedTests of (TestFailureType * ITest) list
    /// <summary>Named container for grouping failed test containers.</summary>
    /// <param name="name">The name of the container.</param>
    /// <param name="TestFailContainer list">The list of child fail containers.</param>
    | FailContainer of name: string * (TestFailContainer list)

/// <summary>
/// Container for successful test results.
/// </summary>
type TestSuccessContainer =
    /// <summary>No successes.</summary>
    | EmptySuccesses
    /// <summary>List of succeeded tests.</summary>
    /// <param name="ITest list">The succeeded tests.</param>
    | SucceededTests of ITest list
    /// <summary>Named container for grouping successful test containers.</summary>
    /// <param name="name">The name of the container.</param>
    /// <param name="TestSuccessContainer list">The list of child success containers.</param>
    | SuccessContainer of name: string * (TestSuccessContainer list)

/// <summary>
/// Container for ignored test results.
/// </summary>
type TestIgnoreContainer =
    /// <summary>No ignored tests.</summary>
    | EmptyIgnore
    /// <summary>List of ignored tests with optional message and location.</summary>
    /// <param name="(string option * CodeLocation * ITest) list">The ignored tests, their messages, and locations.</param>
    | IgnoredTests of (string option * CodeLocation * ITest) list
    /// <summary>Named container for grouping ignored test containers.</summary>
    /// <param name="name">The name of the container.</param>
    /// <param name="TestIgnoreContainer list">The list of child ignore containers.</param>
    | IgnoreContainer of name: string * (TestIgnoreContainer list)

/// <summary>
/// Represents the results of a test run, including failures, successes, and ignored tests.
/// </summary>
type RunResults =
    {
        /// <summary>The list of failure containers.</summary>
        Failures: TestFailContainer list
        /// <summary>The list of success containers.</summary>
        Successes: TestSuccessContainer list
        /// <summary>The list of ignore containers.</summary>
        Ignored: TestIgnoreContainer list
        /// <summary>The random seed used for the run.</summary>
        Seed: int
        /// <summary>The time the run began.</summary>
        Began: DateTime
        /// <summary>The time the run ended.</summary>
        End: DateTime
    }
    /// <summary>Gets the total time taken for the test run.</summary>
    member this.TotalTime with get () = this.End - this.Began

/// <summary>
/// Represents the lifecycle events that can occur during runner execution.
/// </summary>
type RunnerEventLifecycle =
    /// <summary>Event fired when runner execution is about to start.</summary>
    /// <param name="CancelEventArgs">Arguments for cancellation.</param>
    | RunnerStartExecution of CancelEventArgs
    /// <summary>Event fired for a test's lifecycle event within the runner.</summary>
    /// <param name="ITest">The test involved.</param>
    /// <param name="TestEventLifecycle">The lifecycle event for the test.</param>
    /// <param name="CancelEventArgs">Arguments for cancellation.</param>
    | RunnerTestLifeCycle of ITest * TestEventLifecycle * CancelEventArgs
    /// <summary>Event fired when runner execution has ended.</summary>
    | RunnerEndExecution

/// <summary>
/// Delegate type for handling runner execution lifecycle events.
/// </summary>
type RunnerExecutionDelegate = delegate of obj * RunnerEventLifecycle -> unit

/// <summary>
/// Event arguments for runner test events.
/// </summary>
type RunnerTestArgs (test: ITest) =
    inherit EventArgs ()
    /// <summary>The test associated with the event.</summary>
    member _.Test with get () = test

/// <summary>
/// Event arguments for runner test result events.
/// </summary>
type RunnerTestResultArgs (test: ITest, result: TestResult) =
    inherit EventArgs ()
    /// <summary>The test associated with the event.</summary>
    member _.Test with get () = test
    /// <summary>The result of the test.</summary>
    member _.Result with get () = result

/// <summary>
/// Event arguments for runner test cancellation events.
/// </summary>
type RunnerTestCancelArgs (cancel: bool, test: ITest) =
    inherit CancelEventArgs (cancel)
    /// <summary>Initializes a new instance with the specified test and cancellation state.</summary>
    /// <param name="cancel">Whether the event is cancelled.</param>
    /// <param name="test">The test associated with the event.</param>
    new (test) = RunnerTestCancelArgs (false, test)
    /// <summary>The test associated with the event.</summary>
    member _.Test with get () = test

/// <summary>
/// Event arguments for runner test result cancellation events.
/// </summary>
type RunnerTestResultCancelArgs (cancel: bool, test: ITest, result: TestResult) =
    inherit CancelEventArgs (cancel)
    /// <summary>Initializes a new instance with the specified test, result, and cancellation state.</summary>
    /// <param name="cancel">Whether the event is cancelled.</param>
    /// <param name="test">The test associated with the event.</param>
    /// <param name="result">The result of the test.</param>
    new (test: ITest) = RunnerTestResultCancelArgs (false, test, TestSuccess)
    new (test: ITest, result: TestResult) = RunnerTestResultCancelArgs (false, test, result)
    /// <summary>The test associated with the event.</summary>
    member _.Test with get () = test
    /// <summary>The result of the test.</summary>
    member _.TestResult with get () = result

/// <summary>
/// Delegate type for handling generic runner events.
/// </summary>
type RunnerDelegate = delegate of obj * EventArgs -> unit
/// <summary>
/// Delegate type for handling runner cancellation events.
/// </summary>
type RunnerCancelDelegate = delegate of obj * CancelEventArgs -> unit
/// <summary>
/// Delegate type for handling runner test events.
/// </summary>
type RunnerTestDelegate = delegate of obj * RunnerTestArgs -> unit
/// <summary>
/// Delegate type for handling runner test result events.
/// </summary>
type RunnerTestResultDelegate = delegate of obj * RunnerTestResultArgs -> unit
/// <summary>
/// Delegate type for handling runner test cancellation events.
/// </summary>
type RunnerTestCancelDelegate = delegate of obj * RunnerTestCancelArgs -> unit
/// <summary>
/// Delegate type for handling runner test result cancellation events.
/// </summary>
type RunnerTestResultCancelDelegate = delegate of obj * RunnerTestResultCancelArgs -> unit

/// <summary>
/// Interface for a test runner that can execute tests and manage their lifecycle.
/// </summary>
type IRunner =
    /// <summary>Runs all tests and returns the results.</summary>
    abstract member Run: unit -> RunResults
    /// <summary>Runs all tests with a custom seed and returns the results.</summary>
    /// <param name="getSeed">A function to get the random seed.</param>
    abstract member Run: getSeed: (unit -> int) -> RunResults
    /// <summary>Runs filtered tests and returns the results.</summary>
    /// <param name="filter">A function to filter the tests to run.</param>
    abstract member Run: filter: (ITest list -> ITest list) -> RunResults
    /// <summary>Runs filtered tests with a custom seed and returns the results.</summary>
    /// <param name="filter">A function to filter the tests to run.</param>
    /// <param name="getSeed">A function to get the random seed.</param>
    abstract member Run: filter: (ITest list -> ITest list) * getSeed: (unit -> int) -> RunResults
    /// <summary>Gets the list of test tags for the runner.</summary>
    abstract member TestTags: TestTag list with get
    /// <summary>Adds new tests to the runner.</summary>
    /// <param name="newTests">The new tests to add.</param>
    abstract member AddTests: newTests: ITest seq -> IRunner
    /// <summary>Event triggered for runner lifecycle changes.</summary>
    [<CLIEvent>]
    abstract member RunnerLifecycleEvent: IEvent<RunnerExecutionDelegate, RunnerEventLifecycle>