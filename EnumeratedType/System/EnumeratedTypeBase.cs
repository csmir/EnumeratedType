namespace System
{
    /// <summary>
    ///     Represents a field in an enumeration, providing access to the attributes of the specified field.
    /// </summary>
    /// <param name="attributes">The attributes marked on the enum field.</param>
    public abstract class EnumeratedTypeBase(IEnumerable<object> attributes)
    {
        /// <summary>
        ///     The definitions of the attributes marked on the enum field.
        /// </summary>
        protected IEnumerable<object> Definitions { get; } = attributes;
    }
}
