using System;
using Leopotam.EcsProto.Unity;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.BoundedContexts.Components.Events
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Rectangle)]
    [Aspect(AspectName.Game)]
    public struct OnDropEvent
    {
        public RectangleModule Value;
    }
}