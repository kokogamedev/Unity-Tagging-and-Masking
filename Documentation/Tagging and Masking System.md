# **Documentation: Tagging and Masking System**

---

### **Key Components and Their Responsibilities**

This bitmask-based categorization system facilitates lightweight tagging and masking using **32-bit integers** or **8-bit bytes**. The system prioritizes performance by relying on primitive types (`int` and `byte`), with enhanced Unity Inspector usability via dropdown-driven custom property drawers.

---

### **Key Responsibilities**

#### **TagMaskLibrary/ByteTagMaskLibrary**
- Stores string-based labels for:
    - **`TagMaskLibrary`**: 32-bit fields associated with `int` properties.
    - **`ByteTagMaskLibrary`**: 8-bit fields associated with `byte` properties.
- Provides mappings between human-readable labels and bit positions.
- Derives bitmask values based on label strings.
- Can be referenced explicitly via serialized fields or dynamically through a **path-specific attribute**.

#### **TagMaskLibraryEditor/ByteTagMaskLibraryEditor**
- Custom Editor ensures array integrity:
    - Enforces **32 labels** for `TagMaskLibrary`.
    - Enforces **8 labels** for `ByteTagMaskLibrary`.

#### **TagMaskAttribute/ByteTagMaskAttribute**
- Custom attributes that flag:
    - **`TagMaskAttribute`**: For `int` fields (32-bit tagging).
    - **`ByteTagMaskAttribute`**: For `byte` fields (8-bit tagging).
- Supports dropdown-driven tagging with:
    - **Parameterless configuration**: Relies on serialized `TagMaskLibrary`/`ByteTagMaskLibrary` references in the field's parent object.
    - **Path-specific overloads**: Allows specifying the path to a `TagMaskLibrary`/`ByteTagMaskLibrary` asset directly in the attribute.

#### **TagMaskDrawer/ByteTagMaskDrawer**
- Custom property drawers render dropdown menus:
    - Display tag options for selecting active bits in an `int` or `byte`.
    - Validate the presence of a valid `TagMaskLibrary` or `ByteTagMaskLibrary`.
    - Display warnings in the Unity Inspector when no valid library is found.

---

### **Example Workflow**

#### 1. **Create a Tag Library**
- Use `Create > PsigenVision > TagMaskLibrary` or `ByteTagMaskLibrary` to create the desired tag library asset.
- Enter string labels for each bit position:
    - **`TagMaskLibrary`**: 32 labels, corresponding to 32-bit integer fields.
    - **`ByteTagMaskLibrary`**: 8 labels, corresponding to 8-bit byte fields.

#### 2. **Mark Fields with Attributes**
- Add `[TagMask]` or `[ByteTagMask]` above an `int` or `byte` field to enable dropdown-driven tagging in the Inspector.

#### 3. **Reference a Tag Library**
- There are two ways to point fields to the desired library:
    1. **Serialized Reference**:  
       Use the parameterless `[TagMask]` or `[ByteTagMask]` attribute. This requires the script to include serialized fields referencing a `TagMaskLibrary` or `ByteTagMaskLibrary`.
       ```csharp
       [SerializeField]
       private TagMaskLibrary myLibrary;
  
       [TagMask]
       public int myTagMask;
       ```
    2. **Path-Based Reference**:  
       Use the path-specific overload to directly associate the field with a library stored at a specific location.
       ```csharp
       [TagMask("Assets/MyLibrary.asset")]
       public int myTagMask;
       ```

---

### **Key Benefits of the System**

- **Minimal Overhead**:  
  Lightweight storage by using `int` for 32-bit tags and `byte` for 8-bit tags.
- **Inspector-Friendly**:  
  Dropdown menus make tagging quick and user-friendly.
- **Configurable References**:  
  Flexibility to reference tag libraries either via serialized fields or explicitly by paths.
- **Validation**:  
  Prevents mismatched data by ensuring fixed sizes for tag libraries corresponding to their bitmasks.

---

### **System Limitations**

1. **Bitmask Capacity**:
    - `int` fields are limited to **32 tags**.
    - `byte` fields are limited to **8 tags**.
2. **Single Tag Library (Parameterless Attribute)**:
    - A parameterless `[TagMask]` or `[ByteTagMask]` attribute assumes a single tag library per script. To use multiple libraries, you must utilize the path overload with the attribute.
3. **No Cross-Library Comparison Safety**:
    - Comparisons between bitmasks from different tag libraries are not enforced by type safety and could result in bugs.

---

### **Component Breakdown**

#### 1. **`TagMaskLibrary`/`ByteTagMaskLibrary`**

##### Purpose:
Stores string-based tag definitions and provides mappings to their respective bitmask values.

##### Features:
- **Fixed Array Size**:
    - `TagMaskLibrary`: Always 32 labels for 32-bit fields.
    - `ByteTagMaskLibrary`: Always 8 labels for 8-bit fields.
- **Efficient Mapping**:
    - Label-to-bit mask conversion.
    - Bit-position lookups.

##### Key API:
- **`Tags`**:  
  Array of tag labels. Each index corresponds to a bit position.
- **`ToMask(string label)`**:  
  Converts a label into its corresponding bitmask value.
- **`ToLabel(int bitIndex)`**:  
  Converts a bit index into a label string.

---

#### 2. **`TagMaskLibraryEditor`/`ByteTagMaskLibraryEditor`**

##### Purpose:
Custom Editor scripts enforce strict label counts for their respective tag libraries.

##### Features:
- Prevents developers from resizing the label arrays:
    - 32-label constraint for `TagMaskLibrary`.
    - 8-label constraint for `ByteTagMaskLibrary`.
- Displays warnings for incomplete or invalid arrays.

---

#### 3. **`TagMaskAttribute`/`ByteTagMaskAttribute`**

##### Purpose:
Identifies `int` or `byte` fields as Tag Masks, rendering them with dropdown menus in the Inspector.

##### Features:
- Parameterless (`[TagMask]`/`[ByteTagMask]`): Relies on serialized `TagMaskLibrary`/`ByteTagMaskLibrary`.
- Path-Specific (`[TagMask("path")]`/`[ByteTagMask("path")]`): Dynamically loads a tag library from the given path.

---

#### 4. **`TagMaskDrawer`/`ByteTagMaskDrawer`**

##### Purpose:
Custom property drawers render dropdown menus in the Inspector for fields marked with `[TagMask]` or `[ByteTagMask]`.

##### Features:
- Dropdown-driven tag selection corresponding to the referenced tag library.
- Displays warnings if no valid tag library is found.

---

### **Summary of Component Responsibilities**

| Component                    | Responsibility                                      |
|------------------------------|----------------------------------------------------|
| **TagMaskLibrary**           | Stores 32-bit tag definitions.                     |
| **ByteTagMaskLibrary**       | Stores 8-bit tag definitions.                      |
| **TagMaskLibraryEditor**     | Ensures fixed 32-label structure.                  |
| **ByteTagMaskLibraryEditor** | Ensures fixed 8-label structure.                   |
| **TagMaskAttribute**         | Flags `int` fields for 32-bit tag masks.           |
| **ByteTagMaskAttribute**     | Flags `byte` fields for 8-bit tag masks.           |
| **TagMaskDrawer**            | Renders dropdowns for tag selection in Inspector.  |
| **ByteTagMaskDrawer**        | Renders dropdowns for tag selection in Inspector.  |

---