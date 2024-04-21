using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionController : MonoBehaviour
{
    private Camera _camera;
    [SerializeField]
    private GameObject _explosionPrefab;
    private float _timer;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private Slider _durationSlider;
    [SerializeField]
    private TextMeshProUGUI _durationText;
    void Start()
    {
        _camera = GetComponent<Camera>();
        _durationSlider.maxValue = _duration;
        _durationSlider.value = _duration;
        _durationText.text = _duration.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > _timer)
        {
            SpawnExplosion();
        }
    }
    private void SpawnExplosion()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Instantiate(_explosionPrefab, hit.point, Quaternion.identity);
            _timer = Time.time + _duration;
            StartCoroutine(SliderCounting());
        }
    }
    private IEnumerator SliderCounting()
    {
        float timer = _duration;
        while(timer>0)
        {
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
            _durationSlider.value = timer;
            _durationText.text = timer.ToString("F2");
        }
        _durationSlider.value = _duration;
        _durationText.text = _duration.ToString();
    }
}
