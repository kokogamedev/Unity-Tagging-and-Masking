using UnityEngine;

namespace PsigenVision.TagMasking
{
    /// <summary>
    /// Attribute used to mark integer fields in Unity's inspector with a custom mask field UI for selecting tags.
    /// </summary>
    /// <remarks>
    /// This attribute is paired with a custom property drawer that displays an integer field as a bitmask dropdown populated with tags.
    /// The tags are retrieved from a <see cref="TagMaskLibrary"/>, which serves as the source of the available labels.
    /// Apply this attribute to integer fields where bitmask-based tag selection is required.
    /// </remarks>
    public class TagMaskAttribute : PropertyAttribute
    {
        //Noop - this attribute is meant solely for marking an int/bitmask for redrawing by the TagMaskDrawer
    }
}
