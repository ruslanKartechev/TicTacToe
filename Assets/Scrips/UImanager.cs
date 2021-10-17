using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace TicTacToe.Display
{


    public class UImanager : MonoBehaviour
    {
        public Button playButton;
        public Button checkWinnerButton;
        public Button gameModeChange;
        public Button exitGameButton;
        public Button startScreenButton;
        public TextMeshProUGUI winnerTextout;
        public TextMeshProUGUI gamemodeTextout;
        [Header("TTT canvases")]
        [SerializeField] protected Canvas tttTextCanvas;
        [SerializeField] protected Canvas tttMenuButtons;
        [SerializeField] protected Canvas tttBackground;
        [SerializeField] protected Canvas tttGameoutput;
        [Header("Starting Screen")]
        [SerializeField] protected Canvas startingScreenCanvas;
        [SerializeField] protected Canvas startingButtons;
        [SerializeField] protected GameObject introAnimatorController;
        [SerializeField] protected Button playTTTButton;
        [SerializeField] protected Button returnButton;
        [SerializeField] protected Transform buttonPlace;
        protected Animator introAnimator;

        private void Awake()
        {
            OnAppStart();
        }
        private void SubscribeButtons()
        {
            playButton.onClick.AddListener(PlayButton);
            checkWinnerButton.onClick.AddListener(CheckWinnerButton);
            gameModeChange.onClick.AddListener(GameModeChange);
            exitGameButton.onClick.AddListener(ExitGame);
            playTTTButton.onClick.AddListener(PlayTTT);
            returnButton.onClick.AddListener(Return);
        }
        private void SubscribeToEvents()
        {
            GameManager.Instance.eventManager.EnterGamingScreen.AddListener(OnEnterGamingScreen);
            GameManager.Instance.eventManager.EnterStartingScreen.AddListener(OnEnterStartingScreen);
            GameManager.Instance.eventManager.PlayTurnByTurnTTT.AddListener(OnPlayTurnByTurn);
            GameManager.Instance.eventManager.PlayImmidiateTTT.AddListener(OnPlayImmidiate);
            GameManager.Instance.eventManager.TTTWinnerFound.AddListener(OnWinnerFound);
            GameManager.Instance.eventManager.ChangeGameModeTTT.AddListener(OnGamemodeChange);
        }

        public void OutputWinnerText(string outputText)
        {
            winnerTextout.text = outputText;
        }
        public void PlayButton()
        {
            GameManager.Instance.tttManager.PlayTTT();
        }
        public void CheckWinnerButton()
        {
            GameManager.Instance.tttManager.CheckAnswers();
        }
        public void GameModeChange()
        {
            GameManager.Instance.tttManager.ChangeGameMode();
        }

        public void ExitGame( )
        {
            GameManager.Instance.eventManager.GameExit.Invoke();
            Invoke( "Application.Quit", 1f );
        }
        public void PlayTTT()
        {
            GameManager.Instance.eventManager.EnterGamingScreen.Invoke();
        }
        public void Return()
        {
            GameManager.Instance.eventManager.EnterStartingScreen.Invoke();
        }

        private void OnAppStart()
        {
            SubscribeToEvents();
            SubscribeButtons();
            introAnimator = introAnimatorController.gameObject.GetComponent<Animator>();
            returnButton.transform.localPosition = Vector2.zero;
            playTTTButton.transform.localPosition = Vector2.zero;

        }


        private void OnEnterStartingScreen()
        {
            tttTextCanvas.gameObject.SetActive(false);
            tttMenuButtons.gameObject.SetActive(false);
            tttBackground.gameObject.SetActive(false);
            tttGameoutput.gameObject.SetActive(false);
            startingScreenCanvas.gameObject.SetActive(true);
            startingButtons.gameObject.SetActive(true);
            returnButton.gameObject.SetActive(false);
            playTTTButton.gameObject.SetActive(true);

            introAnimator.enabled = true;
            introAnimator.SetBool("Exit", false);
            introAnimator.Play("introAnim1");
        }

        private void OnEnterGamingScreen()
        {

            introAnimator.StopPlayback();
            introAnimator.enabled = false;
            startingScreenCanvas.gameObject.SetActive(false);
            tttTextCanvas.gameObject.SetActive(true);
            tttMenuButtons.gameObject.SetActive(true);
            tttBackground.gameObject.SetActive(true);
            tttGameoutput.gameObject.SetActive(true);
            OutputWinnerText("Play the Game");
            returnButton.gameObject.SetActive(true);    
            playTTTButton.gameObject.SetActive(false);

        }
        private void OnPlayTurnByTurn()
        {
            winnerTextout.gameObject.SetActive(true);
            OutputWinnerText("Playing ... ");
        }
        private void OnPlayImmidiate()
        {
            OutputWinnerText("Winner... ?");
        }
        private void OnWinnerFound()
        {
            string winnerName = GameManager.Instance.tttManager.currentWinnerName;
            if(winnerName != null)
                OutputWinnerText("Winner: " + winnerName);
            else
                Debug.LogWarning("winner name is null");
        }

        private void OnGamemodeChange()
        {
            string currentMode = GameManager.Instance.tttManager.currentMode;
            if (currentMode != null)
                gamemodeTextout.text = "Mode: " + currentMode;
            else
                Debug.LogWarning("current gamemode is null");
        }
    }


}
