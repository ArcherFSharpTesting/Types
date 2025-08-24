<!-- GENERATED DOCUMENT DO NOT EDIT! -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->

<!-- Compiled with doculisp https://www.npmjs.com/package/doculisp -->

# Archer.CoreTypes: Common Types for F# Testing Frameworks #

1. Overview: [Archer.CoreTypes Overview](#archercoretypes-overview)
2. PublicTypes: [Archer.CoreTypes Public Types](#archercoretypes-public-types)

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

### AssertionResult ###

Represents the result of an assertion in a test. Typically includes information about success, failure, and error messages.

### TestCase ###

Defines a single test case, including its name, input data, and expected outcome.

### TestSuite ###

A collection of related test cases, often grouped by feature or module.

### TestRunner ###

Responsible for executing test cases and reporting results.

---
For detailed type definitions, see the `PublicTypes.fs` source file.

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->
<!-- GENERATED DOCUMENT DO NOT EDIT! -->