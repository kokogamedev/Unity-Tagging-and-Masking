using UnityEditor;
using UnityEngine;

namespace PsigenVision.TagMasking.Editor
{
	[CustomPropertyDrawer(typeof(ByteTagMaskAttribute))]
	public class ByteTagMaskDrawer: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Wrap the drawing logic to introduce Undo/Redo functionality and automatically perform "change checks"
            label = EditorGUI.BeginProperty(position, label, property);
            
            //0. Get a reference to the current attribute to determine which overload is being used, as this will decide how the ByteTagMaskLibrary path is found
            var currentAttribute = (ByteTagMaskAttribute)attribute;
            
            //1. Search the current ScriptableObject for ANY field of type ByteTaskMaskLibrary (this will be the source of bit-labels) OR assign it to the passed in path (if that overload was used)
            ByteTagMaskLibrary library = (currentAttribute.hasSpecifiedPath) 
                ? AssetDatabase.LoadAssetAtPath<ByteTagMaskLibrary>(currentAttribute.LibraryPath) 
                : FindLibraryInObject(property.serializedObject);
            
            //2. As a fallback, display a warning if a ByteTaskMaskLibrary object reference is missing on the object to which this property belongs
            if (library == null)
            {
                var errorMessage = (currentAttribute.hasSpecifiedPath)
                    ? $"No ByteTagMaskLibrary reference could be found at {currentAttribute.LibraryPath}"
                    : "Add a ByteTagMaskLibrary reference to this object to display bitmask with its labels";
	            EditorGUI.HelpBox(position, errorMessage, MessageType.Error);
                return;
            }
            //3. If the ByteTaskMaskLibrary object reference is found, display the raw integer as a user-friendly bitmask dropdown populated from the labels array of the ByteTaskMaskLibrary
            //Begin change check specifically during this UI-call
            EditorGUI.BeginChangeCheck();
            int newBitValue = EditorGUI.MaskField(position, label, property.intValue, library.Labels);

            if (EditorGUI.EndChangeCheck()) //If a change was registered (the user changed the bitmask via the mask field dropdown), save that value to the property
	            property.intValue = (byte)newBitValue;
            
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Searches the provided serialized object for a reference of type <see cref="TagMaskLibrary"/>.
        /// </summary>
        /// <param name="propertySerializedObject">The serialized object to search for a <see cref="TagMaskLibrary"/> reference.</param>
        /// <returns>
        /// Returns an instance of <see cref="TagMaskLibrary"/> if a matching reference is found in the serialized object;
        /// otherwise, returns null if no such reference exists.
        /// </returns>
	    private ByteTagMaskLibrary FindLibraryInObject(SerializedObject propertySerializedObject)
        {
            //Iterate through all properties in the object to locate (if present) the ByteTaskMaskLibrary object reference 
            SerializedProperty iterator = propertySerializedObject.GetIterator();
            if (iterator.NextVisible(true)) //Note: NextVisible skips fields hidden by [HideInInspector] - we use this to respect this attribute in this custom inspector
            {
                do
                {
                    if (iterator.propertyType ==
                        SerializedPropertyType
                            .ObjectReference //must be a reference type (since we are searchign for an SO object reference)
	                    && iterator.objectReferenceValue is ByteTagMaskLibrary library) //must be of type ByteTagMaskLibrary
                        return library; //return the found TagMaskLibrary
                } while (iterator.NextVisible(false));
            }

	        return null; // This will indicate no ByteTagMaskLibrary was found on the serialized object
        }
    }
}
