using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.VisualScripting;
using Card;

namespace Battle
{
    public class DropPlace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
            CardController card = eventData.pointerDrag.GetComponent<CardController>();
            
            if (card != null)
            {
                card.DefaultParent = this.transform;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }
}
