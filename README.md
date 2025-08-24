<!-- GENERATED DOCUMENT DO NOT EDIT! -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->

<!-- Compiled with doculisp https://www.npmjs.com/package/doculisp -->

# Archer.CoreTypes: Common Types for F# Testing Frameworks #

1. Overview: [Archer.CoreTypes Overview](#archercoretypes-overview)
2. PublicTypes: [Archer.CoreTypes Public Types](#archercoretypes-public-types)
3. Helpers: [Archer.CoreTypes Helpers](#archercoretypes-helpers)
4. InternalTypes: [Archer.CoreTypes Internal Types](#archercoretypes-internal-types)
5. RunnerTypes: [Archer.CoreTypes Runner Types](#archercoretypes-runner-types)

## Archer.CoreTypes Overview ##

Archer.CoreTypes is a foundational library for F# testing frameworks, providing a set of common types that are shared and reused across the Archer ecosystem. These types are designed to promote consistency, type safety, and ease of use when building and running tests.

### Key Features ###

- **Unified Type Definitions:** Centralizes core types for assertions, test results, runners, and more.
- **Extensible:** Designed to be extended by other libraries and frameworks in the Archer suite.
- **F#-Friendly:** Leverages F#'s strong type system and functional paradigms.

### Main Type Categories ###

- **PublicTypes:** Types intended for use by consumers of the framework, such as test authors.
- **InternalTypes:** Types used internally by the framework to manage test execution and results.
- **RunnerTypes:** Types related to test runners and execution flow.
- **Helpers:** Utility types and functions to support common operations.

### Usage ###

Reference Archer.CoreTypes in your F# test projects to access these shared types and ensure compatibility with other Archer libraries.

---
For more details, see the source files in the `Lib` directory.

## Archer.CoreTypes Public Types ##

This document describes the main public types exposed by the Archer.CoreTypes library. These types are intended for use by consumers of the framework, such as test authors and library integrators.

### TestTag ###

**TestTag**: Represents tags that can be associated with tests, such as categories, Only, or Serial execution.

### CodeLocation ###

**CodeLocation**: Contains file path, file name, and line number information for a test.

### ITestNameInfo ###

**ITestNameInfo**: Interface providing properties for container path, container name, and test name.

### ITestMetaData ###

**ITestMetaData**: Interface providing access to a sequence of test tags.

### ITestLocationInfo ###

**ITestLocationInfo**: Interface providing access to the code location of a test.

### ITestInfo ###

**ITestInfo**: Combines `ITestNameInfo`, `ITestMetaData`, and `ITestLocationInfo` for complete test information.

### RunnerEnvironment ###

**RunnerEnvironment**: Contains runner name, version, and test info for the current test execution environment.

### IVerificationInfo ###

**IVerificationInfo**: Interface providing expected and actual values for verification in assertions.

### AssertionResult ###

Represents the result of an assertion in a test. Typically includes information about success, failure, and error messages.

### TestCase ###

Defines a single test case, including its name, input data, and expected outcome.

### TestSuite ###

A collection of related test cases, often grouped by feature or module.

### TestRunner ###

Responsible for executing test cases and reporting results.

### TestFailure ###

**TestFailure**: Represents different ways a test can fail, including expectation failures, ignored tests, and exceptions.

### TestResult ###

**TestResult**: Represents the outcome of a test, either success or failure, and supports combining results.

### SetupResult ###

**SetupResult**: Represents the result of a test setup phase, indicating success or the type of failure.

### TeardownResult ###

**TeardownResult**: Represents the result of a test teardown phase, indicating success or the type of failure.

### GeneralTestingFailure ###

**GeneralTestingFailure**: Represents general failures that can occur during testing, such as exceptions or cancellations.

### TestExecutionResult ###

**TestExecutionResult**: Represents the result of executing a test, including setup, test, teardown, and general execution failures.

---
For detailed type definitions, see the `PublicTypes.fs` source file.

## Archer.CoreTypes Helpers ##

This document provides an overview of the helper functions available in the Archer.CoreTypes library. Helpers are utility constructs designed to simplify common operations and support the core functionality of the framework.

### addMany ###

**addMany**: Adds multiple lists of tests to a runner by flattening the lists and passing them to the runner's `AddTests` method.

### add ###

**add**: Adds a list of tests to a runner using the runner's `AddTests` method.

### getTestName ###

**getTestName**: Retrieves the name of a test.

### getTags ###

**getTags**: Retrieves the tags associated with a test.

### getContainerName ###

**getContainerName**: Retrieves the container name of a test.

### getContainerPath ###

**getContainerPath**: Retrieves the container path of a test.

### getFilePath ###

**getFilePath**: Retrieves the file path where the test is located.

### getFileName ###

**getFileName**: Retrieves the file name where the test is located.

### getLineNumber ###

**getLineNumber**: Retrieves the line number where the test is defined.

### getTestLocation ###

**getTestLocation**: Retrieves the location information of a test.

### getTestExecutor ###

**getTestExecutor**: Retrieves the executor function for a test.

---
For the full list and detailed definitions, see the `Helpers.fs` source file.

## Archer.CoreTypes Internal Types ##

This document describes key internal types used within Archer.CoreTypes, which are essential for test execution and lifecycle management.

### TestEventLifecycle ###

**TestEventLifecycle**: Represents the different stages and events in the lifecycle of a test, such as start, setup, execution, teardown, and completion.

### TestExecutionDelegate ###

**TestExecutionDelegate**: A delegate type for handling test event lifecycle notifications, typically used for event-driven test execution.

### ITestExecutor ###

**ITestExecutor**: Interface for executing tests, exposing lifecycle events and execution methods, as well as a reference to the parent test.

### ITest ###

**ITest**: Interface representing a test, combining test info and providing a method to get its executor.

### TestTiming ###

**TestTiming**: Record type capturing timing information for setup, test, teardown, and total duration of a test.

---
For detailed type definitions, see the `InternalTypes.fs` source file.

## Archer.CoreTypes Runner Types ##

This document describes the runner types and containers used in Archer.CoreTypes, which are essential for managing test execution, results, and events.

### TestFailureType ###

**TestFailureType**: Represents the type of failure that can occur during a test run, including setup, test, teardown, and general failures.

### TestFailContainer ###

**TestFailContainer**: Container for failed tests, supporting nested failure structures and grouping by name.

### TestSuccessContainer ###

**TestSuccessContainer**: Container for successful tests, supporting nested success structures and grouping by name.

### TestIgnoreContainer ###

**TestIgnoreContainer**: Container for ignored tests, supporting nested ignore structures and grouping by name.

### RunResults ###

**RunResults**: Holds the results of a test run, including lists of failures, successes, ignored tests, the random seed, and timing information.

### RunnerEventLifecycle ###

**RunnerEventLifecycle**: Represents the lifecycle events of a test runner, such as start, per-test events, and end.

### RunnerExecutionDelegate ###

**RunnerExecutionDelegate**: Delegate type for handling runner event lifecycle notifications.

### RunnerTestArgs, RunnerTestResultArgs, RunnerTestCancelArgs, RunnerTestResultCancelArgs ###

**RunnerTestArgs, RunnerTestResultArgs, RunnerTestCancelArgs, RunnerTestResultCancelArgs**: Event argument types used for runner events, providing access to test and result data.

### IRunner ###

**IRunner**: Interface for a test runner, providing methods to run tests, add tests, access test tags, and subscribe to runner lifecycle events.

---
For detailed type definitions, see the `RunnerTypes.fs` source file.

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->
<!-- GENERATED DOCUMENT DO NOT EDIT! -->