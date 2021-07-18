using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KASM
{
    public class ResizePanel : MonoBehaviour, IDragHandler
    {
        public Canvas canvas;
        public RectTransform pannel;
        public Vector2 range = new Vector2(200, 400);

        public void OnDrag(PointerEventData eventData)
        {
            pannel.sizeDelta = new Vector2(Mathf.Clamp(pannel.rect.width - eventData.delta.x / canvas.scaleFactor, range.x, range.y), pannel.sizeDelta.y);
        }
    }
}