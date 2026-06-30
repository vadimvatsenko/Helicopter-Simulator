using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Camera
{
    public class IP_Camera_Manager : MonoBehaviour
    {
        [Header("Manager Properties")] 
        [SerializeField] private int startIndexCamera = 0;
        private List<IP_Base_HeliCamera> cameras;
        private int camIndex = 0;

        private void Start()
        {
            cameras = GetComponentsInChildren<IP_Base_HeliCamera>().ToList();
            
            HandleSwitchCamera(startIndexCamera);
        }

        public void SwitchCamera()
        {
            camIndex = (camIndex + 1) % cameras.Count;
            HandleSwitchCamera(camIndex);
        }

        private void HandleSwitchCamera(int index)
        {
            for (int i = 0; i < cameras.Count; i++)
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
            cameras[index].GetComponent<UnityEngine.Camera>().enabled = active;
        }
    }
}
