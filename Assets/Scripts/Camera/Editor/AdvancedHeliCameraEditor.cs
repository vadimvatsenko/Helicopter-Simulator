using UnityEditor;
using UnityEngine;

namespace Camera.Editor
{
    [CustomEditor(typeof(AdvancedHeliCamera))]
    public class AdvancedHeliCameraEditor : UnityEditor.Editor
    {
        private AdvancedHeliCamera _targetCamera;
        private void OnEnable()
        {
            _targetCamera = (AdvancedHeliCamera)target;
        }

        private void OnSceneGUI()
        {
            float minDist = _targetCamera.MinDistance;
            float maxDist = _targetCamera.MaxDistance;
            Vector3 targetFwd = _targetCamera.Rb.transform.forward;
            
            Handles.color = Color.blue;
            Handles.DrawWireDisc(_targetCamera.Rb.position, Vector3.up, minDist);
            Handles.DrawWireDisc(_targetCamera.Rb.position, Vector3.up, maxDist);
            
            _targetCamera.MinDistance 
                = Handles.ScaleSlider
                    (_targetCamera.MinDistance, _targetCamera.Rb.position + (targetFwd * minDist) , Vector3.forward, Quaternion.identity, 1f, 1f);
            
            _targetCamera.MaxDistance 
                = Handles.ScaleSlider
                    (_targetCamera.MaxDistance, _targetCamera.Rb.position + (targetFwd * maxDist) , Vector3.forward, Quaternion.identity, 1f, 1f);
        }
    }
}
