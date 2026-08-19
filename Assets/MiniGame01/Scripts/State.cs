public abstract class State
{
    protected FSM _fsm;

    public State(FSM fsm)
    {
        _fsm = fsm;
    }
    public virtual void  Update()
    {  
    }

    public virtual void  FixedUpdate()
    {  
    }

    public virtual void  Enter()
    {  
    }

    public virtual void  Exit()
    {  
    }

    
}
