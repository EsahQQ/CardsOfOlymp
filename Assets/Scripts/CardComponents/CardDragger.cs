using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardComponents
{
    [RequireComponent(typeof(Card))] 
    public class CardDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField] private GameObject placeholderPrefab;

        private Canvas _rootCanvas;
        private CanvasGroup _canvasGroup;
        private GameObject _placeholder;

        [HideInInspector] public Transform DefaultParent;

        private Transform _gameField;
        private Transform _handField;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            
            _gameField = GameObject.Find("GameField").transform;
            _handField = GameObject.Find("Hand").transform;
            
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
            (transform as RectTransform).anchoredPosition += eventData.delta / _rootCanvas.scaleFactor;
            
            if (_placeholder.transform.parent != DefaultParent)
            {
                _placeholder.transform.SetParent(DefaultParent);
                StartCoroutine(AnimatePlaceholder(false));
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

        private IEnumerator AnimatePlaceholder(bool into)
        {
            var rect = _placeholder.GetComponent<RectTransform>();
            var target = 210;
            if (into)
                target = 0;
            float elapsedTime = 0f;
            while (elapsedTime < 0.35)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / 0.35f;
                if (into)
                    rect.sizeDelta = new Vector2(Mathf.SmoothStep(210, target, t), rect.sizeDelta.y);
                else
                    rect.sizeDelta = new Vector2(Mathf.SmoothStep(0, target, t), rect.sizeDelta.y);
                yield return null;
            }
            if (into)
                rect.sizeDelta = new Vector2(0, rect.sizeDelta.y);
            else
                rect.sizeDelta = new Vector2(210, rect.sizeDelta.y);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (transform.parent == _gameField)
                transform.SetParent(_handField);
            else
                transform.SetParent(_gameField);
        }
    }
}