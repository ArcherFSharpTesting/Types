<!-- (dl
(section-meta
    (title Archer.CoreTypes Runner Types)
)
) -->

This document describes the runner types and containers used in Archer.CoreTypes, which are essential for managing test execution, results, and events.

<!-- (dl (# TestFailureType)) -->
**TestFailureType**: Represents the type of failure that can occur during a test run, including setup, test, teardown, and general failures.

<!-- (dl (# TestFailContainer)) -->
**TestFailContainer**: Container for failed tests, supporting nested failure structures and grouping by name.

<!-- (dl (# TestSuccessContainer)) -->
**TestSuccessContainer**: Container for successful tests, supporting nested success structures and grouping by name.

<!-- (dl (# TestIgnoreContainer)) -->
**TestIgnoreContainer**: Container for ignored tests, supporting nested ignore structures and grouping by name.

<!-- (dl (# RunResults)) -->
**RunResults**: Holds the results of a test run, including lists of failures, successes, ignored tests, the random seed, and timing information.

<!-- (dl (# RunnerEventLifecycle)) -->
**RunnerEventLifecycle**: Represents the lifecycle events of a test runner, such as start, per-test events, and end.

<!-- (dl (# RunnerExecutionDelegate)) -->
**RunnerExecutionDelegate**: Delegate type for handling runner event lifecycle notifications.

<!-- (dl (# RunnerTestArgs, RunnerTestResultArgs, RunnerTestCancelArgs, RunnerTestResultCancelArgs)) -->
**RunnerTestArgs, RunnerTestResultArgs, RunnerTestCancelArgs, RunnerTestResultCancelArgs**: Event argument types used for runner events, providing access to test and result data.

<!-- (dl (# IRunner)) -->
**IRunner**: Interface for a test runner, providing methods to run tests, add tests, access test tags, and subscribe to runner lifecycle events.

---
For detailed type definitions, see the `RunnerTypes.fs` source file.
