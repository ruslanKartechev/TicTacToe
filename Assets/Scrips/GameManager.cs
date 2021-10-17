using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe.Display;
using TicTacToe.EventSystem;
public class Singleton<T>: MonoBehaviour where T: MonoBehaviour 
{
    private static T instance;
    public static T Instance 
    { get {
            if (instance == null)
                instance = GameObject.FindObjectOfType<T>();
            return instance; } 
    }
}
namespace TicTacToe
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : Singleton<GameManager>
    {


        public GridManager gridManager;
        public RandomGenerator answerGenerator;
        public AnswerChecker answerChecker;
        public WinnerDisplay winnerDisplayer;
        public UImanager uiManager;
        public EventManager eventManager;
        public TTTManager tttManager;

        private void Awake()
        {
            if(gridManager == null)
                gridManager = GetComponent<GridManager>();
            if (answerGenerator == null)
                answerGenerator = GetComponent<RandomGenerator>();
            if (answerChecker == null)
                answerChecker = GetComponent<AnswerChecker>();
            if (uiManager == null)
                uiManager = GetComponent<UImanager>();
            if (eventManager == null)
                eventManager = GetComponent<EventManager>();
            if (tttManager == null)
                tttManager = GetComponent<TTTManager>();

            eventManager.GameExit.AddListener(OnExitGame);
        }
        private void Start()
        {
            eventManager.EnterStartingScreen.Invoke();
        }
        private void OnExitGame()
        {
            Application.Quit();
        }

    }
}