

/// <summary>
/// Singly-linked node class.
/// </summary>
/// <typeparam name="T">The element type of the node.</typeparam>
public class Node<T>
{
    public T data;
    public Node<T> next;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="data">The contents of the node.</param>
    /// <param name="next">The node this one points to.</param>
	public Node(T data, Node<T> next)
	{
        this.data = data;
        this.next = next;
	}
}
