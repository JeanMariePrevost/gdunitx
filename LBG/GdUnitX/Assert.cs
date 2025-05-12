
using System;
using System.Collections.Generic;
using GD = GdUnit4.Assertions;

namespace LBG.GdUnitX;

/// <summary>
/// A static assertion class that wraps GDUnit4 assertions with a clearer, less error-prone,
/// C#-idiomatic API inspired by xUnit-style testing.
///
/// This is a lightweight C#-style wrapper around GDUnit4's existing assertions.
/// It is not a replacement for GDUnit4, but provides a more familiar API for C# developers.
/// </summary>
/// <example>
/// <code>
/// using GdUnit4; // GDUnit4 is required to access the decorators
/// using LBG.GdUnitX.Assert; // Gives you access to the xUnit-style assertions
///
/// [TestCase]
/// public void ExampleTest() { // Punctual, sync test
///     var positionBefore = object.Position;
///     object.MoveRight();
///     var positionAfter = object.Position;
///     Assert.GreaterThan(positionAfter.X, positionBefore.X);
///     Assert.Equal(positionBefore.Y, positionAfter.Y, 0.00001f);
/// }
///
/// [TestCase(Timeout = 2500)]
/// public async Task ExampleTest() { // Asynchronous test with a 2500ms timeout
///     bool callbackWorked = false;
///     new Sequence()
///     .WaitForSignal(button, "pressed")
///     .WaitRealSeconds(1.0f)
///     .Do(() => callbackWorked = true)
///     .Start();
///     await Task.Delay(2000);
///     Assert.True(callbackWorked);
/// </code>
/// </example>
public static class Assert {
    // ------------------------------
    // Core
    // ------------------------------

    /// <summary>Asserts that two values are equal.</summary>
    /// <remarks> Use <see cref="ApproxEqual"/> for floating-point comparisons. </remarks>
    public static void Equal<T>(T expected, T actual) {
        GD.AssertThat(actual).IsEqual(expected);
    }

    /// <summary>Asserts that two values are not equal.</summary>
    /// <remarks> Use <see cref="NotApproxEqual"/> for floating-point comparisons. </remarks>
    public static void NotEqual<T>(T notExpected, T actual) {
        GD.AssertThat(actual).IsNotEqual(notExpected);
    }

    /// <summary>Asserts that a value is null.</summary>
    public static void Null(object? value) {
        GD.AssertThat(value).IsNull();
    }

    /// <summary>Asserts that a value is not null.</summary>
    public static void NotNull(object? value) {
        GD.AssertThat(value).IsNotNull();
    }

    /// <summary>Asserts that two objects are the same instance.</summary>
    public static void Same(object expected, object actual) {
        GD.AssertObject(actual).IsSame(expected);
    }

    /// <summary>Asserts that two objects are not the same instance.</summary>
    public static void NotSame(object expected, object actual) {
        GD.AssertObject(actual).IsNotSame(expected);
    }

    /// <summary>Asserts that an object is of a specific type.</summary>
    public static void IsType<T>(object value) {
        GD.AssertObject(value).IsInstanceOf<T>();
    }

    /// <summary>Asserts that an object is not of a specific type.</summary>
    public static void IsNotType<T>(object value) {
        GD.AssertObject(value).IsNotInstanceOf<T>();
    }

    /// <summary>Asserts that an action throws an exception of a specific type.</summary>
    /// <remarks> Fails if the action does not throw an exception or throws an exception of a different type. </remarks>
    public static void Throws<TException>(Action action)
        where TException : Exception {
        var thrown = GD.AssertThrown(action);
        thrown.IsInstanceOf<TException>();
    }

    /// <summary>Manually fails the test</summary>
    public static void Fail() {
        GD.AssertThat(false).IsTrue();
    }

    // ------------------------------
    // Floating-point
    // ------------------------------
    /// <summary>Asserts that a floating-point value is "equal to" another.</summary>
    public static void ApproxEqual(float expected, float actual, float delta = 0.00001f) {
        InRange(actual, expected - delta, expected + delta);
    }

    /// <summary>Asserts that a floating-point value is not "equal to" another.</summary>
    public static void NotApproxEqual(float notExpected, float actual, float delta = 0.00001f) {
        NotInRange(actual, notExpected - delta, notExpected + delta);
    }

    /// <summary>Asserts that a double-precision floating-point value is "equal to" another.</summary>
    public static void ApproxEqual(double expected, double actual, double delta = 0.000000000000001) {
        InRange(actual, expected - delta, expected + delta);
    }

    /// <summary>Asserts that a double-precision floating-point value is not "equal to" another.</summary>
    public static void NotApproxEqual(double notExpected, double actual, double delta = 0.000000000000001) {
        NotInRange(actual, notExpected - delta, notExpected + delta);
    }

    // ------------------------------
    // Boolean
    // ------------------------------
    /// <summary>Asserts that a condition is true.</summary>
    public static void True(bool condition) {
        GD.AssertThat(condition).IsTrue();
    }

    /// <summary>Asserts that a condition is false.</summary>
    public static void False(bool condition) {
        GD.AssertThat(condition).IsFalse();
    }

    // ------------------------------
    // Comparison
    // ------------------------------
    /// <summary>Asserts that one value is greater than another.</summary>
    public static void GreaterThan<T>(T a, T b) where T : IComparable<T> {
        GD.AssertThat(a.CompareTo(b) > 0).IsTrue();
    }

    /// <summary>Asserts that one value is less than another.</summary>
    public static void LessThan<T>(T a, T b) where T : IComparable<T> {
        GD.AssertThat(a.CompareTo(b) < 0).IsTrue();
    }

    /// <summary>Asserts that one value is greater than or equal to another.</summary>
    public static void GreaterThanOrEqual<T>(T a, T b) where T : IComparable<T> {
        GD.AssertThat(a.CompareTo(b) >= 0).IsTrue();
    }

    /// <summary>Asserts that one value is less than or equal to another.</summary>
    public static void LessThanOrEqual<T>(T a, T b) where T : IComparable<T> {
        GD.AssertThat(a.CompareTo(b) <= 0).IsTrue();
    }

    /// <summary>Asserts that a value is within a range (inclusive).</summary>
    public static void InRange<T>(T value, T min, T max) where T : IComparable<T> {
        bool inRange = value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        GD.AssertThat(inRange).IsTrue();
    }

    /// <summary>Asserts that a value is not within a range (exclusive).</summary>
    public static void NotInRange<T>(T value, T min, T max) where T : IComparable<T> {
        bool inRange = value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        GD.AssertThat(!inRange).IsTrue();
    }

    // ------------------------------
    // String-specific
    // ------------------------------
    /// <summary>Asserts that a string contains a substring.</summary>
    public static void Contains(string expectedSubstring, string actual) {
        GD.AssertThat(actual).Contains(expectedSubstring);
    }

    /// <summary>Asserts that a string does not contain a substring.</summary>
    public static void DoesNotContain(string expectedSubstring, string actual) {
        GD.AssertThat(actual).NotContains(expectedSubstring);
    }

    /// <summary>Asserts that a string starts with a prefix.</summary>
    public static void StartsWith(string expectedPrefix, string actual) {
        GD.AssertThat(actual).StartsWith(expectedPrefix);
    }

    /// <summary>Asserts that a string ends with a suffix.</summary>
    public static void EndsWith(string expectedSuffix, string actual) {
        GD.AssertThat(actual).EndsWith(expectedSuffix);
    }

    // ------------------------------
    // Collection-specific
    // ------------------------------
    /// <summary>Asserts that a collection is empty.</summary>
    public static void IsEmpty<T>(ICollection<T> collection) {
        GD.AssertThat(collection).IsEmpty();
    }

    /// <summary>Asserts that a collection is not empty.</summary>
    public static void IsNotEmpty<T>(ICollection<T> collection) {
        GD.AssertThat(collection).IsNotEmpty();
    }

    /// <summary>Asserts that a collection contains an item.</summary>
    public static void Contains<T>(T item, ICollection<T> collection) {
        GD.AssertThat(collection).Contains(item);
    }

    /// <summary>Asserts that a collection does not contain an item.</summary>
    public static void DoesNotContain<T>(T item, ICollection<T> collection) {
        GD.AssertThat(collection).NotContains(item);
    }

    /// <summary>Asserts that a collection has a specific count.</summary>
    public static void CountEquals<T>(int expectedCount, ICollection<T> collection) {
        GD.AssertInt(collection.Count).IsEqual(expectedCount);
    }

    /// <summary>Asserts that a collection does not have a specific count.</summary>
    public static void CountNotEquals<T>(int expectedCount, ICollection<T> collection) {
        GD.AssertInt(collection.Count).IsNotEqual(expectedCount);
    }

}