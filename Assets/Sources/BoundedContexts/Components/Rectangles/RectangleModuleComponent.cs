using System;
using Leopotam.EcsProto.Unity;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.BoundedContexts.Components.Rectangles
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Rectangle)]
    [Aspect(AspectName.Game)]
    public struct RectangleModuleComponent
    {
        public RectangleModule Value;
    }
}