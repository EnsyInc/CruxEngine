using CruxEngine.Core.Models.Matrices;
using CruxEngine.Core.Models.Vectors;

namespace CruxEngine.Core.Unit.Tests.Matrices;

public class Matrix3DTests
{
    [Fact]
    public void DefaultConstructor_FillsMatrix3DWith0()
    {
        var matrix = new Matrix3D();

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Assert.Equal(0, matrix[i, j]);
            }
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(1.1f)]
    [InlineData(float.Epsilon)]
    [InlineData(float.MinValue)]
    [InlineData(float.MaxValue)]
    public void FloatConstructor_FillsMatrix3DWithValue(float value)
    {
        var matrix = new Matrix3D(value);

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Assert.Equal(value, matrix[i, j]);
            }
        }
    }

    [Fact]
    public void Array2DConstructor_FillsMatrix3DWith2DArrayValues()
    {
        var source = new float[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var matrix = new Matrix3D(source);

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Assert.Equal(source[i, j], matrix[i, j]);
            }
        }
    }

    [Fact]
    public void Array2DConstructor_DoesCopyOf2DArray()
    {
        var source = new float[,]
        {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 }
        };

        var matrix = new Matrix3D(source);
        source[0, 0] = 0;

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Assert.Equal(1, matrix[i, j]);
            }
        }
    }

    [Fact]
    public void SmallArray2DConstructor_ThrowsArgumentException()
    {
        var source = new float[,]
        {
            { 1, 1},
            { 1, 1},
        };
        
        Assert.Throws<ArgumentException>(() => new Matrix3D(source));
    }

    [Fact]
    public void BigArray2DConstructor_ThrowsArgumentException()
    {
        var source = new float[,]
        {
            { 1, 1, 1, 1},
            { 1, 1, 1, 1},
            { 1, 1, 1, 1},
            { 1, 1, 1, 1},
        };

        Assert.Throws<ArgumentException>(() => new Matrix3D(source));
    }

    [Fact]
    public void Vector3DConstructor_FillsMatrix3DWithVector3DValues()
    {
        var v1 = new Vector3D(1, 2, 3);
        var v2 = new Vector3D(4, 5, 6);
        var v3 = new Vector3D(7, 8, 9);

        var matrix = new Matrix3D(v1, v2, v3);

        Assert.Equal(v1.X, matrix[0, 0]);
        Assert.Equal(v1.Y, matrix[1, 0]);
        Assert.Equal(v1.Z, matrix[2, 0]);
        Assert.Equal(v2.X, matrix[0, 1]);
        Assert.Equal(v2.Y, matrix[1, 1]);
        Assert.Equal(v2.Z, matrix[2, 1]);
        Assert.Equal(v3.X, matrix[0, 2]);
        Assert.Equal(v3.Y, matrix[1, 2]);
        Assert.Equal(v3.Z, matrix[2, 2]);
    }

    [Fact]
    public void ColumnAccessor_ReturnsExpectedValue()
    {
        var source = new float[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        var matrix = new Matrix3D(source);

        var column0 = matrix[0];
        var column1 = matrix[1];
        var column2 = matrix[2];

        Assert.Equal(new Vector3D(1, 4, 7), column0);
        Assert.Equal(new Vector3D(2, 5, 8), column1);
        Assert.Equal(new Vector3D(3, 6, 9), column2);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    public void OutOfRange_ColumnAccessor_ThrowsIndexOutOfRangeException(int columnIndex)
    {
        var matrix = new Matrix3D();

        Assert.Throws<IndexOutOfRangeException>(() => matrix[columnIndex]);
    }

    [Fact]
    public void IndexAccessor_ReturnsExpectedValue()
    {
        var source = new float[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        var matrix = new Matrix3D(source);

        var value = 1;
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Assert.Equal(value++, matrix[i, j]);
            }
        }
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(3, 0)]
    [InlineData(0, -1)]
    [InlineData(0, 3)]
    [InlineData(-1, 3)]
    [InlineData(3, -1)]
    [InlineData(-1, -1)]
    [InlineData(3, 3)]
    public void OutOfRange_IndexAccessor_ThrowsIndexOutOfRangeException(int row, int column)
    {
        var matrix = new Matrix3D();

        Assert.Throws<IndexOutOfRangeException>(() => matrix[row, column]);
    }

    [Fact]
    public void IndexSetter_SetsValueCorrectly()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var matrix = new Matrix3D();

        matrix[0, 0] = 1;

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Assert.Equal(i == 0 && j == 0 ? 1 : 0, matrix[i, j]);
            }
        }
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(3, 0)]
    [InlineData(0, -1)]
    [InlineData(0, 3)]
    [InlineData(-1, 3)]
    [InlineData(3, -1)]
    [InlineData(-1, -1)]
    [InlineData(3, 3)]
    public void OutOfRange_IndexSetter_ThrowsIndexOutOfRangeException(int row, int column)
    {
        var matrix = new Matrix3D();

        Assert.Throws<IndexOutOfRangeException>(() => matrix[row, column] = 1);
    }

    [Theory]
    [MemberData(nameof(MatrixEqualityTestCases))]
    public void Matrices_Equality_ReturnsEqual(Matrix3D first, Matrix3D second)
    {
        var areEqual1 = first == second;
        var areEqual2 = second == first;
        var areEqual3 = first.Equals(second);
        var areEqual4 = second.Equals(first);

        Assert.True(areEqual1);
        Assert.True(areEqual2);
        Assert.True(areEqual3);
        Assert.True(areEqual4);
    }

    [Theory]
    [MemberData(nameof(MatrixInequalityTestCases))]
    public void Matrices_Equality_ReturnsNotEqual(Matrix3D first, Matrix3D second)
    {
        var areEqual1 = first == second;
        var areEqual2 = second == first;
        var areEqual3 = first.Equals(second);
        var areEqual4 = second.Equals(first);

        Assert.False(areEqual1);
        Assert.False(areEqual2);
        Assert.False(areEqual3);
        Assert.False(areEqual4);
    }

    [Fact]
    public void Null_Equality_ReturnsNotEqual()
    {
        Matrix3D matrix = new();
        Matrix3D? nullMatrix = null;

#pragma warning disable CA1508
        var areEqual1 = matrix == nullMatrix;
        var areEqual2 = nullMatrix == matrix;
        var areEqual3 = matrix.Equals(nullMatrix);
#pragma warning restore CA1508

        Assert.False(areEqual1);
        Assert.False(areEqual2);
        Assert.False(areEqual3);
    }

    [Fact]
    public void SameMatrix_Equality_ReturnsTrue()
    {
        Matrix3D matrix = new();

        // ReSharper disable once EqualExpressionComparison
        var areEqual1 = matrix == matrix;
        var areEqual2 = matrix.Equals(matrix);

        Assert.True(areEqual1);
        Assert.True(areEqual2);
    }

    [Fact]
    public void MatricesWithSameValues_HashCode_ReturnsSameHash()
    {
        var matrix1 = new Matrix3D(1);
        var matrix2 = new Matrix3D(new Vector3D(1), new Vector3D(1), new Vector3D(1));
        var matrix3 = new Matrix3D(new float[,]
        {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 }
        });

        var hash1 = matrix1.GetHashCode();
        var hash2 = matrix2.GetHashCode();
        var hash3 = matrix3.GetHashCode();

        Assert.Equal(hash1, hash2);
        Assert.Equal(hash1, hash3);
        Assert.Equal(hash2, hash3);
    }

    [Theory]
    [MemberData(nameof(MatricesAdditionTestCases))]
    public void Matrices_Addition_ReturnsExpectedValues(Matrix3D left, Matrix3D right, Matrix3D expected)
    {
        var result1 = left + right;
        var result2 = left + right;

        Assert.Equal(expected, result1);
        Assert.Equal(expected, result2);
        Assert.Equal(result1, result2);
    }

    [Theory]
    [MemberData(nameof(MatricesSubtractionTestCases))]
    public void Matrices_Subtraction_ReturnsExpectedValues(Matrix3D left, Matrix3D right, Matrix3D expected)
    {
        var result = left - right;

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(MatricesScalarMultiplicationTestCases))]
    public void MatrixAndScalar_Multiplication_ReturnsExpectedValues(Matrix3D matrix, float scalar, Matrix3D expected)
    {
        var result1 = matrix * scalar;
        var result2 = scalar * matrix;

        Assert.Equal(expected, result1);
        Assert.Equal(expected, result2);
        Assert.Equal(result1, result2);
    }

    [Theory]
    [MemberData(nameof(MatricesScalarDivisionTestCases))]
    public void MatrixAndScalar_Division_ReturnsExpectedValues(Matrix3D matrix, float scalar, Matrix3D expected)
    {
        var result = matrix / scalar;

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(MatricesMultiplicationTestCases))]
    public void Matrices_Multiplication_ReturnsExpectedValues(Matrix3D left, Matrix3D right, Matrix3D expected)
    {
        var result = left * right;

        Assert.Equal(expected, result);
    }

    public static IEnumerable<TheoryDataRow<Matrix3D, Matrix3D>> MatrixEqualityTestCases
        => [
            (new Matrix3D(), new Matrix3D()),
            (new Matrix3D(1), new Matrix3D(1)),
            (new Matrix3D(1.1f), new Matrix3D(1.1f)),
            (new Matrix3D(float.MinValue), new Matrix3D(float.MinValue)),
            (new Matrix3D(float.MaxValue), new Matrix3D(float.MaxValue)),
            (new Matrix3D(float.Epsilon), new Matrix3D(float.Epsilon)),
            (new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f }
            }), new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f }
            })),
        ];

    public static IEnumerable<TheoryDataRow<Matrix3D, Matrix3D>> MatrixInequalityTestCases
        => [
            (new Matrix3D(1), new Matrix3D(2)),
            (new Matrix3D(1.1f), new Matrix3D(2.2f)),
            (new Matrix3D(float.MinValue), new Matrix3D(float.MaxValue)),
            (new Matrix3D(float.Epsilon), new Matrix3D(float.MinValue)),
            (new Matrix3D(float.Epsilon), new Matrix3D(float.MaxValue)),
            (new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f }
            }), new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 0f }
            })),
        ];

    public static IEnumerable<TheoryDataRow<Matrix3D, Matrix3D, Matrix3D>> MatricesAdditionTestCases
        => [
            (new Matrix3D(), new Matrix3D(), new Matrix3D()),
            (new Matrix3D(1), new Matrix3D(1), new Matrix3D(2)),
            (new Matrix3D(1.1f), new Matrix3D(2.2f), new Matrix3D(3.3f)),
            (new Matrix3D(float.MinValue), new Matrix3D(float.MaxValue), new Matrix3D(float.MinValue + float.MaxValue)),
            (new Matrix3D(float.Epsilon), new Matrix3D(float.MinValue), new Matrix3D(float.Epsilon + float.MinValue)),
            (new Matrix3D(float.Epsilon), new Matrix3D(float.MaxValue), new Matrix3D(float.Epsilon + float.MaxValue)),
            (new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f }
            }), new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f }
            }), new Matrix3D(new[,]
            {
                { 2f, 4f, 6f },
                { 8f, 10f, 12f },
                { 14f, 16f, 18f }
            })),
        ];

    public static IEnumerable<TheoryDataRow<Matrix3D, Matrix3D, Matrix3D>> MatricesSubtractionTestCases
        => [
            (new Matrix3D(), new Matrix3D(), new Matrix3D()),
            (new Matrix3D(1), new Matrix3D(1), new Matrix3D()),
            (new Matrix3D(1.1f), new Matrix3D(2.2f), new Matrix3D(-1.1f)),
            (new Matrix3D(2.2f), new Matrix3D(1.1f), new Matrix3D(1.1f)),
            (new Matrix3D(float.Epsilon), new Matrix3D(float.MinValue), new Matrix3D(float.Epsilon - float.MinValue)),
            (new Matrix3D(float.MinValue), new Matrix3D(float.Epsilon), new Matrix3D(float.MinValue - float.Epsilon)),
            (new Matrix3D(float.Epsilon), new Matrix3D(float.MaxValue), new Matrix3D(float.Epsilon - float.MaxValue)),
            (new Matrix3D(float.MaxValue), new Matrix3D(float.Epsilon), new Matrix3D(float.MaxValue - float.Epsilon)),
            (new Matrix3D(float.MaxValue), new Matrix3D(float.MaxValue), new Matrix3D()),
            (new Matrix3D(float.MinValue), new Matrix3D(float.MinValue), new Matrix3D()),
            (new Matrix3D(float.Epsilon), new Matrix3D(float.Epsilon), new Matrix3D()),
            (new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f }
            }), new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f }
            }), new Matrix3D()),
        ];

    public static IEnumerable<TheoryDataRow<Matrix3D, float, Matrix3D>> MatricesScalarMultiplicationTestCases
        => [
            (new Matrix3D(), 1, new Matrix3D()),
            (new Matrix3D(1), 2.2f, new Matrix3D(2.2f)),
            (new Matrix3D(2.2f), 1.1f, new Matrix3D(2.2f * 1.1f)),
            (new Matrix3D(float.MaxValue), 0.5f, new Matrix3D(float.MaxValue * 0.5f)),
            (new Matrix3D(float.Epsilon), 1, new Matrix3D(float.Epsilon)),
            (new Matrix3D(new Vector3D(1), new Vector3D(2), new Vector3D(3)), 2, new Matrix3D(new Vector3D(2), new Vector3D(4), new Vector3D(6))),
            (new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f }
            }), 2, new Matrix3D(new[,]
            {
                { 2f, 4f, 6f },
                { 8f, 10f, 12f },
                { 14f, 16f, 18f }
            })),
        ];

    public static IEnumerable<TheoryDataRow<Matrix3D, float, Matrix3D>> MatricesScalarDivisionTestCases
        => [
            (new Matrix3D(), 1, new Matrix3D()),
            (new Matrix3D(1), 2.2f, new Matrix3D(1f / 2.2f)),
            (new Matrix3D(2.2f), 1.1f, new Matrix3D(2.2f / 1.1f)),
            (new Matrix3D(float.MaxValue), 10, new Matrix3D(float.MaxValue / 10)),
            (new Matrix3D(float.Epsilon), 1, new Matrix3D(float.Epsilon)),
            (new Matrix3D(new Vector3D(1), new Vector3D(2), new Vector3D(3)), 2, new Matrix3D(new Vector3D(0.5f), new Vector3D(1), new Vector3D(1.5f))),
            (new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f }
            }), 3, new Matrix3D(new[,]
            {
                { 1f/3, 2f/3, 1f },
                { 4f/3, 5f/3, 2f },
                { 7f/3, 8f/3, 3f }
            })),
        ];

    public static IEnumerable<TheoryDataRow<Matrix3D, Matrix3D, Matrix3D>> MatricesMultiplicationTestCases
        => [
            (new Matrix3D(), new Matrix3D(), new Matrix3D()),
            (new Matrix3D(1), new Matrix3D(1), new Matrix3D(3)),
            (new Matrix3D(1.1f), new Matrix3D(1), new Matrix3D(3.3f)),
            (new Matrix3D(new[,]
            {
                { 1f, 2f, 3f },
                { 3f, 2f, 1f },
                { 1f, 2f, 3f }
            }), new Matrix3D(new[,]
            {
                { 4f, 5f, 6f },
                { 6f, 5f, 4f },
                { 4f, 6f, 5f }
            }), new Matrix3D(new[,]
            {
                { 28f, 33f, 29f },
                { 28f, 31f, 31f },
                { 28f, 33f, 29f }
            })),
        ];
}
