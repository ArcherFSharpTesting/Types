namespace Archer

open System

/// <summary>
/// Represents a tag that can be applied to a test for categorization or control.
/// </summary>
type TestTag =
    ///<summary>Specifies a category for the test.</summary>
    | Category of string
    ///<summary>Marks the test to be run exclusively.</summary>
    | Only
    ///<summary>Marks the test to be run serially.</summary>
    | Serial
    
/// <summary>
/// Represents the location of code in a file.
/// </summary>
type CodeLocation = {
    /// <summary>The full file path where the code is located.</summary>
    FilePath: string
    /// <summary>The file name where the code is located.</summary>
    FileName: string
    /// <summary>The line number in the file.</summary>
    LineNumber: int
}

/// <summary>The namespace of the test container.</summary>
type ITestNameInfo = 
    abstract member ContainerPath: string with get
    /// <summary>The name of the container (e.g., module or class) for the test.</summary>
    abstract member ContainerName: string with get
    /// <summary>The name of the test.</summary>
    abstract member TestName: string with get
    
/// <summary>Tags associated with the test.</summary>
type ITestMetaData =
    abstract member Tags: TestTag seq
    
/// <summary>The code location information for the test.</summary>
type ITestLocationInfo =
    abstract member Location: CodeLocation with get
    
/// <summary>Aggregates test name, metadata, and location information.</summary>
type ITestInfo =
    inherit ITestNameInfo
    inherit ITestMetaData
    inherit ITestLocationInfo
    
/// <summary>The name of the test runner.</summary>
type RunnerEnvironment = {
    RunnerName: string
    /// <summary>The version of the test runner.</summary>
    RunnerVersion: Version
    /// <summary>Information about the test being executed.</summary>
    TestInfo: ITestInfo
}
/// <summary>
/// Provides information about the expected and actual values used in a test verification.
/// </summary>
type IVerificationInfo = 
    /// <summary>The expected value in a verification.</summary>
    abstract member Expected: string with get
    /// <summary>The actual value in a verification.</summary>
    abstract member Actual: string with get


/// <summary>Represents a failure in test expectation.</summary>
type TestExpectationFailure =
    /// <summary>Failure with a message and nested failure.</summary>
    /// <param name="message">The failure message.</param>
    /// <param name="failure">The nested expectation failure.</param>
    | FailureWithMessage of message: string * failure: TestExpectationFailure
    /// <summary>Failure due to verification mismatch.</summary>
    /// <param name="failure">The verification failure information.</param>
    | ExpectationVerificationFailure of failure: IVerificationInfo
    /// <summary>Other expectation failure with a message.</summary>
    /// <param name="message">The failure message.</param>
    | ExpectationOtherFailure of message: string
    
/// <summary>Represents a failure that occurred during test execution.</summary>
/// <summary>Represents a failure that occurred during test execution.</summary>
type TestFailure =
    /// <summary>Combination of two failures.</summary>
    /// <param name="failureA">The first failure and its optional code location.</param>
    /// <param name="failureB">The second failure and its optional code location.</param>
    | CombinationFailure of failureA: (TestFailure * CodeLocation option) * failureB: (TestFailure * CodeLocation option)
    /// <summary>Test failed due to expectation failure at a location.</summary>
    /// <param name="TestExpectationFailure">The expectation failure.</param>
    /// <param name="CodeLocation">The code location where the failure occurred.</param>
    | TestExpectationFailure of TestExpectationFailure * CodeLocation
    /// <summary>Test was ignored with an optional message and location.</summary>
    /// <param name="message">The optional ignore message.</param>
    /// <param name="CodeLocation">The code location where the test was ignored.</param>
    | TestIgnored of message: string option * CodeLocation
    /// <summary>Test failed due to an exception.</summary>
    /// <param name="e">The exception that caused the failure.</param>
    | TestExceptionFailure of e: exn
    
/// <summary>Represents the result of a test execution.</summary>
type TestResult =
    ///<summary>The test failed with a failure reason.</summary>
    | TestFailure of TestFailure
    ///<summary>The test succeeded.</summary>
    | TestSuccess
    /// <summary>
    /// Combines two <see cref="TestResult"/> values.
    /// </summary>
    static member (+) (left: TestResult, right: TestResult) =
        match left, right with
        | TestFailure leftFailure, TestFailure rightFailure  ->
            CombinationFailure ((leftFailure, None), (rightFailure, None))
            |> TestFailure
        | TestFailure testFailure, TestSuccess
        | TestSuccess, TestFailure testFailure ->
            testFailure |> TestFailure
        | TestSuccess, TestSuccess -> TestSuccess

/// <summary>Represents a failure that occurred during setup or teardown.</summary>
type SetupTeardownFailure =
    /// <summary>Failure due to an exception.</summary>
    /// <param name="e">The exception that caused the failure.</param>
    | SetupTeardownExceptionFailure of e: exn
    /// <summary>Failure due to cancellation.</summary>
    | SetupTeardownCanceledFailure
    /// <summary>General failure with a message and location.</summary>
    /// <param name="message">The failure message.</param>
    /// <param name="location">The code location where the failure occurred.</param>
    | GeneralSetupTeardownFailure of message: String * location: CodeLocation
    
/// <summary>Represents the result of a setup operation.</summary>
type SetupResult =
    /// <summary>Setup failed with a failure reason.</summary>
    /// <param name="SetupTeardownFailure">The failure reason.</param>
    | SetupFailure of SetupTeardownFailure
    /// <summary>Setup succeeded.</summary>
    | SetupSuccess
    
/// <summary>Represents the result of a teardown operation.</summary>
type TeardownResult =
    /// <summary>Teardown failed with a failure reason.</summary>
    /// <param name="SetupTeardownFailure">The failure reason.</param>
    | TeardownFailure of SetupTeardownFailure
    /// <summary>Teardown succeeded.</summary>
    | TeardownSuccess

/// <summary>Represents a general failure during testing.</summary>
type GeneralTestingFailure =
    /// <summary>General failure due to an exception.</summary>
    /// <param name="e">The exception that caused the failure.</param>
    | GeneralExceptionFailure of e: exn
    /// <summary>General failure due to cancellation.</summary>
    | GeneralCancelFailure
    /// <summary>General failure with a message.</summary>
    /// <param name="message">The failure message.</param>
    | GeneralFailure of message: string

/// <summary>Represents the result of executing a test, including setup and teardown phases.</summary>
type TestExecutionResult =
    /// <summary>Failure during setup execution.</summary>
    /// <param name="SetupTeardownFailure">The failure that occurred during setup.</param>
    | SetupExecutionFailure of SetupTeardownFailure
    /// <summary>Result of the test execution.</summary>
    /// <param name="TestResult">The result of the test execution.</param>
    | TestExecutionResult of TestResult
    /// <summary>Failure during teardown execution.</summary>
    /// <param name="SetupTeardownFailure">The failure that occurred during teardown.</param>
    | TeardownExecutionFailure of SetupTeardownFailure
    /// <summary>General failure during execution.</summary>
    /// <param name="GeneralTestingFailure">The general failure that occurred during execution.</param>
    | GeneralExecutionFailure of GeneralTestingFailure
