using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace TicTacToe.EventSystem
{


    public class EventManager : MonoBehaviour
    {
        public UnityEvent GameExit = new UnityEvent();
        // switching between screens
        public UnityEvent EnterGamingScreen = new UnityEvent();
        public UnityEvent EnterStartingScreen = new UnityEvent();
        // events withing TTT game
        public UnityEvent ChangeGameModeTTT = new UnityEvent();
        public UnityEvent PlayTurnByTurnTTT = new UnityEvent();
        public UnityEvent PlayImmidiateTTT = new UnityEvent();
        public UnityEvent TTTWinnerFound = new UnityEvent();

    }
}