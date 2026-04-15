# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.1.0] - 2026-03-26

### Added

- **TagMaskLibrary**:
    - Stores the source of string labels for the 32-bit tagging system.
    - Provides mappings from string labels to bitmask values.

- **TagMaskLibraryEditor**:
    - Enforces fixed 32-label constraint on the tag library.

- **TagMaskAttribute**:
    - Flags `int` fields for tag-based dropdown renders.

- **TagMaskDrawer**:
    - Renders dropdown bitmask fields in the Inspector.

- **IUseTagMask Interface**:
    - Ensures object compatibility with the tag system by enforcing a required reference to the `TagMaskLibrary`.

---
## [0.2.0] - 2026-04-15

### Byte-based bitmasks and path-specifying attribute-overloads

### Added

- **ByteTagMaskLibrary**:
  - Stores the source of string labels for the 8-bit tagging system based on `byte`.
  - Provides mappings from string labels to `byte`-based bitmask values.

- **ByteTagMaskLibraryEditor**:
  - Enforces fixed 8-label constraint on the byte-based tag library.

- **ByteTagMaskAttribute**:
  - Flags `byte` fields for tag-based dropdown renders.

- **ByteTagMaskDrawer**:
  - Renders dropdown byte-bitmask fields in the Inspector.

### Removed
- **IUseTagMask Interface**: usefulness was deemed minimal, and obsolete with the introduction of path-specific library references in attributes which removed the need for serialized references to tagged libraries.

### Addressed
- A script is no longer requied to cache a reference to the `TagMaskLibrary`/`ByteTagMaskLibrary` being used in order to take advantage of the `[TagMask]`/`[ByteTagMask]` attribute.

---



