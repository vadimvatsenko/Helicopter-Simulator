using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helicopter_Game.Scripts.Camera
{
    public class IP_Camera_Manager : MonoBehaviour
    {
        private List<IP_Base_HeliCamera> cameras;

        private void Start()
        {
            cameras = GetComponentsInChildren<IP_Base_HeliCamera>().ToList();
            foreach (IP_Base_HeliCamera cam in cameras)
            {
                Debug.Log(cam.name);
            }
        }
    }
}
