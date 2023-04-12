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
    | FailureWithMessage of string * TestingFailure
    | CombinationFailure of TestingFailure * TestingFailure
    | ExceptionFailure of exn
    | VerificationFailure of VerificationInfo * CodeLocation
    | GeneralFailure of string * CodeLocation
    | SetupFailure of string * CodeLocation
    | CancelFailure
    | TearDownFailure of string * CodeLocation
    
type TestResult =
    | Ignored of string option * CodeLocation
    | TestSuccess
    | TestFailure of TestingFailure