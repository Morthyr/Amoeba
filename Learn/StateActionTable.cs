namespace Learn;
class StateActionTable
{
    internal Dictionary<StateAction, double> _stateAction = new();

    static object _lockObj = new();

    private double Create(StateAction sa)
    {
        lock(_lockObj)
        {
            int r = 0;
            if(!_stateAction.ContainsKey(sa))
            {
                if(!_stateAction.TryAdd(sa, r))
                {
                    Console.WriteLine("Couldn't set {0} to {1}", sa, r);
                }
            }
            return r;
        }
    }

    public double this[bool?[,] state, A action]
    {
        get 
        {
            StateAction sa = new() { State = (bool?[,])state.Clone(), Action = action };
            if(!_stateAction.ContainsKey(sa))
            {
                return Create(sa);
            }
            return _stateAction[sa];
        }
        set 
        {
            StateAction sa = new() { State = (bool?[,])state.Clone(), Action = action };
            if(!_stateAction.ContainsKey(sa))
            {
                Create(sa);
            }
            _stateAction[sa] = value;
        }
    }
}
