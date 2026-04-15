using UnityEngine;
using UnityEditor;
using PsigenVision.TagMasking;

namespace PsigenVision.TagMasking.Editor
{
	[CustomEditor(typeof(ByteTagMaskLibrary))]
	public class ByteTagMaskLibraryEditor : UnityEditor.Editor
    {
        private SerializedProperty labelsProp; // Cache a reference to the labels array as a serialized property for redrawing

        /// <summary>
        /// Called when the ByteTagMaskLibraryEditor is initialized or reloaded.
        /// This method is responsible for setting up and ensuring the serialized property's size is consistent.
        /// </summary>
        /// <remarks>
	    /// Ensures the serialized property `labelsProp` representing the array of labels is initialized and resized to a fixed size of 8,
	    /// to maintain compatibility with a byte bitmask system.
	    /// If the array size is not 8, it automatically resizes and applies the modification to ensure consistency.
        /// </remarks>
        private void OnEnable() //called whenever the scriptable object is loaded into memory
        {
            //obtain a reference to the serialized property of the labels array
            labelsProp = serializedObject.FindProperty("labels");
            
	        //Safety Check: Force the array to be of length 8 if it is not (as this array is for an byte bitmask)
	        if (labelsProp.arraySize != 8)
            {
		        //Resize the array to 8
		        labelsProp.arraySize = 8;
                //Apply the resizing
                serializedObject.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// Renders and manages the custom inspector GUI for the TagMaskLibrary asset.
        /// This method ensures the labels array is displayed and editable while maintaining its fixed size of 32 elements,
        /// which corresponds to the bitmask tagging system.
        /// </summary>
        /// <remarks>
        /// The method updates the serialized object reference, provides user guidance for working with the bitmask system,
        /// and iterates over the labels array to draw individual elements with corresponding bit indices from 0 to 31.
        /// Modifications to the serialized properties are applied immediately to ensure the changes are reflected in the asset.
        /// </remarks>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
	        EditorGUILayout.HelpBox("Bitmask Registry: Indices 0-7 are mapped to system bits. " +
                                    "Do not leave gaps if using sequential logic.", MessageType.Info);
            
	        //Draw the labels array manually to avoid +/- buttons as the array size must be fixed at 8 for a bitmask tagging system
            for (int i = 0; i < labelsProp.arraySize; i++)
            {
                //Obtain the serialized property for the element in the labels array at index i
                SerializedProperty element = labelsProp.GetArrayElementAtIndex(i);
                
                //Draw a label showing the bit index
                EditorGUILayout.PropertyField(element, new GUIContent($"Bit {i:00}"));
                
                //Ensure changes are applied
                serializedObject.ApplyModifiedProperties(); 
            }
        }
    }
}
