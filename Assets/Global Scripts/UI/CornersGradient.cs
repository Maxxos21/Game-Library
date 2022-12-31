using UnityEngine;
using UnityEngine.UI;

namespace PolygonPlanet.Gradient
{
    [AddComponentMenu("UI/Effects/Corners Gradient")]
    public class CornersGradient : BaseMeshEffect
    {
        [Header("Variables")]
        public Color topLeftColor = Color.white;
        public Color topRightColor = Color.white;
        public Color bottomLeftColor = Color.white;
        public Color bottomRightColor = Color.white;

        public override void ModifyMesh(VertexHelper vertextHelper)
        {
            if (enabled == true)
            {
                Rect rect = graphic.rectTransform.rect;
                GradientUtils.Matrix2x3 localPositionMatrix = GradientUtils.LocalPositionMatrix(rect, Vector2.right);
                UIVertex uiVertex = default(UIVertex);

                for (int i = 0; i < vertextHelper.currentVertCount; i++)
                {
                    vertextHelper.PopulateUIVertex(ref uiVertex, i);
                    Vector2 normalizedPosition = localPositionMatrix * uiVertex.position;
                    uiVertex.color *= GradientUtils.Bilerp(bottomLeftColor, bottomRightColor, topLeftColor, topRightColor, normalizedPosition);
                    vertextHelper.SetUIVertex(uiVertex, i);
                }
            }
        }
    }
}