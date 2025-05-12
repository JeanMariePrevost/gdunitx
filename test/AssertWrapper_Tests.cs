// File: AssertWrapperTests.cs
// Location suggestion: res://addons/gdunit_x/tests/AssertWrapperTests.cs

using System;
using System.Collections.Generic;
using GdUnit4;                    // decorators
using GD = GdUnit4.Assertions;   // low‑level asserts
using static LBG.GdUnitX.Assert; // the wrapper under test

namespace LBG.GdUnitX.Tests;

[TestSuite] // marks the class as a GDUnit4 test suite
public partial class AssertWrapper_Tests {
    /* -------------------------------------------------
     * CORE
     * ------------------------------------------------- */

    [TestCase] public void Equal_Pass() => Equal(5, 5); // Fails?
    [TestCase]
    public void Equal_Fail() =>
        GD.AssertThrown(() => Equal(5, 6)).IsInstanceOf<Exception>();

    [TestCase] public void NotEqual_Pass() => NotEqual(5, 6);
    [TestCase]
    public void NotEqual_Fail() =>
        GD.AssertThrown(() => NotEqual(5, 5)).IsInstanceOf<Exception>();

    [TestCase] public void Null_Pass() => Null(null);
    [TestCase]
    public void Null_Fail() =>
        GD.AssertThrown(() => Null("x")).IsInstanceOf<Exception>();

    [TestCase] public void NotNull_Pass() => NotNull("x");
    [TestCase]
    public void NotNull_Fail() =>
        GD.AssertThrown(() => NotNull(null)).IsInstanceOf<Exception>();

    [TestCase]
    public void Same_Pass() {
        var obj = new object();
        Same(obj, obj);
    }
    [TestCase]
    public void Same_Fail() {
        var a = new object(); var b = new object();
        GD.AssertThrown(() => Same(a, b)).IsInstanceOf<Exception>();
    }

    [TestCase]
    public void NotSame_Pass() {
        var a = new object(); var b = new object();
        NotSame(a, b);
    }
    [TestCase]
    public void NotSame_Fail() {
        var obj = new object();
        GD.AssertThrown(() => NotSame(obj, obj)).IsInstanceOf<Exception>();
    }

    [TestCase] public void IsType_Pass() => IsType<string>("hello");
    [TestCase]
    public void IsType_Fail() =>
        GD.AssertThrown(() => IsType<int>("hello")).IsInstanceOf<Exception>();

    [TestCase] public void IsNotType_Pass() => IsNotType<int>("hello");
    [TestCase]
    public void IsNotType_Fail() =>
        GD.AssertThrown(() => IsNotType<string>("hello")).IsInstanceOf<Exception>();

    [TestCase]
    public void Throws_Pass() =>
        Throws<InvalidOperationException>(() => throw new InvalidOperationException());

    [TestCase]
    public void Throws_Fail_NoException() =>
        GD.AssertThrown(() => Throws<InvalidOperationException>(() => { }))
          .IsInstanceOf<Exception>();

    [TestCase]
    public void Throws_Fail_WrongType() =>
        GD.AssertThrown(() => Throws<InvalidOperationException>(() => throw new ArgumentException()))
          .IsInstanceOf<Exception>();

    [TestCase]
    public void Fail_AlwaysFails() =>
        GD.AssertThrown(() => Fail()).IsInstanceOf<Exception>();

    /* -------------------------------------------------
     * FLOATING‑POINT
     * ------------------------------------------------- */

    [TestCase] public void ApproxEqual_Float_Pass() => ApproxEqual(1f, 1.000001f);
    [TestCase]
    public void ApproxEqual_Float_Fail() =>
        GD.AssertThrown(() => ApproxEqual(1f, 1.01f)).IsInstanceOf<Exception>();

    [TestCase] public void NotApproxEqual_Float_Pass() => NotApproxEqual(1f, 1.05f);
    [TestCase]
    public void NotApproxEqual_Float_Fail() =>
        GD.AssertThrown(() => NotApproxEqual(1f, 1.000001f)).IsInstanceOf<Exception>();

    [TestCase] public void ApproxEqual_Double_Pass() => ApproxEqual(1d, 1.0 + 1e-14);
    [TestCase]
    public void ApproxEqual_Double_Fail() =>
        GD.AssertThrown(() => ApproxEqual(1d, 1.1)).IsInstanceOf<Exception>();

    [TestCase] public void NotApproxEqual_Double_Pass() => NotApproxEqual(1d, 1.1);
    [TestCase]
    public void NotApproxEqual_Double_Fail() =>
        GD.AssertThrown(() => NotApproxEqual(1d, 1.0 + 1e-14)).IsInstanceOf<Exception>();

    /* -------------------------------------------------
     * BOOLEAN
     * ------------------------------------------------- */

    [TestCase] public void True_Pass() => True(true);
    [TestCase]
    public void True_Fail() =>
        GD.AssertThrown(() => True(false)).IsInstanceOf<Exception>();

    [TestCase] public void False_Pass() => False(false);
    [TestCase]
    public void False_Fail() =>
        GD.AssertThrown(() => False(true)).IsInstanceOf<Exception>();

    /* -------------------------------------------------
     * COMPARISON
     * ------------------------------------------------- */

    [TestCase] public void GreaterThan_Pass() => GreaterThan(5, 3);
    [TestCase]
    public void GreaterThan_Fail() =>
        GD.AssertThrown(() => GreaterThan(3, 5)).IsInstanceOf<Exception>();

    [TestCase] public void LessThan_Pass() => LessThan(3, 5);
    [TestCase]
    public void LessThan_Fail() =>
        GD.AssertThrown(() => LessThan(5, 3)).IsInstanceOf<Exception>();

    [TestCase] public void GreaterThanOrEqual_Pass() { GreaterThanOrEqual(5, 5); GreaterThanOrEqual(6, 5); }
    [TestCase]
    public void GreaterThanOrEqual_Fail() =>
        GD.AssertThrown(() => GreaterThanOrEqual(4, 5)).IsInstanceOf<Exception>();

    [TestCase] public void LessThanOrEqual_Pass() { LessThanOrEqual(5, 5); LessThanOrEqual(4, 5); }
    [TestCase]
    public void LessThanOrEqual_Fail() =>
        GD.AssertThrown(() => LessThanOrEqual(6, 5)).IsInstanceOf<Exception>();

    [TestCase] public void InRange_Pass() => InRange(5, 1, 10);
    [TestCase]
    public void InRange_Fail() =>
        GD.AssertThrown(() => InRange(15, 1, 10)).IsInstanceOf<Exception>();

    [TestCase] public void NotInRange_Pass() => NotInRange(15, 1, 10);
    [TestCase]
    public void NotInRange_Fail() =>
        GD.AssertThrown(() => NotInRange(5, 1, 10)).IsInstanceOf<Exception>();

    /* -------------------------------------------------
     * STRING
     * ------------------------------------------------- */

    [TestCase] public void String_Contains_Pass() => Contains("hello", "hello world");
    [TestCase]
    public void String_Contains_Fail() =>
        GD.AssertThrown(() => Contains("bye", "hello world")).IsInstanceOf<Exception>();

    [TestCase] public void String_DoesNotContain_Pass() => DoesNotContain("bye", "hello world");
    [TestCase]
    public void String_DoesNotContain_Fail() =>
        GD.AssertThrown(() => DoesNotContain("hello", "hello world")).IsInstanceOf<Exception>();

    [TestCase] public void String_StartsWith_Pass() => StartsWith("he", "hello");
    [TestCase]
    public void String_StartsWith_Fail() =>
        GD.AssertThrown(() => StartsWith("lo", "hello")).IsInstanceOf<Exception>();

    [TestCase] public void String_EndsWith_Pass() => EndsWith("lo", "hello");
    [TestCase]
    public void String_EndsWith_Fail() =>
        GD.AssertThrown(() => EndsWith("he", "hello")).IsInstanceOf<Exception>();

    /* -------------------------------------------------
     * COLLECTION
     * ------------------------------------------------- */

    [TestCase] public void Collection_IsEmpty_Pass() => IsEmpty(new List<int>());
    [TestCase]
    public void Collection_IsEmpty_Fail() =>
        GD.AssertThrown(() => IsEmpty(new List<int> { 1 })).IsInstanceOf<Exception>();

    [TestCase] public void Collection_IsNotEmpty_Pass() => IsNotEmpty(new List<int> { 1 });
    [TestCase]
    public void Collection_IsNotEmpty_Fail() =>
        GD.AssertThrown(() => IsNotEmpty(new List<int>())).IsInstanceOf<Exception>();

    [TestCase]
    public void Collection_Contains_Pass() =>
        Contains(3, new List<int> { 1, 2, 3 });
    [TestCase]
    public void Collection_Contains_Fail() =>
        GD.AssertThrown(() => Contains(4, new List<int> { 1, 2, 3 })).IsInstanceOf<Exception>();

    [TestCase]
    public void Collection_DoesNotContain_Pass() =>
        DoesNotContain(4, new List<int> { 1, 2, 3 });
    [TestCase]
    public void Collection_DoesNotContain_Fail() =>
        GD.AssertThrown(() => DoesNotContain(3, new List<int> { 1, 2, 3 })).IsInstanceOf<Exception>();

    [TestCase]
    public void Collection_CountEquals_Pass() =>
        CountEquals(3, new List<int> { 1, 2, 3 });
    [TestCase]
    public void Collection_CountEquals_Fail() =>
        GD.AssertThrown(() => CountEquals(2, new List<int> { 1, 2, 3 })).IsInstanceOf<Exception>();

    [TestCase]
    public void Collection_CountNotEquals_Pass() =>
        CountNotEquals(2, new List<int> { 1, 2, 3 });
    [TestCase]
    public void Collection_CountNotEquals_Fail() =>
        GD.AssertThrown(() => CountNotEquals(3, new List<int> { 1, 2, 3 })).IsInstanceOf<Exception>();
}

