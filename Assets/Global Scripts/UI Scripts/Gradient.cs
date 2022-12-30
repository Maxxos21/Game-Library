using UnityEngine;
using UnityEngine.UI;


[AddComponentMenu("UI/Effects/Gradient")]
public class Gradient : BaseMeshEffect
{
    [Header("Variables")]
    public Color color1 = Color.white;
    public Color color2 = Color.white;
    [Range(-180f, 180f)]
    public float angle;
    public bool ignoreRatio;

    public override void ModifyMesh(VertexHelper vertexHelper)
    {
        if (enabled == true)
        {
            Rect rect = graphic.rectTransform.rect;
            Vector2 direction = GradientUtils.RotationDir(angle);
            if (!ignoreRatio)
            {
                direction = GradientUtils.CompensateAspectRatio(rect, direction);
            }

            GradientUtils.Matrix2x3 localPositionMatrix = GradientUtils.LocalPositionMatrix(rect, direction);
            UIVertex uiVertex = default(UIVertex);
            for (int i = 0; i < vertexHelper.currentVertCount; i++)
            {
                vertexHelper.PopulateUIVertex(ref uiVertex, i);
                Vector2 localPosition = localPositionMatrix * uiVertex.position;
                uiVertex.color *= Color.Lerp(color2, color1, localPosition.y);
                vertexHelper.SetUIVertex(uiVertex, i);
            }
        }
    }
}
