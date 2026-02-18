using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components;
using Sources.BoundedContexts.Components.Events;
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
        private readonly HudView _hudView;

        [DI] private readonly ProtoItExc _it = new(
            It.Inc<
                RectangleTag,
                OnBeginDragEvent>(),
            It.Exc<InPoolComponent>());
        
        public RectangleBeginDragSystem(HudView hudView)
        {
            _hudView = hudView;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                RectangleModule module = entity.GetRectangleModule().Value;

                DisableScroll();
                module.RectTransform.SetParent(_hudView.transform);
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