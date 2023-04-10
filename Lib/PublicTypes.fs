namespace Archer

type VerificationInfo = {
    Expected: string
    Actual: string
}

type TestingFailure =
    | VerificationFailure of VerificationInfo
    | ExceptionFailure of exn
    | GeneralFailure of string
    | SetupFailure of string
    | CancelFailure
    | IgnoredFailure of string option
    | TearDownFailure of string
    | FailureWithMessage of string * TestingFailure
    | CombinationFailure of TestingFailure * TestingFailure
    
type TestResult =
    | TestSuccess
    | TestFailure of TestingFailure