using UnityEngine;
using UnityEditor;

public class BajasRegrets : EditorWindow {
    [MenuItem("Window/Regrets window")]
    public static void ShowWindow() {
        if (!EditorWindow.HasOpenInstances<BajasRegrets>())
            EditorWindow.CreateWindow<BajasRegrets>().Show();
        else
            EditorWindow.GetWindow<BajasRegrets>().Show();
        EditorWindow.FocusWindowIfItsOpen<BajasRegrets>();
    }

    public Transform root;


    public void OnGUI() {
        root = (Transform)EditorGUILayout.ObjectField(root, typeof(Transform), true);

        if (GUILayout.Button("shit")) {
            for (int i = 0; i < root.childCount; i++) {
                var t = root.GetChild(i);
                var s = t.localScale;

                t.localScale = new Vector3(s.x, s.x, s.x);
            }
        }
    }
}