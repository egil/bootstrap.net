# BootstrapDotNet - Strongly Typed Bootstrap Razor Components #
**NOTE:** Very experimental at the moment. Feedback is much appreciated.  

**NOTE:** Is based on Preview-5 of ASP.NET 3.

**NOTE:** Documentation is far from complete. [TestClient](tests/Egil.RazorComponents.Bootstrap.BlazorTestClient/) *may* contain a working examples of how to use this library.

## Strongly Typed Bootstrap - options verified at compile time
The idea is to make it less likely that somebody breaks the Bootstrap
conventions and creates invalid "Bootstrap HTML". 

At the moment, the functionality around Bootstraps grid system is done, i.e. the components `<Container>`, `<Row>` and `<Column>`. Almost all properties 
that can be set on one of the grid components are implemented. E.g. with `<Column>`, there is support for compile-time check of column options, 
e.g. `col-{breakpoint}-{width}` and `col-auto`.

The compile time check works by having a strongly typed parameter in `Column`, `[Parameter] SpanParameter Span { get; set; }`, and using static 
imports to make variants of `ISpanOption`'s available in the razor views. The last piece of the puzzle is utilizing C# implicit operator overloading 
to convert between the different subtypes of `SpanParameter` and `int`.

That makes it possible to specify complex column configurations such as `<div class="col-12 col-md-8 col-lg-4">` 
as `<Column Span="12 | md-8 | lg-4">`, which the compiler checks. If you e.g. by accident `mx-8` instead 
of `md-8`, i.e. `<Column Span="12 | mx-8 | lg-4">`, the compiler will complain that `mx-8` is not a valid option.

### Runtime checks as fallback
In some cases the compiler cannot be made to validate that correct options are applied, so BootstrapDotNet resorts 
to runtime validation instead. Currently, this is done in the following cases:

- When specifying a number in a parameter or in a combination with another option, then the number range is validated at runtime.

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
Responsive container:

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="container"></div>` | `<Container></Container>` |

Fluid container:

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="container-fluid"></div>` | `<Container Type="fluid"></Container>` |

### Row
**Row without options:**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="row"></div>` | `<Row></Row>` |

**Row with no gutters:**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="row no-gutters"></div>` | `<Row NoGutters="true"></Row>` |

**Vertical alignment:**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="row align-items-start"></div>`                       | `<Row VerticalAlign="start"></Row>`             |
| `<div class="row align-items-center"></div>`                      | `<Row VerticalAlign="center"></Row>`            |
| `<div class="row align-items-end"></div>`                         | `<Row VerticalAlign="end"></Row>`               |
| `<div class="row align-items-md-center"></div>`                   | `<Row VerticalAlign="md-center"></Row>`         |
| `<div class="row align-items-start align-items-xl-center"></div>` | `<Row VerticalAlign="start \| xl-center"></Row>` |

**Horizontal alignment:**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="row justify-content-start">`                           | `<Row Align="start"></Row>`             |
| `<div class="row justify-content-center">`                          | `<Row Align="center"></Row>`            |
| `<div class="row justify-content-end">`                             | `<Row Align="end"></Row>`               |
| `<div class="row justify-content-around">`                          | `<Row Align="around"></Row>`            |
| `<div class="row justify-content-between">`                         | `<Row Align="between"></Row>`           |
| `<div class="row justify-content-md-around">`                       | `<Row Align="md-around"></Row>`         |
| `<div class="row justify-content-start justify-content-lg-center">` | `<Row Align="start \| lg-center"></Row>` |

### Column
**Column without options:**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="col"></div>` | `<Column></Column>` |

**Span with width (1-12) and breakpoints:**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="col-5"></div>`                      | `<Column Span="5"></Column>`                  |
| `<div class="col-md"></div>`                     | `<Column Span="md"></Column>`                 |
| `<div class="col-md-5"></div>`                   | `<Column Span="md-5"></Column>`               |
| `<div class="col-auto"></div>`                   | `<Column Span="auto"></Column>`               |
| `<div class="col-lg-auto"></div>`                | `<Column Span="lg-auto"></Column>`            |
| `<div class="col-3 col-md-6 col-xl-auto"></div>` | `<Column Span="3 \| md-6 \| xl-auto"></Column>` |

**Order with index (0-12), first, last, and breakpoints:**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="col order-4"></div>`                                      | `<Column Order="4"></Column>`                          |
| `<div class="col order-first"></div>`                                  | `<Column Order="first"></Column>`                      |
| `<div class="col order-last"></div>`                                   | `<Column Order="last"></Column>`                       |
| `<div class="col order-sm-4"></div>`                                   | `<Column Order="sm-4"></Column>`                       |
| `<div class="col order-4 order-md-2 order-lg-0 order-xl-first"></div>` | `<Column Order="4 \| md-2 \| lg-0 \| xl-first"></Column>` |

**Offset with number (1-11) and breakpoint and number (0-11):**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="col offset-4"></div>`                          | `<Column Offset="4"></Column>`               |
| `<div class="col offset-sm-4"></div>`                       | `<Column Offset="sm-4"></Column>`            |
| `<div class="col offset-2 offset-md-4 offset-xl-8"></div>`  | `<Column Offset="2 \| md-4 \| xl-8"></Column>` |

### Shared options that all components have
**All components have the following parameters:**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="some-additional-class"></div>`  | `<Component AdditionalCssClasses="some-additional-class"></Component>` |

### Spacing utilities
From [Bootstraps documentation on their Spacing utilities](https://getbootstrap.com/docs/4.3/utilities/spacing/), we see a 
number of different ways to specify margin and padding on Bootstrap components. On components that support padding and margin,
currently `Row` and `Column`, one of the following syntax' are used:

- {size}
- {sides}-{size}
- {breakpoint}-{size}
- {sides}-{breakpoint}-{size}

Where *size* is: 

- a number in the range from `-5` to `5` for both Margin and Padding
- `auto` for Margin only.

Where *sides* is one of:

- `top`, which matches the Bootstrap side param `t`
- `bottom`, which matches the Bootstrap side param `b`
- `left`, which matches the Bootstrap side param `l`
- `right`, which matches the Bootstrap side param `r`
- `horizontal`, which matches the Bootstrap side param `x`
- `vertical`, which matches the Bootstrap side param `y`

And where *breakpoint* is one of:

- `sm`
- `md`
- `lg`
- `xl`

**Examples, compared to Bootstrap:**

| Bootstrap syntax  | BootstrapDotNet syntax |
| ------------- | ------------- |
| `<div class="row m-3 p-5"></div>`                      | `<Row Margin="3" Padding="5"></Row>`                        |
| `<div class="row m-auto p-5"></div>`                   | `<Row Margin="auto" Padding="5"></Row>`                     |
| `<div class="row mb-3 pt-5"></div>`                    | `<Row Margin="bottom-3" Padding="top-5"></Row>`             |
| `<div class="row m-md-3 p-xl-auto"></div>`             | `<Row Margin="md-3" Padding="xl-5"></Row>`                  |
| `<div class="row mr-md-3 px-xl-5"></div>`              | `<Row Margin="right-md-3" Padding="horizontal-xl-5"></Row>` |
| `<div class="row mr-md-3 px-xl-5"></div>`              | `<Row Margin="right-md-3" Padding="horizontal-xl-5"></Row>` |
| `<div class="row m-1 m-md-3 m-lg-5 p-2 p-md-3"></div>` | `<Row Margin="1 \| md-3 \| lg-5" Padding="2 \| md-3"></Row>`   |
