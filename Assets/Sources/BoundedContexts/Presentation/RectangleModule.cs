using System;
using DG.Tweening;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Presentation
{
    public class RectangleModule : EntityModule, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [field: Required] [field: SerializeField] public Image Background { get; private set; }
        [field: Required] [field: SerializeField] public RectangleSlotModule SlotModule { get; private set; }
        [Required] [SerializeField] private RectTransform _rectTransform;
        [Required] [SerializeField] private Canvas _mainCanvas;
        [Required] [SerializeField] public CanvasGroup _canvasGroup;
        [field: SerializeField] public RectTransform TopTransform { get; private set; }
        [field: SerializeField] public RectTransform LeftTransform { get; private set; }
        [field: SerializeField] public RectTransform RightTransform { get; private set; }

        public bool _isOnGameBoard;
        private GameBoardModule _gameboardModule;
        [FormerlySerializedAs("_isFirst")] public bool IsFirst;
        public bool _isLust;
        
        [Header("Настройки")]
        [SerializeField] private float dragThreshold = 10f; // Порог для определения драга
        [SerializeField] private bool blockScrollOnDrag = true;
    
        private ScrollRect parentScrollRect;
        private RectTransform rectTransform;
        private Canvas canvas;
        private Vector2 startDragPosition;
        private bool isDragging = false;
        private bool isHorizontalDrag = false;
        
        public HoleView holeView;
        public Text debugText;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();

            holeView = FindObjectOfType<HoleView>();
        
            // Находим родительский ScrollRect
            parentScrollRect = GetComponentInParent<ScrollRect>();
        }

        public void SetGameBoard(GameBoardModule gameBoardModule)
        {
            _gameboardModule = gameBoardModule;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            startDragPosition = eventData.position;
            isDragging = true;
            
            // Блокируем скролл на время драга
            if (parentScrollRect != null && blockScrollOnDrag)
            {
                parentScrollRect.StopMovement();
                parentScrollRect.enabled = false;
            }
            
            
            //Transform slotTransform = _rectTransform.parent;
            //slotTransform.SetAsLastSibling();

            if (_isOnGameBoard)
            {
                FindObjectOfType<GameFieldModule>(true).gameObject.SetActive(true);
            }
            
            _rectTransform.SetParent(_mainCanvas.transform);
            _rectTransform.SetAsLastSibling();
            
            _canvasGroup.blocksRaycasts = false;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            // if (!isDragging) return;
            //
            // // Определяем направление движения
            // Vector2 dragDirection = eventData.position - startDragPosition;
            //
            // // Если движение больше по горизонтали, чем по вертикали
            // if (Mathf.Abs(dragDirection.x) > Mathf.Abs(dragDirection.y))
            // {
            //     isHorizontalDrag = true;
            //
            //     // Если движение достаточно сильное, отдаем управление скроллу
            //     if (Mathf.Abs(dragDirection.x) > dragThreshold)
            //     {
            //         // Возвращаем управление скроллу
            //         //ReturnControlToScroll();
            //         if (parentScrollRect != null)
            //         {
            //             parentScrollRect.enabled = true;
            //         }
            //         return;
            //     }
            // }
            //
            // // Если движение вертикальное или слабое горизонтальное - дражим объект
            // if (!isHorizontalDrag)
            // {
            //     //DragObject(eventData);
            //     _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
            // }
            
            
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            isHorizontalDrag = false;
            
            if (parentScrollRect != null)
            {
                parentScrollRect.enabled = true;
            }
            
            
            HoleView holeView =
                (HoleView)eventData.pointerEnter.GetComponent<HoleView>();
            var isPointInside = IsPointInside2(Input.mousePosition);
            Debug.Log($"Is point inside {isPointInside}");

            if (_isOnGameBoard && isPointInside)
            {
                transform
                    .DOJump(holeView.transform.position, 300, 1, 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                {
                    FindObjectOfType<GameBoardModule>().Remove(this);
                    Debug.Log($"Попал в дыру");
                    FindObjectOfType<GameFieldModule>().gameObject.SetActive(false);
                });
                _canvasGroup.blocksRaycasts = true;
                return;
            }

            // if (_isOnGameBoard)
            // {
            //     FindObjectOfType<GameBoardModule>().Remove(this);
            // }

            GameBoardModule module =
                (GameBoardModule)eventData.pointerEnter.GetComponent<GameBoardModule>();

            if (module == null)
            {
                transform.SetParent(SlotModule.transform);
                transform.localPosition = Vector3.zero;
                Debug.Log($"Не попал в игровое поле");
            }

            //transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var isPointInside = IsPointInside(Input.mousePosition);
                Debug.Log($"Is point inside {isPointInside}");
            }
            
            // if (holeView == null) return;
            //
            // Vector2 mousePos = Input.mousePosition;
            // bool isInside = holeView.IsPointInside(mousePos);
            //
            // // Визуализация
            // if (isInside)
            // {
            //     Debug.Log($"<color=green>ВНУТРИ! {mousePos}</color>");
            //     if (debugText != null)
            //         debugText.text = $"Внутри! {mousePos}";
            // }
            // else
            // {
            //     if (debugText != null)
            //         debugText.text = $"Снаружи {mousePos}";
            // }
            //
            // // Рисуем точку в месте курсора
            // Debug.DrawLine(
            //     Camera.main.ScreenToWorldPoint(mousePos),
            //     Camera.main.ScreenToWorldPoint(mousePos) + Vector3.up * 10,
            //     isInside ? Color.green : Color.red
            // );
        }

        public void MoveDown(Vector3 pos)
        {
            // if (_isOnGameBoard)
            // {
            //     Debug.Log($"Если не попал в дыру то уничтожить");
            //     _gameboardModule.Remove(this);
            //     Destroy(gameObject);
            //     return;
            // }
            
            SlotModule.FillSlot();
            //transform.position = pos;
            transform.DOJump(pos, 300, 1, 0.5f).SetEase(Ease.Linear);
            _isOnGameBoard = true;
            //Debug.Log($"Заполнить слот");
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector3.down);
        }
        // Временная замена метода для проверки
        public bool IsPointInside2(Vector2 screenPosition)
        {
            // Простейшая проверка - просто попадание в прямоугольник
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, screenPosition, Camera.main, out Vector2 localPoint);
    
            Rect rect = rectTransform.rect;
            return rect.Contains(localPoint);
        }
        
        public bool IsPointInside(Vector2 screenPosition)
        {
            //Debug.Log($"IsPointInside mouse position {screenPosition}");
            var _holeRect = (RectTransform)FindObjectOfType<HoleView>().gameObject.transform;
            
            // Debug.Log($"Размер: {_holeRect.rect.size}");
            // Debug.Log($"Позиция: {_holeRect.position}");
            // Debug.Log($"Pivot: {_holeRect.pivot}");
            //
            // // 2. Проверка камеры
            // Debug.Log($"Camera.main: {Camera.main}");

            if (_holeRect == null)
            {
                Debug.Log($"Hole Rect null");
                return false;
            }
            

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _holeRect, screenPosition, FindObjectOfType<HudView>().GetComponent<Canvas>().worldCamera, out var localPoint))
            {
                Debug.Log($"Не попал");
                return false;
            }

            // Получаем прямоугольник
            Rect rect = _holeRect.rect;
            
            var r = _holeRect.rect;
            var semiX = r.width * 0.5f;
            var semiY = r.height * 0.5f;
            
            if (semiX <= 0f || semiY <= 0f)
            {
                Debug.Log($"Не попал");
                return false;
            }
            
            // // Полуоси
            // float halfWidth = rect.width * 0.5f;
            // float halfHeight = rect.height * 0.5f;
            //
            // if (halfWidth <= 0f || halfHeight <= 0f)
            //     return false;


            var cx = r.center.x;
            var cy = r.center.y;
            var dx = (localPoint.x - cx) / semiX;
            var dy = (localPoint.y - cy) / semiY;
            
            var result = dx * dx + dy * dy <= 1f;
            Debug.Log($"result {result}");
            return result;
            
            // // Нормализуем координаты относительно центра
            // float normalizedX = localPoint.x / halfWidth;
            // float normalizedY = localPoint.y / halfHeight;
            //
            // // Отладка
            // Debug.Log($"=== Проверка попадания ===");
            // Debug.Log($"Экранная точка: {screenPosition}");
            // Debug.Log($"Локальная точка: {localPoint}");
            // Debug.Log($"Размеры: width={rect.width}, height={rect.height}");
            // Debug.Log($"Полуоси: X={halfWidth}, Y={halfHeight}");
            // Debug.Log($"Нормализованные: X={normalizedX}, Y={normalizedY}");
            // Debug.Log($"Формула: {normalizedX}² + {normalizedY}² = {normalizedX * normalizedX + normalizedY * normalizedY}");
            //
            // // Проверка по формуле эллипса
            // bool result = (normalizedX * normalizedX + normalizedY * normalizedY) <= 1f;
            // Debug.Log($"РЕЗУЛЬТАТ: {result}");
            //
            // return result;
        }
    }
}