using UnityEngine;

public class PatrolState : State
{
    FoxController _fox;
    string _animationName;
    public PatrolState(FSM fsm, FoxController fox, string animationName) : base(fsm)
    {
        _fox = fox;
        _animationName = animationName;
    }

    public override void Enter()
    {
        Debug.Log(_animationName);
        _fox.animator.SetBool(_animationName, true);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        _fox.animator.SetBool(_animationName, false);
    }

    public override void Update()
    {
        if (_fox.currentWaypoint >= _fox.waypoints.Length)
        {
            _fsm.SetCurrentState(_fsm.states[FOXSTATES.DANCE]);
            return;
        }
        _fox.Patrol();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}