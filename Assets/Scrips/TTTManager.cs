using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{


    public class TTTManager : MonoBehaviour
    {
        public int gridSize;
        public Canvas gameCanvas;
        public Sprite cellBackGroundSprite;
        public Sprite crossSprite;
        public Sprite circleSprite;
        public Sprite winnerHighlightSprite;
        public bool showGridFirst;

        public Dictionary<int, string> currentAnswers { get; private set; }
        public List<Transform> currentGrid { get; private set; }
        public Dictionary<int, string> currentWinnerAnswers { get; private set; }
        public string currentWinnerName { get; private set; }

        public string currentMode { get; private set; }
        private List<string> gameModes = new List<string>();
        private void Start()
        {
            InitTTT();
        }
        public void InitTTT()
        {
            currentAnswers = new Dictionary<int, string>();
            currentGrid = new List<Transform>();
            currentWinnerAnswers = new Dictionary<int, string>();
            gameModes.Add("ComputerTurnBased");
            gameModes.Add("ComputerImmidiate");
            ChangeGameMode();
            if (showGridFirst)
                ShowGrid();

        }
        public void SetAnswers(Dictionary<int, string> myAnswers)
        {
            currentAnswers = myAnswers;
        }
        public void SetWinner(Dictionary<int, string> winner, string winnerName)
        {
            currentWinnerAnswers = winner;
            currentWinnerName = winnerName;
        }
        public void CheckAnswers()
        {
            if (currentMode == "ComputerTurnBased")
                return;
            string currentWinnerName;
            (currentWinnerAnswers, currentWinnerName) = GameManager.Instance.answerChecker.CheckAnswers(currentGrid, currentAnswers);
            SetWinner(currentWinnerAnswers, currentWinnerName);
            GameManager.Instance.eventManager.TTTWinnerFound.Invoke();
        }
        public void ChangeGameMode()
        {
            if(currentMode == null)
            {
                currentMode = gameModes[0];
                GameManager.Instance.eventManager.ChangeGameModeTTT.Invoke();
                return;
            }

            int index = gameModes.IndexOf(currentMode);
            if (index != gameModes.Count - 1)
            {
                currentMode = gameModes[index + 1];
            }
            else
            {
                currentMode = gameModes[0];
            }
            GameManager.Instance.eventManager.ChangeGameModeTTT.Invoke();
        }
        public void PlayTTT()
        {
            ShowGrid();
            currentAnswers = new Dictionary<int, string>();
            GameManager.Instance.answerGenerator.InitGenerator(gridSize, currentGrid, crossSprite, circleSprite);
            if (currentMode == "ComputerTurnBased")
            {
                GameManager.Instance.eventManager.PlayTurnByTurnTTT.Invoke();
                GameManager.Instance.answerGenerator.GenerateAnswers(true);
            }
            else if (currentMode == "ComputerImmidiate")
            {
                GameManager.Instance.eventManager.PlayImmidiateTTT.Invoke();
                GameManager.Instance.answerGenerator.GenerateAnswers(false);
            }
            GameManager.Instance.winnerDisplayer.Init(winnerHighlightSprite);
        }
        private void ShowGrid()
        {
            currentGrid = new List<Transform>();
            GameManager.Instance.gridManager.InitGrid(gridSize, gameCanvas, cellBackGroundSprite);
            currentGrid = GameManager.Instance.gridManager.GenerateGrid(true);
        }
    }
}
