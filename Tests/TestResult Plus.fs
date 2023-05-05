module Archer.CoreTypes.Tests.``TestResult Plus``

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal
open Archer.MicroLang.VerificationTypes

let private feature = Arrow.NewFeature (
    TestTags [
        Category "TestResult"
        Category "+"
    ]
)

let private failureBuilder = TestResultFailureBuilder id

let ``TestSuccess + TestSuccess returns TestSuccess`` =
    feature.Test (fun _ ->
        TestSuccess + TestSuccess
    )
    
let ``TestSuccess + TestException Failure returns the TestExceptionFailure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            System.Exception "Boom Boom breaks things"
            |> failureBuilder.ExceptionFailure
            
        let result =
            TestSuccess + expectedFailure
            
        result
        |> Should.BeEqualTo expectedFailure
    )
    
let ``TestException + TestSuccess Failure returns the TestExceptionFailure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            System.Exception "Boom Boom breaks things"
            |> failureBuilder.ExceptionFailure
            
        let result =
            expectedFailure + TestSuccess
            
        result
        |> Should.BeEqualTo expectedFailure
    )
    
let ``TestSuccess + TestIgnored Failure returns TestIgnored Failure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            None |> failureBuilder.IgnoreFailure
            
        let result =
            TestSuccess + expectedFailure
            
        result
        |> Should.BeEqualTo expectedFailure
    )
    
let ``TestIgnored Failure + TestSuccess returns TestIgnored Failure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            None |> failureBuilder.IgnoreFailure
            
        let result =
            expectedFailure + TestSuccess
            
        result
        |> Should.BeEqualTo expectedFailure
    )
    
let ``TestSuccess + TestExpectation Failure returns the TestExpectation Failure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            failureBuilder.ValidationFailure (1, 2)
            
        let result =
            TestSuccess + expectedFailure
            
        result
        |> Should.BeEqualTo expectedFailure
    )
    
let ``TestExpectation Failure + TestSuccess returns the TestExpectation Failure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            failureBuilder.ValidationFailure (1, 2)
            
        let result =
            expectedFailure + TestSuccess
            
        result
        |> Should.BeEqualTo expectedFailure
    )
    
let ``TestSuccess + Combination Failure returns the Combination Failure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            let (TestFailure a) = failureBuilder.ValidationFailure (3, "Hello")
            let (TestFailure b) = failureBuilder.ExceptionFailure (System.Exception "bad things here")
            CombinationFailure ((a, None), (b, None))
            |> TestFailure
            
        let result =
            TestSuccess + expectedFailure
            
        result
        |> Should.BeEqualTo expectedFailure
    )
    
let ``Combination Failure + TestSuccess returns the Combination Failure`` =
    feature.Test (fun _ ->
        let expectedFailure =
            let (TestFailure a) = failureBuilder.ValidationFailure (3, "Hello")
            let (TestFailure b) = failureBuilder.ExceptionFailure (System.Exception "bad things here")
            CombinationFailure ((a, None), (b, None))
            |> TestFailure
            
        let result =
            expectedFailure + TestSuccess
            
        result
        |> Should.BeEqualTo expectedFailure
    )
    
let ``TestException Failure A + TestException Failure B returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = System.Exception "A bad thing" |> failureBuilder.ExceptionFailure
        let failureB = System.ArgumentException "Should not argue" |> failureBuilder.ExceptionFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``TestException Failure B + TestException Failure A returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = System.Exception "A bad thing" |> failureBuilder.ExceptionFailure
        let failureB = System.ArgumentException "Should not argue" |> failureBuilder.ExceptionFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )
    
let ``TestException Failure + TestIgnored Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = System.Exception "A bad thing" |> failureBuilder.ExceptionFailure
        let failureB = None |> failureBuilder.IgnoreFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``TestIgnored Failure + TestException Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = System.Exception "A bad thing" |> failureBuilder.ExceptionFailure
        let failureB = None |> failureBuilder.IgnoreFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )
    
let ``TestException Failure + TestExpectation Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = System.Exception "A bad thing" |> failureBuilder.ExceptionFailure
        let failureB = failureBuilder.ValidationFailure ("good", "bad")
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``TestExpectation Failure + TestException Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = System.Exception "A bad thing" |> failureBuilder.ExceptionFailure
        let failureB = failureBuilder.ValidationFailure ("good", "bad")
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )
    
let ``TestException Failure + Combination Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = System.Exception "A bad thing" |> failureBuilder.ExceptionFailure
        let failureB =
            let (TestFailure ba) = failureBuilder.ValidationFailure ("good", "bad")
            let (TestFailure bb) = failureBuilder.GeneralTestExpectationFailure "This failed"
            
            CombinationFailure ((ba, None), (bb, None))
            |> TestFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``Combination Failure + TestException Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = System.Exception "A bad thing" |> failureBuilder.ExceptionFailure
        let failureB =
            let (TestFailure ba) = failureBuilder.ValidationFailure ("good", "bad")
            let (TestFailure bb) = failureBuilder.GeneralTestExpectationFailure "This failed"
            
            CombinationFailure ((ba, None), (bb, None))
            |> TestFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )
    
let ``TestIgnored Failure A + TestIgnored Failure B returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.IgnoreFailure None
        let failureB = failureBuilder.IgnoreFailure (Some "Stuff")
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``TestIgnored Failure B + TestIgnored Failure A returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.IgnoreFailure None
        let failureB = failureBuilder.IgnoreFailure (Some "Stuff")
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )
    
let ``TestIgnored Failure + TestExpectation Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.IgnoreFailure (Some "Some")
        let failureB = failureBuilder.ValidationFailure (2, "bad")
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``TestExpectation Failure + TestIgnored Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.IgnoreFailure (Some "Some")
        let failureB = failureBuilder.ValidationFailure (2, "bad")
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )
    
let ``TestIgnored Failure + Combination Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.IgnoreFailure (Some "Some")
        let failureB =
            let (TestFailure ba) = failureBuilder.GeneralTestExpectationFailure "no good"
            let (TestFailure bb) = failureBuilder.ExceptionFailure (System.Exception "Boom-ta")
            
            CombinationFailure ((ba, None), (bb, None)) |> TestFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``Combination Failure + TestIgnored Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.IgnoreFailure (Some "Some")
        let failureB =
            let (TestFailure ba) = failureBuilder.GeneralTestExpectationFailure "no good"
            let (TestFailure bb) = failureBuilder.ExceptionFailure (System.Exception "Boom-ta")
            
            CombinationFailure ((ba, None), (bb, None)) |> TestFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )
    
let ``TestExpectation Failure A + TestExpectation Failure B returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.GeneralTestExpectationFailure "a general failure"
        let failureB = failureBuilder.ValidationFailure (true, false)
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``TestExpectation Failure B + TestExpectation Failure A returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.GeneralTestExpectationFailure "a general failure"
        let failureB = failureBuilder.ValidationFailure (true, false)
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )
    
let ``TestExpectation Failure + Combination Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.GeneralTestExpectationFailure "a general failure"
        let failureB =
            let (TestFailure ba) = failureBuilder.ValidationFailure (true, false)
            let (TestFailure bb) = failureBuilder.IgnoreFailure None
            
            CombinationFailure ((ba, None), (bb, None)) |> TestFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``Combination Failure + TestExpectation Failure returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA = failureBuilder.GeneralTestExpectationFailure "a general failure"
        let failureB =
            let (TestFailure ba) = failureBuilder.ValidationFailure (true, false)
            let (TestFailure bb) = failureBuilder.IgnoreFailure None
            
            CombinationFailure ((ba, None), (bb, None)) |> TestFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )
    
let ``Combination Failure A + Combination Failure B returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA =
            let (TestFailure aa) = failureBuilder.GeneralTestExpectationFailure "a general failure"
            let (TestFailure bb) = failureBuilder.IgnoreFailure (Some "Thing to ignore")
            
            CombinationFailure ((aa, None), (bb, None)) |> TestFailure
            
        let failureB =
            let (TestFailure ba) = failureBuilder.ValidationFailure (true, false)
            let (TestFailure bb) = failureBuilder.IgnoreFailure None
            
            CombinationFailure ((ba, None), (bb, None)) |> TestFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureA + failureB
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        )
    )
    
let ``Combination Failure B + Combination Failure A returns a combination failure with both`` =
    feature.Test (fun _ ->
        let failureA =
            let (TestFailure aa) = failureBuilder.GeneralTestExpectationFailure "a general failure"
            let (TestFailure bb) = failureBuilder.IgnoreFailure (Some "Thing to ignore")
            
            CombinationFailure ((aa, None), (bb, None)) |> TestFailure
            
        let failureB =
            let (TestFailure ba) = failureBuilder.ValidationFailure (true, false)
            let (TestFailure bb) = failureBuilder.IgnoreFailure None
            
            CombinationFailure ((ba, None), (bb, None)) |> TestFailure
        
        let (TestFailure a) = failureA
        let (TestFailure b) = failureB
        
        let result =
            failureB + failureA
            
        result
        |> Should.BeEqualTo (
            CombinationFailure ((b, None), (a, None)) |> TestFailure
        )
    )

let ``Test Cases`` = feature.GetTests ()