namespace Archer

open System

type TestTag =
    | Category of string
    
type ITestInfo =
    abstract member ContainerPath: string with get
    abstract member ContainerName: string with get
    abstract member TestName: string with get
    abstract member FilePath: string with get
    abstract member FileName: string with get
    abstract member LineNumber: int with get
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
    | VerificationFailure of VerificationInfo
    | GeneralFailure of string
    | SetupFailure of string
    | CancelFailure
    | TearDownFailure of string
    
type TestResult =
    | Ignored of string option
    | TestSuccess
    | TestFailure of TestingFailure