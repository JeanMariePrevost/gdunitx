# GdUnitX - xUnit-style syntax for GdUnit4

**GdUnitX** is a lightweight, C#-idiomatic assertion wrapper for [GDUnit4](https://github.com/MikeSchulze/gdUnit4), inspired by xUnit and designed for use in Godot projects using C#.

It provides a familiar, readable assertion API while avoiding certain pitfalls of GDUnit that I personally kept running into.

The main goals were to have a unified style between my xUnit tests and my "Godot tests"
and to stop myself from making mistakes such as:

```csharp
AssertThat(value == 5);           // ❌ No tests made — this just creating an IAssertBase
AssertThat(value).Equals(5);      // ❌ Reads like a GDUnit assertion — is just a comparison between an IAssertBase and something else
```

Or quirks such as:

```csharp
AssertThat(5).IsEqual(5);     // ✅ Works
AssertThat<int>(5).IsEqual(5); // ❌ Fails due to GDUnit typing idiosyncrasies
```

---

## Style Comparison

GdUnitX does not add any extended features to GdUnit4, which is doing all the work under the hood.

It only aims to replace the syntax with one that mirrors xUnit, for example:

| GDUnit4                          | GdUnitX Equivalent                        |
| -------------------------------- | ----------------------------------------- |
| `AssertThat(5).IsEqual(5);`      | `Assert.Equal(5, 5);`                     |
| `AssertThat(obj).IsNull();`      | `Assert.Null(obj);`                       |
| `AssertThat(flag).IsTrue();`     | `Assert.True(flag);`                      |
| `AssertThrown(() => action);`    | `Assert.Throws<Exception>(() => action);` |
| `AssertThat(list).IsEmpty();`    | `Assert.IsEmpty(list);`                   |
| `AssertThat(str).Contains("x");` | `Assert.Contains("x", str);`              |
| `AssertThat(val).IsGreater(0);`  | `Assert.GreaterThan(val, 0);`             |

---

## Installation

This is a source-level dependency.

1. Simply copy `LBG/GdUnitX/Assert.cs` into your Godot C# project.
2. Add `using static LBG.GdUnitX.Assert;` to your test files.
3. That’s it. You create your tests as you would with GdUnit4, run them using GdUnit4, you simply have access to an alternate syntax.

---

## Minimal Example

```csharp
using GdUnit4;
using LBG.GdUnitX;

[TestSuite]
public class Dummy_Tests {
    [TestCase]
    public void ExampleBasicTest_ShouldPass()
    {
        var x = 4;
        x += 6;
        Assert.Equal(10, x);
    }

    [TestCase]
    public void ExampleListTest_ShouldPass()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.NotEmpty(list);
        Assert.Equal(3, list.Count);
        Assert.Contains(2, list);
    }
}
```

---

## Known Limitations

- GdUnitX sacrifices GDUnit’s richer failure messages for equalities in favor of a less finicky API.
  - This is because I chose to rely on "IsTrue()" to have a single uniform generic interface for all types.
- `Equal(...)` uses `.Equals()`, meaning value equality for primitives, reference equality for most classes

---

## 📜 License

MIT License

> Built on top of [GDUnit4](https://github.com/MikeSchulze/gdUnit4) by Mike Schulze — also licensed under MIT.

---

## 👤 Author

Jean-Marie Prévost
[github.com/JeanMariePrevost](https://github.com/JeanMariePrevost)
