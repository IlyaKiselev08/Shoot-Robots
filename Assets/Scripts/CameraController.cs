using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _multiply;
    private float _speedCache;
    [SerializeField]
    private BorderClamp _borderPositionX;
    [SerializeField]
    private BorderClamp _borderPositionY;
    [SerializeField]
    private BorderClamp _borderPositionZ;
    [SerializeField]
    private float _mouseSpeed;
    private Vector3 _currentEulerAngels;
    [SerializeField]
    private BorderClamp _borderRotationX;
    [SerializeField]
    public Texture2D _eyeIcon;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _padding;
    void Start()
    {
        _speedCache = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        MovementCamera();
        HotKeyMovementCamera();
        BorderControl();
        if(Input.GetMouseButton(1))
        {
            RotateCamera();
        }
        CheckBorderSphere();
    }
    private void MovementCamera()
    {
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * _speed * Time.deltaTime * verticalInput);
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * _speed * Time.deltaTime * horizontalInput);
        if(Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }
    private void HotKeyMovementCamera()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed = _speed * 2;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = _speedCache;
        }
        if(Input.GetMouseButtonDown(1))
        {
            Cursor.SetCursor(_eyeIcon, Vector2.zero, CursorMode.Auto);
        }
        if(Input.GetMouseButtonUp(1))
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    public void BorderControl()
    {
        Vector3 currentPosition = transform.position;
        float clampX = Mathf.Clamp(currentPosition.x, _borderPositionX.GetMinimal, _borderPositionX.GetMaximal);
        float clampY = Mathf.Clamp(currentPosition.y, _borderPositionY.GetMinimal, _borderPositionY.GetMaximal);
        float clampZ = Mathf.Clamp(currentPosition.z, _borderPositionZ.GetMinimal, _borderPositionZ.GetMaximal);
        transform.position = new Vector3(clampX, clampY, clampZ);
    }
    private void RotateCamera()
    {
        float rotationX = Input.GetAxis("Mouse Y");
        float rotationY = Input.GetAxis("Mouse X");
        _currentEulerAngels += new Vector3(-rotationX, rotationY, 0) * (Time.deltaTime * _mouseSpeed);
        _currentEulerAngels.x = Mathf.Clamp(_currentEulerAngels.x, _borderRotationX.GetMinimal, _borderRotationX.GetMaximal);
        transform.eulerAngles = _currentEulerAngels;
    }
    private void CheckBorderSphere()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _padding);
        foreach(Collider coll in colliders)
        {
            if (coll.gameObject.CompareTag("Obstacle"))
            {
                Vector3 direction = (coll.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(coll.transform.position, transform.position);
                transform.position = coll.transform.position - direction * (distance + _padding);
            }
        }
     }
}
[Serializable]
public class BorderClamp
{
    [SerializeField]
    private float _minimal;
    [SerializeField]
    private float _maximal;

    public float GetMinimal => _minimal;
    public float GetMaximal => _maximal;
}

