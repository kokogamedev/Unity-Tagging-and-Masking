using System;
using System.Numerics;
using UnityEngine;

namespace PsigenVision.TagMasking
{
    [CreateAssetMenu(fileName = "TagMaskLibrary", menuName = "PsigenVision/TagMaskLibrary")]

    public class TagMaskLibrary : ScriptableObject
    {
        /// <summary>
        /// Represents an array of fixed-size string labels corresponding to the 32 bits in an Int32.
        /// Each label is used to identify or associate a specific bit in the bitmask, allowing a readable mapping
        /// for operations that involve bit-level manipulation.
        /// </summary>
        /// <remarks>
        /// This private array stores the labels internally for the class and provides a mapping between human-readable
        /// tags and specific bit positions. It is primarily intended for use within the context of tag-based masking systems.
        /// </remarks>
        [SerializeField] private string[] labels = new string[32]; // A fixed array of 32 strings (mapping to the bits in an Int32)

        /// <summary>
        /// Gets the array of labels used for identifying and associating bit positions in a bitmask.
        /// Each label corresponds to one of the 32 bits in an Int32, enabling a mapping between human-readable
        /// string labels and specific bitmask values.
        /// </summary>
        /// <remarks>
        /// This property is used to expose the labels array, often utilized when drawing custom bitmask fields
        /// in a Unity Inspector or for other contexts where descriptive tags are required for individual bits.
        /// </remarks>
        public string[] Labels => labels;

        /// <summary>
        /// Converts a label to its corresponding bit position in a bitmask.
        /// </summary>
        /// <param name="label">The label to look up in the labels array.</param>
        /// <returns>
        /// An integer representing the bit value derived from the index of the label in the labels array.
        /// Returns -1 if the label is not found.
        /// </returns>
        public int ToMask(string label) // We only use this when we absolutely MUST convert a string to a bit.
        {
            for (int i = 0; i < labels.Length; i++)
            {
                // Using Ordinal comparison is the fastest way to compare strings in C#
                if (string.Equals(labels[i], label, System.StringComparison.Ordinal))
                {
                    return 1 << i; // Return the bit value derived from the index (the bit position)
                }
            }
            return -1; // Not found
        }

        /// <summary>
        /// Converts a bit position in a bitmask to its corresponding label in the labels array.
        /// </summary>
        /// <param name="bit">An integer representing a single bit in a bitmask. Must be a power of two.</param>
        /// <returns>
        /// A string representing the label associated with the given bit.
        /// Returns null if the bit is invalid, out of range, or does not correspond to any label.
        /// </returns>
        public string ToLabel(int bit) // We only use this when we absolutely MUST convert the bit to the appropriate label according to this TagMaskLibrary
        {
            // Validate input bit is a power of two.
            if (bit > 0 && (bit & (bit - 1)) == 0) //The bit cannot be negative or 0 (won't result in a real index), and due to the 1 << i operation used to derive our bitmask, only power of 2 values will result in a valid index (i.e. (bit & (bit - 1)) == 0) 
            {
                int index = BitOperations.TrailingZeroCount(bit); // Get the index from the bit.
                if (index >= 0 && index < labels.Length) // Ensure the index is within valid range.
                {
                    return labels[index]; // Return the label for this bit.
                }
            }
            return null; // Return `null` if the bit is invalid or out of range.
        }
    }
}
