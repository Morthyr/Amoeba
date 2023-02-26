using System.Drawing;

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

    public static string StateToString(bool?[,] state) => new string(state.Cast<bool?>().Select(c => c switch
    {
        true => 'x',
        false => 'o',
        _ => '.'
    }).ToArray());
}