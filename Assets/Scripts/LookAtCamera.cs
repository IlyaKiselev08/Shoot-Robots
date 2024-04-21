using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTransform;
    private RectTransform _rectTransform;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Vector3 _offset;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_cameraTransform.forward, _cameraTransform.up);
        transform.position = _playerTransform.position + _offset;
        
    }
}
