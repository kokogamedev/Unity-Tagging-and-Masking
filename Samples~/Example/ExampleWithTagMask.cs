using UnityEngine;

namespace PsigenVision.TagMasking.Examples
{
    [CreateAssetMenu(menuName = "Configs/ExampleWithTagMask", fileName = "ExampleWithTagMask")]
    public class ExampleWithTagMask : ScriptableObject, IUseTagMask
    {
        [field: SerializeField] public TagMaskLibrary TagLibrary { get; private set; }
        
        [TagMask]
        [SerializeField] int mask;

    }
}