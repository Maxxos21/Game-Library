using System.Linq;
using UnityEditor;
using UnityEngine;
 
[InitializeOnLoad]
public class CustomHierarchy : MonoBehaviour
{
    private static Vector2 offset = new Vector2(0, 2);
    [SerializeField] private Color fontColor, backgroundColor;

    static CustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        Color fontColor = Color.red;
        Color backgroundColor = new Color(0.16f, 0.16f, 0.16f);

        var receiver = EditorUtility.InstanceIDToObject(instanceID);
        if (receiver != null)
        {
            // only if name contains "Receiver"
            if (receiver.name.Contains("Receiver"))
            {
                if (Selection.instanceIDs.Contains(instanceID))
                {
                    fontColor = Color.white;
                    backgroundColor = new Color(0.24f, 0.48f, 0.90f);
                }

                Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
                EditorGUI.DrawRect(selectionRect, backgroundColor);
                EditorGUI.LabelField(offsetRect, receiver.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = fontColor },
                    fontStyle = FontStyle.Bold
                }
                );
            }
        }
    }
}