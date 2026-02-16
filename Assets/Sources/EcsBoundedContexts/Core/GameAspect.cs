using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.GameServices.EntityPools.Domain.Components;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Animators;
using Sources.BoundedContexts.Components;

namespace Sources.EcsBoundedContexts.Core
{
	public class GameAspect : ProtoAspectInject
	{
		public readonly Dictionary<Type, IProtoPool> Pools;

		//Default

		//Common
		public readonly ProtoPool<ReturnToPoolActionComponent> ReturnToPoolAction = new ();
		public readonly ProtoPool<TimerComponent> Timer = new ();
		public readonly ProtoPool<ClearableDataComponent> ClearableData = new ();
		public readonly ProtoPool<ClearDataEvent> ClearDataEvent = new ();
		public readonly ProtoPool<SavableDataComponent> SavableData = new ();
		public readonly ProtoPool<SaveDataEvent> SaveDataEvent = new ();
		public readonly ProtoPool<ActiveComponent> Active = new ();
		public readonly ProtoPool<DisableGameObjectEvent> DisableGameObjectEvent = new ();
		public readonly ProtoPool<EnableGameObjectEvent> EnableGameObjectEvent = new ();
		public readonly ProtoPool<GameObjectComponent> GameObject = new ();
		public readonly ProtoPool<AvailableComponent> Available = new ();
		public readonly ProtoPool<CompleteComponent> Complete = new ();
		public readonly ProtoPool<DecreaseEvent> DecreaseEvent = new ();
		public readonly ProtoPool<DistanceComponent> Distance = new ();
		public readonly ProtoPool<EntityLinkComponent> EntityLink = new ();
		public readonly ProtoPool<IncreaseEvent> IncreaseEvent = new ();
		public readonly ProtoPool<InitializedComponent> Initialized = new ();
		public readonly ProtoPool<InitializeEvent> InitializeEvent = new ();
		public readonly ProtoPool<InPoolComponent> InPool = new ();
		public readonly ProtoPool<ScaleComponent> Scale = new ();
		public readonly ProtoPool<StringIdComponent> StringId = new ();
		public readonly ProtoPool<TransformComponent> Transform = new ();
		public readonly ProtoPool<AnimatorComponent> Animator = new ();

		//EventBuffer
		public readonly ProtoPool<EventBufferTag> EventBuffer = new ();

		//Rectangle
		public readonly ProtoPool<RectangleColorComponent> RectangleColor = new ();
		public readonly ProtoPool<RectangleTag> Rectangle = new ();

		public GameAspect()
		{
			Pools = new ()
			{
				[typeof(ProtoPool<EventBufferTag>)] = EventBuffer,
				[typeof(ProtoPool<ReturnToPoolActionComponent>)] = ReturnToPoolAction,
				[typeof(ProtoPool<TimerComponent>)] = Timer,
				[typeof(ProtoPool<ClearableDataComponent>)] = ClearableData,
				[typeof(ProtoPool<ClearDataEvent>)] = ClearDataEvent,
				[typeof(ProtoPool<SavableDataComponent>)] = SavableData,
				[typeof(ProtoPool<SaveDataEvent>)] = SaveDataEvent,
				[typeof(ProtoPool<ActiveComponent>)] = Active,
				[typeof(ProtoPool<DisableGameObjectEvent>)] = DisableGameObjectEvent,
				[typeof(ProtoPool<EnableGameObjectEvent>)] = EnableGameObjectEvent,
				[typeof(ProtoPool<GameObjectComponent>)] = GameObject,
				[typeof(ProtoPool<AvailableComponent>)] = Available,
				[typeof(ProtoPool<CompleteComponent>)] = Complete,
				[typeof(ProtoPool<DecreaseEvent>)] = DecreaseEvent,
				[typeof(ProtoPool<DistanceComponent>)] = Distance,
				[typeof(ProtoPool<EntityLinkComponent>)] = EntityLink,
				[typeof(ProtoPool<IncreaseEvent>)] = IncreaseEvent,
				[typeof(ProtoPool<InitializedComponent>)] = Initialized,
				[typeof(ProtoPool<InitializeEvent>)] = InitializeEvent,
				[typeof(ProtoPool<InPoolComponent>)] = InPool,
				[typeof(ProtoPool<ScaleComponent>)] = Scale,
				[typeof(ProtoPool<StringIdComponent>)] = StringId,
				[typeof(ProtoPool<TransformComponent>)] = Transform,
				[typeof(ProtoPool<AnimatorComponent>)] = Animator,
				[typeof(ProtoPool<RectangleColorComponent>)] = RectangleColor,
				[typeof(ProtoPool<RectangleTag>)] = Rectangle,
			};

			GameAspectExt.Construct(this);
		}
	}
}
