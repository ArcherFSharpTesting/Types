namespace Archer

open System

type TestTag =
    | Category of string
    
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

type TestFailure =
    | CombinationFailure of failureA: TestFailure * failureB: TestFailure
    | FailureWithMessage of message: string * failure: TestFailure
    | VerificationFailure of failure: VerificationInfo * location: CodeLocation
    | TestCanceledFailure
    | TestExceptionFailure of e: exn
    | OtherFailure of message: string * location: CodeLocation
    
type TestResult =
    | TestFailure of TestFailure
    | TestIgnored of message: string option * CodeLocation
    | TestSuccess

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