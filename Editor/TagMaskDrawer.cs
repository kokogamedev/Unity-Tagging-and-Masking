using UnityEditor;
using UnityEngine;

namespace PsigenVision.TagMasking.Editor
{
    [CustomPropertyDrawer(typeof(TagMaskAttribute))]
    public class TagMaskDrawer: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Wrap the drawing logic to introduce Undo/Redo functionality and automatically perform "change checks"
            label = EditorGUI.BeginProperty(position, label, property);
            
            //1. Search the current ScriptableObject for ANY field of type TagMaskLibrary (this will be the source of bit-labels)
            TagMaskLibrary library = FindLibraryInObject(property.serializedObject);
            
            //2. As a fallback, display a warning if a TaskMaskLibrary object reference is missing on the object to which this property belongs
            if (library == null)
            {
                EditorGUI.HelpBox(position, "Add a TagMaskLibrary reference to this object to display bitmask with its labels", MessageType.Error);
                return;
            }
            //3. If the TaskMaskLibrary object reference is found, display the raw integer as a user-friendly bitmask dropdown populated from the labels array of the TaskMaskLibrary
            //Begin change check specifically during this UI-call
            EditorGUI.BeginChangeCheck();
            int newBitValue = EditorGUI.MaskField(position, label, property.intValue, library.Labels);

            if (EditorGUI.EndChangeCheck()) //If a change was registered (the user changed the bitmask via the mask field dropdown), save that value to the property
                property.intValue = newBitValue;
            
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
        private TagMaskLibrary FindLibraryInObject(SerializedObject propertySerializedObject)
        {
            //Iterate through all properties in the object to locate (if present) the TaskMaskLibrary object reference 
            SerializedProperty iterator = propertySerializedObject.GetIterator();
            if (iterator.NextVisible(true)) //Note: NextVisible skips fields hidden by [HideInInspector] - we use this to respect this attribute in this custom inspector
            {
                do
                {
                    if (iterator.propertyType ==
                        SerializedPropertyType
                            .ObjectReference //must be a reference type (since we are searchign for an SO object reference)
                        && iterator.objectReferenceValue is TagMaskLibrary library) //must be of type TagMaskLibrary
                        return library; //return the found TagMaskLibrary
                } while (iterator.NextVisible(false));
            }

            return null; // This will indicate no TagMaskLibrary was found on the serialized object
        }
    }
}
