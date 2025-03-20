using System.Collections.Concurrent;

namespace System.ComponentModel
{
    public static class EnumeratedTypeExtensions
    {
        private static readonly ConcurrentDictionary<Enum, EnumeratedTypeBase> _metadataContainerCache = new();

        /// <summary>
        ///     Gets a metadata object for the specified enumerated type definition. The metadata object is represented as an instance of the <see cref="EnumeratedType"/> class, which provides access to the attributes of the specified field.
        /// </summary>
        /// <remarks>
        ///     After first load, this method caches the metadata object for the specified field. Subsequent calls to this method will return the cached metadata object.
        /// </remarks>
        /// <param name="enumeratedTypeKey">The field to retrieve a metadata object for.</param>
        /// <returns>The definition of <see cref="EnumeratedType"/> covering access to the attributes for an <see cref="Enum"/> field.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the provided field is not a declared field on the <see cref="Enum"/> type targetted in this operation.</exception>
        public static EnumeratedType Extend(this Enum enumeratedTypeKey)
        {
            var value = _metadataContainerCache.GetOrAdd(enumeratedTypeKey, x => Factory(x, a => new EnumeratedType(a)));

            if (value is not EnumeratedType enumeratedType)
            {
                // If the value is not the expected type, remove it from the cache and try to add it again.
                _metadataContainerCache.TryRemove(enumeratedTypeKey, out _);
                _metadataContainerCache.TryAdd(enumeratedTypeKey, enumeratedType = Factory(enumeratedTypeKey, a => new EnumeratedType(a)));
            }

            return enumeratedType;
        }

        /// <summary>
        ///     Gets a metadata object for the specified enumerated type definition. The metadata object is represented as an implementation of the <see cref="EnumeratedTypeBase"/> class, which provides access to the attributes of the specified field.
        /// </summary>
        /// <remarks>
        ///     After first load, this method caches the metadata object for the specified field. Subsequent calls to this method will return the cached metadata object.
        /// </remarks>
        /// <typeparam name="T">The implementation of <see cref="EnumeratedTypeBase"/> to create or get.</typeparam>
        /// <param name="enumeratedTypeKey">The field to retrieve a metadata object for.</param>
        /// <param name="metadataContainerFactory">The creation factory used to create the metadata object this method is expected to return.</param>
        /// <returns>The implementation of <see cref="EnumeratedTypeBase"/> covering access to the attributes for an <see cref="Enum"/> field.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the provided field is not a declared field on the <see cref="Enum"/> type targetted in this operation.</exception>
        public static T Extend<T>(this Enum enumeratedTypeKey, Func<IEnumerable<object>, T> metadataContainerFactory)
            where T : EnumeratedTypeBase
        {
            var value = _metadataContainerCache.GetOrAdd(enumeratedTypeKey, x => Factory(x, metadataContainerFactory));

            if (value is not T enumeratedType)
            {
                // If the value is not the expected type, remove it from the cache and try to add it again.
                _metadataContainerCache.TryRemove(enumeratedTypeKey, out _);
                _metadataContainerCache.TryAdd(enumeratedTypeKey, enumeratedType = Factory(enumeratedTypeKey, metadataContainerFactory));
            }

            return enumeratedType;
        }

        private static T Factory<T>(Enum e, Func<IEnumerable<object>, T> f)
        {
            var fld = e.GetType().GetField(e.ToString())
                ?? throw new ArgumentOutOfRangeException(nameof(e));
            return f(fld.GetCustomAttributes(true));
        }
    }
}
