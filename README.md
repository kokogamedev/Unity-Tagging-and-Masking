# Tagging and Masking System for Unity

## Overview

The **Tagging and Masking System** provides a lightweight mechanism for bitmask-based tagging, using 32-bit integers for categorization. The system features dropdowns in the Unity Inspector for improved usability, allowing developers to interact seamlessly with tags.

---

## Core Features

1. **32-Bit Tag System**:  
   Associating up to 32 string labels with individual bits (representing categories).

2. **Custom Attribute for Tagging**:  
   Use `[TagMask]` attribute to seamlessly integrate tags into your `int` fields.

3. **Custom Inspector Enhancements**:  
   The system includes a `ScriptableObject` (`TagMaskLibrary`) for managing tag definitions and dynamic custom drawers for dropdown-based tag selection.

4. **Enforces Fixed Array Size**:  
   Ensures consistent array lengths of 32 for valid tagging behavior.

---

## How It Works

### Components:

#### `TagMaskLibrary`
- Editable asset serving as the source for tag names.
- Supports retrieving integer values for a given label.

#### `TagMaskLibraryEditor`
- Prevents accidental resizing of tag arrays and ensures a 32-bit alignment.

#### `TagMaskAttribute`
- Marks integer fields for tag-based dropdowns.

#### `TagMaskDrawer`
- Renders dropdown menus for all attributes marked with `[TagMask]`.

#### `IUseTagMask`
- Ensures classes include a reference to a `TagMaskLibrary` to enable proper association of dropdowns.

---

### Example Setup

1. Create a new `TagMaskLibrary` asset under `Assets`.
2. Add labels corresponding to each bit position in the Inspector.
3. Add a reference to the `TagMaskLibrary` in your script via the `IUseTagMask` interface.
4. Mark any `int` fields with `[TagMask]` to enforce the dropdown UI.

---

## Use Case

Primarily useful when a lightweight, readable tagging mechanism with Inspector-based usability is required. Example applications can include:
- Categorized game object tagging.
- Layer filtering systems.
- Visibility toggles or state representations.

---

## Limitations

- Limited to 32 tags.
- A single tag library per script.
- No enforced type safety for cross-library comparisons.
- Each script that uses the tag library requires a serialized reference to it

---