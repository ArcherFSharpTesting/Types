module Archer.CoreTypes.Tests.``TestResult Plus``

open Archer
open Archer.Arrows
open Archer.MicroLang.VerificationTypes

let private feature = Arrow.NewFeature ()
let private failureBuilder = FailureBuilder ()

let ``TestSuccess + TestSuccess returns TestSuccess`` =
    feature.Test (fun _ ->
        TestSuccess + TestSuccess
    )
    
let ``TestSuccess + TestException Failure returns the TestExceptionFailure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            System.Exception "Boom Boom breaks things"
            |> failureBuilder.With.TestExecutionExceptionFailure
            |> TestFailure
            
        let result =
            TestSuccess + expectedFailure
            
        result
        |> Should.BeEqualTo expectedFailure
    )
    
let ``TestException + TestSuccess Failure returns the TestExceptionFailure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            System.Exception "Boom Boom breaks things"
            |> failureBuilder.With.TestExecutionExceptionFailure
            |> TestFailure
            
        let result =
            TestSuccess + expectedFailure
            
        result
        |> Should.BeEqualTo expectedFailure
    )

let ``Test Cases`` = feature.GetTests ()