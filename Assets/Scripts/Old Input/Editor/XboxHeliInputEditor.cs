using UnityEditor;

namespace Old_Input.Editor
{
    [CustomEditor(typeof(XboxHeliInput))]
    public class XboxHeliInputEditor : UnityEditor.Editor
    {
        private XboxHeliInput _targetInput;

        private void OnEnable()
        {
            _targetInput = (XboxHeliInput)target;
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            DrawDebugUI();
            Repaint();
        }

        private void DrawDebugUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;
            
            EditorGUILayout.LabelField("Throttle: " + _targetInput.RawThrottleInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("CollectiveInput: " + _targetInput.CollectiveInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("CyclicInput: " + _targetInput.CyclicInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Pedal: " + _targetInput.PedalInput.ToString("0.00"), EditorStyles.boldLabel);
            
            EditorGUILayout.Space();
            EditorGUI.indentLevel--;
            
            EditorGUILayout.EndVertical();
        }
    }
}
