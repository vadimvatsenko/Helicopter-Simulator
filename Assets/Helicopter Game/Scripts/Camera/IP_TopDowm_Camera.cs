using UnityEngine;

namespace Helicopter_Game.Scripts.Camera
{
    public class IP_TopDowm_Camera : IP_Base_HeliCamera, IP_IHeliCamera
    {
        [Header("TopDown Camera Properties")] 
        [SerializeField] private float height = 2f;
        [SerializeField] private float distance = 2f;
        private void OnEnable() => UpdateEvent += UpdateCamera;
        private void OnDisable() => UpdateEvent -= UpdateCamera;
        
        public void UpdateCamera()
        {
            Debug.Log("Updating camera");
        }
    }
}
