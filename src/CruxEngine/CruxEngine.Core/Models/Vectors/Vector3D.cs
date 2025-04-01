namespace CruxEngine.Core.Models.Vectors;

/// <summary>
/// Model representing a 3D vector.
/// </summary>
public sealed record Vector3D
{
    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public float X { get; init; }
    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public float Y { get; init; }
    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    public float Z { get; init; }

    /// <summary>
    /// A reference to a vector with all components set to 0.
    /// </summary>
    public static readonly Vector3D Zero = new(0, 0, 0);

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <remarks>Will set all components to 0.</remarks>
    public Vector3D() { }

    /// <summary>
    /// Constructor that sets all components to the same value.
    /// </summary>
    /// <param name="value">The value for the components.</param>
    /// <remarks>Will set all components to the provided value.</remarks>
    public Vector3D(float value) : this(value, value, value) { }

    /// <summary>
    /// Constructor that sets all components to the values provided.
    /// </summary>
    /// <param name="x">The value for the <see cref="X"/> component.</param>
    /// <param name="y">The value for the <see cref="Y"/> component.</param>
    /// <param name="z">The value for the <see cref="Z"/> component.</param>
    public Vector3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Calculates the magnitude of the vector.
    /// </summary>
    /// <returns>The magnitude of the vector.</returns>
    public float Magnitude()
        => MathF.Sqrt(X * X + Y * Y + Z * Z);

    /// <summary>
    /// Creates a normalized version of the vector.
    /// </summary>
    /// <returns>A new instance of a <see cref="Vector3D"/> with the components normalized.</returns>
    public Vector3D Normalize()
    {
        if (this == Zero)
        {
            return Zero;
        }

        var magnitude = Magnitude();
        return this / magnitude;
    }

    /// <summary>
    /// Negates the vector.
    /// </summary>
    /// <param name="original">The vector to negate</param>
    /// <returns>A new instance of a <see cref="Vector3D"/> with all the components negated.</returns>
    public static Vector3D operator -(Vector3D original)
        => new(-original.X, -original.Y, -original.Z);

    /// <summary>
    /// Adds two vectors together.
    /// </summary>
    /// <param name="left">First vector of the operation.</param>
    /// <param name="right">Second vector of the operation.</param>
    /// <returns>A new instance of a <see cref="Vector3D"/> that represents the sum of the input vectors.</returns>
    public static Vector3D operator +(Vector3D left, Vector3D right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    /// <summary>
    /// Subtracts a vector from another one.
    /// </summary>
    /// <param name="left">The vector from which to subtract.</param>
    /// <param name="right">The vector to subtract.</param>
    /// <returns>A new instance of a <see cref="Vector3D"/> that represents the result of the subtraction of the input vectors.</returns>
    public static Vector3D operator -(Vector3D left, Vector3D right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="original">The vector that needs to be multiplied.</param>
    /// <param name="scalar">The scalar by which to multiply the vector.</param>
    /// <returns>A new instance of a <see cref="Vector3D"/> that represents the original vector multiplied by the scalar</returns>
    public static Vector3D operator *(Vector3D original, float scalar)
        => new(original.X * scalar, original.Y * scalar, original.Z * scalar);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="original">The vector that needs to be multiplied.</param>
    /// <param name="scalar">The scalar by which to multiply the vector.</param>
    /// <returns>A new instance of a <see cref="Vector3D"/> that represents the original vector multiplied by the scalar</returns>
    public static Vector3D operator *(float scalar, Vector3D original)
        => original * scalar;

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="original">The vector that needs to be divided.</param>
    /// <param name="scalar">The scalar by which to divide the vector.</param>
    /// <returns>A new instance of a <see cref="Vector3D"/> that represents the original vector divided by the scalar</returns>
    public static Vector3D operator /(Vector3D original, float scalar)
        => original * (1 / scalar);
}
