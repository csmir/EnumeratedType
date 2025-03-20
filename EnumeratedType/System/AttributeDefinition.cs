namespace System
{
    /// <summary>
    ///     Represents a definition of an attribute, declaring if it is defined and providing access to its value.
    /// </summary>
    /// <typeparam name="T">The type of the value to access.</typeparam>
    /// <param name="selector">The selector for the value to access.</param>
    public struct AttributeDefinition<T>(Func<(Attribute?, T?)> selector)
    {
        private bool _hasValue;
        private T? _value;

        /// <summary>
        ///     Gets the value of the attribute.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the attribute is not defined.</exception>
        public T? Value
        {
            get
            {
                if (!_hasValue)
                {
                    var (attribute, value) = selector();

                    if (attribute is null)
                        throw new InvalidOperationException($"Value unavailable; Attempted to get value of non-existent attribute definition of type {nameof(T)}.");

                    _value = value;
                    _hasValue = true;
                }

                return _value;
            }
        }

        /// <summary>
        ///     Gets whether the attribute is defined.
        /// </summary>
        public readonly bool IsDefined
            => selector().Item1 is not null;

        /// <summary>
        ///     Implicitly converts the attribute definition to its value.
        /// </summary>
        /// <param name="definition"></param>
        public static implicit operator T?(AttributeDefinition<T> definition)
            => definition.Value;

        /// <inheritdoc />
        public override string ToString()
            => IsDefined ? Value?.ToString() ?? string.Empty : string.Empty;
    }

    /// <summary>
    ///     Provides methods to define an attribute.
    /// </summary>
    public static class AttributeDefinition
    {
        /// <summary>
        ///     Creates a new attribute definition based on the provided attributes and value selector.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="attributes"></param>
        /// <param name="valueSelector"></param>
        /// <returns>A new <see cref="AttributeDefinition{T}"/>.</returns>
        public static AttributeDefinition<TValue> Define<T, TValue>(IEnumerable<object> attributes, Func<T, TValue?> valueSelector)
            where T : Attribute
        {
            return new AttributeDefinition<TValue>(() =>
            {
                var a = attributes.OfType<T>().FirstOrDefault();
                return (a, a != null ? valueSelector(a) : default);
            });
        }
    }
}
