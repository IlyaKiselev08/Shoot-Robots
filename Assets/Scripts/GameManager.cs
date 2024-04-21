using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RobotController[] _robots;
    public GameStatus gameStatus;
    [SerializeField]
    private GameObject _winWindow;
    [SerializeField]
    private GameObject _loseWindow;
    private Timer _timer;
    void Start()
    {
        FindAllRobots();
        _timer = GetComponent<Timer>();
        _timer.StartTimer(120);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FindAllRobots()
    {
        GameObject[] robotObjects = GameObject.FindGameObjectsWithTag("Player");
        if (robotObjects.Length == 0)
        {
            Debug.LogWarning("Роботы не найдены");
            return;
        }
        int counter = 0;
        _robots = new RobotController[robotObjects.Length];
        foreach (var robotObject in robotObjects)
        {
            _robots[counter] = robotObject.GetComponent<RobotController>();
            counter++;
        }
    }
    public void CheckWinGame()
    {
        if (HasAnyRobotAlive() == true)
        {
            return;
        }
        Win();
    }
    public bool HasAnyRobotAlive()
    {
        foreach (var robot in _robots)
        {
            if (robot.IsDie == false)
            {
                return true;
            }
        }
        return false;
    }
    private void Lose()
    {
        if(gameStatus != GameStatus.Process)
        {
            return;
        }
        gameStatus = GameStatus.Lose;
        Instantiate(_loseWindow);
    }
    private void Win()
    {
        if(gameStatus != GameStatus.Process)
        {
            return;
        }
        gameStatus = GameStatus.Win;
        Instantiate(_winWindow);
    }
}
public enum GameStatus
{
    Process, Lose, Win, Pause
}
