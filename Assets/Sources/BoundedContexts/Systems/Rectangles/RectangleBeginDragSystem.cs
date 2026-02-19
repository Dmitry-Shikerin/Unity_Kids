using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.Rectangles;
using Sources.BoundedContexts.Domain;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.BoundedContexts.Systems.Rectangles
{
    [EcsSystem(20)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class RectangleBeginDragSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoItExc _it = new(
            It.Inc<
                RectangleTag,
                OnBeginDragEvent>(),
            It.Exc<InPoolComponent>());

        private readonly HudView _hudView;
        private readonly UiView _parentView;

        public RectangleBeginDragSystem(HudView hudView)
        {
            _hudView = hudView;
            _parentView = _hudView.GetView(UiViewId.Gameplay);
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                RectangleModule module = entity.GetRectangleModule().Value;

                DisableScroll();
                module.RectTransform.SetParent(_parentView.transform);
                module.RectTransform.SetAsLastSibling();
                module.CanvasGroup.blocksRaycasts = false;
            }
        }

        private void DisableScroll()
        {
            _hudView.ScrollRect.StopMovement();
            _hudView.ScrollRect.enabled = false;
        }
    }
}