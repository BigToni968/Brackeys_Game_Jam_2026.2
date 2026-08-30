using UnityEngine.SceneManagement;
using Game.Abstraction;
using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class UI_Menu : MonoView
    {
        [SerializeField] private Canvas self;
        [SerializeField] private string jumpButtonText;
        [SerializeField] private int jumpIndexScene;
        [SerializeField] private TextMeshProUGUI jumpText;

        private void Awake()
        {
            jumpText.SetText(jumpButtonText);
        }

        public void OnJump()
        {
            SceneManager.LoadScene(jumpIndexScene);
        }

        public void OnExit()
        {
            Application.Quit();
        }
    }
}