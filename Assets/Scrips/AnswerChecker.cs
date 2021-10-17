using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TicTacToe
{


    public class AnswerChecker : MonoBehaviour
    {
        
        public (Dictionary<int, string>,string ) CheckAnswers(List<Transform> gridCells, Dictionary<int,string> answersByIndex)
        {
            Dictionary<int, string> winner = new Dictionary<int, string>();
            string winnerName = null;
            (winner,winnerName) = CheckColomns(gridCells, answersByIndex);
            
            if (winner == null)
            {
                (winner, winnerName) = CheckRows(gridCells, answersByIndex);
                if(winner == null)
                {
                    (winner, winnerName) = CheckDiagonals(gridCells, answersByIndex);
                }
            }
            if (winnerName == null)
                winnerName = "Even";
            return (winner, winnerName );
        }
        private (Dictionary<int, string>,string) CheckColomns(List<Transform> gridCells, Dictionary<int, string> answersByIndex)
        {
            Dictionary<int, string> winner = new Dictionary<int, string>();
            int size = (int)Mathf.Sqrt(gridCells.Count);
            string winnerName = null;
            for (int i = 0; i < size; i++)
            {
                string mAnswer;
                answersByIndex.TryGetValue(i, out mAnswer);
                for(int k = 0; k < size; k++)
                {
                    string mAnswer2;
                    int mkey = i + k * size;
                    answersByIndex.TryGetValue(mkey, out mAnswer2);
                    if (mAnswer2 == mAnswer && winner.ContainsKey(mkey)==false && mAnswer2 != null && mAnswer != null)
                    {
                        winner.Add(mkey, mAnswer);
                        if(winner.Count == size)
                        {
                            winnerName = mAnswer2;
                            return (winner, winnerName);
                        }
                    } else
                    {
                        winnerName = null;
                        winner.Clear();
                        break;
                    }
                }
            }
            return (null, winnerName);
        }

        private (Dictionary<int, string>, string) CheckRows(List<Transform> gridCells, Dictionary<int, string> answersByIndex)
        {
            Dictionary<int, string> winner = new Dictionary<int, string>();
            int size = (int)Mathf.Sqrt(gridCells.Count);
            string winnerName = null;
            for (int i = 0; i < size; i++)
            {
                int rowIndex = i * size;
                string mAnswer;
                answersByIndex.TryGetValue(rowIndex, out mAnswer);
                for (int k=0; k < size; k++)
                {
                    int colomn = rowIndex + k;
                    string mAnswer2;
                    answersByIndex.TryGetValue(colomn, out mAnswer2);
                    if (mAnswer2 == mAnswer && winner.ContainsKey(colomn) == false && mAnswer2 != null && mAnswer != null)
                    {
                        winner.Add(colomn, mAnswer);
                        if (winner.Count == size) {
                            winnerName = mAnswer2;
                            return (winner, winnerName);
                        }
                    }
                    else
                    {
                        winnerName = null;
                        winner.Clear();
                        break;
                    }
                }
            }
            return (null, winnerName);
        }
        private (Dictionary<int, string>, string) CheckDiagonals(List<Transform> gridCells, Dictionary<int, string> answersByIndex)
        {
            Dictionary<int, string> winner = new Dictionary<int, string>();
            int size = (int)Mathf.Sqrt(gridCells.Count);
            string winnerName = null;
            //Cheking first diagonal
            string mAnswer;
            answersByIndex.TryGetValue(0, out mAnswer);
            for (int i = 0; i < size; i++)
            {
                int mIndex = i * (size + 1);
                string mAnswer2;
                answersByIndex.TryGetValue(mIndex, out mAnswer2);
                if (mAnswer2 == mAnswer && winner.ContainsKey(mIndex) == false && mAnswer2 != null && mAnswer != null)
                {
                    winner.Add(mIndex, mAnswer);
                    winnerName = mAnswer2;
                    if (winner.Count == size)
                    {
                        winnerName = mAnswer2;
                        return (winner, winnerName);
                    }
                }
                else
                {
                    winnerName = null;
                    winner.Clear();
                    break;
                }
            }
            // checking second diagpnal
            answersByIndex.TryGetValue(size - 1, out mAnswer);
            for (int i = 0; i < size; i++)
            {
                int mIndex = (size-1)+ i * (size - 1);
                string mAnswer2;
                answersByIndex.TryGetValue(mIndex, out mAnswer2);
                if (mAnswer2 == mAnswer && winner.ContainsKey(mIndex) == false && mAnswer2 != null && mAnswer != null)
                {
                    winner.Add(mIndex, mAnswer);
                    winnerName = mAnswer2;
                    if (winner.Count == size)
                    {
                        winnerName = mAnswer2;
                        return (winner, winnerName);
                    }
                }
                else
                {
                    winnerName = null;
                    winner.Clear();
                    break;
                }
            }
            return (null, winnerName);
        }
    }
    
}