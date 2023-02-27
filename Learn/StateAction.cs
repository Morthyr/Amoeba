using System.Drawing;
using System.Text;

namespace Learn;
class StateAction : IEquatable<StateAction>
{
    public bool?[,] State { get; set; }
    public A Action { get; set; }
    
    public bool Equals(StateAction other)
    {
        if(other == null)
            return false;

        bool stateEquals = this.State.Cast<bool?>().SequenceEqual(other.State.Cast<bool?>());
        
        if(!stateEquals)
            return false;

        return (Point)Action == (Point)other.Action;
    }

    public override bool Equals(object? obj) => Equals(obj as StateAction);

    public override int GetHashCode()
    {
        return StateToString(State).GetHashCode() + ((Point)Action).GetHashCode();
    }

    public static string StateToString(bool?[,] state)
    {
        int size = state.GetLength(0);
        StringBuilder builder = new();

        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                builder.Append(state[x, y] switch
                {
                    true => 'x',
                    false => 'o',
                    _ => '.'
                });
            }
        }
        return builder.ToString();
    }
}