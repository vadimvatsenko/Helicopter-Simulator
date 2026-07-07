using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuBtnController : MonoBehaviour
    {
        [SerializeField] private Button r22SimulatorBtn;
        [SerializeField] private Button r22VRSimulatorBtn;
        [SerializeField] private Button heavyHeliArcadeBtn;
        [SerializeField] private Button exitBtn;

        private void OnEnable()
        {
            r22SimulatorBtn.onClick.AddListener(StartR22SimulatorBtn);
            r22VRSimulatorBtn.onClick.AddListener(StartR22VRSimulatorBtn);
            heavyHeliArcadeBtn.onClick.AddListener(StartHeavyHeliArcadeBtn);
            exitBtn.onClick.AddListener(StartExitBtn);
        }

        private void OnDisable() => r22SimulatorBtn.onClick.RemoveAllListeners();
    
        private void StartR22SimulatorBtn() => SceneManager.LoadScene(1);

        private void StartR22VRSimulatorBtn()
        {
            Debug.Log("Not Ready");
            // SceneManager.LoadScene(2);
        }
        private void StartHeavyHeliArcadeBtn() => SceneManager.LoadScene(3);
        private void StartExitBtn() => Application.Quit();
    
    }
}
