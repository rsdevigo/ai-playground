using UnityEngine;

public class FoxController : MonoBehaviour
{

    [SerializeField]
    Transform[] _waypoints;
    [SerializeField]
    float _patrolSpeed = 2f;
    [SerializeField]
    float _runSpeed = 5f;
    [SerializeField]
    Animator _animator;

    int _currentWaypoint = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator.SetBool("idle", true);
        _currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.Dance();
    }

    public void Patrol()
    {
        if (_waypoints.Length == 0) return;

        if (!_animator.GetBool("walk"))
        {
            _animator.SetBool("idle", false);
            _animator.SetBool("walk", true);
        }
            

        Transform target = _waypoints[_currentWaypoint];
        _MoveTo(target, _patrolSpeed);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            _currentWaypoint++;

            if (_currentWaypoint >= _waypoints.Length)
                _currentWaypoint = 0;
        }
    }

    void Run()
    {
        if (_waypoints.Length == 0) return;

        if (!_animator.GetBool("run"))
        {
            _animator.SetBool("idle", false);
            _animator.SetBool("run", true);
        }
            

        Transform target = _waypoints[_currentWaypoint];
        _MoveTo(target, _runSpeed);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            _currentWaypoint++;

            if (_currentWaypoint >= _waypoints.Length)
                _currentWaypoint = 0;
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

    public void Dance()
    {
        if (!_animator.GetBool("dance"))
        {
            _animator.SetBool("idle", false);
            _animator.SetBool("dance", true);
        }
    }
}
