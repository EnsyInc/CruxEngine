using CruxEngine.Core.Helpers;
using CruxEngine.Core.Models.Vectors;

namespace CruxEngine.Core.Unit.Tests.Vectors;

public class Vector3DTests
{
    [Fact]
    public void DefaultConstructor_FillsVector3DWith0()
    {
        var vector = new Vector3D();

        Assert.Equal(0, vector.X);
        Assert.Equal(0, vector.Y);
        Assert.Equal(0, vector.Z);
    }

    [Fact]
    public void DefaultConstructor_WithCustomValues_FillsVector3DWithCustomValues()
    {
        var vector = new Vector3D()
        {
            X = 1,
            Y = 2,
            Z = 3
        };

        Assert.Equal(1, vector.X);
        Assert.Equal(2, vector.Y);
        Assert.Equal(3, vector.Z);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1.1f)]
    [InlineData(float.MinValue)]
    [InlineData(float.MaxValue)]
    [InlineData(float.Epsilon)]
    public void SingleFloatConstructor_FillsVector3DWithTheValue(float value)
    {
        var vector = new Vector3D(value);

        Assert.Equal(value, vector.X);
        Assert.Equal(value, vector.Y);
        Assert.Equal(value, vector.Z);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(1.1f, 2.2f, 3.3f)]
    [InlineData(float.Epsilon, float.Epsilon, float.Epsilon)]
    [InlineData(float.MinValue, float.MinValue, float.MinValue)]
    [InlineData(float.MaxValue, float.MaxValue, float.MaxValue)]
    [InlineData(float.MinValue, float.MaxValue, float.Epsilon)]
    public void FloatConstructor_FillsVector3DWithValue(float x, float y, float z)
    {
        var vector = new Vector3D(x, y, z);

        Assert.Equal(x, vector.X, FloatHelpers.Precision);
        Assert.Equal(y, vector.Y, FloatHelpers.Precision);
        Assert.Equal(z, vector.Z, FloatHelpers.Precision);
    }

    [Fact]
    public void GetStaticZeroVector3D_ReturnsSameReference()
    {
        var vector1 = Vector3D.Zero;
        var vector2 = Vector3D.Zero;

        Assert.Same(vector1, vector2);
    }

    [Theory]
    [MemberData(nameof(NormalizeTestCases))]
    public void Normalize_ReturnsExpectedValue(Vector3D vector, Vector3D expected)
    {
        var actual = vector.Normalize();

        Assert.Equal(expected.X, actual.X, FloatHelpers.Precision);
        Assert.Equal(expected.Y, actual.Y, FloatHelpers.Precision);
        Assert.Equal(expected.Z, actual.Z, FloatHelpers.Precision);
    }

    [Theory]
    [InlineData(1, 1, 1, 1.73205078f)]
    [InlineData(0, 0, 0, 0)]
    [InlineData(3, 4, 0, 5)]
    [InlineData(0, 3, 4, 5)]
    [InlineData(1, 2, 2, 3)]
    [InlineData(-1, -1, -1, 1.73205078f)]
    [InlineData(1e-7f, 1e-7f, 1e-7f, 1.73205078e-7f)]
    public void Magnitude_ReturnsExpectedValue(float x, float y, float z, float expectedMagnitude)
    {
        var vector = new Vector3D(x, y, z);

        var actualMagnitude = vector.Magnitude();

        Assert.Equal(expectedMagnitude, actualMagnitude, FloatHelpers.Precision);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-1, -2, -3)]
    [InlineData(0, 0, 0)]
    [InlineData(1.1f, -2.2f, 3.3f)]
    [InlineData(float.MaxValue, float.MinValue, float.Epsilon)]
    public void Negate_ReturnsExpectedValue(float x, float y, float z)
    {
        var vector = new Vector3D(x, y, z);

        var negated = -vector;
        
        Assert.Equal(-x, negated.X, FloatHelpers.Precision);
        Assert.Equal(-y, negated.Y, FloatHelpers.Precision);
        Assert.Equal(-z, negated.Z, FloatHelpers.Precision);
    }

    [Theory]
    [MemberData(nameof(AdditionTestCases))]
    public void Addition_ReturnsExpectedValue(Vector3D first, Vector3D second, Vector3D expected)
    {
        var actual1 = first + second;
        var actual2 = second + first;

        Assert.Equal(expected.X, actual1.X, FloatHelpers.Precision);
        Assert.Equal(expected.Y, actual1.Y, FloatHelpers.Precision);
        Assert.Equal(expected.Z, actual1.Z, FloatHelpers.Precision);
        Assert.Equal(expected.X, actual2.X, FloatHelpers.Precision);
        Assert.Equal(expected.Y, actual2.Y, FloatHelpers.Precision);
        Assert.Equal(expected.Z, actual2.Z, FloatHelpers.Precision);
    }

    [Theory]
    [MemberData(nameof(SubtractionTestCases))]
    public void Subtraction_ReturnsExpectedValue(Vector3D first, Vector3D second, Vector3D expected)
    {
        var actual = first - second;

        Assert.Equal(expected.X, actual.X, FloatHelpers.Precision);
        Assert.Equal(expected.Y, actual.Y, FloatHelpers.Precision);
        Assert.Equal(expected.Z, actual.Z, FloatHelpers.Precision);
    }

    [Theory]
    [MemberData(nameof(MultiplicationTestCases))]
    public void Multiplication_ReturnsExpectedValue(Vector3D vector, float scalar, Vector3D expected)
    {
        var result1 = vector * scalar;
        var result2 = scalar * vector;

        Assert.Equal(expected.X, result1.X, FloatHelpers.Precision);
        Assert.Equal(expected.Y, result1.Y, FloatHelpers.Precision);
        Assert.Equal(expected.Z, result1.Z, FloatHelpers.Precision);
        Assert.Equal(expected.X, result2.X, FloatHelpers.Precision);
        Assert.Equal(expected.Y, result2.Y, FloatHelpers.Precision);
        Assert.Equal(expected.Z, result2.Z, FloatHelpers.Precision);
    }

    [Theory]
    [MemberData(nameof(DivisionTestCases))]
    public void Division_ReturnsExpectedValue(Vector3D vector, float scalar, Vector3D expected)
    {
        var result = vector / scalar;

        Assert.Equal(expected.X, result.X, FloatHelpers.Precision);
        Assert.Equal(expected.Y, result.Y, FloatHelpers.Precision);
        Assert.Equal(expected.Z, result.Z, FloatHelpers.Precision);
    }

    public static IEnumerable<TheoryDataRow<Vector3D, Vector3D, Vector3D>> AdditionTestCases
        => [
            (new Vector3D(1, 2, 3), new Vector3D(4, 5, 6), new Vector3D(5, 7, 9)),
            (new Vector3D(-1, -2, -3), new Vector3D(-4, -5, -6), new Vector3D(-5, -7, -9)),
            (new Vector3D(0, 0, 0), new Vector3D(0, 0, 0), new Vector3D()),
            (new Vector3D(1.1f, 2.2f, 3.3f), new Vector3D(-4.4f, -5.5f, -6.6f), new Vector3D(-3.3f, -3.3f, -3.3f)),
            (new Vector3D(float.MaxValue, float.MaxValue, float.MaxValue), new Vector3D(float.MinValue, float.MinValue, float.MinValue), new Vector3D(float.MaxValue + float.MinValue, float.MaxValue + float.MinValue, float.MaxValue + float.MinValue)),
        ];

    public static IEnumerable<TheoryDataRow<Vector3D, Vector3D, Vector3D>> SubtractionTestCases
        => [
            (new Vector3D(1, 2, 3), new Vector3D(4, 5, 6), new Vector3D(-3, -3, -3)),
            (new Vector3D(4, 5, 6), new Vector3D(1, 2, 3), new Vector3D(3, 3, 3)),
            (new Vector3D(-1, -2, -3), new Vector3D(-4, -5, -6), new Vector3D(3, 3, 3)),
            (new Vector3D(-4, -5, -6), new Vector3D(-1, -2, -3), new Vector3D(-3, -3, -3)),
            (new Vector3D(0, 0, 0), new Vector3D(0, 0, 0), new Vector3D()),
            (new Vector3D(1.1f, 2.2f, 3.3f), new Vector3D(-4.4f, -5.5f, -6.6f), new Vector3D(5.5f, 7.7f, 9.9f)),
            (new Vector3D(-4.4f, -5.5f, -6.6f), new Vector3D(1.1f, 2.2f, 3.3f), new Vector3D(-5.5f, -7.7f, -9.9f)),
            (new Vector3D(float.MaxValue, float.MaxValue, float.MaxValue), new Vector3D(float.MinValue, float.MinValue, float.MinValue), new Vector3D(float.MaxValue - float.MinValue, float.MaxValue - float.MinValue, float.MaxValue - float.MinValue)),
            (new Vector3D(float.MinValue, float.MinValue, float.MinValue), new Vector3D(float.MaxValue, float.MaxValue, float.MaxValue), new Vector3D(float.MinValue - float.MaxValue, float.MinValue - float.MaxValue, float.MinValue - float.MaxValue)),
        ];

    public static IEnumerable<TheoryDataRow<Vector3D, Vector3D>> NormalizeTestCases
        => [
            (new Vector3D(1, 1, 1), new Vector3D(0.57735026f, 0.57735026f, 0.57735026f)),
            (new Vector3D(0, 0, 0), new Vector3D(0, 0, 0)),
            (new Vector3D(3, 4, 0), new Vector3D(0.6f, 0.8f, 0)),
            (new Vector3D(0, 3, 4), new Vector3D(0, 0.6f, 0.8f)),
            (new Vector3D(1, 2, 2), new Vector3D(0.33333334f, 0.66666669f, 0.66666669f)),
            (new Vector3D(-1, -1, -1), new Vector3D(-0.57735026f, -0.57735026f, -0.57735026f)),
            (new Vector3D(1e-7f, 1e-7f, 1e-7f), new Vector3D(1/MathF.Sqrt(3), 1/MathF.Sqrt(3), 1/MathF.Sqrt(3))),
        ];

    public static IEnumerable<TheoryDataRow<Vector3D, float, Vector3D>> MultiplicationTestCases
        => [
            (new Vector3D(1, 2, 3), 2, new Vector3D(2, 4, 6)),
            (new Vector3D(1, 2, 3), 0.5f, new Vector3D(0.5f, 1, 1.5f)),
            (new Vector3D(-1, -2, -3), 2, new Vector3D(-2, -4, -6)),
            (new Vector3D(0, 0, 0), 2, new Vector3D(0, 0, 0)),
            (new Vector3D(1.1f, 2.2f, 3.3f), 2, new Vector3D(2.2f, 4.4f, 6.6f)),
            (new Vector3D(float.MaxValue, float.MaxValue, float.MaxValue), 2, new Vector3D(float.MaxValue * 2, float.MaxValue * 2, float.MaxValue * 2)),
            (new Vector3D(float.MinValue, float.MinValue, float.MinValue), 2, new Vector3D(float.MinValue * 2, float.MinValue * 2, float.MinValue * 2)),
        ];

    public static IEnumerable<TheoryDataRow<Vector3D, float, Vector3D>> DivisionTestCases
        => [
            (new Vector3D(1, 2, 3), 2, new Vector3D(0.5f, 1, 1.5f)),
            (new Vector3D(1, 2, 3), 0.5f, new Vector3D(2, 4, 6)),
            (new Vector3D(-1, -2, -3), 2, new Vector3D(-0.5f, -1, -1.5f)),
            (new Vector3D(0, 0, 0), 2, new Vector3D(0, 0, 0)),
            (new Vector3D(1.1f, 2.2f, 3.3f), 2, new Vector3D(1.1f / 2, 2.2f / 2, 3.3f / 2)),
            (new Vector3D(float.MaxValue, float.MaxValue, float.MaxValue), 2, new Vector3D(float.MaxValue / 2, float.MaxValue / 2, float.MaxValue / 2)),
            (new Vector3D(float.MinValue, float.MinValue, float.MinValue), 2, new Vector3D(float.MinValue / 2, float.MinValue / 2, float.MinValue / 2)),
        ];
}
