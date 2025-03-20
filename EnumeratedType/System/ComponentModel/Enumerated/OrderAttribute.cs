namespace System.ComponentModel.Enumerated;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class OrderAttribute(int order) : Attribute
{
    public int Order { get; } = order;
}
