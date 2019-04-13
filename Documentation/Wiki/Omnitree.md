An omnitree is an N-D spacial partitioning tree (SPT). It allows sorting along multiple dimensions. An SPT works by continually dividing spaces so that massive culling can be performed based on the spacial groupings.

There are two version of the Omnitree in the Towel Framework: **OmnitreePoints** and **OmnitreeBounds**. OmnitreePoints stores vectors while OmnitreeBounds stores spaces. If you want to store people (first name, last name, date of birth) then you probably want to use OmnitreePoints because those values are points rather than ranges. If you want to store 3D objects (minX -> maxX, minY -> maxY, minZ -> maxZ) then you probably want to use OmnitreeBounds because those values are ranges rather than points.

The maximum number of children any node will have in an SPT is equal to 2 ^ N where "N" is the number of dimensions the tree is sorting on. Here is a table showing the maximum number of child nodes per sorting dimensions:

| Dimensions | Max # Children |
|------------|----------------|
| 1          | 2 ^ 1 = 2      |
| 2          | 2 ^ 2 = 4      |
| 3          | 2 ^ 3 = 8      |
| 4          | 2 ^ 4 = 16     |
| ...        | ...            |

Note: A 2D SPT is often refered to as a "_Quadtree_." A 3D SPT is often refered to as an "_Octree_."

## 1-Dimensional Omnitree

Max # Children = 2 ^ 1 = 2

### Visualization

![](https://github.com/ZacharyPatten/Towel/blob/master/Documentation/Resources/Omnitree%20Example%201D.svg)

### Code Example

Here is an example of a 1D SPT that stores Employee instances sorted along their FirstName... 
```csharp
public class Employee
{
    public string FirstName;
    public string LastName;
    public DateTime DateOfBirth;
    public DateTime StartDate;
}

// construction
IOmnitreePoints<Employee, string> omnitree = new OmnitreePointsLinked<Employee, string>(
    (Employee employee, out string firstName) => { firstName = employee.FirstName; });

// adding
omnitree.Add(new Employee()
{
    FirstName = "Dixie",
    LastName = "Normous",
    DateOfBirth = new DateTime(2000, 8, 30),
    StartDate = new DateTime(2010, 5, 7),
});

omnitree.Add(new Employee()
{
    FirstName = "Harry",
    LastName = "Cox",
    DateOfBirth = new DateTime(1990, 3, 15),
    StartDate = new DateTime(2013, 8, 20),
});

// spacial querying
// print all the employees with first names from "A" to "F"
omnitree.Stepper(employee => Console.WriteLine(employee.FirstName), "A", "F");

// removing
omnitree.Remove("F", "Z");
```
## 2-Dimensional Omnitree (aka Quadtree)

Max # Children = 2 ^ 2 = 4

![](https://github.com/ZacharyPatten/Towel/blob/master/Documentation/Resources/Omnitree%20Example%202D.svg)