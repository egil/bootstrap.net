# Grid system

| Component                              | Col   | Col    | Col     | Col        | Row         | Row             | Row             | Row        | Utils  | Utils   |
|----------------------------------------|-------|--------|---------|------------|-------------|-----------------|-----------------|------------|--------|---------|
| Param                                  | Span  | Order  | Offset  | Align      | Align       | Justify         | Fluid           | NoGutters  | Margin | Padding |
| Css class                              | col-  | order- | offset- | align-self | align-items | justify-content | container-fluid | no-gutters | m      | p       |
|----------------------------------------|-------|--------|---------|------------|-------------|-----------------|-----------------|------------|--------|---------|
| True                                   |       |        |         |            |             |                 | x               | x          |        |         |
| Number                                 | 1..12 | 1..12  | 1..12   |            |             |                 |                 |            |        |         |
| Breakpoint                             | x     |        |         |            |             |                 |                 |            |        |         |
| BreakpointNumber                       | x     | x      | x       |            |             |                 |                 |            |        |         |
| Auto                                   | x     |        |         | x          |             |                 |                 |            |        |         |
| First                                  |       | x      |         |            |             |                 |                 |            |        |         |
| Last                                   |       | x      |         |            |             |                 |                 |            |        |         |
| BreakpointAuto                         | x     |        |         | x          |             |                 |                 |            |        |         |
| BreakpointFirst                        |       | x      |         |            |             |                 |                 |            |        |         |
| BreakpointLast                         |       | x      |         |            |             |                 |                 |            |        |         |
| Start                                  |       |        |         | x          | x           | x               |                 |            |        |         |
| End                                    |       |        |         | x          | x           | x               |                 |            |        |         |
| Center                                 |       |        |         | x          | x           | x               |                 |            |        |         |
| Stretch                                |       |        |         | x          | x           |                 |                 |            |        |         |
| Around                                 |       |        |         |            |             | x               |                 |            |        |         |
| Between                                |       |        |         |            |             | x               |                 |            |        |         |
| BreakpointStart                        |       |        |         | x          | x           | x               |                 |            |        |         |
| BreakpointEnd                          |       |        |         | x          | x           | x               |                 |            |        |         |
| BreakpointCenter                       |       |        |         | x          | x           | x               |                 |            |        |         |
| BreakpointStretch                      |       |        |         | x          | x           |                 |                 |            |        |         |
| BreakpointAround                       |       |        |         |            |             | x               |                 |            |        |         |
| BreakpointBetween                      |       |        |         |            |             | x               |                 |            |        |         |
| {T|B|L|R|X|Y}-{0..5|auto}              |       |        |         |            |             |                 |                 |            | x      | x       |
| {T|B|L|R|X|Y}-{breakpoint}-{0..5|auto} |       |        |         |            |             |                 |                 |            | x      | x       |
| {T|B|L|R|X|Y}-n{0..5}                  |       |        |         |            |             |                 |                 |            | x      | x       |
| {T|B|L|R|X|Y}-{breakpoint}-n{0..5}     |       |        |         |            |             |                 |                 |            | x      | x       |