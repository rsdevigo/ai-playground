using UnityEngine;

public class DanceState : State
{
    FoxController _fox;
    string _animationName;
    public DanceState(FSM fsm, FoxController fox, string animationName) : base(fsm)
    {
        _fox = fox;
        _animationName = animationName;
    }

    public override void Enter()
    {
        _fox.animator.SetBool(_animationName, true);
        _fox.timerDance = _fox.danceDuration;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        _fox.animator.SetBool(_animationName, false);
    }

    public override void Update()
    {
        _fox.timerDance -= Time.deltaTime;

        if (_fox.timerDance < 0f)
        {
            _fox.currentWaypoint = 0;
            _fsm.SetCurrentState(_fsm.states[FOXSTATES.PATROL]);
        }
            
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}