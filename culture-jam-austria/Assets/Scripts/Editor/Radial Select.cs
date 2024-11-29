using UnityEngine;
using UnityEditor;
using System.Linq;
using NaughtyAttributes;
using UnityEditorInternal;
using System.Collections.Generic;

public class RadialSelect : EditorWindow {
    [MenuItem("Window/Selection window")]
    public static void ShowWindow() {
        if (!EditorWindow.HasOpenInstances<RadialSelect>())
            EditorWindow.CreateWindow<RadialSelect>().Show();
        else
            EditorWindow.GetWindow<RadialSelect>().Show();
        EditorWindow.FocusWindowIfItsOpen<RadialSelect>();


    }

    private void OnEnable() { SceneView.duringSceneGui += this.OnSceneGUI; }
    private void OnDisable() { SceneView.duringSceneGui -= this.OnSceneGUI; }

    public void OnGUI() {
        center = EditorGUILayout.Vector3Field("position", center);
        radius = EditorGUILayout.FloatField("radius", radius);

        LayerMask tempMask = EditorGUILayout.MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(mask), InternalEditorUtility.layers);
        mask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);

        if (GUILayout.Button("Nya")) {
            var obs = Physics.OverlapCapsule(center + Vector3.up * 100, center + Vector3.down * 100, radius, mask);
            var objects = obs.Select(o => o.gameObject).Where(o => o.activeInHierarchy);

            Selection.objects = objects.ToArray();
        }

        parent = (Transform)EditorGUILayout.ObjectField(parent, typeof(Transform), true);
        if (parent != null) {
            if (GUILayout.Button("Shwoot")) {
                foreach (var s in Selection.objects) {
                    if (s is not GameObject) continue;
                    ((GameObject)s).transform.SetParent(parent);

                    objects.Clear();
                    for (int i = 0; i < parent.childCount; i++) {
                        if (!objects.Contains(parent.GetChild(i).gameObject)) objects.Add(parent.GetChild(i).gameObject);
                    }

                }
                Selection.objects = new Object[] { parent };
            }

            if (parent != oldParent) {
                objects.Clear();
                for (int i = 0; i < parent.childCount; i++) {
                    if (!objects.Contains(parent.GetChild(i).gameObject)) objects.Add(parent.GetChild(i).gameObject);
                }
            }

        }
        if (parent == null) objects.Clear();
        oldParent = parent;
    }

    public void OnSceneGUI(SceneView view) {
        center = Handles.PositionHandle(center, Quaternion.identity);
        Handles.color = new Color(1f, 1f, 0.2f, 0.2f);
        Handles.DrawSolidDisc(center, Vector3.up, radius);

        foreach (var o in objects) Handles.DrawWireCube(o.transform.position, o.transform.localScale);

    }


    public float radius = 10;
    public LayerMask mask;
    public Vector3 center;
    public Transform parent;
    public Transform oldParent;
    public List<GameObject> objects = new List<GameObject>();

}
