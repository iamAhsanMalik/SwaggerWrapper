namespace SW.Attibutes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class SwaggerSchemaNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
