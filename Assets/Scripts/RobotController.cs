using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private Rigidbody[] _rigidbodies;
    private float _health;
    [SerializeField]
    private Slider _hpSlider;
    [SerializeField]
    private float _maxHp;
    [SerializeField]
    private TextMeshProUGUI _hpText;
    [SerializeField]
    private AudioClip _dieAudio;
    private AudioSource _audioSource;
    private RobotPatrol _robotPatrol;
    private bool _isDie;
    public bool IsDie => _isDie;
    private GameManager _gameManager;
    
    void Start()
    {
        _animator = GetComponent <Animator>();
        _audioSource = GetComponent<AudioSource>();
        Revival();
        _robotPatrol = GetComponent<RobotPatrol>();
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("Game Manager");
        _gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Die();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Revival();
        }
    }
    private void Revival()
    {
        _hpText.text = _maxHp.ToString();
        _health = _maxHp;
        _hpSlider.maxValue = _maxHp;
        _hpSlider.value = _health;
        _animator.enabled = true;
        foreach(Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
    }
    private void Die()
    {
        _animator.enabled = false;
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = false;
        }
        _audioSource.PlayOneShot(_dieAudio);
        _robotPatrol.StopPatrol();
        _isDie = true;
        _gameManager.CheckWinGame();
    }
    public void ApplyDamage(float damage)
    {
        _health -= damage;
        if(_health < 0)
        {
            Die();
            _health = 0;
        }
        _hpSlider.value = _health;
        _hpText.text = _health.ToString("F2");

        
    }
    
}
