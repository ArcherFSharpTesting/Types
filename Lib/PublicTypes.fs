namespace Archer.CoreTypes

type VerificationInfo = {
    Expected: string
    Actual: string
}

type Failure =
    | VerificationFailure of VerificationInfo
    | ExceptionFailure of exn
    | GeneralFailure of string
    | SetupFailure of string
    | CancelFailure
    | IgnoredFailure of string option
    | TearDownFailure of string
    | FailureWithMessage of string * Failure
    | CombinationFailure of Failure * Failure
    
type TestResult =
    | TestSuccess
    | TestFailure of Failure