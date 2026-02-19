using System;
using Leopotam.EcsProto.Unity;
using Sources.BoundedContexts.Domain;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.BoundedContexts.Components.Rectangles
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Rectangle)]
    [Aspect(AspectName.Game)]
    public struct RectangleColorComponent
    {
        public RectangleColors Value;
    }
}