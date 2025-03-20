using System.ComponentModel.Enumerated;

namespace System.ComponentModel
{
    /// <summary>
    ///     Represents a field in an enumeration.
    /// </summary>
    /// <param name="attributes">The attributes marked on the enum field.</param>
    public class EnumeratedType(IEnumerable<object> attributes) : EnumeratedTypeBase(attributes)
    {
        /// <summary>
        ///     Gets the order of the field.
        /// </summary>
        public virtual AttributeDefinition<int> Order { get; } 
            = AttributeDefinition.Define<OrderAttribute, int>(attributes, x => x.Order);

        /// <summary>
        ///     Gets the aliases of the field.
        /// </summary>
        public virtual AttributeDefinition<string[]> Aliases { get; } 
            = AttributeDefinition.Define<AliasesAttribute, string[]>(attributes, x => x.Aliases);

        /// <summary>
        ///     Gets the description of the field.
        /// </summary>
        public virtual AttributeDefinition<string> Description { get; } 
            = AttributeDefinition.Define<DescriptionAttribute, string>(attributes, x => x.Description);

        /// <summary>
        ///     Gets the category of the field.
        /// </summary>
        public virtual AttributeDefinition<string> Category { get; } 
            = AttributeDefinition.Define<CategoryAttribute, string>(attributes, x => x.Category);

        /// <summary>
        ///     Gets the default value of the field.
        /// </summary>
        public virtual AttributeDefinition<object> DefaultValue { get; } 
            = AttributeDefinition.Define<DefaultValueAttribute, object>(attributes, x => x.Value);

        /// <summary>
        ///     Gets whether the field is read-only.
        /// </summary>
        public virtual AttributeDefinition<bool> IsReadOnly { get; } 
            = AttributeDefinition.Define<ReadOnlyAttribute, bool>(attributes, x => x.IsReadOnly);
    }
}
