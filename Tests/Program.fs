open Archer
open Archer.Bow
open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.RunnerTypes
open Archer.CoreTypes.Tests
open MicroLang.Lang

let runner = bow.Runner ()

runner.RunnerLifecycleEvent
|> Event.add (fun args ->
    match args with
    | RunnerStartExecution _ ->
        printfn ""
    | RunnerTestLifeCycle (test, testEventLifecycle, _) ->
        match testEventLifecycle with
        | TestEndExecution testExecutionResult ->
            let successMsg =
                match testExecutionResult with
                | TestExecutionResult TestSuccess -> "Success"
                | _ -> "Fail"
                
            let report = $"%A{test} : (%s{successMsg})"
            printfn $"%s{report}"
        | _ -> ()
    | RunnerEndExecution ->
        printfn "\n"
)

runner
|> addMany [
    ``TestResult Plus``.``Test Cases``
]
|> runAndReport
