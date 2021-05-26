using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KASM
{
    public class ScaleWindow : MonoBehaviour, IDragHandler
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

        public Vector2 minSize = new Vector2(100, 100);

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 sizeChange = eventData.delta / Canvas.scaleFactor * new Vector2(1, -1);
            WindowTransform.sizeDelta = new Vector2(Mathf.Max(WindowTransform.sizeDelta.x + sizeChange.x, minSize.x), Mathf.Max(WindowTransform.sizeDelta.y + sizeChange.y, minSize.y));
        }
    }
}