using System.Diagnostics;
using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;

Console.WriteLine("=== SINGLETON: BEFORE ===\n");

var db = new DatabaseService();
var ui = new UiService();

Console.WriteLine();

db.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");

ui.Render();

Console.WriteLine($"\nSame config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");

Console.WriteLine("\n=== PROTOTYPE: BEFORE ===\n");

var sw = Stopwatch.StartNew();
var army = new List<Enemy>();
for (int i = 1; i <= 5; i++)
{
    var orc = new Orc();
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}
Console.WriteLine($"Created 5 orcs in {sw.ElapsedMilliseconds} ms\n");

Enemy original = new Orc();
original.Name = "Boss Orc";
Enemy copy = EnemyCopyHelper.CopyEnemy(original);

Console.WriteLine($"\nOriginal model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;
Console.WriteLine($"\nWe changed the COPY's weapon damage to 999.");
Console.WriteLine($"Original's weapon damage is now: {original.Weapon.Damage}");

copy.Abilities.Add("Fire Breath");
Console.WriteLine($"Original abilities: {string.Join(", ", original.Abilities)}");

#region AFTER Using Prototype
Console.WriteLine("\n=== PROTOTYPE: AFTER ===\n");

// R1: Create one prototype and clone it instead of
// loading the 3D model again for every Orc.
sw = Stopwatch.StartNew();

army = new List<Enemy>();

Enemy prototype = new Orc();
prototype.Name = "Orc-Prototype";

army.Add(prototype);

for (int i = 2; i <= 5; i++)
{
    var orc = prototype.Clone();
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}

Console.WriteLine($"Created 5 orcs in {sw.ElapsedMilliseconds} ms\n");

// R3 + R4: Verify that the clone keeps the same model data
// and that its Weapon and Abilities are independent.
original = new Orc();
original.Name = "Boss Orc";

copy = EnemyCopyHelper.CopyEnemy(original);

Console.WriteLine($"Original model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;

Console.WriteLine("\nWe changed the COPY's weapon damage to 999.");
Console.WriteLine($"Original's weapon damage is now: {original.Weapon.Damage}");

copy.Abilities.Add("Fire Breath");

Console.WriteLine(
    $"Original abilities: {string.Join(", ", original.Abilities)}");


// R5 Bonus: Prototype Registry
Console.WriteLine("\n=== PROTOTYPE REGISTRY ===");

var registry = new EnemyPrototypeRegistry();

var orcPrototype = new Orc();
registry.Register("orc", orcPrototype);

var elfPrototype = new Elf();
registry.Register("elf", elfPrototype);

var orc1 = registry.Create("orc");
var orc2 = registry.Create("orc");

Console.WriteLine($"Prototype model id: {orcPrototype.ModelId}");
Console.WriteLine($"Orc 1 model id:     {orc1.ModelId}");
Console.WriteLine($"Orc 2 model id:     {orc2.ModelId}");

Console.WriteLine(
    $"Orc 1 is same object as prototype? " +
    $"{ReferenceEquals(orcPrototype, orc1)}");

Console.WriteLine(
    $"Orc 2 is same object as Orc 1? " +
    $"{ReferenceEquals(orc1, orc2)}");
#endregion


#region BUILDER VALIDATION
Console.WriteLine("\n=== BUILDER VALIDATION ===");

try
{
    var invalidLive = new CourseRegistrationBuilder()
        .ForStudent("test@mail.com")
        .ForCourse("SEF-101")
        .AsLiveGroup()
        .Build();

    Console.WriteLine("ERROR: Invalid LiveGroup was accepted.");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"LiveGroup validation: {ex.Message}");
}

try
{
    var invalidVideos = new CourseRegistrationBuilder()
        .ForStudent("test@mail.com")
        .ForCourse("SEF-101")
        .AsVideosOnly()
        .WithGroupCode("G1")
        .Build();

    Console.WriteLine("ERROR: Invalid VideosOnly was accepted.");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"VideosOnly validation: {ex.Message}");
}
#endregion


Console.WriteLine("\n=== BUILDER: BEFORE ===\n");
Console.WriteLine(RegistrationCallSites.CreateLiveStudentUgly());
Console.WriteLine(RegistrationCallSites.CreateVideosOnlyUgly());
