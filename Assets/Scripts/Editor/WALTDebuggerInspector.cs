using UnityEditor;
using UnityEngine;

namespace WALTApp
{
    [CustomEditor(typeof(WALTDebugger))]
    public class WALTDebuggerInspector : Editor
    {
        private WALTDebugger _debugger;

        private void OnSceneGUI()
        {
            _debugger = target as WALTDebugger;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            if(!Application.isPlaying || _debugger == null) return;

            EditorGUILayout.LabelField("", GUILayout.Height(3));

            if (GUILayout.Button("Send Message", GUILayout.Height(50)))
                if (!_debugger.Send()) Debug.LogWarning("Message not sent.");
        }
    }
}