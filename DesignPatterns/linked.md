LinkedIn Profile:  
https://www.linkedin.com/in/mo-abdella

Note:  
The three required LinkedIn technical posts for this assignment have been completed and included below as drafts.

As mentioned in the previous assignment, I will publish LinkedIn posts when they provide meaningful value to my professional development. Therefore, these posts have not been published on LinkedIn. However, the required technical content, explanations, C# examples, and practical applications for Singleton, Prototype, and Builder are included below for review.


### Singleton Design Pattern — Controlling Shared Configuration

While working on a C# design patterns lab, I applied the Singleton pattern to an `AppConfig` class.

**The problem:**  
Before Singleton, different parts of the application could create separate `AppConfig` objects. That meant loading the same configuration more than once and having different objects hold different states.

**Before:**  
Multiple `AppConfig` instances → multiple configuration loads.

**After:**  
`AppConfig.Instance` provides the same shared instance:

`Same config object? True`  
`Times config was loaded from disk: 1`

This ensures that the application works with one shared configuration object.

**When to use it:**  
Singleton can be useful when an application genuinely needs exactly one shared instance, such as a configuration object or another controlled shared resource.

**One thing to watch out for:**  
Singleton introduces shared/global state, which can increase coupling and make testing harder if it is overused.

This lab helped me understand that Singleton is not simply about creating a static object — it is about controlling the creation and access of a single shared instance.

#CSharp #DotNet #DesignPatterns #Singleton #SoftwareDevelopment


### Prototype Design Pattern — Creating Objects by Cloning

In my C# design patterns lab, I applied the Prototype pattern to an `Enemy` hierarchy.

**The problem:**  
Creating an `Enemy` could be expensive because loading its 3D model was a slow operation. Creating every new enemy from scratch meant repeating that expensive work.

**Before:**  
Create a new `Enemy` → load the 3D model again.

**After:**  
Create one configured enemy as a prototype, then use `Clone()` to create new enemies without loading the model again.

The cloned enemies keep the same `ModelId`, but they are independent objects. Changes to the clone's `Weapon` or `Abilities` do not affect the original.

**When to use it:**  
Prototype is useful when creating an object is expensive or complicated, and new objects can start from an existing configured object.

**One thing to watch out for:**  
A shallow copy can leave referenced objects shared between the original and the clone. When independent objects are required, you need a proper deep copy.

This lab helped me understand that Prototype is not just about copying an object — it is about creating new independent objects from an existing configured instance.

#CSharp #DotNet #DesignPatterns #Prototype #SoftwareDevelopment



### Builder Design Pattern — Making Complex Object Creation Clearer

In my C# design patterns lab, I applied the Builder pattern to `CourseRegistration`.

**The problem:**  
`CourseRegistration` originally had a long constructor with many parameters. At the call site, it was difficult to understand what each argument represented, especially with multiple optional values and boolean flags.

**Before:**
```csharp
new CourseRegistration(
    "sara@mail.com",
    "SEF-101",
    "LiveGroup",
    "G1",
    "EARLY10",
    true,
    true,
    "Needs evening slot",
    new DateOnly(2026, 10, 1));
```

**After:**
```csharp
new CourseRegistrationBuilder()
    .ForStudent("sara@mail.com")
    .ForCourse("SEF-101")
    .AsLiveGroup()
    .WithGroupCode("G1")
    .WithDiscount("EARLY10")
    .SendWhatsApp()
    .SendEmailWelcome()
    .Build();
```

The Builder makes the creation process more readable and allows `Build()` to validate rules such as `LiveGroup` requiring a `GroupCode` and `VideosOnly` not allowing one.

**When to use it:**  
Builder is useful when an object has many parameters, especially when some are optional or when the creation process has rules that need to be validated.

**One thing to watch out for:**  
Don't introduce a Builder for simple objects where a normal constructor is already clear. The Builder adds extra code and complexity.

This lab helped me understand how Builder can make complex object creation easier to read, maintain, and validate.

#CSharp #DotNet #DesignPatterns #Builder #SoftwareDevelopment