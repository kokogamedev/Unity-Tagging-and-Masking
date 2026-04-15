using UnityEngine;

namespace PsigenVision.TagMasking
{
    /// <summary>
	/// Attribute used to mark byte fields in Unity's inspector with a custom mask field UI for selecting tags.
    /// </summary>
    /// <remarks>
	/// This attribute is paired with a custom property drawer that displays a byte field as a bitmask dropdown populated with tags.
    /// The tags are retrieved from a <see cref="TagMaskLibrary"/>, which serves as the source of the available labels.
	/// Apply this attribute to byte fields where bitmask-based tag selection is required.
    /// </remarks>
	public class ByteTagMaskAttribute : PropertyAttribute
	{
		public bool hasSpecifiedPath = false;
		public string LibraryPath;
	    //Noop - this attribute is meant solely for marking a byte-bitmask for redrawing by the TagMaskDrawer
	    public ByteTagMaskAttribute()
	    {
		    //This overload assumes a serialized field to which the ByteTagMaskLibrary is assigned
		    hasSpecifiedPath = false;
		    LibraryPath = null;
	    }
	    
	    public ByteTagMaskAttribute(string path)
	    {
		    //This overload assumes an SO-instance of ByteTagMaskLibrary is stored at the passed in path
		    hasSpecifiedPath = true;
		    LibraryPath = path;
	    }
    }
}
