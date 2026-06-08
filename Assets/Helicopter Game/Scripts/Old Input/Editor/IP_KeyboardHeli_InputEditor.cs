using UnityEngine;
using UnityEditor;

namespace Helicopter_Game.Scripts.Old_Input.Editor
{
    [CustomEditor(typeof(IP_KeyboardHeli_Input))]
    public class IP_KeyboardHeli_InputEditor : UnityEditor.Editor
    {
        private IP_KeyboardHeli_Input targetInput;

        private void OnEnable()
        {
            targetInput = (IP_KeyboardHeli_Input)target;
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
