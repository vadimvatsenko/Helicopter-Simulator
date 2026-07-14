using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        [Header("Manager Properties")] 
        [SerializeField] private int startIndexCamera = 0;
        private List<BaseHeliCamera> _cameras;
        private int _camIndex = 0;

        private void Start()
        {
            _cameras = GetComponentsInChildren<BaseHeliCamera>().ToList();
            
            HandleSwitchCamera(startIndexCamera);
        }

        public void SwitchCamera()
        {
            _camIndex = (_camIndex + 1) % _cameras.Count;
            HandleSwitchCamera(_camIndex);
        }

        private void HandleSwitchCamera(int index)
        {
            for (int i = 0; i < _cameras.Count; i++)
            {
                if (i == index)
                {
                    SwitchCameraComponent(i, true);
                }
                else
                {
                    SwitchCameraComponent(i, false);
                }
            }
        }
        
        private void SwitchCameraComponent(int index, bool active)
        {
            _cameras[index].GetComponent<UnityEngine.Camera>().enabled = active;
        }
    }
}
