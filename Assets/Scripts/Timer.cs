using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timerText;
    private float _seconds;
   public void StartTimer(float seconds)
    {
        _seconds = seconds;
        StartCoroutine(TimerCoroutine());
    }
    private IEnumerator TimerCoroutine()
    {
        while(_seconds > 0)
        {
            _seconds -= Time.deltaTime;
            _timerText.text = ConvertTime();
            yield return new WaitForEndOfFrame();
        }
    }
    private string ConvertTime()
    {
        int minutes = (int) ((_seconds%3600) /60);
        int seconds = (int) (_seconds % 60);
        return $"{minutes:D2}:{seconds:D2}";
    }
}
