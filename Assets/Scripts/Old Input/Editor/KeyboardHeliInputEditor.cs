using UnityEditor;

namespace Old_Input.Editor
{
    [CustomEditor(typeof(KeyboardHeliInput))]
    public class KeyboardHeliInputEditor : UnityEditor.Editor
    {
        private KeyboardHeliInput targetInput;

        private void OnEnable()
        {
            targetInput = (KeyboardHeliInput)target;
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
            
            EditorGUILayout.LabelField("Throttle: " + targetInput.RawThrottleInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("CollectiveInput: " + targetInput.CollectiveInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("CyclicInput: " + targetInput.CyclicInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Pedal: " + targetInput.PedalInput.ToString("0.00"), EditorStyles.boldLabel);
            
            EditorGUILayout.Space();
            EditorGUI.indentLevel--;
            
            EditorGUILayout.EndVertical();
        }
    }
}
