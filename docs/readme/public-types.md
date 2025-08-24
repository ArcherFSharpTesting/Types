
<!-- (dl
(section-meta
	(title Archer.CoreTypes Public Types)
)
) -->

This document describes the main public types exposed by the Archer.CoreTypes library. These types are intended for use by consumers of the framework, such as test authors and library integrators.

<!-- (dl (# AssertionResult)) -->
Represents the result of an assertion in a test. Typically includes information about success, failure, and error messages.

<!-- (dl (# TestCase)) -->
Defines a single test case, including its name, input data, and expected outcome.

<!-- (dl (# TestSuite)) -->
A collection of related test cases, often grouped by feature or module.

<!-- (dl (# TestRunner)) -->
Responsible for executing test cases and reporting results.

---
For detailed type definitions, see the `PublicTypes.fs` source file.
