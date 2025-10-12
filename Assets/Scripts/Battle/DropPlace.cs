using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.VisualScripting;
using Card;

namespace Battle
{
    public class DropPlace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // OnDrop больше не нужен в его старом виде.
        // OnEndDrag на самой карте теперь делает всю работу.
        // public void OnDrop(PointerEventData eventData) { }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Проверяем, что мы вообще что-то тащим, и что это карта
            if (eventData.pointerDrag == null) return;
            CardController card = eventData.pointerDrag.GetComponent<CardController>();
            
            if (card != null)
            {
                // Сообщаем карте, что ее новый "дом" - это мы
                card.DefaultParent = this.transform;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Этот метод менее важен, но полезен, если у вас есть "нейтральная" зона.
            // Сейчас, если вы уведете курсор с DropPlace, карта будет считать его своим
            // домом до тех пор, пока не зайдет на другой DropPlace. Это нормальное поведение.
        }
    }
}
