using CruxEngine.Core.Models.Matrixes;
using CruxEngine.Core.Models.Vectors;

namespace CruxEngine.Core.Unit.Tests.Matrixes;

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
        var source = new float[3, 3]
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
        var source = new float[3, 3]
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
        var source = new float[2, 2]
        {
            { 1, 1},
            { 1, 1},
        };
        
        Assert.Throws<ArgumentException>(() => new Matrix3D(source));
    }

    [Fact]
    public void BigArray2DConstructor_ThrowsArgumentException()
    {
        var source = new float[4, 4]
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
        var source = new float[3, 3]
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
        var source = new float[3, 3]
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
    public void OutOfRange_IndexAccessor_ThrowsIndexOutOfRangeException(int row, int column)
    {
        var matrix = new Matrix3D();

        Assert.Throws<IndexOutOfRangeException>(() => matrix[row, column]);
    }
}
