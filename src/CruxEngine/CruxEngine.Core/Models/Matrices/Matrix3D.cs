using CruxEngine.Core.Helpers;
using CruxEngine.Core.Models.Vectors;

namespace CruxEngine.Core.Models.Matrices;

/// <summary>
/// Model representing a 3 by 3 matrix.
/// </summary>
public sealed record Matrix3D
{
    /// <summary>
    /// First index is for the row, second index is for the column.
    /// </summary>
    private readonly float[,] _matrix;

    /// <summary>
    /// Creates a 3D Matrix with all values set to 0.
    /// </summary>
    public Matrix3D() : this(0) { }

    /// <summary>
    /// Creates a 3D Matrix with all values set to the given value.
    /// </summary>
    /// <param name="value">The value to be used to fill the matrix.</param>
    public Matrix3D(float value)
    {
        _matrix = new float[3, 3];
        _matrix[0, 0] = value; _matrix[0, 1] = value; _matrix[0, 2] = value;
        _matrix[1, 0] = value; _matrix[1, 1] = value; _matrix[1, 2] = value;
        _matrix[2, 0] = value; _matrix[2, 1] = value; _matrix[2, 2] = value;
    }

    /// <summary>
    /// Creates a 3D Matrix from a 2-dimensional array.
    /// </summary>
    /// <param name="matrix">The 2-dimensional array to be used to fill the matrix.</param>
    public Matrix3D(float[,] matrix)
    {
        if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
        {
            throw new ArgumentException("Input 2d array must be 3 by 3.", nameof(matrix));
        }
        _matrix = (float[,])matrix.Clone();
    }

    /// <summary>
    /// Creates a 3D Matrix from 3 <see cref="Vector3D"/> instances.
    /// </summary>
    /// <param name="col0">Vector for the first column.</param>
    /// <param name="col1">Vector for the second column.</param>
    /// <param name="col2">Vector for the third column.</param>
    public Matrix3D(Vector3D col0, Vector3D col1, Vector3D col2)
    {
        _matrix = new float[3, 3];
        _matrix[0, 0] = col0.X; _matrix[0, 1] = col1.X; _matrix[0, 2] = col2.X;
        _matrix[1, 0] = col0.Y; _matrix[1, 1] = col1.Y; _matrix[1, 2] = col2.Y;
        _matrix[2, 0] = col0.Z; _matrix[2, 1] = col1.Z; _matrix[2, 2] = col2.Z;
    }

    /// <summary>
    /// Gets a column as a <see cref="Vector3D"/>.
    /// </summary>
    /// <param name="column">The column to retrieve.</param>
    /// <returns>A <see cref="Vector3D"/> with the data from the specified column.</returns>
    public Vector3D this[int column]
    {
        get => new(_matrix[0, column], _matrix[1, column], _matrix[2, column]);
    }

    /// <summary>
    /// Indexer for an entry in the 3D Matrix.
    /// </summary>
    /// <param name="row">The row of the data.</param>
    /// <param name="column">The column of the data.</param>
    /// <returns>The data at the specified row and column.</returns>
    public float this[int row, int column]
    {
        get => _matrix[row, column];
        set => _matrix[row, column] = value;
    }

    /// <summary>
    /// Checks if the current instance is equal to another <see cref="Matrix3D"/>.
    /// </summary>
    /// <param name="other">The <see cref="Matrix3D"/> used for the equality check.</param>
    /// <returns>True if the matrices have equal values, False otherwise.</returns>
    public bool Equals(Matrix3D? other)
    {
        if (other is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Math.Abs(this[0, 0] - other[0, 0]) < FloatHelpers.Precision && Math.Abs(this[0, 1] - other[0, 1]) < FloatHelpers.Precision && Math.Abs(this[0, 2] - other[0, 2]) < FloatHelpers.Precision
            && Math.Abs(this[1, 0] - other[1, 0]) < FloatHelpers.Precision && Math.Abs(this[1, 1] - other[1, 1]) < FloatHelpers.Precision && Math.Abs(this[1, 2] - other[1, 2]) < FloatHelpers.Precision
            && Math.Abs(this[2, 0] - other[2, 0]) < FloatHelpers.Precision && Math.Abs(this[2, 1] - other[2, 1]) < FloatHelpers.Precision && Math.Abs(this[2, 2] - other[2, 2]) < FloatHelpers.Precision;
    }

    /// <inheritdoc />
    public override int GetHashCode()
        => HashCode.Combine(
            HashCode.Combine(this[0, 0], this[0, 1], this[0, 2]),
            HashCode.Combine(this[1, 0], this[1, 1], this[1, 2]),
            HashCode.Combine(this[2, 0], this[2, 1], this[2, 2])
        );

    /// <inheritdoc />
    public override string ToString() 
        => $$"""
           Matrix 3D
           {
                { {{this[0, 0]}}, {{this[0, 1]}}, {{this[0, 2]}} },
                { {{this[1, 0]}}, {{this[1, 1]}}, {{this[1, 2]}} },
                { {{this[2, 0]}}, {{this[2, 1]}}, {{this[2, 2]}} }
           }
           """;

    /// <summary>
    /// Adds two 3D Matrices together.
    /// </summary>
    /// <param name="left">First Matrix of the operation.</param>
    /// <param name="right">Second Matrix of the operation.</param>
    /// <returns>A new instance of <see cref="Matrix3D"/> that represents the sum of the two input matrices.</returns>
    public static Matrix3D operator +(Matrix3D left, Matrix3D right)
    {
        var result = new Matrix3D();
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                result[i, j] = left[i, j] + right[i, j];
            }
        }
        return result;
    }

    /// <summary>
    /// Subtracts two 3D Matrices together.
    /// </summary>
    /// <param name="left">First Matrix of the operation.</param>
    /// <param name="right">Second Matrix of the operation.</param>
    /// <returns>A new instance of <see cref="Matrix3D"/> that represents the result of the subtraction of the two input matrices.</returns>
    public static Matrix3D operator -(Matrix3D left, Matrix3D right)
    {
        var result = new Matrix3D();
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                result[i, j] = left[i, j] - right[i, j];
            }
        }
        return result;
    }

    /// <summary>
    /// Multiplies a 3D Matrix by a scalar.
    /// </summary>
    /// <param name="original">Original matrix.</param>
    /// <param name="scalar">Scalar by which to multiply</param>
    /// <returns>A new instance of <see cref="Matrix3D"/> that represents the original matrix multiplied by the scalar.</returns>
    public static Matrix3D operator *(Matrix3D original, float scalar)
    {
        var result = new Matrix3D();
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                result[i, j] = original[i, j] * scalar;
            }
        }
        return result;
    }

    /// <summary>
    /// Multiplies a 3D Matrix by a scalar.
    /// </summary>
    /// <param name="original">Original matrix.</param>
    /// <param name="scalar">Scalar by which to multiply</param>
    /// <returns>A new instance of <see cref="Matrix3D"/> that represents the original matrix multiplied by the scalar.</returns>
    public static Matrix3D operator *(float scalar, Matrix3D original)
        => original * scalar;

    /// <summary>
    /// Divides a 3D Matrix by a scalar.
    /// </summary>
    /// <param name="original">Original matrix.</param>
    /// <param name="scalar">Scalar by which to divide</param>
    /// <returns>A new instance of <see cref="Matrix3D"/> that represents the original matrix divided by the scalar.</returns>
    public static Matrix3D operator /(Matrix3D original, float scalar)
        => original * (1 / scalar);

    /// <summary>
    /// Multiplies two 3D Matrices together.
    /// </summary>
    /// <param name="left">First Matrix of the operation.</param>
    /// <param name="right">Second Matrix of the operation.</param>
    /// <returns>A new instance of <see cref="Matrix3D"/> that represents the value of the multiplication of the 2 matrices.</returns>
    public static Matrix3D operator *(Matrix3D left, Matrix3D right)
        => new(new[,]
        {
            {left[0, 0] * right[0, 0] + left[0, 1] * right[1, 0] + left[0, 2] * right[2, 0], left[0, 0] * right[0, 1] + left[0, 1] * right[1, 1] + left[0, 2] * right[2, 1], left[0, 0] * right[0, 2] + left[0, 1] * right[1, 2] + left[0, 2] * right[2, 2]},
            {left[1, 0] * right[0, 0] + left[1, 1] * right[1, 0] + left[1, 2] * right[2, 0], left[1, 0] * right[0, 1] + left[1, 1] * right[1, 1] + left[1, 2] * right[2, 1], left[1, 0] * right[0, 2] + left[1, 1] * right[1, 2] + left[1, 2] * right[2, 2]},
            {left[2, 0] * right[0, 0] + left[2, 1] * right[1, 0] + left[2, 2] * right[2, 0], left[2, 0] * right[0, 1] + left[2, 1] * right[1, 1] + left[2, 2] * right[2, 1], left[2, 0] * right[0, 2] + left[2, 1] * right[1, 2] + left[2, 2] * right[2, 2]},
        });
}
