using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float _power;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _upwards;
    [SerializeField]
    private ParticleSystem _explosionParicle;
    [SerializeField]
    private float _defaultDamage;
    void Start()
    {
        StartExplosion();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartExplosion();
        }
    }
    private void StartExplosion()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _radius);
        foreach(Collider coll in colliders)
        {
            float distance = Vector3.Distance(transform.position, coll.transform.position);
            Rigidbody rb = coll.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(_power / distance, explosionPosition, _radius, _upwards);
            }
            BodyPart bodyPart = coll.GetComponent<BodyPart>();
            if (bodyPart != null)
            {
                bodyPart.ApplyDamage(_defaultDamage / distance);
            }
        }
        _explosionParicle.Play();
    }

}
