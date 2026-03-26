# Documentation: Tagging and Masking System

---

### Key Components and Their Responsibilities

This bitmask-based categorization system facilitates lightweight tagging and masking using 32-bit integer bitmasks. The system utilizes `int` instead of wrapping structs for performance reasons, while custom property drawers enhance Inspector usability with dropdowns.

---

#### Key Responsibilities:

**TagMaskLibrary**
- Stores string-based labels for 32-bit fields.
- Provides mapping between human-readable labels and bit positions.
- Derives bitmask values based on label strings.

**TagMaskLibraryEditor**
- Custom Inspector that ensures the `TagMaskLibrary` array always maintains a fixed size of 32 labels.

**TagMaskAttribute**
- Flags `int` fields for being drawn with bitmask dropdowns utilizing the appropriate tag library.

**TagMaskDrawer**
- Custom property drawer that renders a dropdown of tag options for an `int` field marked with `[TagMask]`.
- Ensures fields are associated with a valid `TagMaskLibrary`. Displays warnings otherwise.

**IUseTagMask Interface**
- Enforces the implementation of a `TagMaskLibrary` reference when `[TagMask]` is utilized.

---

### Example Workflow

1. **Create a Tag Library**:
	- Use `Create > PsigenVision > TagMaskLibrary` to create a `TagMaskLibrary` asset.
	- Enter string labels into the Inspector for each bit position (0–31).

2. **Add Tags to a Script**:
	- Reference the `TagMaskLibrary` in your script using the `IUseTagMask` interface.
	- Mark `int` fields with `[TagMask]` to enable dropdown-based tag selection.

3. **Inspector Visualization**:
	- Fields marked with `[TagMask]` display a dropdown of tags in the Inspector, leveraging the associated `TagMaskLibrary`.

---

### Benefits of the System

- Minimizes overhead by storing masks as `int` values.
- Provides Inspector-friendly workflows for selecting bitmask-based tags.
- Enforces strict structure with 32-bit tagging systems, avoiding logic gaps.

---

### Limitations
1. Only 32 tags are supported based on `int` bit size.
2. A script can reference only one `TagMaskLibrary`.
3. A script must cache a reference to the TagMaskLibrary being used in order to take advantage of the [TagMask] attribute
4. Comparisons between bitmasks from different tag libraries may lead to bugs due to the lack of type safety.

---
## Component Breakdown

Here’s a detailed breakdown of each component, its role, and how it contributes to the overall tagging and masking system.

---

### 1. **`TagMaskLibrary` Class**

#### Purpose:
The `TagMaskLibrary` stores string-based labels that map to the 32-bit fields of a bitmask. This class allows developers to maintain a human-readable set of tag labels and retrieve their associated bitmask values.

#### Key Features:
1. **Fixed Size**: Always contains exactly 32 string labels to correspond with the number of bits in an `int`.
2. **Mapping Between Strings and Bits**: Associates each tag label with a specific bit position for easy lookups.

#### Key API:
- **`string[] Tags`**  
  The array of 32 string labels representing the tags. Each position in the array maps to a bit position.

- **`int ToMask(string label)`**  
  Returns the bitmask value corresponding to the specified label. If the label is invalid, returns `0`.

- **`string ToLabel(int bitIndex)`**  
  Returns the label corresponding to the specified bit position. If the bit position is invalid (out of range), returns `null`.

---

### 2. **`TagMaskLibraryEditor` Class**

#### Purpose:
Custom Editor script for `TagMaskLibrary`. It enforces constraints on the `Tags` array to ensure a consistent 32-label size. If a developer tries to alter the size of the array, the editor will reset it to 32 elements.

#### Features:
- Prevents accidental modifications of the number of tags.
- Provides warnings if incomplete or mismatched data is detected during runtime.

---

### 3. **`TagMaskAttribute`**

#### Purpose:
A custom attribute used to mark `int` fields as Tag Masks. It indicates that an integer field in a MonoBehaviour or other Unity script should use the dropdown-based tagging system in the Inspector.

#### Key Considerations:
- Any field marked with `[TagMask]` must have a valid reference to a `TagMaskLibrary` to function correctly.

#### Syntax:
```csharp
[TagMask]
public int myTagMask;
```

---

### 4. **`TagMaskDrawer`**

#### Purpose:
A custom property drawer that renders a dropdown list of tags for any field marked with `[TagMask]`. The dropdown options correspond to the tags defined in the associated `TagMaskLibrary`.

#### Key Responsibilities:
1. **Inspector UI**: Renders a dropdown menu for selecting which tags should be active in the bitmask.
2. **Validation**: Warns if no `TagMaskLibrary` is found or if the associated library contains incomplete data.
3. **Dynamic Updates**: Reflects changes in the `TagMaskLibrary` immediately in the Inspector.

---

### 5. **`IUseTagMask` Interface**

#### Purpose:
Enforces a contract that classes using `[TagMask]` fields must provide a reference to a valid `TagMaskLibrary`. This ensures that `[TagMask]`-decorated fields are always tied to a well-defined set of tags.

#### Key Members:
- **`TagMaskLibrary Library { get; }`**  
  A property that returns the `TagMaskLibrary` being used for tag definitions.

#### Use Cases:
- Ensures valid `TagMaskLibrary` references are provided, preventing uncaught runtime issues.
- Encourages standardized implementation for any system using the tagging system.

---

### Summary of Component Responsibilities

| Component                 | Key Role                                   |
|---------------------------|-------------------------------------------|
| **TagMaskLibrary**        | Stores tag definitions and mappings.      |
| **TagMaskLibraryEditor**  | Ensures a strict 32-tag structure.        |
| **TagMaskAttribute**      | Tags `int` fields for dropdowns.          |
| **TagMaskDrawer**         | Renders dropdown in the Unity Inspector.  |
| **IUseTagMask Interface** | Enforces `TagMaskLibrary` validation.     |

---
