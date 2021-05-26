using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KASM
{
    public class DragWindow : MonoBehaviour, IDragHandler
    {
        private RectTransform windowTransform;
        public RectTransform WindowTransform
        {
            get
            {
                if (windowTransform == null)
                {
                    windowTransform = transform.parent.gameObject.GetComponent<RectTransform>();
                }
                return windowTransform;
            }
            set
            {
                windowTransform = value;
            }
        }
        private Canvas canvas;
        public Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = transform.parent.parent.gameObject.GetComponent<Canvas>();
                }
                return canvas;
            }
            set
            {
                Canvas = value;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            WindowTransform.anchoredPosition += eventData.delta / Canvas.scaleFactor;
        }
    }
}