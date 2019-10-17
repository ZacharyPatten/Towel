# Code Standards

These code standards are goals of the code in the Towel project. They are not set in stone, but they
should be followed if possible.

### Tabs

Please use tabs rather than spaces.

### Fields First

When declaring types, all fields (instance + static + const) should come before other members and be
outside any "#region" or other code groupings. This makes it easy for people to understand the structure
and purpose of a type.

### Runtime Algorithmic Complexity

One of the goals of the project is to have members documented with algoritmic complexity using the "runtime" XML tag.
Here is the notation in use:

- O(...): upper bound
- Ω(...): lower bound
- Θ(...): upper and lower bound
- ε(...): expected average

_Note: Runtime documentation is not a requirement for pull requests._
