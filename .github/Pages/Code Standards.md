# Code Standards

This is just some syntax preference notes for Towel. They are not strict rules and will not be enforced at this time. Just follow them if it makes sense. :)

### Runtime Algorithmic Complexity

One of the goals of Towel is to have members documented with algoritmic complexity using the `runtime` XML tag. Here is the notation in use:

- O(...): upper bound
- Ω(...): lower bound
- Θ(...): upper and lower bound
- ε(...): expected average

Examples:
- `/// <runtime>O(1)</runtime>` the member has constant time
- `/// <runtime>O(n), Ω(1), ε(1)</runtime>` the member may have to iterate n items, but it will generally run in constant time
- `/// <runtime>Θ(n)</runtime>` the member iterates n items
- `/// <runtime>Θ(n^2)</runtime>` the member iterates n items and has a nested loop

### Tabs

Please use tabs rather than spaces.

### Fields First

When declaring types, all fields (instance + static + const) should come before other members and be outside any "#region" or other code groupings. This makes it easy for people to understand the structure and purpose of a type.

### Internal Over Private

In general, prefer `internal` over `private`. If people want to access `internal` members for whatever reason, they can just download the code and add an `InternalsVisibleTo` attribute to the code so they can access internal members.

### Expression Boded Members Are Prefered

In general, prefer expression body definition syntax `member => expression;` over traditional C# syntax `member { expression; }` where applicable.
