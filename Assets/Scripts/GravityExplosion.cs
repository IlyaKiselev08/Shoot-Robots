using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityExplosion : MonoBehaviour
{
    [SerializeField]
    private float _power;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _upwards;
    [SerializeField]
    private ParticleSystem _explosionParicle;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartExplosion();
        }
    }
    private void StartExplosion()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _radius);
        foreach (Collider coll in colliders)
        {
            Rigidbody rb = coll.GetComponent<Rigidbody>();
            if (rb != null)
            {
                _rb.useGravity = false;
            }
        }
        _explosionParicle.Play();
    }
}
