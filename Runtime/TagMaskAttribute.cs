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
        public bool hasSpecifiedPath = false;
        public string LibraryPath;
        //Noop - this attribute is meant solely for marking a int-bitmask for redrawing by the TagMaskDrawer
        public TagMaskAttribute()
        {
            //This overload assumes a serialized field to which the TagMaskLibrary is assigned
            hasSpecifiedPath = false;
            LibraryPath = null;
        }
	    
        public TagMaskAttribute(string path)
        {
            //This overload assumes an SO-instance of TagMaskLibrary is stored at the passed in path
            hasSpecifiedPath = true;
            LibraryPath = path;
        }
    }
}
