namespace PsigenVision.TagMasking
{
    /// <summary>
    /// Represents an interface for utilizing a TagMaskLibrary. In order for the [TagMask] attribute to be usable, the members defined in this interface must be implemented by that script (an object reference to the TagMaskLibrary)
    /// </summary>
    public interface IUseTagMask
    {
        public TagMaskLibrary TagLibrary { get; }
    }
}