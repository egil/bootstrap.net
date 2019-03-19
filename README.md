# Strongly typed Bootstrap in Blazor/Razor Components #
**NOTE:** Very experimental at the moment. Feedback is very welcome.  

**NOTE:** Documentation is far from complete. [TestClient](tests/Egil.BlazorComponents.Bootstrap.TestClient/) *may* contain a working examples of how to use this library.

## Strongly typed Bootstrap - options verified at compile time
The idea is to make it less likely that somebody breaks the Bootstrap
conventions and creates invalid "Bootstrap HTML". 

At the moment, the basic functionality around Bootstraps grid system is done, i.e. the components `<Container>`, `<Row>` and `<Column>`. `<Container>` and `<Row>` are
right now pretty basic. However, `<Column>` supports compile-time check of column options, e.g. `col-{breakpoint}-{width}` and `col-auto`.

The compile time check works by having a strongly typed parameter in `Column.cshtml`, `[Parameter] SpanOptionBase Span { get; set; }`, and using static imports to make variants of `SpanOptionBase` available in the razor views. The last piece of the puzzle is utilizing C# implicit operator overloading to convert between the different subtypes of `SpanOptionBase` and `int`.

That makes it possible to specify complex column configurations such as `<div class="col-12 col-md-8 col-lg-4">` as `<Column Span="12 | Medium-8 | Large-4">`, which the compiler checks. If you e.g. by accident `Middle-8` instead of `Medium-8`, i.e. `<Column Span="12 | Middle-8 | Large-4">`, the compiler will complain that `Middle-8` is not a valid option.

## Usage
Add the following to your `_ViewImports.cshtml`, e.g. in the root of your application.

```cshtml
@using Egil.BlazorComponents.Bootstrap
@using Egil.BlazorComponents.Bootstrap.Grid
@using Egil.BlazorComponents.Bootstrap.Grid.Columns
@using static Egil.BlazorComponents.Bootstrap.Grid.Columns.SpanOption
@addTagHelper *, Egil.BlazorComponents.Bootstrap
```

## Examples
This section contains the basic examples from Bootstraps 
[Grid system](https://getbootstrap.com/docs/4.3/layout/grid) documentation, as well as the produced HTML.

### Equal-width
```cshtml
<Container>
    <Row>
        <Column>1 of 2</Column>
        <Column>2 of 2</Column>
    </Row>
    <Row>
        <Column>1 of 3</Column>
        <Column>2 of 3</Column>
        <Column>3 of 3</Column>
    </Row>
</Container>
```
Results in:
```html
<div class="container">
  <div class="row">
    <div class="col">
      1 of 2
    </div>
    <div class="col">
      2 of 2
    </div>
  </div>
  <div class="row">
    <div class="col">
      1 of 3
    </div>
    <div class="col">
      2 of 3
    </div>
    <div class="col">
      3 of 3
    </div>
  </div>
</div>
```

### Setting one column width
Notice that both `Span="5"` and `Span=5` works.
```cshtml
<Container>
    <Row>
        <Column>1 of 3</Column>
        <Column Span="6">2 of 3 (wider)</Column>
        <Column>3 of 3</Column>
    </Row>
    <Row>
        <Column>1 of 3</Column>
        <Column Span=5>2 of 3</Column>
        <Column>3 of 3</Column>
    </Row>
</Container>
```
Results in:
```html
<div class="container">
  <div class="row">
    <div class="col">
      1 of 3
    </div>
    <div class="col-6">
      2 of 3 (wider)
    </div>
    <div class="col">
      3 of 3
    </div>
  </div>
  <div class="row">
    <div class="col">
      1 of 3
    </div>
    <div class="col-5">
      2 of 3 (wider)
    </div>
    <div class="col">
      3 of 3
    </div>
  </div>
</div>
```

### Mix and match
```cshtml
<h2>Mix and match</h2>
<Container>
    <Row>
        <Column Span="12 | Medium-8 | Large-4">.col-12 .col-md-8 .col-lg-4</Column>
        <Column Span="6 | Medium-4 | Large-2">.col-6 .col-md-4 .col-lg-2</Column>
    </Row>
    <Row>
        <Column Span=@(6 | Medium-4)>.col-6 .col-md-4</Column>
        <Column Span=@(6 | Medium-4)>.col-6 .col-md-4</Column>
        <Column Span=@(6 | Medium-4)>.col-6 .col-md-4</Column>
    </Row>
    <Row>
        <Column Span="6">.col-6</Column>
        <Column Span="6">.col-6</Column>
    </Row>
</Container>
```
Results in:
```html
<div class="container">
  <div class="row">
    <div class="col-12 col-md-8 col-lg-4">.col-12 .col-md-8 .col-lg-4</div>
    <div class="col-6 col-md-4 col-lg-2">.col-6 .col-md-4 .col-lg-2</div>
  </div>

  <div class="row">
    <div class="col-6 col-md-4">.col-6 .col-md-4</div>
    <div class="col-6 col-md-4">.col-6 .col-md-4</div>
    <div class="col-6 col-md-4">.col-6 .col-md-4</div>
  </div>

  <div class="row">
    <div class="col-6">.col-6</div>
    <div class="col-6">.col-6</div>
  </div>
</div>
```

### Variable width content
```cshtml
<h2>Variable width content</h2>
<Container>
    <Row>
        <Column Span="Default | Large-2">1 of 3</Column>
        <Column Span="Medium-Auto">Variable width content</Column>
        <Column Span="Default | Large-2">3 of 3</Column>
    </Row>
    <Row>
        <Column>1 of 3</Column>
        <Column Span=@(Medium-Auto)>Variable width content</Column>
        <Column Span=@(Default | Large-2)>3 of 3</Column>
    </Row>
</Container>
```
Results in:
```html
<div class="container">
  <div class="row">
    <div class="col col-lg-2">
      1 of 3
    </div>
    <div class="col-md-auto">
      Variable width content
    </div>
    <div class="col col-lg-2">
      3 of 3
    </div>
  </div>
  <div class="row">
    <div class="col">
      1 of 3
    </div>
    <div class="col-md-auto">
      Variable width content
    </div>
    <div class="col col-lg-2">
      3 of 3
    </div>
  </div>
</div>
```
