using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.GameServices.EntityPools.Domain.Components;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.BoundedContexts.Components.Slots;
using Sources.BoundedContexts.Components.Rectangles;
using Sources.BoundedContexts.Components.GameBoards;
using Sources.BoundedContexts.Components.Game;
using Sources.BoundedContexts.Components.Events;

namespace Sources.EcsBoundedContexts.Core
{
	public class GameAspect : ProtoAspectInject
	{
		public readonly Dictionary<Type, IProtoPool> Pools;

		//Default

		//Common
		public readonly ProtoPool<ReturnToPoolActionComponent> ReturnToPoolAction = new ();
		public readonly ProtoPool<ClearableDataComponent> ClearableData = new ();
		public readonly ProtoPool<ClearDataEvent> ClearDataEvent = new ();
		public readonly ProtoPool<SavableDataComponent> SavableData = new ();
		public readonly ProtoPool<SaveDataEvent> SaveDataEvent = new ();
		public readonly ProtoPool<ActiveComponent> Active = new ();
		public readonly ProtoPool<DisableGameObjectEvent> DisableGameObjectEvent = new ();
		public readonly ProtoPool<EnableGameObjectEvent> EnableGameObjectEvent = new ();
		public readonly ProtoPool<GameObjectComponent> GameObject = new ();
		public readonly ProtoPool<EntityLinkComponent> EntityLink = new ();
		public readonly ProtoPool<InPoolComponent> InPool = new ();
		public readonly ProtoPool<TransformComponent> Transform = new ();

		//EventBuffer
		public readonly ProtoPool<EventBufferTag> EventBuffer = new ();

		//Rectangle
		public readonly ProtoPool<InGameBoardComponent> InGameBoard = new ();
		public readonly ProtoPool<LastComponent> Last = new ();
		public readonly ProtoPool<RectangleColorComponent> RectangleColor = new ();
		public readonly ProtoPool<RectangleModuleComponent> RectangleModule = new ();
		public readonly ProtoPool<RectangleTag> Rectangle = new ();
		public readonly ProtoPool<ChildRectangleComponent> ChildRectangle = new ();
		public readonly ProtoPool<RectangleSlotColor> RectangleSlotColor = new ();
		public readonly ProtoPool<RectangleSlotModuleComponent> RectangleSlotModule = new ();
		public readonly ProtoPool<RectangleSlotTag> RectangleSlot = new ();
		public readonly ProtoPool<ParentSlotComponent> ParentSlot = new ();
		public readonly ProtoPool<GameBoardModuleComponent> GameBoardModule = new ();
		public readonly ProtoPool<GameBoardTag> GameBoard = new ();
		public readonly ProtoPool<GameTag> Game = new ();
		public readonly ProtoPool<DestroyEvent> DestroyEvent = new ();
		public readonly ProtoPool<DropRectanglesEvent> DropRectanglesEvent = new ();
		public readonly ProtoPool<FillSlotEvent> FillSlotEvent = new ();
		public readonly ProtoPool<LoadGameEvent> LoadGameEvent = new ();
		public readonly ProtoPool<MoveToEvent> MoveToEvent = new ();
		public readonly ProtoPool<OnBeginDragEvent> OnBeginDragEvent = new ();
		public readonly ProtoPool<OnDropEvent> OnDropEvent = new ();
		public readonly ProtoPool<OnEndDragEvent> OnEndDragEvent = new ();
		public readonly ProtoPool<PrintEvent> PrintEvent = new ();

		public GameAspect()
		{
			Pools = new ()
			{
				[typeof(ProtoPool<EventBufferTag>)] = EventBuffer,
				[typeof(ProtoPool<ReturnToPoolActionComponent>)] = ReturnToPoolAction,
				[typeof(ProtoPool<ClearableDataComponent>)] = ClearableData,
				[typeof(ProtoPool<ClearDataEvent>)] = ClearDataEvent,
				[typeof(ProtoPool<SavableDataComponent>)] = SavableData,
				[typeof(ProtoPool<SaveDataEvent>)] = SaveDataEvent,
				[typeof(ProtoPool<ActiveComponent>)] = Active,
				[typeof(ProtoPool<DisableGameObjectEvent>)] = DisableGameObjectEvent,
				[typeof(ProtoPool<EnableGameObjectEvent>)] = EnableGameObjectEvent,
				[typeof(ProtoPool<GameObjectComponent>)] = GameObject,
				[typeof(ProtoPool<EntityLinkComponent>)] = EntityLink,
				[typeof(ProtoPool<InPoolComponent>)] = InPool,
				[typeof(ProtoPool<TransformComponent>)] = Transform,
				[typeof(ProtoPool<InGameBoardComponent>)] = InGameBoard,
				[typeof(ProtoPool<LastComponent>)] = Last,
				[typeof(ProtoPool<RectangleColorComponent>)] = RectangleColor,
				[typeof(ProtoPool<RectangleModuleComponent>)] = RectangleModule,
				[typeof(ProtoPool<RectangleTag>)] = Rectangle,
				[typeof(ProtoPool<ChildRectangleComponent>)] = ChildRectangle,
				[typeof(ProtoPool<RectangleSlotColor>)] = RectangleSlotColor,
				[typeof(ProtoPool<RectangleSlotModuleComponent>)] = RectangleSlotModule,
				[typeof(ProtoPool<RectangleSlotTag>)] = RectangleSlot,
				[typeof(ProtoPool<ParentSlotComponent>)] = ParentSlot,
				[typeof(ProtoPool<GameBoardModuleComponent>)] = GameBoardModule,
				[typeof(ProtoPool<GameBoardTag>)] = GameBoard,
				[typeof(ProtoPool<GameTag>)] = Game,
				[typeof(ProtoPool<DestroyEvent>)] = DestroyEvent,
				[typeof(ProtoPool<DropRectanglesEvent>)] = DropRectanglesEvent,
				[typeof(ProtoPool<FillSlotEvent>)] = FillSlotEvent,
				[typeof(ProtoPool<LoadGameEvent>)] = LoadGameEvent,
				[typeof(ProtoPool<MoveToEvent>)] = MoveToEvent,
				[typeof(ProtoPool<OnBeginDragEvent>)] = OnBeginDragEvent,
				[typeof(ProtoPool<OnDropEvent>)] = OnDropEvent,
				[typeof(ProtoPool<OnEndDragEvent>)] = OnEndDragEvent,
				[typeof(ProtoPool<PrintEvent>)] = PrintEvent,
			};

			GameAspectExt.Construct(this);
		}
	}
}
