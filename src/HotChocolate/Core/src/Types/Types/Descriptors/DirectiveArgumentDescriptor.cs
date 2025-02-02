using System;
using System.Reflection;
using HotChocolate.Language;
using HotChocolate.Types.Descriptors.Definitions;
using static HotChocolate.Types.MemberKind;

#nullable enable

namespace HotChocolate.Types.Descriptors;

/// <summary>
/// A fluent configuration API for GraphQL directive arguments.
/// </summary>
public class DirectiveArgumentDescriptor
    : ArgumentDescriptorBase<DirectiveArgumentDefinition>
    , IDirectiveArgumentDescriptor
{
    /// <summary>
    ///  Creates a new instance of <see cref="DirectiveArgumentDescriptor"/>
    /// </summary>
    protected internal DirectiveArgumentDescriptor(
        IDescriptorContext context,
        NameString argumentName)
        : base(context)
    {
        Definition.Name = argumentName;
    }

    /// <summary>
    ///  Creates a new instance of <see cref="DirectiveArgumentDescriptor"/>
    /// </summary>
    protected internal DirectiveArgumentDescriptor(
        IDescriptorContext context,
        PropertyInfo property)
        : base(context)
    {
        Definition.Name = context.Naming.GetMemberName(property, DirectiveArgument);
        Definition.Description = context.Naming.GetMemberDescription(property, DirectiveArgument);
        Definition.Type = context.TypeInspector.GetInputReturnTypeRef(property);
        Definition.Property = property;

        if (context.TypeInspector.TryGetDefaultValue(property, out object? defaultValue))
        {
            Definition.RuntimeDefaultValue = defaultValue;
        }

        if (context.Naming.IsDeprecated(property, out string? reason))
        {
            Deprecated(reason);
        }
    }

    /// <summary>
    ///  Creates a new instance of <see cref="DirectiveArgumentDescriptor"/>
    /// </summary>
    protected internal DirectiveArgumentDescriptor(
        IDescriptorContext context,
        DirectiveArgumentDefinition definition)
        : base(context)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
    }

    /// <inheritdoc />
    protected override void OnCreateDefinition(DirectiveArgumentDefinition definition)
    {
        if (!Definition.AttributesAreApplied && Definition.Property is not null)
        {
            Context.TypeInspector.ApplyAttributes(Context, this, Definition.Property);
            Definition.AttributesAreApplied = true;
        }

        base.OnCreateDefinition(definition);
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor SyntaxNode(
        InputValueDefinitionNode inputValueDefinition)
    {
        base.SyntaxNode(inputValueDefinition);
        return this;
    }

    /// <inheritdoc />
    public IDirectiveArgumentDescriptor Name(NameString value)
    {
        Definition.Name = value.EnsureNotEmpty(nameof(value));
        return this;
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor Deprecated(string? reason)
    {
        base.Deprecated(reason);
        return this;
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor Deprecated()
    {
        base.Deprecated();
        return this;
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor Description(string value)
    {
        base.Description(value);
        return this;
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor Type<TInputType>()
        where TInputType : IInputType
    {
        base.Type<TInputType>();
        return this;
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor Type<TInputType>(TInputType inputType)
        where TInputType : class, IInputType
    {
        base.Type(inputType);
        return this;
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor Type(ITypeNode typeNode)
    {
        base.Type(typeNode);
        return this;
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor Type(Type type)
    {
        base.Type(type);
        return this;
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor DefaultValue(IValueNode value)
    {
        base.DefaultValue(value);
        return this;
    }

    /// <inheritdoc />
    public new IDirectiveArgumentDescriptor DefaultValue(object value)
    {
        base.DefaultValue(value);
        return this;
    }

    /// <inheritdoc />
    public IDirectiveArgumentDescriptor Ignore(bool ignore = true)
    {
        Definition.Ignore = ignore;
        return this;
    }

    /// <summary>
    /// Creates a new instance of <see cref="DirectiveArgumentDescriptor "/>
    /// </summary>
    /// <param name="context">The descriptor context</param>
    /// <param name="argumentName">The name of the argument</param>
    /// <returns>An instance of <see cref="DirectiveArgumentDescriptor "/></returns>
    public static DirectiveArgumentDescriptor New(
        IDescriptorContext context,
        NameString argumentName)
        => new(context, argumentName);

    /// <summary>
    /// Creates a new instance of <see cref="DirectiveArgumentDescriptor "/>
    /// </summary>
    /// <param name="context">The descriptor context</param>
    /// <param name="property">The property this argument is used for</param>
    /// <returns>An instance of <see cref="DirectiveArgumentDescriptor "/></returns>
    public static DirectiveArgumentDescriptor New(
        IDescriptorContext context,
        PropertyInfo property)
        => new(context, property);

    /// <summary>
    /// Creates a new instance of <see cref="DirectiveArgumentDescriptor "/>
    /// </summary>
    /// <param name="context">The descriptor context</param>
    /// <param name="definition">The definition of the argument</param>
    /// <returns>An instance of <see cref="DirectiveArgumentDescriptor "/></returns>
    public static DirectiveArgumentDescriptor From(
        IDescriptorContext context,
        DirectiveArgumentDefinition definition)
        => new(context, definition);
}
