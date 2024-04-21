using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPatrol : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Vector3[] _points;
    private int _currentPointIndex;
    [SerializeField]
    private float _waitTime;
    [SerializeField]
    private Animator _animator;
    private Coroutine _patrolCoroutine;
    void Start()
    {
        transform.position = _points[0];
       _patrolCoroutine = StartCoroutine(Patrol());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Patrol()
    {
        while(true)
        {
            int nextPointIndex = (_currentPointIndex + 1) % _points.Length;
            Vector3 nextPoint = _points[nextPointIndex];
            _animator.SetBool("IsRun", true);
            while (Vector3.Distance(transform.position, nextPoint) > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(nextPoint - transform.position);
                transform.rotation = targetRotation;
                transform.position = Vector3.MoveTowards(transform.position, nextPoint, _speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            _currentPointIndex = nextPointIndex;
            _animator.SetBool("IsRun", false);
            yield return new WaitForSeconds(_waitTime);
        }
    }
    public void StopPatrol()
    {
        if(_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
        }
    }
}
