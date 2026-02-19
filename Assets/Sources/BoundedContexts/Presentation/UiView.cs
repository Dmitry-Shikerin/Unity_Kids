using Sources.BoundedContexts.Domain;
using UnityEngine;

namespace Sources.BoundedContexts.Presentation
{
    public class UiView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [field: SerializeField] public UiViewId UiViewId { get; private set; }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }
}