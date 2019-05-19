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

### Container
| Container            | Boostrap syntax                     | Library syntax                       |
|----------------------|-------------------------------------|--------------------------------------|
| Responsive container | <div class="container"></div>       | <Container></Container>              |
| Fluid container      | <div class="container-fluid"></div> | <Container Type="fluid"></Container> |

### Row

| Row                                   | Boostrap syntax                                                   | Library syntax                                |
|---------------------------------------|-------------------------------------------------------------------|-----------------------------------------------|
| Row without options                   | <div class="row"></div>                                           | <Row></Row>                                   |
|                                       |                                                                   |                                               |
| No gutters                            | <div class="row no-gutters"></div>                                | <Row NoGutters="true"></Row>                  |
|                                       |                                                                   |                                               |
| Vertical alignment                    | <div class="row align-items-start"></div>                         | <Row VerticalAlign="start"></Row>             |
| Vertical alignment                    | <div class="row align-items-center"></div>                        | <Row VerticalAlign="center"></Row>            |
| Vertical alignment                    | <div class="row align-items-end"></div>                           | <Row VerticalAlign="end"></Row>               |
| Vertical alignment with breakpoint    | <div class="row align-items-md-center"></div>                     | <Row VerticalAlign="md-center"></Row>         |
| Multiple vertical alignment options   | <div class="row align-items-start align-items-xl-center"></div>   | <Row VerticalAlign="start | xl-center"></Row> |
|                                       |                                                                   |                                               |
| Horizontal alignment                  | <div class="row justify-content-start">                           | <Row Align="start"></Row>                     |
| Horizontal alignment                  | <div class="row justify-content-center">                          | <Row Align="center"></Row>                    |
| Horizontal alignment                  | <div class="row justify-content-end">                             | <Row Align="end"></Row>                       |
| Horizontal alignment                  | <div class="row justify-content-around">                          | <Row Align="around"></Row>                    |
| Horizontal alignment                  | <div class="row justify-content-between">                         | <Row Align="between"></Row>                   |
| Horizontal alignment with breakpoint  | <div class="row justify-content-md-around">                       | <Row Align="md-around"></Row>                 |
| Multiple horizontal alignment options | <div class="row justify-content-start justify-content-lg-center"> | <Row Align="start | lg-center"></Row>         |

### Column
| Column                                  | Boostrap syntax                                          | Library syntax                              |
|-----------------------------------------|----------------------------------------------------------|---------------------------------------------|
| Column without options                  | <div class="col"></div>                                  | <Column></Column>                           |
|                                         |                                                          |                                             |
| Column with width                       | <div class="col-5"></div>                                | <Column Span="5"></Column>                  |
| Column with breakpoint                  | <div class="col-md"></div>                               | <Column Span="md"></Column>                 |
| Column with breakpoint and width        | <div class="col-md-5"></div>                             | <Column Span="md-5"></Column>               |
| Column with auto-width                  | <div class="col-auto"></div>                             | <Column Span="auto"></Column>               |
| Column with breakpoint and auto-width   | <div class="col-lg-auto"></div>                          | <Column Span="lg-auto"></Column>            |
| Multiple span options                   | <div class="col-3 col-md-6 col-xl-auto"></div>           | <Column Span="3 | md-6 | xl-auto"></Column> |
|                                         |                                                          |                                             |
| Order by number (0-12)                  | <div class="col order-4"></div>                          | <Column Order="4"></Column>                 |
| Order first                             | <div class="col order-first"></div>                      | <Column Order="first"></Column>             |
| Order last                              | <div class="col order-last"></div>                       | <Column Order="last"></Column>              |
| Order with breakpoint (0-12|first|last) | <div class="col order-sm-4"></div>                       | <Column Order="sm-4"></Column>              |
| Multiple order options                  | <div class="col order-4 order-md-2 order-lg-0"></div>    | <Column Order="4 | md-2 | lg-0"></Column>   |
|                                         |                                                          |                                             |
| Offset by number (0-11)                 | <div class="col offset-4"></div>                         | <Column Offset="4"></Column>                |
| Offset with breakpoint                  | <div class="col offset-sm-4"></div>                      | <Column Offset="sm-4"></Column>             |
| Multiple offset options                 | <div class="col offset-2 offset-md-4 offset-xl-8"></div> | <Column Offset="2 | md-4 | xl-8"></Column>  |

### Shared options that all components have
| Shared                 | Boostrap syntax                           | Library syntax                                                       |
|------------------------|-------------------------------------------|----------------------------------------------------------------------|
| Additional css classes | <div class="some-additional-class"></div> | <Component AdditionalCssClasses="some-additional-class"></Component> |