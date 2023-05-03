[<AutoOpen>]
module Archer.Helpers

open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.FrameworkTypes

let addMany (tests: ITest list list) (framework: IFramework) =
    let tests = tests |> List.concat
    
    tests |> framework.AddTests
    
let add (tests: ITest list) (framework: IFramework) =
    framework.AddTests tests