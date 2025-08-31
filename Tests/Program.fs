open Archer
open Archer.Runner
open Archer.Types.InternalTypes
open Archer.Types.InternalTypes.RunnerTypes
open Archer.Types.Tests
open Archer.Reporting.Summaries
open MicroLang.Lang

let runner = runnerFactory.Runner ()

runner.RunnerLifecycleEvent
|> Event.add (fun args ->
    match args with
    | RunnerStartExecution _ ->
        printfn ""
    | RunnerTestLifeCycle (test, testEventLifecycle, _) ->
        match testEventLifecycle with
        | TestEndExecution testExecutionResult ->
            match testExecutionResult with
            | TestExecutionResult TestSuccess -> ()
            | result ->
                let transformedResult = defaultTestExecutionResultSummaryTransformer result test
                printfn $"%s{transformedResult}"
            
        | _ -> ()
    | RunnerEndExecution ->
        printfn "\n"
)

runner
|> addMany [
    ``TestResult Plus``.``Test Cases``
]
|> runAndReport
