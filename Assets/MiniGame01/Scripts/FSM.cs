using System.Collections.Generic;

public class FSM
{
    State _currentState;
    public Dictionary<FOXSTATES, State> states;

    public FSM()
    {
        states = new Dictionary<FOXSTATES, State>();
    }
    // Update is called once per frame
    public void Update()
    {
        _currentState?.Update();
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }

    public void SetCurrentState(State newState)
    {
        if (_currentState != null)
            _currentState.Exit();
        _currentState = newState;
        if (_currentState != null)
            _currentState.Enter();
    }
}
