// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.",
    Justification = "Part of the library's context adaptable logic",
    Scope = "namespaceanddescendants",
    Target = "Egil.RazorComponents.Bootstrap.Components")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", 
    Justification = "Locallization is on the TODO list", 
    Scope = "namespaceanddescendants",
    Target = "Egil.RazorComponents.Bootstrap")]
