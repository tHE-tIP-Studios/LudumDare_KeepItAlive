using UnityEditor;
using UnityEngine;

namespace WALTApp
{
    [CustomEditor(typeof(WALTDebugger))]
    public class WALTDebuggerInspector : Editor
    {
        private WALTDebugger _debugger;

        public override void OnInspectorGUI()
        {
            _debugger = (WALTDebugger)target;
            DrawDefaultInspector();

            if (!Application.isPlaying) return;
            
            EditorGUILayout.LabelField("", GUILayout.Height(3));
            if (GUILayout.Button("Send Message", GUILayout.Height(50)))
                if (!_debugger.Send()) Debug.LogWarning("Message not sent.");
        }
    }
}