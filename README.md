# Strongly Typed Bootstrap Razor Components #
**NOTE:** Very experimental at the moment. Feedback is much appreciated.  

**NOTE:** Is based on Preview-5 of ASP.NET 3.

**NOTE:** Documentation is far from complete. [TestClient](tests/Egil.RazorComponents.Bootstrap.BlazorTestClient/) *may* contain a working examples of how to use this library.

## Strongly Typed Bootstrap - options verified at compile time
The idea is to make it less likely that somebody breaks the Bootstrap
conventions and creates invalid "Bootstrap HTML". 

At the moment, the basic functionality around Bootstraps grid system is done, i.e. the components `<Container>`, `<Row>` and `<Column>`. Almost all properties 
that can be passed to one of the grid components are implemented. E.g. with `<Column>`, there is support for compile-time check of column options, 
e.g. `col-{breakpoint}-{width}` and `col-auto`.

The compile time check works by having a strongly typed parameter in `Column.cshtml`, `[Parameter] SpanParameter Span { get; set; }`, and using static 
imports to make variants of `ISpanOption`'s available in the razor views. The last piece of the puzzle is utilizing C# implicit operator overloading 
to convert between the different subtypes of `SpanParameter` and `int`.

That makes it possible to specify complex column configurations such as `<div class="col-12 col-md-8 col-lg-4">` 
as `<Column Span="12 | md-8 | lg-4">`, which the compiler checks. If you e.g. by accident `mx-8` instead 
of `md-8`, i.e. `<Column Span="12 | mx-8 | lg-4">`, the compiler will complain that `mx-8` is not a valid option.

## Setup
1. Install the nuget package in your Blazor-client or Blazor-server-side application.
2. Add the following to your `_Imports.cshtml`, e.g. in the root of your application.

```cshtml
@using Egil.RazorComponents.Bootstrap
@using Egil.RazorComponents.Bootstrap.Grid
@using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr
@using static Egil.RazorComponents.Bootstrap.Options.SpacingOptions.Factory.LowerCase
```

**NOTE:** Currently only client-side apps can pull the embedded Bootstrap library. If you are building a server-side app, add the
Bootstrap css and JavaScript files you need. This will probably change in future verions.

## Specifying options
The following table compares specifying Bootstraps options using normal [Bootstrap grid-syntax](https://getbootstrap.com/docs/4.3/layout/grid) and 
that of using the library's syntax. That should make it possible for anyone familiar with Bootstraps syntax to quickly pick-up the library's syntax.
