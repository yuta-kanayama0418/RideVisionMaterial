using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MethodCaller))]
public class MethodCallerInspector : Editor {
    private bool _enabledRunInEditorMode = false;
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        MethodCaller methodCaller = this.target as MethodCaller;

        if (!Application.isPlaying) {
            this._enabledRunInEditorMode = EditorGUILayout.Toggle("Enable Run in Editor Mode", _enabledRunInEditorMode);
            if (this._enabledRunInEditorMode) {
                EditorGUILayout.HelpBox("UnityEventCallStates are called when set to 'Editor & Runtime' only", MessageType.Warning);
            }
        }

        if (Application.isPlaying || this._enabledRunInEditorMode) {
            if (methodCaller.call != null && GUILayout.Button("Run")) {
                methodCaller.call.Invoke();
            }
        }
    }
}