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
    [SerializeField]
    Transform[] _runWaypoints;
    int _currentWaypoint = 0;
    [SerializeField]
    float _timerDance;
    [SerializeField]
    float _danceDuration = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator.SetBool("idle", true);
        _currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.Patrol();
    }

    public void Patrol()
    {
        if (_waypoints.Length == 0) return;

        if (_currentWaypoint >= _waypoints.Length)
        {
            this.Dance();
            return;
        }

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
        }
    }

    void Run()
    {
        if (_runWaypoints.Length == 0) return;

        if (_currentWaypoint >= _runWaypoints.Length)
        {
            if (_animator.GetBool("run"))
            {
                _animator.SetBool("idle", true);
                _animator.SetBool("run", false);
            }

            return;
        }

        if (!_animator.GetBool("run"))
        {
            _animator.SetBool("idle", false);
            _animator.SetBool("run", true);
        }


        Transform target = _runWaypoints[_currentWaypoint];
        _MoveTo(target, _runSpeed);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            _currentWaypoint++;
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
            if (_animator.GetBool("idle"))
                _animator.SetBool("idle", false);
            if (_animator.GetBool("walk"))
                _animator.SetBool("walk", false);
            _animator.SetBool("dance", true);
            _timerDance = _danceDuration;
        }

        _timerDance -= Time.deltaTime;

        if (_timerDance < 0f)
        {
            _animator.SetBool("dance", false);
            _currentWaypoint = 0;
        }
    }
}
