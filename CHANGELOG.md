# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-03-26

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