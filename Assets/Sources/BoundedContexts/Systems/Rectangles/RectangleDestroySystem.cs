using DG.Tweening;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.Rectangles;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.BoundedContexts.Systems.Rectangles
{
    [EcsSystem(30)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class RectangleDestroySystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _destroyIt = new(
            It.Inc<
                RectangleTag,
                DestroyEvent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _destroyIt)
            {
                RectangleModule module = entity.GetRectangleModule().Value;

                Sequence sequence = DOTween.Sequence();
                sequence.Join(
                    module.transform
                        .DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                        .SetEase(Ease.Linear));
                sequence.Join(
                    module.CanvasGroup
                        .DOFade(0f, 1f)
                        .SetEase(Ease.InOutQuad));
                sequence.onComplete = () =>
                {
                    entity.GetReturnToPoolAction().ReturnToPool.Invoke();
                };
                sequence.Play();
            }
        }
    }
}