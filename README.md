# Strongly typed Bootstrap in Blazor/Razor Components #
**NOTE:** Very experimental at the moment. Feedback is very welcome.  

**NOTE:** Documentation is far from complete. [TestClient](tests/Egil.BlazorComponents.Bootstrap.TestClient/) *may* contain a working examples of how to use this library.

## Usage
Add the following to your `_ViewImports.cshtml`, e.g. in the root of your application.

```cshtml
@using Egil.BlazorComponents.Bootstrap
@using Egil.BlazorComponents.Bootstrap.Grid
@using Egil.BlazorComponents.Bootstrap.Grid.Columns
@using static Egil.BlazorComponents.Bootstrap.Grid.Columns.SpanOption
@addTagHelper *, Egil.BlazorComponents.Bootstrap
```

## Auto-layout columns
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
        <Column Span=@(12 | Medium-8 | Large-4)>.col-12 .col-md-8 .col-lg-4</Column>
        <Column Span=@(6 | Medium-4 | Large-2)>.col-6 .col-md-4 .col-lg-2</Column>
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
        <Column Span=@(Default | Large-2)>1 of 3</Column>
        <Column Span=@(Medium-Auto)>Variable width content</Column>
        <Column Span=@(Default | Large-2)>3 of 3</Column>
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
