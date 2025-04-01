using CruxEngine.Core.Models.Vectors;

namespace CruxEngine.Core.Models.Matrixes;

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
    /// Creates a 3D Matrix from a 2 dimensional array.
    /// </summary>
    /// <param name="matrix">The 2 dimansional array to be used to fill the matrix.</param>
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
}
