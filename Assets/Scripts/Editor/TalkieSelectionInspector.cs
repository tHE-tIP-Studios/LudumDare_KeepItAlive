using UnityEditor;
using UnityEngine;

namespace Talkie
{
    [CustomEditor(typeof(TalkieSelection))]
    public class WALTDebuggerInspector : Editor
    {
        private TalkieSelection _selection;

        public override void OnInspectorGUI()
        {
            _selection = (TalkieSelection)target;
            DrawDefaultInspector();

            if (!Application.isPlaying) return;
            
            EditorGUILayout.LabelField("", GUILayout.Height(3));
            if (GUILayout.Button("Move Next", GUILayout.Height(50)))
                if (!_selection.MoveNext()) Debug.LogWarning("Unable to move.");
            EditorGUILayout.LabelField("", GUILayout.Height(3));
            if (GUILayout.Button("Move Previous", GUILayout.Height(50)))
                if (!_selection.MovePrevious()) Debug.LogWarning("Unable to move.");
        }
    }
}