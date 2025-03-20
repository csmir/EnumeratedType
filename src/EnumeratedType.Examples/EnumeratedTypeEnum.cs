using System.ComponentModel;
using System.ComponentModel.Enumerated;

namespace EnumeratedType.Examples
{
    public enum EnumeratedTypeEnum
    {
        // .ComponentModel
        [Description("First")]
        [Category("Category 1")]
        [DefaultValue(1)]
        [ReadOnly(true)]
        [Aliases("First", "1")]
        [Order(1)]
        EnumeratedValue,
    }
}
