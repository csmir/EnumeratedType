using System.ComponentModel;
using System.ComponentModel.Enumerated;

namespace EnumeratedType.Examples;

public enum EnumeratedTypeEnum
{
    [Description("First")]
    [Category("Category 1")]
    [DefaultValue(1)]
    [ReadOnly(true)]
    [Aliases("First", "1")]
    [Order(1)]
    EnumeratedValue,
}
