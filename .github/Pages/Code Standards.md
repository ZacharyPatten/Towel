# Code Standards

These code standards are goals of the code in the Towel project. They are not set in stone, but they
should be followed if possible.

#### Tabs

Please use tabs rather than spaces.

#### Fields First

When declaring types, all fields (instance + static + const) should come before other members and be
outside any "#region" or other code groupings. This makes it easy for people to understand the structure
and purpose of a type.

#### Runtime Algorithm Complexity

Towel aims to have runtime algorithmic complexity documented in the XML of applicable code members (using
the "runtime" tag). Here is the notation:

- O(...): upper bound
- Ω(...): lower bound
- Θ(...): upper and lower bound
- ε(...): expected average
