﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace AppsGenerator.CSharpGenerator.Web.MvcView
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.AspNet.Scaffolding.Core.Metadata;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class Delete : DeleteBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 7 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"
// include file="Imports.include.t4" 
            
            #line default
            #line hidden
            this.Write("@model ");
            
            #line 8 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewDataTypeName));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 9 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

// The following chained if-statement outputs the file header code and markup for a partial view, a view using a layout page, or a regular view.
if(IsPartialView) {

            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 14 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

} else if(IsLayoutPageSelected) {

            
            #line default
            #line hidden
            this.Write("\r\n@{\r\n    ViewBag.Title = \"");
            
            #line 19 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewName +" "+ ViewDataTypeShortName));
            
            #line default
            #line hidden
            this.Write("\";\r\n");
            
            #line 20 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

if (!String.IsNullOrEmpty(LayoutPageFile)) {

            
            #line default
            #line hidden
            this.Write("    Layout = \"");
            
            #line 23 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(LayoutPageFile));
            
            #line default
            #line hidden
            this.Write("\";\r\n");
            
            #line 24 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

}

            
            #line default
            #line hidden
            this.Write("}\r\n\r\n<h2>");
            
            #line 29 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewDataTypeShortName +"-"+ ViewName));
            
            #line default
            #line hidden
            this.Write("</h2>\r\n\r\n");
            
            #line 31 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

} else {

            
            #line default
            #line hidden
            this.Write("\r\n@{\r\n    Layout = null;\r\n}\r\n\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n<head>\r\n    <meta name=" +
                    "\"viewport\" content=\"width=device-width\" />\r\n    <title>");
            
            #line 44 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewName +" "+ ViewDataTypeShortName));
            
            #line default
            #line hidden
            this.Write("</title>\r\n</head>\r\n<body>\r\n");
            
            #line 47 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

    PushIndent("    ");
}

            
            #line default
            #line hidden
            this.Write("<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <hr />\r\n    <dl class=" +
                    "\"dl-horizontal\">\r\n");
            
            #line 55 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

foreach (PropertyMetadata property in ModelMetadata.Properties) {
    if (property.Scaffold && !property.IsPrimaryKey && !property.IsForeignKey) {

            
            #line default
            #line hidden
            
            #line 59 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

        // We do not want to show any association properties for which there is
        // no associated foreign key.
        if (property.IsAssociation && GetRelatedModelMetadata(property) == null) {
            continue;
        }

            
            #line default
            #line hidden
            this.Write("        <dt>\r\n            @Html.DisplayNameFor(model => model.");
            
            #line 67 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValueExpression(property)));
            
            #line default
            #line hidden
            this.Write(")\r\n        </dt>\r\n\r\n        <dd>\r\n            @Html.DisplayFor(model => model.");
            
            #line 71 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValueExpression(property)));
            
            #line default
            #line hidden
            this.Write(")\r\n        </dd>\r\n\r\n");
            
            #line 74 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

    }
}

            
            #line default
            #line hidden
            this.Write(@"    </dl>

    @using (Html.BeginForm()) {
        @Html.AntiForgeryToken()

        <div class=""form-actions no-color"">
            <input type=""submit"" value=""Delete"" class=""btn btn-default"" /> |
            @Html.ActionLink(""Back to List"", ""Index"")
        </div>
    }
</div>
");
            
            #line 89 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

// The following code closes the tag used in the case of a view using a layout page and the body and html tags in the case of a regular view page

            
            #line default
            #line hidden
            
            #line 92 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

if(!IsPartialView && !IsLayoutPageSelected) {
    ClearIndent();

            
            #line default
            #line hidden
            this.Write("</body>\r\n</html>\r\n");
            
            #line 98 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

}

            
            #line default
            #line hidden
            this.Write("\r\n\r\n");
            this.Write("\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 103 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\Delete.tt"

    public string ViewDataTypeName{get;set;}
    public string ViewDataTypeShortName{get;set;}
    public bool IsPartialView{get;set;}
    public bool IsLayoutPageSelected{get;set;}
    public bool ReferenceScriptLibraries{get;set;}
    public bool IsBundleConfigPresent{get;set;}
    public string ViewName{get;set;}
    public string LayoutPageFile{get;set;}
    public string JQueryVersion{get;set;}
    public Version MvcVersion{get;set;}
    public Microsoft.AspNet.Scaffolding.Core.Metadata.ModelMetadata ModelMetadata{get;set;}
 
        
        #line default
        #line hidden
        
        #line 4 "D:\Dev\Work\AppsgeneratorNew\New\AppsGenerator\CSharpGenerator\Web\MvcView\ModelMetadataFunctions.cs.include.t4"

// Gets the related entity information for an association property where there is an associated foreign key property.
// Note: ModelMetadata.RelatedEntities contains only the related entities associated through a foreign key property.

RelatedModelMetadata GetRelatedModelMetadata(PropertyMetadata property){
    RelatedModelMetadata propertyModel;
    IDictionary<string, RelatedModelMetadata> relatedProperties;
    if(ModelMetadata.RelatedEntities != null)
    {
        relatedProperties = ModelMetadata.RelatedEntities.ToDictionary(item => item.AssociationPropertyName);
    }
    else
    {
        relatedProperties = new Dictionary<string, RelatedModelMetadata>();
    }
    relatedProperties.TryGetValue(property.PropertyName, out propertyModel);

    return propertyModel;
}

// A foreign key, e.g. CategoryID, will have an association name of Category
string GetAssociationName(PropertyMetadata property) {
    RelatedModelMetadata propertyModel = GetRelatedModelMetadata(property);
    return propertyModel != null ? propertyModel.AssociationPropertyName : property.PropertyName;
}

// A foreign key, e.g. CategoryID, will have a value expression of Category.CategoryID
string GetValueExpression(PropertyMetadata property) {
    RelatedModelMetadata propertyModel = GetRelatedModelMetadata(property);
    return propertyModel != null ? propertyModel.AssociationPropertyName + "." + propertyModel.DisplayPropertyName : property.PropertyName;
}

// This will return the primary key property name, if and only if there is exactly
// one primary key. Returns null if there is no PK, or the PK is composite.
string GetPrimaryKeyName() {
    return (ModelMetadata.PrimaryKeys != null && ModelMetadata.PrimaryKeys.Count() == 1) ? ModelMetadata.PrimaryKeys[0].PropertyName : null;
}

bool IsPropertyGuid(PropertyMetadata property) {
    return String.Equals("System.Guid", property.TypeName, StringComparison.OrdinalIgnoreCase);
}

        
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
    public class DeleteBase
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
