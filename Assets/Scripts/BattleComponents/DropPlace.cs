using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.VisualScripting;
using CardComponents;

namespace BattleComponents
{
    public class DropPlace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
            var dragger = eventData.pointerDrag.GetComponent<CardDragger>();
            if (dragger != null)
            {
                dragger.DefaultParent = this.transform;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }
}
