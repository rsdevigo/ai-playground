using UnityEngine;

public enum FOXSTATES {
    PATROL = 0,
    DANCE,
    RUN
}


public class FoxController : MonoBehaviour
{
    FSM _fsm;
    [SerializeField]
    public Transform[] waypoints;
    [SerializeField]
    float _patrolSpeed = 2f;
    [SerializeField]
    float _runSpeed = 5f;
    [SerializeField]
    public Animator animator;
    [SerializeField]
    Transform[] _runWaypoints;
    public int currentWaypoint = 0;
    [SerializeField]
    public float timerDance;
    [SerializeField]
    public float danceDuration = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWaypoint = 0;
        _fsm = new FSM();
        _fsm.states[FOXSTATES.PATROL] = new PatrolState(_fsm, this, "walk");
        _fsm.states[FOXSTATES.DANCE] = new DanceState(_fsm, this, "dance");
        _fsm.SetCurrentState(_fsm.states[FOXSTATES.PATROL]);
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.Update();
    }

    public void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypoint];
        _MoveTo(target, _patrolSpeed);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypoint++;                
        }
    }

    void Run()
    {
        if (_runWaypoints.Length == 0) return;

        if (currentWaypoint >= _runWaypoints.Length)
        {
            if (animator.GetBool("run"))
            {
                animator.SetBool("idle", true);
                animator.SetBool("run", false);
            }

            return;
        }

        if (!animator.GetBool("run"))
        {
            animator.SetBool("idle", false);
            animator.SetBool("run", true);
        }


        Transform target = _runWaypoints[currentWaypoint];
        _MoveTo(target, _runSpeed);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypoint++;
        }
    }


    void _MoveTo(Transform target, float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
