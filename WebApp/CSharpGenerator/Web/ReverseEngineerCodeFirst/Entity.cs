﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CSharpGenerator.Web.ReverseEngineerCodeFirst
{
    using System.Linq;
    using System.Data.Metadata.Edm;
    using CSharpGenerator.Code;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class Entity : EntityBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\n\r\nnamespace ");
            
            #line 8 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    public partial class ");
            
            #line 10 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityType.Name));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n");
            
            #line 12 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"

    var code = new CodeGenerator.CodeGenerationTools(this);
    
    var collectionNavigations = entityType.NavigationProperties.Where(
        np => np.DeclaringType == entityType
            && np.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many);

    // Add a ctor to initialize any collections
    if (collectionNavigations.Any())
    {

            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 23 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Escape(entityType)));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 25 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"

        foreach (var navProperty in collectionNavigations)
        {

            
            #line default
            #line hidden
            this.Write("            this.");
            
            #line 29 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Escape(navProperty)));
            
            #line default
            #line hidden
            this.Write(" = new List<");
            
            #line 29 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Escape(navProperty.ToEndMember.GetEntityType())));
            
            #line default
            #line hidden
            this.Write(">();\r\n");
            
            #line 30 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"

        }

            
            #line default
            #line hidden
            this.Write("        }\r\n\r\n");
            
            #line 35 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"

    }
        
    foreach (var property in entityType.Properties)
    {
        var typeUsage = code.Escape(property.TypeUsage);

        // Fix-up spatial types for EF6
        if (EntityFrameworkVersion >= new Version(6, 0)
            && typeUsage.StartsWith("System.Data.Spatial."))
        {
            typeUsage = typeUsage.Replace(
                "System.Data.Spatial.",
                "System.Data.Entity.Spatial.");
        }

            
            #line default
            #line hidden
            this.Write("\r\n        ");
            
            #line 52 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CSharpGenerator.Code.CodeGenerator.Accessibility.ForProperty(property)));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 52 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(typeUsage));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 52 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Escape(property)));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 53 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"

    }

    foreach (var navProperty in entityType.NavigationProperties.Where(np => np.DeclaringType == entityType))
    {
        if (navProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
        {

            
            #line default
            #line hidden
            this.Write("        public virtual ICollection<");
            
            #line 61 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Escape(navProperty.ToEndMember.GetEntityType())));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 61 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Escape(navProperty)));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 62 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"

        }
        else
        {

            
            #line default
            #line hidden
            this.Write("        public virtual ");
            
            #line 67 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Escape(navProperty.ToEndMember.GetEntityType())));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 67 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Escape(navProperty)));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 68 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"

        }
    }

            
            #line default
            #line hidden
            this.Write("    }\r\n}\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        private global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost hostValue;
        /// <summary>
        /// The current host for the text templating engine
        /// </summary>
        public virtual global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost Host
        {
            get
            {
                return this.hostValue;
            }
            set
            {
                this.hostValue = value;
            }
        }
        
        #line 75 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\ReverseEngineerCodeFirst\Entity.tt"

    public EntityType entityType{set;get;}
    public Version EntityFrameworkVersion{set;get;}
    public string Namespace{set;get;}
 
        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class EntityBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
