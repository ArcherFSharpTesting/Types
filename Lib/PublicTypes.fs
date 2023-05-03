namespace Archer

open System

type TestTag =
    | Category of string
    | Only
    | Serial
    
type CodeLocation = {
    FilePath: string
    FileName: string
    LineNumber: int
}
    
type ITestInfo =
    abstract member ContainerPath: string with get
    abstract member ContainerName: string with get
    abstract member TestName: string with get
    abstract member Location: CodeLocation with get
    abstract member Tags: TestTag seq
    
type FrameworkEnvironment = {
    FrameworkName: string
    FrameworkVersion: Version
    TestInfo: ITestInfo
}

type VerificationInfo = {
    Expected: string
    Actual: string
}

type TestExpectationFailure =
    | FailureWithMessage of message: string * failure: TestExpectationFailure
    | ExpectationVerificationFailure of failure: VerificationInfo
    | ExpectationOtherFailure of message: string
    
type TestFailure =
    | CombinationFailure of failureA: (TestFailure * CodeLocation option) * failureB: (TestFailure * CodeLocation option)
    | TestExpectationFailure of TestExpectationFailure * CodeLocation
    | TestIgnored of message: string option * CodeLocation
    | TestExceptionFailure of e: exn
    
type TestResult =
    | TestFailure of TestFailure
    | TestSuccess
    static member (+) (left: TestResult, right: TestResult) =
        match left, right with
        | TestFailure leftFailure, TestFailure rightFailure  ->
            CombinationFailure ((leftFailure, None), (rightFailure, None))
            |> TestFailure
            
        
        | TestFailure testFailure, TestSuccess
        | TestSuccess, TestFailure testFailure ->
            testFailure |> TestFailure
        | TestSuccess, TestSuccess -> TestSuccess

type SetupTeardownFailure =
    | SetupTeardownExceptionFailure of e: exn
    | SetupTeardownCanceledFailure
    | GeneralSetupTeardownFailure of message: String * location: CodeLocation
    
type SetupResult =
    | SetupFailure of SetupTeardownFailure
    | SetupSuccess
    
type TeardownResult =
    | TeardownFailure of SetupTeardownFailure
    | TeardownSuccess

type GeneralTestingFailure =
     | GeneralExceptionFailure of e: exn
     | GeneralCancelFailure
     | GeneralFailure of message: string

type TestExecutionResult =
    | SetupExecutionFailure of SetupTeardownFailure
    | TestExecutionResult of TestResult
    | TeardownExecutionFailure of SetupTeardownFailure
    | GeneralExecutionFailure of GeneralTestingFailure