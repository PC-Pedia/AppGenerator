﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CSharpGenerator.Web.MvcView
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.AspNet.Scaffolding.Core.Metadata;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class Create : CreateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 7 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
// include file="Imports.include.t4" 
            
            #line default
            #line hidden
            this.Write("@model ");
            
            #line 8 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewDataTypeName));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 9 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

// "form-control" attribute is only supported for all EditorFor() in System.Web.Mvc 5.1.0.0 or later versions, except for checkbox, which uses a div in Bootstrap
string boolType = "Edm.Boolean";
Version requiredMvcVersion = new Version("5.1.0.0");
bool isControlHtmlAttributesSupported = MvcVersion >= requiredMvcVersion;
// The following chained if-statement outputs the file header code and markup for a partial view, a view using a layout page, or a regular view.
if(IsPartialView) {

            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 18 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

} else if(IsLayoutPageSelected) {

            
            #line default
            #line hidden
            this.Write("\r\n@{\r\n    ViewBag.Title = \"");
            
            #line 23 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewName +" "+ ViewDataTypeShortName));
            
            #line default
            #line hidden
            this.Write("\";\r\n");
            
            #line 24 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

if (!String.IsNullOrEmpty(LayoutPageFile)) {

            
            #line default
            #line hidden
            this.Write("    Layout = \"");
            
            #line 27 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(LayoutPageFile));
            
            #line default
            #line hidden
            this.Write("\";\r\n");
            
            #line 28 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

}

            
            #line default
            #line hidden
            this.Write("}\r\n\r\n<h2>");
            
            #line 33 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewName +" "+ ViewDataTypeShortName));
            
            #line default
            #line hidden
            this.Write("</h2>\r\n\r\n");
            
            #line 35 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

} else {

            
            #line default
            #line hidden
            this.Write("\r\n@{\r\n    Layout = null;\r\n}\r\n\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n<head>\r\n    <meta name=" +
                    "\"viewport\" content=\"width=device-width\" />\r\n    <title>");
            
            #line 48 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewName +" "+ ViewDataTypeShortName));
            
            #line default
            #line hidden
            this.Write("</title>\r\n</head>\r\n<body>\r\n");
            
            #line 51 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

    PushIndent("    ");
}

            
            #line default
            #line hidden
            
            #line 55 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

if (ReferenceScriptLibraries) {

            
            #line default
            #line hidden
            
            #line 58 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

    if (!IsLayoutPageSelected && IsBundleConfigPresent) {

            
            #line default
            #line hidden
            this.Write("@Scripts.Render(\"~/bundles/jquery\")\r\n@Scripts.Render(\"~/bundles/jqueryval\")\r\n\r\n");
            
            #line 64 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

    }

            
            #line default
            #line hidden
            
            #line 67 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

    else if (!IsLayoutPageSelected) {

            
            #line default
            #line hidden
            this.Write("<script src=\"~/Scripts/jquery-");
            
            #line 70 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(JQueryVersion));
            
            #line default
            #line hidden
            this.Write(".min.js\"></script>\r\n<script src=\"~/Scripts/jquery.validate.min.js\"></script>\r\n<sc" +
                    "ript src=\"~/Scripts/jquery.validate.unobtrusive.min.js\"></script>\r\n\r\n");
            
            #line 74 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

    }

            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 78 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

}

            
            #line default
            #line hidden
            this.Write("@using (Html.BeginForm()) \r\n{\r\n    @Html.AntiForgeryToken()\r\n    \r\n    <div class" +
                    "=\"form-horizontal\">\r\n        <hr />\r\n");
            
            #line 87 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
 
    if (isControlHtmlAttributesSupported) {

            
            #line default
            #line hidden
            this.Write("        @Html.ValidationSummary(true, \"\", new { @class = \"text-danger\" })\r\n");
            
            #line 91 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
        
    } else {

            
            #line default
            #line hidden
            this.Write("        @Html.ValidationSummary(true)\r\n");
            
            #line 95 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
      
    }

            
            #line default
            #line hidden
            
            #line 98 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

foreach (PropertyMetadata property in ModelMetadata.Properties) {
    if (property.Scaffold && !property.IsAutoGenerated && !property.IsReadOnly && !property.IsAssociation) {

        // If the property is a primary key and Guid, then the Guid is generated in the controller. Hence, this propery is not displayed on the view.
        if (property.IsPrimaryKey && IsPropertyGuid(property)) {
            continue;
        }


            
            #line default
            #line hidden
            this.Write("        <div class=\"form-group\">\r\n");
            
            #line 109 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

        if (property.IsForeignKey) {

            
            #line default
            #line hidden
            this.Write("            @Html.LabelFor(model => model.");
            
            #line 112 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write(", \"");
            
            #line 112 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetAssociationName(property)));
            
            #line default
            #line hidden
            this.Write("\", htmlAttributes: new { @class = \"control-label col-md-2\" })\r\n");
            
            #line 113 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

        } else {

            
            #line default
            #line hidden
            this.Write("            @Html.LabelFor(model => model.");
            
            #line 116 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write(", htmlAttributes: new { @class = \"control-label col-md-2\" })\r\n");
            
            #line 117 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

        }

            
            #line default
            #line hidden
            this.Write("            <div class=\"col-md-10\">\r\n");
            
            #line 121 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

        bool isCheckbox = property.TypeName.Equals(boolType);
        bool isDateTime = property.TypeName.Equals("Edm.DateTime");
        if (property.IsForeignKey) {

            
            #line default
            #line hidden
            
            #line 126 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
 
            if (isControlHtmlAttributesSupported) {

            
            #line default
            #line hidden
            this.Write("                @Html.DropDownList(\"");
            
            #line 129 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write("\", null, htmlAttributes: new { @class = \"form-control\" })\r\n");
            
            #line 130 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

            } else {

            
            #line default
            #line hidden
            this.Write("                @Html.DropDownList(\"");
            
            #line 133 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write("\", String.Empty)\r\n");
            
            #line 134 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

            }

            
            #line default
            #line hidden
            
            #line 137 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

        } else  if (isControlHtmlAttributesSupported) {
             if (isCheckbox) {

            
            #line default
            #line hidden
            this.Write("                <div class=\"checkbox\">\r\n");
            
            #line 142 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

                    PushIndent("    ");

            
            #line default
            #line hidden
            this.Write("                @Html.EditorFor(model => model.");
            
            #line 145 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write(")\r\n");
            
            #line 146 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
          
            } else if (isDateTime) {

            
            #line default
            #line hidden
            this.Write("                 @Html.EditorFor(model => model.");
            
            #line 149 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write(",new { htmlAttributes = new { @class = \"form-control\" } })\r\n                <scri" +
                    "pt type=\"text/javascript\">\r\n                    $(function () {\r\n               " +
                    "         $(\'#");
            
            #line 152 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write("\').datetimepicker();\r\n                    });\r\n                </script>\r\n");
            
            #line 155 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

            } else if (property.IsEnum && !property.IsEnumFlags) {

            
            #line default
            #line hidden
            this.Write("                @Html.EnumDropDownListFor(model => model.");
            
            #line 158 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write(", htmlAttributes: new { @class = \"form-control\" })\r\n");
            
            #line 159 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

            } else {

            
            #line default
            #line hidden
            this.Write("                @Html.EditorFor(model => model.");
            
            #line 162 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write(", new { htmlAttributes = new { @class = \"form-control\" } })\r\n");
            
            #line 163 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

            } 
        } else {

            
            #line default
            #line hidden
            this.Write("                @Html.EditorFor(model => model.");
            
            #line 167 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write(")\r\n");
            
            #line 168 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

        }

            
            #line default
            #line hidden
            
            #line 171 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
 
        if (isControlHtmlAttributesSupported) {

            
            #line default
            #line hidden
            this.Write("                @Html.ValidationMessageFor(model => model.");
            
            #line 174 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write(", \"\", new { @class = \"text-danger\" })\r\n");
            
            #line 175 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
        
        } else {

            
            #line default
            #line hidden
            this.Write("                @Html.ValidationMessageFor(model => model.");
            
            #line 178 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.PropertyName));
            
            #line default
            #line hidden
            this.Write(")\r\n");
            
            #line 179 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
      
        }

            
            #line default
            #line hidden
            
            #line 182 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

        if (isCheckbox && isControlHtmlAttributesSupported) {
            PopIndent();

            
            #line default
            #line hidden
            this.Write("                </div>\r\n");
            
            #line 187 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

        }

            
            #line default
            #line hidden
            this.Write("            </div>\r\n        </div>\r\n\r\n");
            
            #line 193 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

    }
}

            
            #line default
            #line hidden
            this.Write(@"        <div class=""form-group"">
            <div class=""col-md-offset-2 col-md-10"">
                <input type=""submit"" value=""Create"" class=""btn btn-default"" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink(""Back to List"", ""Index"")
</div>
");
            
            #line 208 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

if(IsLayoutPageSelected && ReferenceScriptLibraries && IsBundleConfigPresent) {

            
            #line default
            #line hidden
            this.Write("\r\n@section Scripts {\r\n    @Scripts.Render(\"~/bundles/jqueryval\")\r\n}\r\n");
            
            #line 215 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

}

            
            #line default
            #line hidden
            
            #line 218 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

else if(IsLayoutPageSelected && ReferenceScriptLibraries) {

            
            #line default
            #line hidden
            this.Write("\r\n<script src=\"~/Scripts/jquery-");
            
            #line 222 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(JQueryVersion));
            
            #line default
            #line hidden
            this.Write(".min.js\"></script>\r\n<script src=\"~/Scripts/jquery.validate.min.js\"></script>\r\n<sc" +
                    "ript src=\"~/Scripts/jquery.validate.unobtrusive.min.js\"></script>\r\n");
            
            #line 225 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

}

            
            #line default
            #line hidden
            
            #line 228 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

// The following code closes the tag used in the case of a view using a layout page and the body and html tags in the case of a regular view page

            
            #line default
            #line hidden
            
            #line 231 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

if(!IsPartialView && !IsLayoutPageSelected) {
    ClearIndent();

            
            #line default
            #line hidden
            this.Write("</body>\r\n</html>\r\n");
            
            #line 237 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

}

            
            #line default
            #line hidden
            this.Write("\r\n\r\n");
            this.Write("\r\n\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 243 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\Create.tt"

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
        
        #line 4 "E:\Omar\AO\WitrooProjects\AppsGenerator\WebApp\CSharpGenerator\Web\MvcView\ModelMetadataFunctions.cs.include.t4"

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
    public class CreateBase
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
