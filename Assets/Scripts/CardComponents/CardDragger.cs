using UnityEngine;
using UnityEngine.EventSystems;

namespace CardComponents
{
    [RequireComponent(typeof(Card))] // Убедимся, что главный компонент Card всегда есть
    public class CardDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject placeholderPrefab;

        private Canvas _rootCanvas;
        private CanvasGroup _canvasGroup;
        private GameObject _placeholder;
        
        // Ссылка на главный компонент, чтобы знать, куда возвращаться
        [HideInInspector] public Transform DefaultParent;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;

            if (placeholderPrefab != null)
            {
                _placeholder = Instantiate(placeholderPrefab, _rootCanvas.transform);
                _placeholder.name = "Placeholder for " + this.gameObject.name;
                _placeholder.SetActive(false);
            }
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            DefaultParent = transform.parent;
            
            _placeholder.transform.SetParent(DefaultParent);
            _placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());
            _placeholder.SetActive(true);
            
            transform.SetParent(_rootCanvas.transform);
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Важно: RectTransform теперь нужно получать отдельно
            (transform as RectTransform).anchoredPosition += eventData.delta / _rootCanvas.scaleFactor;
            
            if (_placeholder.transform.parent != DefaultParent)
            {
                _placeholder.transform.SetParent(DefaultParent);
            }

            int newSiblingIndex = DefaultParent.childCount;
            for (int i = 0; i < DefaultParent.childCount; i++)
            {
                if (transform.position.x < DefaultParent.GetChild(i).position.x)
                {
                    newSiblingIndex = i;
                    if (_placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    {
                        newSiblingIndex--;
                    }
                    break;
                }
            }
            _placeholder.transform.SetSiblingIndex(newSiblingIndex);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(DefaultParent);
            transform.SetSiblingIndex(_placeholder.transform.GetSiblingIndex());
            
            _placeholder.SetActive(false); 
            _placeholder.transform.SetParent(_rootCanvas.transform); 
            
            _canvasGroup.blocksRaycasts = true;
        }
    }
}