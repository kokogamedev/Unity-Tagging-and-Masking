using UnityEngine;

namespace PsigenVision.TagMasking.Examples
{
    [CreateAssetMenu(menuName = "Configs/ExampleWithTagMask", fileName = "ExampleWithTagMask")]
    public class ExampleWithTagMask : ScriptableObject
    {
        [field: SerializeField] public TagMaskLibrary IntTagLibrary { get; private set; }
        [field: SerializeField] public ByteTagMaskLibrary ByteTagLibrary { get; private set; }
        
        [TagMask]
        [SerializeField] int intMaskFromSerializedField;
        [TagMask("Assets/Samples/Tagging and Masking System/0.1.0/Simple Example/Libraries/TagMaskLibrary.asset")] 
        [SerializeField] int intMaskFromSpecifiedPath;
        [TagMask("Assets/BadPath/NonExistentLibrary.asset")]
        [SerializeField] int intMaskFromBadPath;
        
        [ByteTagMask]
        [SerializeField] int byteMaskFromSerializedField;
        [ByteTagMask("Assets/Samples/Tagging and Masking System/0.1.0/Simple Example/Libraries/ByteTagMaskLibrary.asset")] 
        [SerializeField] int byteMaskFromSpecifiedPath;
        [ByteTagMask("Assets/BadPath/NonExistentLibrary.asset")]
        [SerializeField] int byteMaskFromBadPath;
    }
}