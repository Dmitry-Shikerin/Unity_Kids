using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.BoundedContexts.Components.Events
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Rectangle)]
    [Aspect(AspectName.Game)]
    public struct OnEndDragEvent
    {
        public GameObject Value;
    }
}