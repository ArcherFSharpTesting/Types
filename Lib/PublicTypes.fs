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

type TestingFailure =
    | FailureWithMessage of message: string * failure: TestingFailure
    | CombinationFailure of TestingFailure * TestingFailure
    | ExceptionFailure of exn
    | VerificationFailure of failure: VerificationInfo * location: CodeLocation
    | GeneralFailure of message: string * location: CodeLocation
    | SetupFailure of message: string * location: CodeLocation
    | CancelFailure
    | TearDownFailure of message: string * location: CodeLocation
    
type TestResult =
    | Ignored of message: string option * location: CodeLocation
    | TestSuccess
    | TestFailure of failure: TestingFailure