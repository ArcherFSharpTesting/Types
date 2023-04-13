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

type TestExecutionFailure =
    | CombinationFailure of failureA: TestExecutionFailure * failureB: TestExecutionFailure
    | FailureWithMessage of message: string * failure: TestExecutionFailure
    | VerificationFailure of failure: VerificationInfo * location: CodeLocation
    | TestExceptionFailure of e: exn
    | OtherFailure of message: string * location: CodeLocation

type SetupTearDownFailure =
    | SetupTearDownExceptionFailure of e: exn
    | GeneralSetupTearDownFailure of message: String * location: CodeLocation

type TestingFailure =
    | ExceptionFailure of e: exn
    | CancelFailure
    | TestExecutionFailure of failure: TestExecutionFailure 
    | SetupFailure of failure: SetupTearDownFailure
    | TearDownFailure of failure: SetupTearDownFailure
    
type TestResult =
    | TestIgnored of message: string option * location: CodeLocation
    | TestSuccess
    | TestFailure of failure: TestingFailure