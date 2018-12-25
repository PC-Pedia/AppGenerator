﻿using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Design;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace CSharpGenerator.Code
{
    public class CodeGenerator
    {
        // Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

        public static Dictionary<string, string> TemplateMetadata = new Dictionary<string, string>();

        /// <summary>
        /// Responsible for helping to create source code that is
        /// correctly formated and functional
        /// </summary>
        public class CodeGenerationTools
        {
            private readonly DynamicTextTransformation _textTransformation;
            private readonly CSharpCodeProvider _code;
            private readonly MetadataTools _ef;

            /// <summary>
            /// Initializes a new CodeGenerationTools object with the TextTransformation (T4 generated class)
            /// that is currently running
            /// </summary>
            public CodeGenerationTools(object textTransformation)
            {
                if (textTransformation == null)
                {
                    throw new ArgumentNullException("textTransformation");
                }

                _textTransformation = DynamicTextTransformation.Create(textTransformation);
                _code = new CSharpCodeProvider();
                _ef = new MetadataTools(_textTransformation);
                FullyQualifySystemTypes = false;
                CamelCaseFields = true;
            }

            /// <summary>
            /// When true, all types that are not being generated
            /// are fully qualified to keep them from conflicting with
            /// types that are being generated. Useful when you have
            /// something like a type being generated named System.
            ///
            /// Default is false.
            /// </summary>
            public bool FullyQualifySystemTypes { get; set; }

            /// <summary>
            /// When true, the field names are Camel Cased,
            /// otherwise they will preserve the case they
            /// start with.
            ///
            /// Default is true.
            /// </summary>
            public bool CamelCaseFields { get; set; }

            /// <summary>
            /// Returns the NamespaceName suggested by VS if running inside VS.  Otherwise, returns
            /// null.
            /// </summary>
            public string VsNamespaceSuggestion()
            {
                string suggestion = _textTransformation.Host.ResolveParameterValue("directiveId", "namespaceDirectiveProcessor", "namespaceHint");
                if (String.IsNullOrEmpty(suggestion))
                {
                    return null;
                }

                return suggestion;
            }

            /// <summary>
            /// Returns a string that is safe for use as an identifier in C#.
            /// Keywords are escaped.
            /// </summary>
            public string Escape(string name)
            {
                if (name == null)
                {
                    return null;
                }

                return _code.CreateEscapedIdentifier(name);
            }

            /// <summary>
            /// Returns the name of the TypeUsage's EdmType that is safe for
            /// use as an identifier.
            /// </summary>
            public string Escape(TypeUsage typeUsage)
            {
                if (typeUsage == null)
                {
                    return null;
                }

                if (typeUsage.EdmType is ComplexType ||
                    typeUsage.EdmType is EntityType)
                {
                    return Escape(typeUsage.EdmType.Name);
                }
                else if (typeUsage.EdmType is SimpleType)
                {
                    Type clrType = _ef.UnderlyingClrType(typeUsage.EdmType);
                    string typeName = (typeUsage.EdmType is EnumType) ? Escape(typeUsage.EdmType.Name) : Escape(clrType);
                    if (clrType.IsValueType && _ef.IsNullable(typeUsage))
                    {
                        return String.Format(CultureInfo.InvariantCulture, "Nullable<{0}>", typeName);
                    }

                    return typeName;
                }
                else if (typeUsage.EdmType is CollectionType)
                {
                    return String.Format(CultureInfo.InvariantCulture, "ICollection<{0}>", Escape(((CollectionType)typeUsage.EdmType).TypeUsage));
                }

                throw new ArgumentException("typeUsage");
            }

            /// <summary>
            /// Returns the name of the EdmMember that is safe for
            /// use as an identifier.
            /// </summary>
            public string Escape(EdmMember member)
            {
                if (member == null)
                {
                    return null;
                }

                return Escape(member.Name);
            }

            /// <summary>
            /// Returns the name of the EdmType that is safe for
            /// use as an identifier.
            /// </summary>
            public string Escape(EdmType type)
            {
                if (type == null)
                {
                    return null;
                }

                return Escape(type.Name);
            }

            /// <summary>
            /// Returns the name of the EdmFunction that is safe for
            /// use as an identifier.
            /// </summary>
            public string Escape(EdmFunction function)
            {
                if (function == null)
                {
                    return null;
                }

                return Escape(function.Name);
            }

            /// <summary>
            /// Returns the name of the EnumMember that is safe for
            /// use as an identifier.
            /// </summary>
            public string Escape(EnumMember member)
            {
                if (member == null)
                {
                    return null;
                }

                return Escape(member.Name);
            }

            /// <summary>
            /// Returns the name of the EntityContainer that is safe for
            /// use as an identifier.
            /// </summary>
            public string Escape(EntityContainer container)
            {
                if (container == null)
                {
                    return null;
                }

                return Escape(container.Name);
            }

            /// <summary>
            /// Returns the name of the EntitySet that is safe for
            /// use as an identifier.
            /// </summary>
            public string Escape(EntitySet set)
            {
                if (set == null)
                {
                    return null;
                }

                return Escape(set.Name);
            }

            /// <summary>
            /// Returns the name of the StructuralType that is safe for
            /// use as an identifier.
            /// </summary>
            public string Escape(StructuralType type)
            {
                if (type == null)
                {
                    return null;
                }

                return Escape(type.Name);
            }

            /// <summary>
            /// Returns the NamespaceName with each segment safe to
            /// use as an identifier.
            /// </summary>
            public string EscapeNamespace(string namespaceName)
            {
                if (String.IsNullOrEmpty(namespaceName))
                {
                    return namespaceName;
                }

                string[] parts = namespaceName.Split('.');
                namespaceName = String.Empty;
                foreach (string part in parts)
                {
                    if (namespaceName != String.Empty)
                    {
                        namespaceName += ".";
                    }

                    namespaceName += Escape(part);
                }

                return namespaceName;
            }

            /// <summary>
            /// Returns the name of the EdmMember formatted for
            /// use as a field identifier.
            ///
            /// This method changes behavior based on the CamelCaseFields
            /// setting.
            /// </summary>
            public string FieldName(EdmMember member)
            {
                if (member == null)
                {
                    return null;
                }

                return FieldName(member.Name);
            }

            /// <summary>
            /// Returns the name of the EntitySet formatted for
            /// use as a field identifier.
            ///
            /// This method changes behavior based on the CamelCaseFields
            /// setting.
            /// </summary>
            public string FieldName(EntitySet set)
            {
                if (set == null)
                {
                    return null;
                }

                return FieldName(set.Name);

            }

            private string FieldName(string name)
            {
                if (CamelCaseFields)
                {
                    return "_" + CamelCase(name);
                }
                else
                {
                    return "_" + name;
                }
            }

            /// <summary>
            /// Returns the name of the Type object formatted for
            /// use in source code.
            ///
            /// This method changes behavior based on the FullyQualifySystemTypes
            /// setting.
            /// </summary>
            public string Escape(Type clrType)
            {
                return Escape(clrType, FullyQualifySystemTypes);
            }

            /// <summary>
            /// Returns the name of the Type object formatted for
            /// use in source code.
            /// </summary>
            public string Escape(Type clrType, bool fullyQualifySystemTypes)
            {
                if (clrType == null)
                {
                    return null;
                }

                string typeName;
                if (fullyQualifySystemTypes)
                {
                    typeName = "global::" + clrType.FullName;
                }
                else
                {
                    typeName = _code.GetTypeOutput(new CodeTypeReference(clrType));
                }
                return typeName;
            }

            /// <summary>
            /// Returns the abstract option if the entity is Abstract, otherwise returns String.Empty
            /// </summary>
            public string AbstractOption(EntityType entity)
            {
                if (entity.Abstract)
                {
                    return "abstract";
                }
                return String.Empty;
            }

            /// <summary>
            /// Returns the passed in identifier with the first letter changed to lowercase
            /// </summary>
            public string CamelCase(string identifier)
            {
                if (String.IsNullOrEmpty(identifier))
                {
                    return identifier;
                }

                if (identifier.Length == 1)
                {
                    return identifier[0].ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
                }

                return identifier[0].ToString(CultureInfo.InvariantCulture).ToLowerInvariant() + identifier.Substring(1);
            }

            /// <summary>
            /// If the value parameter is null or empty an empty string is returned,
            /// otherwise it retuns value with a single space concatenated on the end.
            /// </summary>
            public string SpaceAfter(string value)
            {
                return StringAfter(value, " ");
            }

            /// <summary>
            /// If the value parameter is null or empty an empty string is returned,
            /// otherwise it retuns value with a single space concatenated on the end.
            /// </summary>
            public string SpaceBefore(string value)
            {
                return StringBefore(" ", value);
            }

            /// <summary>
            /// If the value parameter is null or empty an empty string is returned,
            /// otherwise it retuns value with append concatenated on the end.
            /// </summary>
            public string StringAfter(string value, string append)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return String.Empty;
                }

                return value + append;
            }

            /// <summary>
            /// If the value parameter is null or empty an empty string is returned,
            /// otherwise it retuns value with prepend concatenated on the front.
            /// </summary>
            public string StringBefore(string prepend, string value)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return String.Empty;
                }

                return prepend + value;
            }

            /// <summary>
            /// Returns false and shows an error if the supplied type names aren't case-insensitively unique,
            /// otherwise returns true.
            /// </summary>
            public bool VerifyCaseInsensitiveTypeUniqueness(IEnumerable<string> types, string sourceFile)
            {
                return false;//VerifyCaseInsensitiveUniqueness(types, t => string.Format(CultureInfo.CurrentCulture, GetResourceString("Template_CaseInsensitiveTypeConflict"), t), sourceFile);
            }

            /// <summary>
            /// Returns false and shows an error if the supplied strings aren't case-insensitively unique,
            /// otherwise returns true.
            /// </summary>
            private bool VerifyCaseInsensitiveUniqueness(IEnumerable<string> items, Func<string, string> formatMessage, string sourceFile)
            {
                HashSet<string> hash = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
                foreach (string item in items)
                {
                    if (!hash.Add(item))
                    {
                        _textTransformation.Errors.Add(new System.CodeDom.Compiler.CompilerError(sourceFile, -1, -1, "6023", formatMessage(item)));
                        return false;
                    }
                }
                return true;
            }

            /// <summary>
            /// Returns the names of the items in the supplied collection that correspond to O-Space types.
            /// </summary>
            public IEnumerable<string> GetAllGlobalItems(EdmItemCollection itemCollection)
            {
                return itemCollection.GetItems<GlobalItem>().Where(i => i is EntityType || i is ComplexType || i is EnumType || i is EntityContainer).Select(g => GetGlobalItemName(g));
            }

            /// <summary>
            /// Returns the name of the supplied GlobalItem.
            /// </summary>
            public string GetGlobalItemName(GlobalItem item)
            {
                if (item is EdmType)
                {
                    return ((EdmType)item).Name;
                }
                else
                {
                    return ((EntityContainer)item).Name;
                }
            }

            /// <summary>
            /// Retuns as full of a name as possible, if a namespace is provided
            /// the namespace and name are combined with a period, otherwise just
            /// the name is returned.
            /// </summary>
            public string CreateFullName(string namespaceName, string name)
            {
                if (String.IsNullOrEmpty(namespaceName))
                {
                    return name;
                }

                return namespaceName + "." + name;
            }

            /// <summary>
            /// Retuns a literal representing the supplied value.
            /// </summary>
            public string CreateLiteral(object value)
            {
                if (value == null)
                {
                    return string.Empty;
                }

                Type type = value.GetType();
                if (type.IsEnum)
                {
                    return type.FullName + "." + value.ToString();
                }
                if (type == typeof(Guid))
                {
                    return string.Format(CultureInfo.InvariantCulture, "new Guid(\"{0}\")",
                                         ((Guid)value).ToString("D", CultureInfo.InvariantCulture));
                }
                else if (type == typeof(DateTime))
                {
                    return string.Format(CultureInfo.InvariantCulture, "new DateTime({0}, DateTimeKind.Unspecified)",
                                         ((DateTime)value).Ticks);
                }
                else if (type == typeof(byte[]))
                {
                    var arrayInit = string.Join(", ", ((byte[])value).Select(b => b.ToString(CultureInfo.InvariantCulture)).ToArray());
                    return string.Format(CultureInfo.InvariantCulture, "new Byte[] {{{0}}}", arrayInit);
                }
                else if (type == typeof(DateTimeOffset))
                {
                    var dto = (DateTimeOffset)value;
                    return string.Format(CultureInfo.InvariantCulture, "new DateTimeOffset({0}, new TimeSpan({1}))",
                                         dto.Ticks, dto.Offset.Ticks);
                }
                else if (type == typeof(TimeSpan))
                {
                    return string.Format(CultureInfo.InvariantCulture, "new TimeSpan({0})",
                                         ((TimeSpan)value).Ticks);
                }

                var expression = new CodePrimitiveExpression(value);
                var writer = new StringWriter();
                CSharpCodeProvider code = new CSharpCodeProvider();
                code.GenerateCodeFromExpression(expression, writer, new CodeGeneratorOptions());
                return writer.ToString();
            }

            /// <summary>
            /// Returns a resource string from the System.Data.Entity.Design assembly.
            /// </summary>
            public static string GetResourceString(string resourceName, CultureInfo culture)
            {
                if (_resourceManager == null)
                {
                    _resourceManager = new System.Resources.ResourceManager("System.Data.Entity.Design",
                        typeof(System.Data.Entity.Design.MetadataItemCollectionFactory).Assembly);
                }

                return _resourceManager.GetString(resourceName, culture);
            }
            static System.Resources.ResourceManager _resourceManager;

            private const string ExternalTypeNameAttributeName = @"http://schemas.microsoft.com/ado/2006/04/codegeneration:ExternalTypeName";

            /// <summary>
            /// Gets the entity, complex, or enum types for which code should be generated from the given item collection.
            /// Any types for which an ExternalTypeName annotation has been applied in the conceptual model
            /// metadata (CSDL) are filtered out of the returned list.
            /// </summary>
            /// <typeparam name="T">The type of item to return.</typeparam>
            /// <param name="itemCollection">The item collection to look in.</param>
            /// <returns>The items to generate.</returns>
            public IEnumerable<T> GetItemsToGenerate<T>(ItemCollection itemCollection) where T : GlobalItem
            {
                return itemCollection.GetItems<T>().Where(i => !i.MetadataProperties.Any(p => p.Name == ExternalTypeNameAttributeName));
            }

            /// <summary>
            /// Returns the escaped type name to use for the given usage of a c-space type in o-space. This might be
            /// an external type name if the ExternalTypeName annotation has been specified in the
            /// conceptual model metadata (CSDL).
            /// </summary>
            /// <param name="typeUsage">The c-space type usage to get a name for.</param>
            /// <returns>The type name to use.</returns>
            public string GetTypeName(TypeUsage typeUsage)
            {
                return typeUsage == null ? null : GetTypeName(typeUsage.EdmType, _ef.IsNullable(typeUsage), null);
            }

            /// <summary>
            /// Returns the escaped type name to use for the given c-space type in o-space. This might be
            /// an external type name if the ExternalTypeName annotation has been specified in the
            /// conceptual model metadata (CSDL).
            /// </summary>
            /// <param name="edmType">The c-space type to get a name for.</param>
            /// <returns>The type name to use.</returns>
            public string GetTypeName(EdmType edmType)
            {
                return GetTypeName(edmType, null, null);
            }

            /// <summary>
            /// Returns the escaped type name to use for the given usage of an c-space type in o-space. This might be
            /// an external type name if the ExternalTypeName annotation has been specified in the
            /// conceptual model metadata (CSDL).
            /// </summary>
            /// <param name="typeUsage">The c-space type usage to get a name for.</param>
            /// <param name="modelNamespace">If not null and the type's namespace does not match this namespace, then a
            /// fully qualified name will be returned.</param>
            /// <returns>The type name to use.</returns>
            public string GetTypeName(TypeUsage typeUsage, string modelNamespace)
            {
                return typeUsage == null ? null : GetTypeName(typeUsage.EdmType, _ef.IsNullable(typeUsage), modelNamespace);
            }

            /// <summary>
            /// Returns the escaped type name to use for the given c-space type in o-space. This might be
            /// an external type name if the ExternalTypeName annotation has been specified in the
            /// conceptual model metadata (CSDL).
            /// </summary>
            /// <param name="edmType">The c-space type to get a name for.</param>
            /// <param name="modelNamespace">If not null and the type's namespace does not match this namespace, then a
            /// fully qualified name will be returned.</param>
            /// <returns>The type name to use.</returns>
            public string GetTypeName(EdmType edmType, string modelNamespace)
            {
                return GetTypeName(edmType, null, modelNamespace);
            }

            /// <summary>
            /// Returns the escaped type name to use for the given c-space type in o-space. This might be
            /// an external type name if the ExternalTypeName annotation has been specified in the
            /// conceptual model metadata (CSDL).
            /// </summary>
            /// <param name="edmType">The c-space type to get a name for.</param>
            /// <param name="isNullable">Set this to true for nullable usage of this type.</param>
            /// <param name="modelNamespace">If not null and the type's namespace does not match this namespace, then a
            /// fully qualified name will be returned.</param>
            /// <returns>The type name to use.</returns>
            private string GetTypeName(EdmType edmType, bool? isNullable, string modelNamespace)
            {
                if (edmType == null)
                {
                    return null;
                }

                var collectionType = edmType as CollectionType;
                if (collectionType != null)
                {
                    return String.Format(CultureInfo.InvariantCulture, "ICollection<{0}>", GetTypeName(collectionType.TypeUsage, modelNamespace));
                }

                // Try to get an external type name, and if that is null, then try to get escape the name from metadata,
                // possibly namespace-qualifying it.
                var typeName = Escape(edmType.MetadataProperties
                                      .Where(p => p.Name == ExternalTypeNameAttributeName)
                                      .Select(p => (string)p.Value)
                                      .FirstOrDefault())
                    ??
                    (modelNamespace != null && edmType.NamespaceName != modelNamespace ?
                     CreateFullName(EscapeNamespace(edmType.NamespaceName), Escape(edmType)) :
                     Escape(edmType));

                if (edmType is StructuralType)
                {
                    return typeName;
                }

                if (edmType is SimpleType)
                {
                    var clrType = _ef.UnderlyingClrType(edmType);
                    if (!(edmType is EnumType))
                    {
                        typeName = Escape(clrType);
                    }

                    return clrType.IsValueType && isNullable == true ?
                        String.Format(CultureInfo.InvariantCulture, "Nullable<{0}>", typeName) :
                        typeName;
                }

                throw new ArgumentException("typeUsage");
            }
        }


        /// <summary>
        /// Responsible for making the Entity Framework Metadata more
        /// accessible for code generation.
        /// </summary>
        public class MetadataTools
        {
            private readonly DynamicTextTransformation _textTransformation;

            /// <summary>
            /// Initializes an MetadataTools Instance  with the
            /// TextTransformation (T4 generated class) that is currently running
            /// </summary>
            public MetadataTools(object textTransformation)
            {
                if (textTransformation == null)
                {
                    throw new ArgumentNullException("textTransformation");
                }

                _textTransformation = DynamicTextTransformation.Create(textTransformation);
            }

            /// <summary>
            /// This method returns the underlying CLR type of the o-space type corresponding to the supplied <paramref name="typeUsage"/>
            /// Note that for an enum type this means that the type backing the enum will be returned, not the enum type itself.
            /// </summary>
            public Type ClrType(TypeUsage typeUsage)
            {
                return UnderlyingClrType(typeUsage.EdmType);
            }

            /// <summary>
            /// This method returns the underlying CLR type given the c-space type.
            /// Note that for an enum type this means that the type backing the enum will be returned, not the enum type itself.
            /// </summary>
            public Type UnderlyingClrType(EdmType edmType)
            {
                var primitiveType = edmType as PrimitiveType;
                if (primitiveType != null)
                {
                    return primitiveType.ClrEquivalentType;
                }

                var enumType = edmType as EnumType;
                if (enumType != null)
                {
                    return enumType.UnderlyingType.ClrEquivalentType;
                }

                return typeof(object);
            }

            /// <summary>
            /// True if the EdmProperty is a key of its DeclaringType, False otherwise.
            /// </summary>
            public bool IsKey(EdmProperty property)
            {
                if (property != null && property.DeclaringType.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                {
                    return ((EntityType)property.DeclaringType).KeyMembers.Contains(property);
                }

                return false;
            }

            /// <summary>
            /// True if the EdmProperty TypeUsage is Nullable, False otherwise.
            /// </summary>
            public bool IsNullable(EdmProperty property)
            {
                return property != null && IsNullable(property.TypeUsage);
            }

            /// <summary>
            /// True if the TypeUsage is Nullable, False otherwise.
            /// </summary>
            public bool IsNullable(TypeUsage typeUsage)
            {
                Facet nullableFacet = null;
                if (typeUsage != null &&
                    typeUsage.Facets.TryGetValue("Nullable", true, out nullableFacet))
                {
                    return (bool)nullableFacet.Value;
                }

                return false;
            }

            /// <summary>
            /// If the passed in TypeUsage represents a collection this method returns final element
            /// type of the collection, otherwise it returns the value passed in.
            /// </summary>
            public TypeUsage GetElementType(TypeUsage typeUsage)
            {
                if (typeUsage == null)
                {
                    return null;
                }

                if (typeUsage.EdmType is CollectionType)
                {
                    return GetElementType(((CollectionType)typeUsage.EdmType).TypeUsage);
                }
                else
                {
                    return typeUsage;
                }
            }

            /// <summary>
            /// Returns the NavigationProperty that is the other end of the same association set if it is
            /// available, otherwise it returns null.
            /// </summary>
            public NavigationProperty Inverse(NavigationProperty navProperty)
            {
                if (navProperty == null)
                {
                    return null;
                }

                EntityType toEntity = navProperty.ToEndMember.GetEntityType();
                return toEntity.NavigationProperties
                    .SingleOrDefault(n => Object.ReferenceEquals(n.RelationshipType, navProperty.RelationshipType) && !Object.ReferenceEquals(n, navProperty));
            }

            /// <summary>
            /// Given a property on the dependent end of a referential constraint, returns the corresponding property on the principal end.
            /// Requires: The association has a referential constraint, and the specified dependentProperty is one of the properties on the dependent end.
            /// </summary>
            public EdmProperty GetCorrespondingPrincipalProperty(NavigationProperty navProperty, EdmProperty dependentProperty)
            {
                if (navProperty == null)
                {
                    throw new ArgumentNullException("navProperty");
                }

                if (dependentProperty == null)
                {
                    throw new ArgumentNullException("dependentProperty");
                }

                ReadOnlyMetadataCollection<EdmProperty> fromProperties = GetPrincipalProperties(navProperty);
                ReadOnlyMetadataCollection<EdmProperty> toProperties = GetDependentProperties(navProperty);
                return fromProperties[toProperties.IndexOf(dependentProperty)];
            }

            /// <summary>
            /// Given a property on the principal end of a referential constraint, returns the corresponding property on the dependent end.
            /// Requires: The association has a referential constraint, and the specified principalProperty is one of the properties on the principal end.
            /// </summary>
            public EdmProperty GetCorrespondingDependentProperty(NavigationProperty navProperty, EdmProperty principalProperty)
            {
                if (navProperty == null)
                {
                    throw new ArgumentNullException("navProperty");
                }

                if (principalProperty == null)
                {
                    throw new ArgumentNullException("principalProperty");
                }

                ReadOnlyMetadataCollection<EdmProperty> fromProperties = GetPrincipalProperties(navProperty);
                ReadOnlyMetadataCollection<EdmProperty> toProperties = GetDependentProperties(navProperty);
                return toProperties[fromProperties.IndexOf(principalProperty)];
            }

            /// <summary>
            /// Gets the collection of properties that are on the principal end of a referential constraint for the specified navigation property.
            /// Requires: The association has a referential constraint.
            /// </summary>
            public ReadOnlyMetadataCollection<EdmProperty> GetPrincipalProperties(NavigationProperty navProperty)
            {
                if (navProperty == null)
                {
                    throw new ArgumentNullException("navProperty");
                }

                return ((AssociationType)navProperty.RelationshipType).ReferentialConstraints[0].FromProperties;
            }

            /// <summary>
            /// Gets the collection of properties that are on the dependent end of a referential constraint for the specified navigation property.
            /// Requires: The association has a referential constraint.
            /// </summary>
            public ReadOnlyMetadataCollection<EdmProperty> GetDependentProperties(NavigationProperty navProperty)
            {
                if (navProperty == null)
                {
                    throw new ArgumentNullException("navProperty");
                }

                return ((AssociationType)navProperty.RelationshipType).ReferentialConstraints[0].ToProperties;
            }

            /// <summary>
            /// True if this entity type requires the HandleCascadeDelete method defined and the method has
            /// not been defined on any base type
            /// </summary>
            public bool NeedsHandleCascadeDeleteMethod(ItemCollection itemCollection, EntityType entity)
            {
                bool needsMethod = ContainsCascadeDeleteAssociation(itemCollection, entity);
                // Check to make sure no base types have already declared this method
                EntityType baseType = entity.BaseType as EntityType;
                while (needsMethod && baseType != null)
                {
                    needsMethod = !ContainsCascadeDeleteAssociation(itemCollection, baseType);
                    baseType = baseType.BaseType as EntityType;
                }
                return needsMethod;
            }

            /// <summary>
            /// True if this entity type participates in any relationships where the other end has an OnDelete
            /// cascade delete defined, or if it is the dependent in any identifying relationships
            /// </summary>
            private bool ContainsCascadeDeleteAssociation(ItemCollection itemCollection, EntityType entity)
            {
                return itemCollection.GetItems<AssociationType>().Where(a =>
                        ((RefType)a.AssociationEndMembers[0].TypeUsage.EdmType).ElementType == entity && IsCascadeDeletePrincipal(a.AssociationEndMembers[1]) ||
                        ((RefType)a.AssociationEndMembers[1].TypeUsage.EdmType).ElementType == entity && IsCascadeDeletePrincipal(a.AssociationEndMembers[0])).Any();
            }

            /// <summary>
            /// True if the source end of the specified navigation property is the principal in an identifying relationship.
            /// or if the source end has cascade delete defined.
            /// </summary>
            public bool IsCascadeDeletePrincipal(NavigationProperty navProperty)
            {
                if (navProperty == null)
                {
                    throw new ArgumentNullException("navProperty");
                }

                return IsCascadeDeletePrincipal((AssociationEndMember)navProperty.FromEndMember);
            }

            /// <summary>
            /// True if the specified association end is the principal in an identifying relationship.
            /// or if the association end has cascade delete defined.
            /// </summary>
            public bool IsCascadeDeletePrincipal(AssociationEndMember associationEnd)
            {
                if (associationEnd == null)
                {
                    throw new ArgumentNullException("associationEnd");
                }

                return associationEnd.DeleteBehavior == OperationAction.Cascade || IsPrincipalEndOfIdentifyingRelationship(associationEnd);
            }

            /// <summary>
            /// True if the specified association end is the principal end in an identifying relationship.
            /// In order to be an identifying relationship, the association must have a referential constraint where all of the dependent properties are part of the dependent type's primary key.
            /// </summary>
            public bool IsPrincipalEndOfIdentifyingRelationship(AssociationEndMember associationEnd)
            {
                if (associationEnd == null)
                {
                    throw new ArgumentNullException("associationEnd");
                }

                ReferentialConstraint refConstraint = ((AssociationType)associationEnd.DeclaringType).ReferentialConstraints.Where(rc => rc.FromRole == associationEnd).SingleOrDefault();
                if (refConstraint != null)
                {
                    EntityType entity = refConstraint.ToRole.GetEntityType();
                    return !refConstraint.ToProperties.Where(tp => !entity.KeyMembers.Contains(tp)).Any();
                }
                return false;
            }

            /// <summary>
            /// True if the specified association type is an identifying relationship.
            /// In order to be an identifying relationship, the association must have a referential constraint where all of the dependent properties are part of the dependent type's primary key.
            /// </summary>
            public bool IsIdentifyingRelationship(AssociationType association)
            {
                if (association == null)
                {
                    throw new ArgumentNullException("association");
                }

                return IsPrincipalEndOfIdentifyingRelationship(association.AssociationEndMembers[0]) || IsPrincipalEndOfIdentifyingRelationship(association.AssociationEndMembers[1]);
            }

            /// <summary>
            /// requires: firstType is not null
            /// effects: if secondType is among the base types of the firstType, return true,
            /// otherwise returns false.
            /// when firstType is same as the secondType, return false.
            /// </summary>
            public bool IsSubtypeOf(EdmType firstType, EdmType secondType)
            {
                if (secondType == null)
                {
                    return false;
                }

                // walk up firstType hierarchy list
                for (EdmType t = firstType.BaseType; t != null; t = t.BaseType)
                {
                    if (t == secondType)
                        return true;
                }
                return false;
            }

            /// <summary>
            /// Returns the subtype of the EntityType in the current itemCollection
            /// </summary>
            public IEnumerable<EntityType> GetSubtypesOf(EntityType type, ItemCollection itemCollection, bool includeAbstractTypes)
            {
                if (type != null)
                {
                    IEnumerable<EntityType> typesInCollection = itemCollection.GetItems<EntityType>();
                    foreach (EntityType typeInCollection in typesInCollection)
                    {
                        if (type.Equals(typeInCollection) == false && this.IsSubtypeOf(typeInCollection, type))
                        {
                            if (includeAbstractTypes || !typeInCollection.Abstract)
                            {
                                yield return typeInCollection;
                            }
                        }
                    }
                }
            }

            public static bool TryGetStringMetadataPropertySetting(MetadataItem item, string propertyName, out string value)
            {
                value = null;
                MetadataProperty property = item.MetadataProperties.FirstOrDefault(p => p.Name == propertyName);
                if (property != null)
                {
                    value = (string)property.Value;
                }
                return value != null;
            }
        }

        /// <summary>
        /// Responsible for loading an EdmItemCollection from a .edmx file or .csdl files
        /// </summary>
        public class MetadataLoader
        {
            private readonly DynamicTextTransformation _textTransformation;

            /// <summary>
            /// Initializes an MetadataLoader Instance  with the
            /// TextTransformation (T4 generated class) that is currently running
            /// </summary>
            public MetadataLoader(object textTransformation)
            {
                if (textTransformation == null)
                {
                    throw new ArgumentNullException("textTransformation");
                }

                _textTransformation = DynamicTextTransformation.Create(textTransformation);
            }

            /// <summary>
            /// Load the metadata for Edm, Store, and Mapping collections and register them
            /// with a new MetadataWorkspace, returns false if any of the parts can't be
            /// created, some of the ItemCollections may be registered and usable even if false is
            /// returned
            /// </summary>
            public bool TryLoadAllMetadata(string inputFile, out MetadataWorkspace metadataWorkspace)
            {
                metadataWorkspace = new MetadataWorkspace();

                EdmItemCollection edmItemCollection = CreateEdmItemCollection(inputFile);
                metadataWorkspace.RegisterItemCollection(edmItemCollection);

                StoreItemCollection storeItemCollection = null;
                if (TryCreateStoreItemCollection(inputFile, out storeItemCollection))
                {
                    StorageMappingItemCollection storageMappingItemCollection = null;
                    if (TryCreateStorageMappingItemCollection(inputFile, edmItemCollection, storeItemCollection, out storageMappingItemCollection))
                    {
                        metadataWorkspace.RegisterItemCollection(storeItemCollection);
                        metadataWorkspace.RegisterItemCollection(storageMappingItemCollection);
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Create an EdmItemCollection loaded with the metadata provided
            /// </summary>
            public EdmItemCollection CreateEdmItemCollection(string sourcePath, params string[] referenceSchemas)
            {
                EdmItemCollection edmItemCollection;
                if (TryCreateEdmItemCollection(sourcePath, referenceSchemas, out edmItemCollection))
                {
                    return edmItemCollection;
                }

                return new EdmItemCollection();
            }

            /// <summary>
            /// Attempts to create a EdmItemCollection from the specified metadata file
            /// </summary>
            public bool TryCreateEdmItemCollection(string sourcePath, out EdmItemCollection edmItemCollection)
            {
                return TryCreateEdmItemCollection(sourcePath, null, out edmItemCollection);
            }

            /// <summary>
            /// Attempts to create a EdmItemCollection from the specified metadata file
            /// </summary>
            public bool TryCreateEdmItemCollection(string sourcePath, string[] referenceSchemas, out EdmItemCollection edmItemCollection)
            {
                edmItemCollection = null;

                if (!ValidateInputPath(sourcePath, _textTransformation))
                {
                    return false;
                }

                if (referenceSchemas == null)
                {
                    referenceSchemas = new string[0];
                }

                ItemCollection itemCollection = null;
                sourcePath = _textTransformation.Host.ResolvePath(sourcePath);
                EdmItemCollectionBuilder collectionBuilder = new EdmItemCollectionBuilder(_textTransformation, referenceSchemas.Select(s => _textTransformation.Host.ResolvePath(s)).Where(s => s != sourcePath));
                if (collectionBuilder.TryCreateItemCollection(sourcePath, out itemCollection))
                {
                    edmItemCollection = (EdmItemCollection)itemCollection;
                }

                return edmItemCollection != null;
            }

            /// <summary>
            /// Attempts to create a StoreItemCollection from the specified metadata file
            /// </summary>
            public bool TryCreateStoreItemCollection(string sourcePath, out StoreItemCollection storeItemCollection)
            {
                storeItemCollection = null;

                if (!ValidateInputPath(sourcePath, _textTransformation))
                {
                    return false;
                }

                ItemCollection itemCollection = null;
                StoreItemCollectionBuilder collectionBuilder = new StoreItemCollectionBuilder(_textTransformation);
                if (collectionBuilder.TryCreateItemCollection(_textTransformation.Host.ResolvePath(sourcePath), out itemCollection))
                {
                    storeItemCollection = (StoreItemCollection)itemCollection;
                }
                return storeItemCollection != null;
            }

            /// <summary>
            /// Attempts to create a StorageMappingItemCollection from the specified metadata file, EdmItemCollection, and StoreItemCollection
            /// </summary>
            public bool TryCreateStorageMappingItemCollection(string sourcePath, EdmItemCollection edmItemCollection, StoreItemCollection storeItemCollection, out StorageMappingItemCollection storageMappingItemCollection)
            {
                storageMappingItemCollection = null;

                if (!ValidateInputPath(sourcePath, _textTransformation))
                {
                    return false;
                }

                if (edmItemCollection == null)
                {
                    throw new ArgumentNullException("edmItemCollection");
                }

                if (storeItemCollection == null)
                {
                    throw new ArgumentNullException("storeItemCollection");
                }

                ItemCollection itemCollection = null;
                StorageMappingItemCollectionBuilder collectionBuilder = new StorageMappingItemCollectionBuilder(_textTransformation, edmItemCollection, storeItemCollection);
                if (collectionBuilder.TryCreateItemCollection(_textTransformation.Host.ResolvePath(sourcePath), out itemCollection))
                {
                    storageMappingItemCollection = (StorageMappingItemCollection)itemCollection;
                }
                return storageMappingItemCollection != null;
            }

            /// <summary>
            /// Gets the Model Namespace from the provided schema file.
            /// </summary>
            public string GetModelNamespace(string sourcePath)
            {
                if (!ValidateInputPath(sourcePath, _textTransformation))
                {
                    return String.Empty;
                }

                EdmItemCollectionBuilder builder = new EdmItemCollectionBuilder(_textTransformation);
                XElement model;
                if (builder.TryLoadRootElement(_textTransformation.Host.ResolvePath(sourcePath), out model))
                {
                    XAttribute attribute = model.Attribute("Namespace");
                    if (attribute != null)
                    {
                        return attribute.Value;
                    }
                }

                return String.Empty;
            }

            /// <summary>
            /// Returns true if the specified file path is valid
            /// </summary>
            private static bool ValidateInputPath(string sourcePath, DynamicTextTransformation textTransformation)
            {
                if (String.IsNullOrEmpty(sourcePath))
                {
                    throw new ArgumentException("sourcePath");
                }

                if (sourcePath == "$edmxInputFile$")
                {
                   // textTransformation.Errors.Add(new CompilerError(textTransformation.Host.TemplateFile ?? CodeGenerationTools.GetResourceString("Template_CurrentlyRunningTemplate"), 0, 0, string.Empty,
                     //   CodeGenerationTools.GetResourceString("Template_ReplaceVsItemTemplateToken")));
                    return false;
                }

                return true;
            }

            /// <summary>
            /// Base class for ItemCollectionBuilder classes that
            /// loads the specific types of metadata
            /// </summary>
            private abstract class ItemCollectionBuilder
            {
                private readonly DynamicTextTransformation _textTransformation;
                private readonly string _fileExtension;
                private readonly string _edmxSectionName;
                private readonly string _rootElementName;

                /// <summary>
                /// FileExtension for individual (non-edmx) metadata file for this
                /// specific ItemCollection type
                /// </summary>
                public string FileExtension
                {
                    get { return _fileExtension; }
                }

                /// <summary>
                /// The name of the XmlElement in the .edmx <Runtime> element
                /// to find this ItemCollection's metadata
                /// </summary>
                public string EdmxSectionName
                {
                    get { return _edmxSectionName; }
                }

                /// <summary>
                /// The name of the root element of this ItemCollection's metadata
                /// </summary>
                public string RootElementName
                {
                    get { return _rootElementName; }
                }

                /// <summary>
                /// Method to build the appropriate ItemCollection
                /// </summary>
                protected abstract ItemCollection CreateItemCollection(IEnumerable<XmlReader> readers, out IList<EdmSchemaError> errors);

                /// <summary>
                /// Ctor to setup the ItemCollectionBuilder members
                /// </summary>
                protected ItemCollectionBuilder(DynamicTextTransformation textTransformation, string fileExtension, string edmxSectionName, string rootElementName)
                {
                    _textTransformation = textTransformation;
                    _fileExtension = fileExtension;
                    _edmxSectionName = edmxSectionName;
                    _rootElementName = rootElementName;
                }

                /// <summary>
                /// Selects a namespace from the supplied constants.
                /// </summary>
                protected abstract string GetNamespace(SchemaConstants constants);

                /// <summary>
                /// Try to create an ItemCollection loaded with the metadata provided
                /// </summary>
                public bool TryCreateItemCollection(string sourcePath, out ItemCollection itemCollection)
                {
                    itemCollection = null;

                    if (!ValidateInputPath(sourcePath, _textTransformation))
                    {
                        return false;
                    }

                    XElement schemaElement = null;
                    if (TryLoadRootElement(sourcePath, out schemaElement))
                    {
                        List<XmlReader> readers = new List<XmlReader>();
                        try
                        {
                            readers.Add(schemaElement.CreateReader());
                            IList<EdmSchemaError> errors = null;

                            ItemCollection tempItemCollection = CreateItemCollection(readers, out errors);
                            if (ProcessErrors(errors, sourcePath))
                            {
                                return false;
                            }

                            itemCollection = tempItemCollection;
                            return true;
                        }
                        finally
                        {
                            foreach (XmlReader reader in readers)
                            {
                                ((IDisposable)reader).Dispose();
                            }
                        }
                    }

                    return false;
                }

                /// <summary>
                /// Tries to load the root element from the metadata file provided
                /// </summary>
                public bool TryLoadRootElement(string sourcePath, out XElement schemaElement)
                {
                    schemaElement = null;
                    string extension = Path.GetExtension(sourcePath);
                    if (extension.Equals(".edmx", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return TryLoadRootElementFromEdmx(sourcePath, out schemaElement);
                    }
                    else if (extension.Equals(FileExtension, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // load from single metadata file (.csdl, .ssdl, or .msl)
                        schemaElement = XElement.Load(sourcePath, LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
                        return true;
                    }

                    return false;
                }

                /// <summary>
                /// Tries to load the root element from the provided edmxDocument
                /// </summary>
                private bool TryLoadRootElementFromEdmx(XElement edmxDocument, SchemaConstants schemaConstants, string sectionName, string rootElementName, out XElement rootElement)
                {
                    rootElement = null;

                    XNamespace edmxNs = schemaConstants.EdmxNamespace;
                    XNamespace sectionNs = GetNamespace(schemaConstants);

                    XElement runtime = edmxDocument.Element(edmxNs + "Runtime");
                    if (runtime == null)
                    {
                        return false;
                    }

                    XElement section = runtime.Element(edmxNs + sectionName);
                    if (section == null)
                    {
                        return false;
                    }

                    string templateVersion;

                    if (!TemplateMetadata.TryGetValue(MetadataConstants.TT_TEMPLATE_VERSION, out templateVersion))
                    {
                        templateVersion = MetadataConstants.DEFAULT_TEMPLATE_VERSION;
                    }

                    if (schemaConstants.MinimumTemplateVersion > new Version(templateVersion))
                    {
                        //_textTransformation.Errors.Add(new CompilerError(
                          //  _textTransformation.Host.TemplateFile ?? CodeGenerationTools.GetResourceString("Template_CurrentlyRunningTemplate"), 0, 0, string.Empty,
                             //   CodeGenerationTools.GetResourceString("Template_UnsupportedSchema")) { IsWarning = true });
                    }

                    rootElement = section.Element(sectionNs + rootElementName);
                    return rootElement != null;
                }

                /// <summary>
                /// Tries to load the root element from the provided .edmx metadata file
                /// </summary>
                private bool TryLoadRootElementFromEdmx(string edmxPath, out XElement rootElement)
                {
                    rootElement = null;

                    XElement element = XElement.Load(edmxPath, LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);

                    return TryLoadRootElementFromEdmx(element, MetadataConstants.V3_SCHEMA_CONSTANTS, EdmxSectionName, RootElementName, out rootElement)
                        || TryLoadRootElementFromEdmx(element, MetadataConstants.V2_SCHEMA_CONSTANTS, EdmxSectionName, RootElementName, out rootElement)
                        || TryLoadRootElementFromEdmx(element, MetadataConstants.V1_SCHEMA_CONSTANTS, EdmxSectionName, RootElementName, out rootElement);
                }

                /// <summary>
                /// Takes an Enumerable of EdmSchemaErrors, and adds them
                /// to the errors collection of the template class
                /// </summary>
                private bool ProcessErrors(IEnumerable<EdmSchemaError> errors, string sourceFilePath)
                {
                    bool foundErrors = false;
                    foreach (EdmSchemaError error in errors)
                    {
                        CompilerError newError = new CompilerError(error.SchemaLocation, error.Line, error.Column,
                                                         error.ErrorCode.ToString(CultureInfo.InvariantCulture),
                                                         error.Message);
                        newError.IsWarning = error.Severity == EdmSchemaErrorSeverity.Warning;
                        foundErrors |= error.Severity == EdmSchemaErrorSeverity.Error;
                        if (error.SchemaLocation == null)
                        {
                            newError.FileName = sourceFilePath;
                        }
                        _textTransformation.Errors.Add(newError);
                    }

                    return foundErrors;
                }
            }

            /// <summary>
            /// Builder class for creating a StorageMappingItemCollection
            /// </summary>
            private class StorageMappingItemCollectionBuilder : ItemCollectionBuilder
            {
                private readonly EdmItemCollection _edmItemCollection;
                private readonly StoreItemCollection _storeItemCollection;

                public StorageMappingItemCollectionBuilder(DynamicTextTransformation textTransformation, EdmItemCollection edmItemCollection, StoreItemCollection storeItemCollection)
                    : base(textTransformation, MetadataConstants.MSL_EXTENSION, MetadataConstants.MSL_EDMX_SECTION_NAME, MetadataConstants.MSL_ROOT_ELEMENT_NAME)
                {
                    _edmItemCollection = edmItemCollection;
                    _storeItemCollection = storeItemCollection;
                }

                protected override ItemCollection CreateItemCollection(IEnumerable<XmlReader> readers, out IList<EdmSchemaError> errors)
                {
                    return MetadataItemCollectionFactory.CreateStorageMappingItemCollection(_edmItemCollection, _storeItemCollection, readers, out errors);
                }

                /// <summary>
                /// Selects a namespace from the supplied constants.
                /// </summary>
                protected override string GetNamespace(SchemaConstants constants)
                {
                    return constants.MslNamespace;
                }
            }

            /// <summary>
            /// Builder class for creating a StoreItemCollection
            /// </summary>
            private class StoreItemCollectionBuilder : ItemCollectionBuilder
            {
                public StoreItemCollectionBuilder(DynamicTextTransformation textTransformation)
                    : base(textTransformation, MetadataConstants.SSDL_EXTENSION, MetadataConstants.SSDL_EDMX_SECTION_NAME, MetadataConstants.SSDL_ROOT_ELEMENT_NAME)
                {
                }

                protected override ItemCollection CreateItemCollection(IEnumerable<XmlReader> readers, out IList<EdmSchemaError> errors)
                {
                    return MetadataItemCollectionFactory.CreateStoreItemCollection(readers, out errors);
                }

                /// <summary>
                /// Selects a namespace from the supplied constants.
                /// </summary>
                protected override string GetNamespace(SchemaConstants constants)
                {
                    return constants.SsdlNamespace;
                }
            }

            /// <summary>
            /// Builder class for creating a EdmItemCollection
            /// </summary>
            private class EdmItemCollectionBuilder : ItemCollectionBuilder
            {
                private List<string> _referenceSchemas = new List<string>();

                public EdmItemCollectionBuilder(DynamicTextTransformation textTransformation)
                    : base(textTransformation, MetadataConstants.CSDL_EXTENSION, MetadataConstants.CSDL_EDMX_SECTION_NAME, MetadataConstants.CSDL_ROOT_ELEMENT_NAME)
                {
                }

                public EdmItemCollectionBuilder(DynamicTextTransformation textTransformation, IEnumerable<string> referenceSchemas)
                    : this(textTransformation)
                {
                    _referenceSchemas.AddRange(referenceSchemas);
                }

                protected override ItemCollection CreateItemCollection(IEnumerable<XmlReader> readers, out IList<EdmSchemaError> errors)
                {
                    List<XmlReader> ownedReaders = new List<XmlReader>();
                    List<XmlReader> allReaders = new List<XmlReader>();
                    try
                    {
                        allReaders.AddRange(readers);
                        foreach (string path in _referenceSchemas.Distinct())
                        {
                            XElement reference;
                            if (TryLoadRootElement(path, out reference))
                            {
                                XmlReader reader = reference.CreateReader();
                                allReaders.Add(reader);
                                ownedReaders.Add(reader);
                            }
                        }

                        return MetadataItemCollectionFactory.CreateEdmItemCollection(allReaders, out errors);
                    }
                    finally
                    {
                        foreach (XmlReader reader in ownedReaders)
                        {
                            ((IDisposable)reader).Dispose();
                        }
                    }
                }

                /// <summary>
                /// Selects a namespace from the supplied constants.
                /// </summary>
                protected override string GetNamespace(SchemaConstants constants)
                {
                    return constants.CsdlNamespace;
                }
            }
        }

        /// <summary>
        /// Responsible for encapsulating the retrieval and translation of the CodeGeneration
        /// annotations in the EntityFramework Metadata to a form that is useful in code generation.
        /// </summary>
        public static class Accessibility
        {
            private const string GETTER_ACCESS = "http://schemas.microsoft.com/ado/2006/04/codegeneration:GetterAccess";
            private const string SETTER_ACCESS = "http://schemas.microsoft.com/ado/2006/04/codegeneration:SetterAccess";
            private const string TYPE_ACCESS = "http://schemas.microsoft.com/ado/2006/04/codegeneration:TypeAccess";
            private const string METHOD_ACCESS = "http://schemas.microsoft.com/ado/2006/04/codegeneration:MethodAccess";
            private const string ACCESS_PROTECTED = "Protected";
            private const string ACCESS_INTERNAL = "Internal";
            private const string ACCESS_PRIVATE = "Private";
            private static readonly Dictionary<string, int> AccessibilityRankIdLookup = new Dictionary<string, int>
        {
            { "private", 1},
            { "internal", 2},
            { "protected", 3},
            { "public", 4},
        };

            /// <summary>
            /// Gets the accessibility that should be applied to a type being generated from the provided GlobalItem.
            ///
            /// defaults to public if no annotation is found.
            /// </summary>
            public static string ForType(GlobalItem item)
            {
                if (item == null)
                {
                    return null;
                }

                return GetAccessibility(item, TYPE_ACCESS);
            }

            /// <summary>
            /// Gets the accessibility that should be applied at the property level for a property being
            /// generated from the provided EdmMember.
            ///
            /// defaults to public if no annotation is found.
            /// </summary>
            public static string ForProperty(EdmMember member)
            {
                if (member == null)
                {
                    return null;
                }

                string getterAccess, setterAccess, propertyAccess;
                CalculatePropertyAccessibility(member, out propertyAccess, out getterAccess, out setterAccess);
                return propertyAccess;
            }

            /// <summary>
            /// Gets the accessibility that should be applied at the property level for a Read-Only property being
            /// generated from the provided EdmMember.
            ///
            /// defaults to public if no annotation is found.
            /// </summary>
            public static string ForReadOnlyProperty(EdmMember member)
            {
                if (member == null)
                {
                    return null;
                }

                return GetAccessibility(member, GETTER_ACCESS);
            }

            /// <summary>
            /// Gets the accessibility that should be applied at the property level for a property being
            /// generated from the provided EntitySet.
            ///
            /// defaults to public if no annotation is found.
            /// </summary>
            public static string ForReadOnlyProperty(EntitySet set)
            {
                if (set == null)
                {
                    return null;
                }

                return GetAccessibility(set, GETTER_ACCESS);
            }

            /// <summary>
            /// Gets the accessibility that should be applied at the property level for a Write-Only property being
            /// generated from the provided EdmMember.
            ///
            /// defaults to public if no annotation is found.
            /// </summary>
            public static string ForWriteOnlyProperty(EdmMember member)
            {
                if (member == null)
                {
                    return null;
                }

                return GetAccessibility(member, SETTER_ACCESS);
            }


            /// <summary>
            /// Gets the accessibility that should be applied at the get level for a property being
            /// generated from the provided EdmMember.
            ///
            /// defaults to empty if no annotation is found or the accessibility is the same as the property level.
            /// </summary>
            public static string ForGetter(EdmMember member)
            {
                if (member == null)
                {
                    return null;
                }

                string getterAccess, setterAccess, propertyAccess;
                CalculatePropertyAccessibility(member, out propertyAccess, out getterAccess, out setterAccess);
                return getterAccess;
            }

            /// <summary>
            /// Gets the accessibility that should be applied at the set level for a property being
            /// generated from the provided EdmMember.
            ///
            /// defaults to empty if no annotation is found or the accessibility is the same as the property level.
            /// </summary>
            public static string ForSetter(EdmMember member)
            {
                if (member == null)
                {
                    return null;
                }

                string getterAccess, setterAccess, propertyAccess;
                CalculatePropertyAccessibility(member, out propertyAccess, out getterAccess, out setterAccess);
                return setterAccess;
            }

            /// <summary>
            /// Gets the accessibility that should be applied to a method being generated from the provided EdmFunction.
            ///
            /// defaults to public if no annotation is found.
            /// </summary>
            public static string ForMethod(EdmFunction function)
            {
                if (function == null)
                {
                    return null;
                }

                return GetAccessibility(function, METHOD_ACCESS);
            }

            private static void CalculatePropertyAccessibility(MetadataItem item,
                out string propertyAccessibility,
                out string getterAccessibility,
                out string setterAccessibility)
            {
                getterAccessibility = GetAccessibility(item, GETTER_ACCESS);
                int getterRank = AccessibilityRankIdLookup[getterAccessibility];

                setterAccessibility = GetAccessibility(item, SETTER_ACCESS);
                int setterRank = AccessibilityRankIdLookup[setterAccessibility];

                int propertyRank = Math.Max(getterRank, setterRank);
                if (setterRank == propertyRank)
                {
                    setterAccessibility = String.Empty;
                }

                if (getterRank == propertyRank)
                {
                    getterAccessibility = String.Empty;
                }

                propertyAccessibility = AccessibilityRankIdLookup.Where(v => v.Value == propertyRank).Select(v => v.Key).Single();
            }

            private static string GetAccessibility(MetadataItem item, string name)
            {
                string accessibility;
                if (MetadataTools.TryGetStringMetadataPropertySetting(item, name, out accessibility))
                {
                    return TranslateUserAccessibilityToCSharpAccessibility(accessibility);
                }

                return "public";
            }

            private static string TranslateUserAccessibilityToCSharpAccessibility(string userAccessibility)
            {
                if (userAccessibility == ACCESS_PROTECTED)
                {
                    return "protected";
                }
                else if (userAccessibility == ACCESS_INTERNAL)
                {
                    return "internal";
                }
                else if (userAccessibility == ACCESS_PRIVATE)
                {
                    return "private";
                }
                else
                {
                    // default to public
                    return "public";
                }
            }
        }

        /// <summary>
        /// Responsible for creating source code regions in code when the loop inside
        /// actually produces something.
        /// </summary>
        public class CodeRegion
        {
            private const int STANDARD_INDENT_LENGTH = 4;

            private readonly DynamicTextTransformation _textTransformation;
            private int _beforeRegionLength;
            private int _emptyRegionLength;
            private int _regionIndentLevel = -1;

            /// <summary>
            /// Initializes an CodeRegion instance with the
            /// TextTransformation (T4 generated class) that is currently running
            /// </summary>
            public CodeRegion(object textTransformation)
            {
                if (textTransformation == null)
                {
                    throw new ArgumentNullException("textTransformation");
                }

                _textTransformation = DynamicTextTransformation.Create(textTransformation);
            }

            /// <summary>
            /// Initializes an CodeRegion instance with the
            /// TextTransformation (T4 generated class) that is currently running,
            /// and the indent level to start the first region at.
            /// </summary>
            public CodeRegion(object textTransformation, int firstIndentLevel)
                : this(textTransformation)
            {
                if (firstIndentLevel < 0)
                {
                    throw new ArgumentException("firstIndentLevel");
                }

                _regionIndentLevel = firstIndentLevel - 1;
            }

            /// <summary>
            /// Starts the begining of a region
            /// </summary>
            public void Begin(string regionName)
            {
                if (regionName == null)
                {
                    throw new ArgumentNullException("regionName");
                }

                Begin(regionName, 1);
            }

            /// <summary>
            /// Start the begining of a region, indented
            /// the numbers of levels specified
            /// </summary>
            public void Begin(string regionName, int levelsToIncreaseIndent)
            {
                if (regionName == null)
                {
                    throw new ArgumentNullException("regionName");
                }

                _beforeRegionLength = _textTransformation.GenerationEnvironment.Length;
                _regionIndentLevel += levelsToIncreaseIndent;
                _textTransformation.Write(GetIndent(_regionIndentLevel));
                _textTransformation.WriteLine("#region " + regionName);
                _emptyRegionLength = _textTransformation.GenerationEnvironment.Length;
            }

            /// <summary>
            /// Ends a region, or totaly removes it if nothing
            /// was generted since the begining of the region.
            /// </summary>
            public void End()
            {
                End(1);
            }

            /// <summary>
            /// Ends a region, or totaly removes it if nothing
            /// was generted since the begining of the region, also outdents
            /// the number of levels specified.
            /// </summary>
            public void End(int levelsToDecrease)
            {
                int indentLevel = _regionIndentLevel;
                _regionIndentLevel -= levelsToDecrease;

                if (_emptyRegionLength == _textTransformation.GenerationEnvironment.Length)
                    _textTransformation.GenerationEnvironment.Length = _beforeRegionLength;
                else
                {
                    _textTransformation.WriteLine(String.Empty);
                    _textTransformation.Write(GetIndent(indentLevel));
                    _textTransformation.WriteLine("#endregion");
                    _textTransformation.WriteLine(String.Empty);
                }
            }

            /// <summary>
            /// Gets the current indent level that the next end region statement will be written
            /// at
            /// </summary>
            public int CurrentIndentLevel { get { return _regionIndentLevel; } }

            /// <summary>
            /// Get a string of spaces equivelent to the number of indents
            /// desired.
            /// </summary>
            public static string GetIndent(int indentLevel)
            {
                if (indentLevel < 0)
                {
                    throw new ArgumentException("indentLevel");
                }

                return String.Empty.PadLeft(indentLevel * STANDARD_INDENT_LENGTH);
            }
        }


        /// <summary>
        /// Responsible for collecting together the actual method parameters
        /// and the parameters that need to be sent to the Execute method.
        /// </summary>
        public class FunctionImportParameter
        {
            public FunctionParameter Source { get; set; }
            public string RawFunctionParameterName { get; set; }
            public string FunctionParameterName { get; set; }
            public string FunctionParameterType { get; set; }
            public string LocalVariableName { get; set; }
            public string RawClrTypeName { get; set; }
            public string ExecuteParameterName { get; set; }
            public string EsqlParameterName { get; set; }
            public bool NeedsLocalVariable { get; set; }
            public bool IsNullableOfT { get; set; }


            /// <summary>
            /// Creates a set of FunctionImportParameter objects from the parameters passed in.
            /// </summary>
            public static IEnumerable<FunctionImportParameter> Create(IEnumerable<FunctionParameter> parameters, CodeGenerationTools code, MetadataTools ef)
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException("parameters");
                }

                if (code == null)
                {
                    throw new ArgumentNullException("code");
                }

                if (ef == null)
                {
                    throw new ArgumentNullException("ef");
                }

                UniqueIdentifierService unique = new UniqueIdentifierService();
                List<FunctionImportParameter> importParameters = new List<FunctionImportParameter>();
                foreach (FunctionParameter parameter in parameters)
                {
                    FunctionImportParameter importParameter = new FunctionImportParameter();
                    importParameter.Source = parameter;
                    importParameter.RawFunctionParameterName = unique.AdjustIdentifier(code.CamelCase(parameter.Name));
                    importParameter.FunctionParameterName = code.Escape(importParameter.RawFunctionParameterName);
                    if (parameter.Mode == ParameterMode.In)
                    {
                        TypeUsage typeUsage = parameter.TypeUsage;
                        importParameter.NeedsLocalVariable = true;
                        importParameter.FunctionParameterType = code.GetTypeName(typeUsage);
                        importParameter.EsqlParameterName = parameter.Name;
                        Type clrType = ef.UnderlyingClrType(parameter.TypeUsage.EdmType);
                        importParameter.RawClrTypeName = (typeUsage.EdmType is EnumType) ? code.GetTypeName(typeUsage.EdmType) : code.Escape(clrType);
                        importParameter.IsNullableOfT = clrType.IsValueType;
                    }
                    else
                    {
                        importParameter.NeedsLocalVariable = false;
                        importParameter.FunctionParameterType = "ObjectParameter";
                        importParameter.ExecuteParameterName = importParameter.FunctionParameterName;
                    }
                    importParameters.Add(importParameter);
                }

                // we save the local parameter uniquification for a second pass to make the visible parameters
                // as pretty and sensible as possible
                for (int i = 0; i < importParameters.Count; i++)
                {
                    FunctionImportParameter importParameter = importParameters[i];
                    if (importParameter.NeedsLocalVariable)
                    {
                        importParameter.LocalVariableName = unique.AdjustIdentifier(importParameter.RawFunctionParameterName + "Parameter");
                        importParameter.ExecuteParameterName = importParameter.LocalVariableName;
                    }
                }

                return importParameters;
            }

            //
            // Class to create unique variables within the same scope
            //
            private sealed class UniqueIdentifierService
            {
                private readonly HashSet<string> _knownIdentifiers;

                public UniqueIdentifierService()
                {
                    _knownIdentifiers = new HashSet<string>(StringComparer.Ordinal);
                }

                /// <summary>
                /// Given an identifier, makes it unique within the scope by adding
                /// a suffix (1, 2, 3, ...), and returns the adjusted identifier.
                /// </summary>
                public string AdjustIdentifier(string identifier)
                {
                    // find a unique name by adding suffix as necessary
                    int numberOfConflicts = 0;
                    string adjustedIdentifier = identifier;

                    while (!_knownIdentifiers.Add(adjustedIdentifier))
                    {
                        ++numberOfConflicts;
                        adjustedIdentifier = identifier + numberOfConflicts.ToString(CultureInfo.InvariantCulture);
                    }

                    return adjustedIdentifier;
                }
            }
        }

        /// <summary>
        /// Responsible for marking the various sections of the generation,
        /// so they can be split up into separate files
        /// </summary>
        public class EntityFrameworkTemplateFileManager
        {
            /// <summary>
            /// Creates the VsEntityFrameworkTemplateFileManager if VS is detected, otherwise
            /// creates the file system version.
            /// </summary>
            public static EntityFrameworkTemplateFileManager Create(object textTransformation)
            {
                DynamicTextTransformation transformation = DynamicTextTransformation.Create(textTransformation);
                IDynamicHost host = transformation.Host;

#if !PREPROCESSED_TEMPLATE
                var hostServiceProvider = host.AsIServiceProvider();

                if (hostServiceProvider != null)
                {
                    //EnvDTE.DTE dte = (EnvDTE.DTE)hostServiceProvider.GetService(typeof(EnvDTE.DTE));

                   // if (dte != null)
                  //  {
                      //  return new VsEntityFrameworkTemplateFileManager(transformation);
                   // }
                }
#endif
                return new EntityFrameworkTemplateFileManager(transformation);
            }

            private sealed class Block
            {
                public String Name;
                public int Start, Length;
            }

            private readonly List<Block> files = new List<Block>();
            private readonly Block footer = new Block();
            private readonly Block header = new Block();
            private readonly DynamicTextTransformation _textTransformation;

            // reference to the GenerationEnvironment StringBuilder on the
            // TextTransformation object
            private readonly StringBuilder _generationEnvironment;

            private Block currentBlock;

            /// <summary>
            /// Initializes an EntityFrameworkTemplateFileManager Instance  with the
            /// TextTransformation (T4 generated class) that is currently running
            /// </summary>
            private EntityFrameworkTemplateFileManager(object textTransformation)
            {
                if (textTransformation == null)
                {
                    throw new ArgumentNullException("textTransformation");
                }

                _textTransformation = DynamicTextTransformation.Create(textTransformation);
                _generationEnvironment = _textTransformation.GenerationEnvironment;
            }

            /// <summary>
            /// Marks the end of the last file if there was one, and starts a new
            /// and marks this point in generation as a new file.
            /// </summary>
            public void StartNewFile(string name)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                CurrentBlock = new Block { Name = name };
            }

            public void StartFooter()
            {
                CurrentBlock = footer;
            }

            public void StartHeader()
            {
                CurrentBlock = header;
            }

            public void EndBlock()
            {
                if (CurrentBlock == null)
                {
                    return;
                }

                CurrentBlock.Length = _generationEnvironment.Length - CurrentBlock.Start;

                if (CurrentBlock != header && CurrentBlock != footer)
                {
                    files.Add(CurrentBlock);
                }

                currentBlock = null;
            }

            /// <summary>
            /// Produce the template output files.
            /// </summary>
            public virtual IEnumerable<string> Process(bool split)
            {
                var generatedFileNames = new List<string>();

                if (split)
                {
                    EndBlock();

                    var headerText = _generationEnvironment.ToString(header.Start, header.Length);
                    var footerText = _generationEnvironment.ToString(footer.Start, footer.Length);
                    var outputPath = Path.GetDirectoryName(_textTransformation.Host.TemplateFile);

                    files.Reverse();

                    foreach (var block in files)
                    {
                        var fileName = Path.Combine(outputPath, block.Name);
                        var content = headerText + _generationEnvironment.ToString(block.Start, block.Length) + footerText;

                        generatedFileNames.Add(fileName);
                        CreateFile(fileName, content);
                        _generationEnvironment.Remove(block.Start, block.Length);
                    }
                }

                return generatedFileNames;
            }

            protected virtual void CreateFile(string fileName, string content)
            {
                if (IsFileContentDifferent(fileName, content))
                {
                    File.WriteAllText(fileName, content);
                }
            }

            protected bool IsFileContentDifferent(String fileName, string newContent)
            {
                return !(File.Exists(fileName) && File.ReadAllText(fileName) == newContent);
            }

            private Block CurrentBlock
            {
                get { return currentBlock; }
                set
                {
                    if (CurrentBlock != null)
                    {
                        EndBlock();
                    }

                    if (value != null)
                    {
                        value.Start = _generationEnvironment.Length;
                    }

                    currentBlock = value;
                }
            }
        }

        /// <summary>
        /// Responsible creating an instance that can be passed
        /// to helper classes that need to access the TextTransformation
        /// members.  It accesses member by name and signature rather than
        /// by type.  This is necessary when the
        /// template is being used in Preprocessed mode
        /// and there is no common known type that can be
        /// passed instead
        /// </summary>
        public class DynamicTextTransformation
        {
            private object _instance;
            IDynamicHost _dynamicHost;

            private readonly MethodInfo _write;
            private readonly MethodInfo _writeLine;
            private readonly PropertyInfo _generationEnvironment;
            private readonly PropertyInfo _errors;
            private readonly PropertyInfo _host;

            /// <summary>
            /// Creates an instance of the DynamicTextTransformation class around the passed in
            /// TextTransformation shapped instance passed in, or if the passed in instance
            /// already is a DynamicTextTransformation, it casts it and sends it back.
            /// </summary>
            public static DynamicTextTransformation Create(object instance)
            {
                if (instance == null)
                {
                    throw new ArgumentNullException("instance");
                }

                DynamicTextTransformation textTransformation = instance as DynamicTextTransformation;
                if (textTransformation != null)
                {
                    return textTransformation;
                }

                return new DynamicTextTransformation(instance);
            }

            private DynamicTextTransformation(object instance)
            {
                _instance = instance;
                Type type = _instance.GetType();
                _write = type.GetMethod("Write", new Type[] { typeof(string) });
                _writeLine = type.GetMethod("WriteLine", new Type[] { typeof(string) });
                _generationEnvironment = type.GetProperty("GenerationEnvironment", BindingFlags.Instance | BindingFlags.NonPublic);
                _host = type.GetProperty("Host");
                _errors = type.GetProperty("Errors");
            }

            /// <summary>
            /// Gets the value of the wrapped TextTranformation instance's GenerationEnvironment property
            /// </summary>
            public StringBuilder GenerationEnvironment { get { return (StringBuilder)_generationEnvironment.GetValue(_instance, null); } }

            /// <summary>
            /// Gets the value of the wrapped TextTranformation instance's Errors property
            /// </summary>
            public System.CodeDom.Compiler.CompilerErrorCollection Errors { get { return (System.CodeDom.Compiler.CompilerErrorCollection)_errors.GetValue(_instance, null); } }

            /// <summary>
            /// Calls the wrapped TextTranformation instance's Write method.
            /// </summary>
            public void Write(string text)
            {
                _write.Invoke(_instance, new object[] { text });
            }

            /// <summary>
            /// Calls the wrapped TextTranformation instance's WriteLine method.
            /// </summary>
            public void WriteLine(string text)
            {
                _writeLine.Invoke(_instance, new object[] { text });
            }

            /// <summary>
            /// Gets the value of the wrapped TextTranformation instance's Host property
            /// if available (shows up when hostspecific is set to true in the template directive) and returns
            /// the appropriate implementation of IDynamicHost
            /// </summary>
            public IDynamicHost Host
            {
                get
                {
                    if (_dynamicHost == null)
                    {
                        if (_host == null)
                        {
                            _dynamicHost = new NullHost();
                        }
                        else
                        {
                            _dynamicHost = new DynamicHost(_host.GetValue(_instance, null));
                        }
                    }
                    return _dynamicHost;
                }
            }
        }


        /// <summary>
        /// Reponsible for abstracting the use of Host between times
        /// when it is available and not
        /// </summary>
        public interface IDynamicHost
        {
            /// <summary>
            /// An abstracted call to Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost ResolveParameterValue
            /// </summary>
            string ResolveParameterValue(string id, string name, string otherName);

            /// <summary>
            /// An abstracted call to Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost ResolvePath
            /// </summary>
            string ResolvePath(string path);

            /// <summary>
            /// An abstracted call to Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost TemplateFile
            /// </summary>
            string TemplateFile { get; }

            /// <summary>
            /// Returns the Host instance cast as an IServiceProvider
            /// </summary>
            IServiceProvider AsIServiceProvider();
        }

        /// <summary>
        /// Reponsible for implementing the IDynamicHost as a dynamic
        /// shape wrapper over the Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost interface
        /// rather than type dependent wrapper.  We don't use the
        /// interface type so that the code can be run in preprocessed mode
        /// on a .net framework only installed machine.
        /// </summary>
        public class DynamicHost : IDynamicHost
        {
            private readonly object _instance;
            private readonly MethodInfo _resolveParameterValue;
            private readonly MethodInfo _resolvePath;
            private readonly PropertyInfo _templateFile;

            /// <summary>
            /// Creates an instance of the DynamicHost class around the passed in
            /// Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost shapped instance passed in.
            /// </summary>
            public DynamicHost(object instance)
            {
                _instance = instance;
                Type type = _instance.GetType();
                _resolveParameterValue = type.GetMethod("ResolveParameterValue", new Type[] { typeof(string), typeof(string), typeof(string) });
                _resolvePath = type.GetMethod("ResolvePath", new Type[] { typeof(string) });
                _templateFile = type.GetProperty("TemplateFile");

            }

            /// <summary>
            /// A call to Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost ResolveParameterValue
            /// </summary>
            public string ResolveParameterValue(string id, string name, string otherName)
            {
                return (string)_resolveParameterValue.Invoke(_instance, new object[] { id, name, otherName });
            }

            /// <summary>
            /// A call to Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost ResolvePath
            /// </summary>
            public string ResolvePath(string path)
            {
                return (string)_resolvePath.Invoke(_instance, new object[] { path });
            }

            /// <summary>
            /// A call to Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost TemplateFile
            /// </summary>
            public string TemplateFile
            {
                get
                {
                    return (string)_templateFile.GetValue(_instance, null);
                }
            }

            /// <summary>
            /// Returns the Host instance cast as an IServiceProvider
            /// </summary>
            public IServiceProvider AsIServiceProvider()
            {
                return _instance as IServiceProvider;
            }
        }

        /// <summary>
        /// Reponsible for implementing the IDynamicHost when the
        /// Host property is not available on the TextTemplating type. The Host
        /// property only exists when the hostspecific attribute of the template
        /// directive is set to true.
        /// </summary>
        public class NullHost : IDynamicHost
        {
            /// <summary>
            /// An abstraction of the call to Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost ResolveParameterValue
            /// that simply retuns null.
            /// </summary>
            public string ResolveParameterValue(string id, string name, string otherName)
            {
                return null;
            }

            /// <summary>
            /// An abstraction of the call to Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost ResolvePath
            /// that simply retuns the path passed in.
            /// </summary>
            public string ResolvePath(string path)
            {
                return path;
            }

            /// <summary>
            /// An abstraction of the call to Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost TemplateFile
            /// that returns null.
            /// </summary>
            public string TemplateFile
            {
                get
                {
                    return null;
                }
            }

            /// <summary>
            /// Returns null.
            /// </summary>
            public IServiceProvider AsIServiceProvider()
            {
                return null;
            }
        }

        /// <summary>
        /// Responsible for encapsulating the constants defined in Metadata
        /// </summary>
        public static class MetadataConstants
        {
            public const string CSDL_EXTENSION = ".csdl";

            public const string CSDL_EDMX_SECTION_NAME = "ConceptualModels";
            public const string CSDL_ROOT_ELEMENT_NAME = "Schema";
            public const string EDM_ANNOTATION_09_02 = "http://schemas.microsoft.com/ado/2009/02/edm/annotation";

            public const string SSDL_EXTENSION = ".ssdl";

            public const string SSDL_EDMX_SECTION_NAME = "StorageModels";
            public const string SSDL_ROOT_ELEMENT_NAME = "Schema";

            public const string MSL_EXTENSION = ".msl";

            public const string MSL_EDMX_SECTION_NAME = "Mappings";
            public const string MSL_ROOT_ELEMENT_NAME = "Mapping";

            public const string TT_TEMPLATE_NAME = "TemplateName";
            public const string TT_TEMPLATE_VERSION = "TemplateVersion";
            public const string TT_MINIMUM_ENTITY_FRAMEWORK_VERSION = "MinimumEntityFrameworkVersion";

            public const string DEFAULT_TEMPLATE_VERSION = "4.0";

            public static readonly SchemaConstants V1_SCHEMA_CONSTANTS = new SchemaConstants(
                "http://schemas.microsoft.com/ado/2007/06/edmx",
                "http://schemas.microsoft.com/ado/2006/04/edm",
                "http://schemas.microsoft.com/ado/2006/04/edm/ssdl",
                "urn:schemas-microsoft-com:windows:storage:mapping:CS",
                new Version("3.5"));

            public static readonly SchemaConstants V2_SCHEMA_CONSTANTS = new SchemaConstants(
                "http://schemas.microsoft.com/ado/2008/10/edmx",
                "http://schemas.microsoft.com/ado/2008/09/edm",
                "http://schemas.microsoft.com/ado/2009/02/edm/ssdl",
                "http://schemas.microsoft.com/ado/2008/09/mapping/cs",
                new Version("4.0"));

            public static readonly SchemaConstants V3_SCHEMA_CONSTANTS = new SchemaConstants(
                "http://schemas.microsoft.com/ado/2009/11/edmx",
                "http://schemas.microsoft.com/ado/2009/11/edm",
                "http://schemas.microsoft.com/ado/2009/11/edm/ssdl",
                "http://schemas.microsoft.com/ado/2009/11/mapping/cs",
                new Version("4.5"));
        }

        public struct SchemaConstants
        {
            public SchemaConstants(string edmxNamespace, string csdlNamespace, string ssdlNamespace, string mslNamespace, Version minimumTemplateVersion)
                : this()
            {
                EdmxNamespace = edmxNamespace;
                CsdlNamespace = csdlNamespace;
                SsdlNamespace = ssdlNamespace;
                MslNamespace = mslNamespace;
                MinimumTemplateVersion = minimumTemplateVersion;
            }

            public string EdmxNamespace { get; private set; }
            public string CsdlNamespace { get; private set; }
            public string SsdlNamespace { get; private set; }
            public string MslNamespace { get; private set; }
            public Version MinimumTemplateVersion { get; private set; }
        }
    }
}