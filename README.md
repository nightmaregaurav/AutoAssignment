# AutoAssignment - Simplified Object Assignment Library
A Light weight and simple alternative to [AutoMapper](https://github.com/AutoMapper/AutoMapper)

AutoAssignment is a lightweight and easy-to-use library that simplifies the process of object assignment and creation in C#. It provides a generic approach to handle complex object assignments without the need for writing repetitive code.

## Features

- Assign values from one object to another effortlessly.
- Create new objects based on the source object's data.
- Supports any type of objects with flexible assignment rules.

## Use Cases

1. DTO (Data Transfer Objects) Mapping
2. Entity Framework: Entity to ViewModel Mapping
3. Copying Data Between Similar Objects
4. Object Cloning and Prototyping
5. Unit Testing


## Getting Started

To get started with AutoAssignment, follow these simple steps:

1. Install the AutoAssignment package from NuGet (coming soon).
2. Import the `AutoAssignment` namespace in your C# file.
    ```csharp
    using AutoAssignment;
    ```
3. Define relationships between types using the static methods `DefineCreation` and `DefineAssignment` somewhere in your `startup.cs` or equivalent program entrypoint.
    ```csharp
   Assignment<SourceClass, DestinationClass>.DefineCreation(source => new DestinationClass(source.Prop1, source.Prop2+1));
   Assignment<SourceClass, DestinationClass>.DefineAssignment((source, destination) =>
   {
       destination.Name = source.Name;
       destination.Age = source.Age;
       // Add more assignments as needed

       return destination;
   });
   ```
4. Start using AutoAssignment to create new objects or assign values from one object to another.
    ```csharp
   // Create a new DestinationClass instance based on the SourceClass object
    var destinationObject = Assignment<SourceClass, DestinationClass>.From(sourceObject);

    // Assign values from the sourceObject to an existing destinationObject
    destinationObject = Assignment<SourceClass, DestinationClass>.From(sourceObject, destinationObject);
    ```
## Contributions and Issues
Contributions to AutoAssignment are always welcomed and highly appreciated. If you find any issues or have suggestions for improvements, please feel free to open an issue or submit a pull request on the [GitHub Repository](https://github.com/nightmaregaurav/AutoAssignment).

## License
AutoAssignment is free and open source, distributed under the MIT License. See the [LICENSE](https://github.com/nightmaregaurav/AutoAssignment/blob/main/LICENSE) file for more details.

Made with love by [NightmareGaurav](https://github.com/nightmaregaurav).
