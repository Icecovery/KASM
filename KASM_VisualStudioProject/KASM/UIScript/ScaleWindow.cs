using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KASM
{
    public class ScaleWindow : MonoBehaviour, IDragHandler
    {
        public Canvas canvas;
        public RectTransform windowTransform;

        public Vector2 minSize = new Vector2(400, 400);

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 sizeChange = eventData.delta / canvas.scaleFactor * new Vector2(1, -1);
            windowTransform.sizeDelta = new Vector2(Mathf.Max(windowTransform.sizeDelta.x + sizeChange.x, minSize.x), Mathf.Max(windowTransform.sizeDelta.y + sizeChange.y, minSize.y));
        }
    }
}