namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon { get; set; }
    public List<string> Abilities { get; set; } = new();

    public string ModelId => _modelData;

    // Normal constructor.
    // This represents the expensive creation of the 3D model.
    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);

        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    // Copy constructor.
    // It copies the private model state without loading the model again.
    protected Enemy(Enemy source)
    {
        _modelData = source._modelData;
    }

    // Every concrete Enemy knows how to clone itself.
    public abstract Enemy Clone();
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;

        Weapon = new Weapon
        {
            Name = "Axe",
            Damage = 25
        };

        Abilities.Add("Rage");
    }

    // Private copy constructor used by Clone().
    private Orc(Orc source) : base(source)
    {
        Name = source.Name;
        Health = source.Health;

        // Deep copy of Weapon.
        Weapon = new Weapon
        {
            Name = source.Weapon.Name,
            Damage = source.Weapon.Damage
        };

        // Create a new List instead of sharing the original List.
        Abilities = new List<string>(source.Abilities);
    }

    public override Enemy Clone()
    {
        return new Orc(this);
    }
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;

        Weapon = new Weapon
        {
            Name = "Bow",
            Damage = 18
        };

        Abilities.Add("Stealth");
    }

    // Private copy constructor used by Clone().
    private Elf(Elf source) : base(source)
    {
        Name = source.Name;
        Health = source.Health;

        // Deep copy of Weapon.
        Weapon = new Weapon
        {
            Name = source.Weapon.Name,
            Damage = source.Weapon.Damage
        };

        // Create a new List instead of sharing the original List.
        Abilities = new List<string>(source.Abilities);
    }

    public override Enemy Clone()
    {
        return new Elf(this);
    }
}

public static class EnemyCopyHelper
{
    public static Enemy CopyEnemy(Enemy e)
    {
        return e.Clone();
    }
}