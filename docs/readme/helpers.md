<!-- (dl
(section-meta
	(title Archer.CoreTypes Helpers)
)
) -->

This document provides an overview of the helper functions available in the Archer.CoreTypes library. Helpers are utility constructs designed to simplify common operations and support the core functionality of the framework.

<!-- (dl (# addMany)) -->
**addMany**: Adds multiple lists of tests to a runner by flattening the lists and passing them to the runner's `AddTests` method.

<!-- (dl (# add)) -->
**add**: Adds a list of tests to a runner using the runner's `AddTests` method.

<!-- (dl (# getTestName)) -->
**getTestName**: Retrieves the name of a test.

<!-- (dl (# getTags)) -->
**getTags**: Retrieves the tags associated with a test.

<!-- (dl (# getContainerName)) -->
**getContainerName**: Retrieves the container name of a test.

<!-- (dl (# getContainerPath)) -->
**getContainerPath**: Retrieves the container path of a test.

<!-- (dl (# getFilePath)) -->
**getFilePath**: Retrieves the file path where the test is located.

<!-- (dl (# getFileName)) -->
**getFileName**: Retrieves the file name where the test is located.

<!-- (dl (# getLineNumber)) -->
**getLineNumber**: Retrieves the line number where the test is defined.

<!-- (dl (# getTestLocation)) -->
**getTestLocation**: Retrieves the location information of a test.

<!-- (dl (# getTestExecutor)) -->
**getTestExecutor**: Retrieves the executor function for a test.

---
For the full list and detailed definitions, see the `Helpers.fs` source file.
