using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class InGameUIBtnController : MonoBehaviour
    {
        [SerializeField] private Button reloadSceneBtn;
        [SerializeField] private Button mainMenuBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private GameObject pausePanel;
        
        private bool _isPaused = false;

        private void OnEnable()
        {
            reloadSceneBtn.onClick.AddListener(ReloadScene);
            mainMenuBtn.onClick.AddListener(MainMenu);
            exitBtn.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            reloadSceneBtn.onClick.RemoveListener(ReloadScene);
            mainMenuBtn.onClick.RemoveListener(MainMenu);
            exitBtn.onClick.RemoveListener(Exit);
        }
        
        private void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        private void MainMenu() => SceneManager.LoadScene(0);
        private void Exit() => Application.Quit();
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("XBoxExitBtn"))
            {
                pausePanel.SetActive(!pausePanel.activeSelf);
                _isPaused = !_isPaused;
            }

            Time.timeScale = _isPaused ? 0 : 1;
            
        }
    }
}
