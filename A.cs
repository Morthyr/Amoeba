using System.Drawing;

class A
{
    public int X { get; set; }
    public int Y { get; set; }
    public static implicit operator Point(A action) => new Point(action.X, action.Y);
    public static implicit operator A(Point p) => new () { X = p.X, Y = p.Y };
}
