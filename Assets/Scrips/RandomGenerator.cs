using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TicTacToe
{
    public class RandomGenerator : MonoBehaviour
    {
        private Sprite cross;
        private Sprite circle;
        [SerializeField] private GameObject imagePF;
        [SerializeField] private float outputDelay;
        private int size;
        private List<Transform> gridCells;
        private string firstTurn;
        private List<int> occupiedIndeces;
        private Dictionary<int, string> answersByindex;
        private void Awake()
        {
            GameManager.Instance.eventManager.ChangeGameModeTTT.AddListener(OnChangeGameMode);
        }

        private void OnChangeGameMode()
        {
            StopAllCoroutines();
        }

        public void InitGenerator(int m_size, List<Transform> m_grid, Sprite m_cross, Sprite m_circle, string m_firstTurn = "Cross", float delay = 0.5f, GameObject m_imagePF = null)
        {
            outputDelay = delay;
            size = m_size;
            gridCells = m_grid;
            circle = m_circle;
            cross = m_cross;
            firstTurn = m_firstTurn;
            answersByindex = new Dictionary<int, string>();
            occupiedIndeces = new List<int>();
            if (m_imagePF != null)
                imagePF = m_imagePF;
        }
        public void GenerateAnswers(bool turnByturn = false)
        {
            if (imagePF == null) { Debug.LogWarning("Image prefab not assigned"); return; }
            if (cross == null || circle == null) {Debug.LogWarning("Cross or circle sprites are not assigned"); return; }
            if(gridCells.Count == 0 || gridCells == null) { Debug.LogError("No grid was generated or assigned"); return; }

            occupiedIndeces.Clear();
            answersByindex.Clear();
            StartCoroutine(GeneratingRouting(turnByturn));
        }
        private IEnumerator GeneratingRouting(bool turnByturn)
        {
            Dictionary<int, string> winner = new Dictionary<int, string>();
            string winnerName;
            int index;
            for (int i = 0; i < size * size; i++)
            {
                if (i % 2 == 0)
                {
                    if (firstTurn == "Cross")
                        index=PlayerTurn("Cross");
                    else
                        index=PlayerTurn("Circle");
                }
                else
                {
                    if (firstTurn == "Cross")
                        index=PlayerTurn("Circle");
                    else
                        index=PlayerTurn("Cross");
                }
                if (turnByturn)
                {
                    string player;
                    answersByindex.TryGetValue(index, out player);
                    PlaceMark(gridCells[index], player);
                    ( winner, winnerName )= GameManager.Instance.answerChecker.CheckAnswers(gridCells, answersByindex);
                    if (winner != null)
                    {
                        GameManager.Instance.tttManager.SetWinner(winner, winnerName);
                        GameManager.Instance.eventManager.TTTWinnerFound.Invoke();
                        yield break;
                    } 
                    yield return new WaitForSeconds(outputDelay);
                }
            }
            yield return null;
            if (turnByturn)
            {
                (winner, winnerName) = GameManager.Instance.answerChecker.CheckAnswers(gridCells, answersByindex);
                GameManager.Instance.tttManager.SetWinner(winner, winnerName);
                GameManager.Instance.eventManager.TTTWinnerFound.Invoke();
            }
            else if(!turnByturn)
            {
                OutputImmidiate(answersByindex);
            }
            GameManager.Instance.tttManager.SetAnswers(answersByindex);
        }


        private int PlayerTurn(string player)
        {
            int index = 0;
            do
            {
                index = (int)UnityEngine.Random.Range(0, size*size);
            } while (occupiedIndeces.Contains(index));
            occupiedIndeces.Add(index);
            if(player == "Cross")
            {
                answersByindex.Add(index, "Cross");
            }                
            else if(player == "Circle")
            {
                answersByindex.Add(index, "Circle");
            }
            return index;
        }
        private void PlaceMark(Transform mCell, string player)
        {
            GameObject turn = Instantiate(imagePF, mCell);
            turn.transform.localPosition = Vector2.zero;
            if (player == "Cross")
            {
                turn.GetComponent<Image>().sprite = cross;
            }
            else if (player == "Circle")
            {
                turn.GetComponent<Image>().sprite = circle;
            }
        }
        private void OutputImmidiate(Dictionary<int,string> answers)
        {
            if(gridCells == null || gridCells.Count == 0) { Debug.LogWarning("No grid or size is 0"); return; }
            for(int i=0; i<size*size; i++)
            {
                string player;
                answers.TryGetValue(i, out player);
                PlaceMark(gridCells[i], player);
            }
        }
    
    }



}