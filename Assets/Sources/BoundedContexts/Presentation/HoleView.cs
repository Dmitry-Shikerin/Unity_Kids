using System;
using UnityEngine;

namespace Sources.BoundedContexts.Presentation
{
    public class HoleView : MonoBehaviour
    {
        private RectTransform _holeRect;

        private void Awake()
        {
            _holeRect = GetComponent<RectTransform>();
        }

        void OnDrawGizmos()
        {
            if (_holeRect != null)
            {
                // Рисуем прямоугольник
                Gizmos.color = Color.yellow;
                Vector3[] corners = new Vector3[4];
                _holeRect.GetWorldCorners(corners);
                for (int i = 0; i < 4; i++)
                {
                    Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
                }
        
                // Рисуем эллипс (приблизительно)
                Gizmos.color = Color.red;
                Vector3 center = _holeRect.position;
                Vector3 scale = _holeRect.lossyScale;
                float width = _holeRect.rect.width * scale.x / 2;
                float height = _holeRect.rect.height * scale.y / 2;
        
                // Рисуем 16 точек эллипса
                for (int i = 0; i < 16; i++)
                {
                    float angle = (i / 16f) * Mathf.PI * 2;
                    float nextAngle = ((i + 1) / 16f) * Mathf.PI * 2;
            
                    Vector3 p1 = center + new Vector3(Mathf.Cos(angle) * width, Mathf.Sin(angle) * height);
                    Vector3 p2 = center + new Vector3(Mathf.Cos(nextAngle) * width, Mathf.Sin(nextAngle) * height);
            
                    Gizmos.DrawLine(p1, p2);
                }
            }
        }
    }
}