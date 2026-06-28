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
            Vector3 targetPos = Rb.position;
            targetPos.y = 0f;
            
            WantedPos = (Vector3.back * -distance) + (Vector3.up * height);
            transform.position = targetPos + WantedPos;
            transform.LookAt(lookAtTarget.position);
        }
    }
}
