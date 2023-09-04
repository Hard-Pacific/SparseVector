namespace SparseVector.Tests;

public class SparseVectorTests
{
    [Test]
    public void AddVectorsTest()
    {
        SparseVector v1 = new();
        SparseVector v2 = new();
        Assert.That(SparseVector.Add(v1, v2).IsZeroVector(), Is.True);

        v1 = new(0, 0, 0);
        v2 = new();
        Assert.That(SparseVector.Add(v1, v2).IsZeroVector(), Is.True);

        v1 = new(0, 0, 1, 0);
        v2 = new(0, 1);
        Assert.That(SparseVector.Add(v1, v2).ToString(), Is.EqualTo("0 1 1"));

        v1 = new(0, -1, 0, 0);
        v2 = new(0, 1);
        Assert.That(SparseVector.Add(v1, v2).IsZeroVector(), Is.True);

        v1 = new(0, 5, 0, 0);
        v2 = new(0, 1, 6, 0, 7);
        Assert.That(SparseVector.Add(v1, v2).ToString(), Is.EqualTo("0 6 6 0 7"));
    }

    [Test]
    public void SubVectorsTest()
    {
        SparseVector v1 = new();
        SparseVector v2 = new();
        Assert.That(SparseVector.Sub(v1, v2).IsZeroVector(), Is.True);

        v1 = new(0, 0, 0);
        v2 = new();
        Assert.That(SparseVector.Sub(v1, v2).IsZeroVector(), Is.True);

        v1 = new(0, 0, 1, 0);
        v2 = new(0, 1, 0, 0, 0, 5);
        Assert.That(SparseVector.Sub(v1, v2).ToString(), Is.EqualTo("0 -1 1 0 0 -5"));

        v1 = new(0, -1, 0, 0);
        v2 = new(0, -1);
        Assert.That(SparseVector.Sub(v1, v2).IsZeroVector(), Is.True);
    }

    [Test]
    public void MultVectorsTest()
    {
        SparseVector v1 = new();
        SparseVector v2 = new();
        Assert.That(SparseVector.Mult(v1, v2), Is.EqualTo(0));

        v1 = new(0, 0, 0);
        v2 = new();
        Assert.That(SparseVector.Mult(v1, v2), Is.EqualTo(0));

        v1 = new(0, 0, 0, 0);
        v2 = new(0, 1, 1, 0, 0, 5);
        Assert.That(SparseVector.Mult(v1, v2), Is.EqualTo(0));

        v1 = new(0, -5, 0, 0);
        v2 = new(0, -6);
        Assert.That(SparseVector.Mult(v1, v2), Is.EqualTo(30));
    }
}