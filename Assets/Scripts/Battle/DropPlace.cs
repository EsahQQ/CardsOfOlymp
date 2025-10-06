using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle
{
    public class DropPlace : MonoBehaviour, IDropHandler
    {
        [SerializeField] private float cardScale = 1f;
    
        public void OnDrop(PointerEventData eventData)
        {
            var card =  eventData.pointerDrag.GetComponent<Card.Card>();

            if (card)
            {
                card.DefaultParent = transform;
             
                card.transform.localScale = new Vector3(cardScale, cardScale, cardScale);
            }
        }
    }
}
