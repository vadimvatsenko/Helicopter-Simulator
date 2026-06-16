using UnityEditor;
using UnityEngine;

namespace Helicopter_Game.Scripts.Camera.Editor
{
    [CustomEditor(typeof(IP_Advanced_HeliCamera))]
    public class IP_AdvancedHeliCamera_Editor : UnityEditor.Editor
    {
        private IP_Advanced_HeliCamera targetCamera;
        private void OnEnable()
        {
            targetCamera = (IP_Advanced_HeliCamera)target;
        }

        private void OnSceneGUI()
        {
            float minDist = targetCamera.MinDistance;
            float maxDist = targetCamera.MaxDistance;
            Vector3 targetFwd = targetCamera.Rb.transform.forward;
            
            Handles.color = Color.blue;
            Handles.DrawWireDisc(targetCamera.Rb.position, Vector3.up, minDist);
            Handles.DrawWireDisc(targetCamera.Rb.position, Vector3.up, maxDist);
            
            targetCamera.MinDistance 
                = Handles.ScaleSlider
                    (targetCamera.MinDistance, targetCamera.Rb.position + (targetFwd * minDist) , Vector3.forward, Quaternion.identity, 1f, 1f);
            
            targetCamera.MaxDistance 
                = Handles.ScaleSlider
                    (targetCamera.MaxDistance, targetCamera.Rb.position + (targetFwd * maxDist) , Vector3.forward, Quaternion.identity, 1f, 1f);
        }
    }
}
