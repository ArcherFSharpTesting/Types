<!-- (dl
(section-meta
	(title Archer.Types Public Types)
)
) -->

This document describes the main public types exposed by the Archer.Types library. These types are intended for use by consumers of the framework, such as test authors and library integrators.

<!-- (dl (# TestTag)) -->
**TestTag**: Represents tags that can be associated with tests, such as categories, Only, or Serial execution.

<!-- (dl (# CodeLocation)) -->
**CodeLocation**: Contains file path, file name, and line number information for a test.

<!-- (dl (# ITestNameInfo)) -->
**ITestNameInfo**: Interface providing properties for container path, container name, and test name.

<!-- (dl (# ITestMetaData)) -->
**ITestMetaData**: Interface providing access to a sequence of test tags.

<!-- (dl (# ITestLocationInfo)) -->
**ITestLocationInfo**: Interface providing access to the code location of a test.

<!-- (dl (# ITestInfo)) -->
**ITestInfo**: Combines `ITestNameInfo`, `ITestMetaData`, and `ITestLocationInfo` for complete test information.

<!-- (dl (# RunnerEnvironment)) -->
**RunnerEnvironment**: Contains runner name, version, and test info for the current test execution environment.

<!-- (dl (# IVerificationInfo)) -->
**IVerificationInfo**: Interface providing expected and actual values for verification in assertions.

<!-- (dl (# AssertionResult)) -->
Represents the result of an assertion in a test. Typically includes information about success, failure, and error messages.

<!-- (dl (# TestCase)) -->
Defines a single test case, including its name, input data, and expected outcome.

<!-- (dl (# TestSuite)) -->
A collection of related test cases, often grouped by feature or module.

<!-- (dl (# TestRunner)) -->

Responsible for executing test cases and reporting results.

<!-- (dl (# TestFailure)) -->
**TestFailure**: Represents different ways a test can fail, including expectation failures, ignored tests, and exceptions.

<!-- (dl (# TestResult)) -->
**TestResult**: Represents the outcome of a test, either success or failure, and supports combining results.

<!-- (dl (# SetupResult)) -->
**SetupResult**: Represents the result of a test setup phase, indicating success or the type of failure.

<!-- (dl (# TeardownResult)) -->
**TeardownResult**: Represents the result of a test teardown phase, indicating success or the type of failure.

<!-- (dl (# GeneralTestingFailure)) -->
**GeneralTestingFailure**: Represents general failures that can occur during testing, such as exceptions or cancellations.

<!-- (dl (# TestExecutionResult)) -->
**TestExecutionResult**: Represents the result of executing a test, including setup, test, teardown, and general execution failures.

---
For detailed type definitions, see the `PublicTypes.fs` source file.
For detailed type definitions, see the `PublicTypes.fs` source file.
