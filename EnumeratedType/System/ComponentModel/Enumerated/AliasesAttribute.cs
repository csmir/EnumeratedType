namespace System.ComponentModel.Enumerated;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class AliasesAttribute(params string[] aliases) : Attribute
{
    public string[] Aliases { get; } = aliases;
}
