namespace Archer.CoreTypes.Lib

type Failure =
    | VerificationFailure of string
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