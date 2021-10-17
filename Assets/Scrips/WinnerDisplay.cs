using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Display
{
    public class WinnerDisplay : MonoBehaviour
    {
        private Sprite highlightSprite;
        private void Awake()
        {
            GameManager.Instance.eventManager.TTTWinnerFound.AddListener(DisplayWinner);
        }
        public void Init(Sprite m_highlight)
        {
            highlightSprite = m_highlight;
        }
        public void DisplayWinner()
        {
            List<Transform> cellGrid = GameManager.Instance.tttManager.currentGrid;
            Dictionary<int, string> winner = GameManager.Instance.tttManager.currentWinnerAnswers; 
            if (highlightSprite == null) { Debug.LogError("Highlight sprite was not assigned"); return; }
            string winPlayer = "";
            if (winner == null) { return; }
            foreach (int key in winner.Keys)
            {
                winner.TryGetValue(key, out winPlayer);
                cellGrid[key].GetComponent<Image>().sprite = highlightSprite;
            }
        }
    }
}
