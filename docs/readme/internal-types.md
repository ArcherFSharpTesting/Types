<!-- (dl
(section-meta
    (title Archer.CoreTypes Internal Types)
)
) -->

This document describes key internal types used within Archer.CoreTypes, which are essential for test execution and lifecycle management.

<!-- (dl (# TestEventLifecycle)) -->
**TestEventLifecycle**: Represents the different stages and events in the lifecycle of a test, such as start, setup, execution, teardown, and completion.

<!-- (dl (# TestExecutionDelegate)) -->
**TestExecutionDelegate**: A delegate type for handling test event lifecycle notifications, typically used for event-driven test execution.

<!-- (dl (# ITestExecutor)) -->
**ITestExecutor**: Interface for executing tests, exposing lifecycle events and execution methods, as well as a reference to the parent test.

<!-- (dl (# ITest)) -->
**ITest**: Interface representing a test, combining test info and providing a method to get its executor.

<!-- (dl (# TestTiming)) -->
**TestTiming**: Record type capturing timing information for setup, test, teardown, and total duration of a test.

---
For detailed type definitions, see the `InternalTypes.fs` source file.
