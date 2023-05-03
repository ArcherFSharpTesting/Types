open Archer
open Archer.Bow
open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.FrameworkTypes
open Archer.CoreTypes.Tests
open MicroLang.Lang

let framework = bow.Framework ()

let testFilter (_test: ITest) =
    true
    
framework.FrameworkLifecycleEvent
|> Event.add (fun args ->
    match args with
    | FrameworkStartExecution _ ->
        printfn ""
    | FrameworkTestLifeCycle (test, testEventLifecycle, _) ->
        match testEventLifecycle with
        | TestEndExecution testExecutionResult ->
            let successMsg =
                match testExecutionResult with
                | TestExecutionResult TestSuccess -> "Success"
                | _ -> "Fail"
                
            let report = $"%A{test} : (%s{successMsg})"
            printfn $"%s{report}"
        | _ -> ()
    | FrameworkEndExecution ->
        printfn "\n"
)

framework
|> addManyTests [
    ``TestResult Plus``.``Test Cases``
]
|> filterRunAndReport testFilter
