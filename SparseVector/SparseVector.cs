namespace SparseVector;

/// <summary>
/// Vector that doesn't store zeroes.
/// </summary>
public class SparseVector
{
    private VectorUnit? head;

    /// <summary>
    /// Initializes a new instance of the <see cref="SparseVector"/> class.
    /// </summary>
    /// <param name="values">Int array of values to be stored in vector.</param>
    public SparseVector(params int[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] != 0)
            {
                if (this.head == null)
                {
                    this.head = new VectorUnit(i, values[i]);
                }
                else
                {
                    this.head.AddValue(i, values[i]);
                }
            }
        }
    }

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="v1">First vector.</param>
    /// <param name="v2">Second vector.</param>
    /// <returns>Result of addition.</returns>
    public static SparseVector Add(SparseVector v1, SparseVector v2)
        => SparseVector.Operator(v1, v2, (a, b) => a + b);

    /// <summary>
    /// Subtracts two vectors.
    /// </summary>
    /// <param name="v1">First vector.</param>
    /// <param name="v2">Second vector.</param>
    /// <returns>Subtraction result.</returns>
    public static SparseVector Sub(SparseVector v1, SparseVector v2)
        => SparseVector.Operator(v1, v2, (a, b) => a - b);

    /// <summary>
    /// Scalar multiplies two vectors.
    /// </summary>
    /// <param name="v1">First vector.</param>
    /// <param name="v2">Second vector.</param>
    /// <returns>Result of scalar multiplication.</returns>
    public static int Mult(SparseVector v1, SparseVector v2)
    {
        var result = 0;
        var a = v1.head;
        var b = v2.head;
        while (a != null && b != null)
        {
            if (a.Index == b.Index)
            {
                result += a.Value * b.Value;
                a = a.Next;
                b = b.Next;
            }
            else if (a.Index < b.Index)
            {
                a = a.Next;
            }
            else
            {
                b = b.Next;
            }
        }

        return result;
    }

    /// <summary>
    /// Checks if vector is zero.
    /// </summary>
    /// <returns>True if vector is zero, false otherwise.</returns>
    public bool IsZeroVector()
        => this.head == null;

    /// <summary>
    /// Returns the string representation of this vector where numbers is splitted with space.
    /// </summary>
    /// <returns>String with vector representation.</returns>
    public override string ToString()
    {
        var output = string.Empty;
        var cursor = this.head;
        var prevIndex = -1;
        while (cursor != null)
        {
            for (int i = 0; i < cursor.Index - prevIndex - 1; i++)
            {
                output += "0 ";
            }

            output += $"{cursor.Value} ";
            prevIndex = cursor.Index;
            cursor = cursor.Next;
        }

        return output.Trim();
    }

    private static SparseVector Operator(SparseVector v1, SparseVector v2, Func<int, int, int> op)
    {
        var newVector = new SparseVector();
        var cursor1 = v1.head;
        var cursor2 = v2.head;

        while (cursor1 != null || cursor2 != null)
        {
            if (cursor1 != null && cursor2 != null)
            {
                if (cursor1.Index < cursor2.Index)
                {
                    newVector.AddValue(cursor1.Index, op(cursor1.Value, 0));
                    cursor1 = cursor1.Next;
                    continue;
                }

                if (cursor1.Index == cursor2.Index)
                {
                    newVector.AddValue(cursor1.Index, op(cursor1.Value, cursor2.Value));
                    cursor1 = cursor1.Next;
                    cursor2 = cursor2.Next;
                    continue;
                }

                newVector.AddValue(cursor2.Index, op(0, cursor2.Value));
                cursor2 = cursor2.Next;
                continue;
            }

            if (cursor1 != null)
            {
                newVector.AddValue(cursor1.Index, op(cursor1.Value, 0));
                cursor1 = cursor1.Next;
                continue;
            }

            newVector.AddValue(cursor2.Index, op(0, cursor2.Value));
            cursor2 = cursor2.Next;
            continue;
        }

        return newVector;
    }

    private void AddValue(int index, int value)
    {
        if (value == 0)
        {
            return;
        }

        if (this.head == null)
        {
            this.head = new VectorUnit(index, value);
            return;
        }

        this.head.AddValue(index, value);
    }

    private class VectorUnit
    {
        public VectorUnit(int index, int value)
        {
            this.Index = index;
            this.Value = value;
        }

        public int Value { get; set; }

        public VectorUnit? Next { get; set; }

        public int Index { get; set; }

        public void AddValue(int index, int value)
        {
            if (this.Index < index)
            {
                if (this.Next == null)
                {
                    this.Next = new VectorUnit(index, value);
                }
                else
                {
                    this.Next.AddValue(index, value);
                }
            }
            else
            {
                var oldUnit = new VectorUnit(this.Index, this.Value)
                { Next = this.Next };
                this.Index = index;
                this.Value = value;
                this.Next = oldUnit;
            }
        }
    }
}