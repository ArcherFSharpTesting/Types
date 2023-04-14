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
    | TestExceptionFailure of e: exn
    | OtherFailure of message: string * location: CodeLocation
    
type TestResult =
    | TestFailure of TestFailure
    | TestIgnored of message: string option * CodeLocation
    | TestSuccess

type SetupTearDownFailure =
    | SetupTearDownExceptionFailure of e: exn
    | GeneralSetupTearDownFailure of message: String * location: CodeLocation
    
type SetupResult =
    | SetupFailure of SetupTearDownFailure
    | SetupSuccess
    
type TeardownResult =
    | TeardownFailure of SetupTearDownFailure
    | TearDownSuccess

type GeneralTestingFailure =
     | ExceptionFailure of e: exn
     | CancelFailure
     | GeneralFailure of message: string

type TestExecutionResult =
    | SetupFailure of SetupTearDownFailure
    | TestResult of TestResult
    | TearDownFailure of SetupTearDownFailure
    | GeneralFailure of GeneralTestingFailure
