using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KASM
{
    public class DragWindow : MonoBehaviour, IDragHandler
    {
        public Canvas canvas;
        public RectTransform windowTransform;

        public void OnDrag(PointerEventData eventData)
        {
            windowTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}