# Tagging and Masking System for Unity

## Overview

The **Tagging and Masking System** provides a lightweight mechanism for bitmask-based tagging, using 32-bit integers for categorization. The system features dropdowns in the Unity Inspector for improved usability, allowing developers to interact seamlessly with tags.

---

## Core Features

1. **32-Bit Tag System**:  
   Associating up to 32 string labels with individual bits (representing categories).

2. **Custom Attribute for Tagging**:  
   Use `[TagMask]`/`[ByteTagMask]` attribute to seamlessly integrate tags into your `int`/`byte` fields.

3. **Custom Inspector Enhancements**:  
   The system includes a `ScriptableObject` (`TagMaskLibrary`/`ByteTagMaskLibrary`) for managing tag definitions and dynamic custom drawers for dropdown-based tag selection.

4. **Enforces Fixed Array Size**:  
   Ensures consistent array lengths (32 for `int`, 8 for `byte`) for valid tagging behavior.

---

## How It Works

### Components:

#### `TagMaskLibrary`/`TagMaskLibrary`
- Editable asset serving as the source for tag names.
- Supports retrieving integer values for a given label.

#### `TagMaskLibraryEditor`/`TagMaskLibraryEditor`
- Prevents accidental resizing of tag arrays and ensures a 32-bit alignment for ints, 8-bit alignment for bytes.

#### `TagMaskAttribute`/`ByteTagMaskAttribute`
- Marks integer/byte fields for tag-based dropdowns.

#### `TagMaskDrawer`/`TagMaskDrawer`
- Renders dropdown menus for all attributes marked with `[TagMask]`/`[ByteTagMask]`.

---

### Example Setup

1. **Create a Tag Library**:
   - Create a `TagMaskLibrary`/`ByteTagMaskLibrary` asset.
   - Enter string labels into the Inspector for each bit position (0–31)/(0-8).

2. **Inspector Visualization**:
   - Mark any fields with `[TagMask]`/`[ByteTagMask]` (`int` type for the former, `byte` for the latter) for which you wish to display a dropdown of tags in the Inspector, leveraging an associated `TagMaskLibrary`/`ByteTagMaskLibrary`.

3. **Reference a Tag Library**:
   - You have two options for how you can reference a tagged library - serializing a reference to it on the same object or specifying the path to that library.
      - To utilitize a reference the `TagMaskLibrary`/`ByteTagMaskLibrary` in your script, use the parameterless `[TagMask]`/`[BitTagMask]` attribute.
      - To utilitize a saved `TagMaskLibrary`/`ByteTagMaskLibrary` in your script, use the single-parameter `[TagMask("Assets/Path/To/Library")]`/`[BitTagMask("Assets/Path/To/Library")]` attribute.

---

## Use Case

Primarily useful when a lightweight, readable tagging mechanism with Inspector-based usability is required. Example applications can include:
- Categorized game object tagging.
- Layer filtering systems.
- Visibility toggles or state representations.

---

## Limitations

- Limited to 32 tags for `int`-types, 8 tags for `byte`-types.
- A script can reference only one `TagMaskLibrary`/`ByteTagMaskLibrary` at a time when relying on the parameterless `[TagMask]`/`[ByteTagMask]` attribute. To use multiple, one must use the path-specific overload of the attribute.
- No enforced type safety for cross-library comparisons.
- Each script that uses the tag library requires a serialized reference to it

---