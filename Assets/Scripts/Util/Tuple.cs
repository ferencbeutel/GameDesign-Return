public class Tuple<T, R>
{
    T left;
    R right;

    public override string ToString()
    {
        return "Left: " + left.ToString() + " | Right: " + right.ToString();
    }

    public Tuple(T left, R right)
    {
        this.left = left;
        this.right = right;
    }

    public T GetLeft()
    {
        return this.left;
    }

    public R GetRight()
    {
        return this.right;
    }
}
