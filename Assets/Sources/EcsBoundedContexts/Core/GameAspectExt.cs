using System;
using UnityEngine;
using Leopotam.EcsProto;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.GameServices.EntityPools.Domain.Components;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Domain;
using Sources.BoundedContexts.Presentation;
using Sources.BoundedContexts.Components.Slots;
using Sources.BoundedContexts.Components.Rectangles;
using Sources.BoundedContexts.Components.GameBoards;
using Sources.BoundedContexts.Components.Game;
using Sources.BoundedContexts.Components.Events;

namespace Sources.EcsBoundedContexts.Core
{
	public static class GameAspectExt
	{
		private static GameAspect s_GameAspect;

		public static void Construct(GameAspect aspect) =>
			s_GameAspect = aspect ?? throw new ArgumentNullException(nameof(aspect));

		//EventBuffer
		public static bool HasEventBuffer(this ProtoEntity entity) =>
			s_GameAspect.EventBuffer.Has(entity);

		public static ref EventBufferTag AddEventBuffer(this ProtoEntity entity)
		{
			ref EventBufferTag eventBufferTag = ref s_GameAspect.EventBuffer.Add(entity);
			return ref eventBufferTag;
		}

		public static void DelEventBuffer(this ProtoEntity entity)
			=> s_GameAspect.EventBuffer.Del(entity);

		//ReturnToPoolAction
		public static bool HasReturnToPoolAction(this ProtoEntity entity) =>
			s_GameAspect.ReturnToPoolAction.Has(entity);

		public static ref ReturnToPoolActionComponent GetReturnToPoolAction(this ProtoEntity entity) =>
			ref s_GameAspect.ReturnToPoolAction.Get(entity);

		public static void ReplaceReturnToPoolAction(this ProtoEntity entity, Action returnToPool)
		{
			ref ReturnToPoolActionComponent returnToPoolActionComponent = ref s_GameAspect.ReturnToPoolAction.Get(entity);
			returnToPoolActionComponent.ReturnToPool = returnToPool;
		}

		public static ref ReturnToPoolActionComponent AddReturnToPoolAction(this ProtoEntity entity, Action returnToPool)
		{
			ref ReturnToPoolActionComponent returnToPoolActionComponent = ref s_GameAspect.ReturnToPoolAction.Add(entity);
			returnToPoolActionComponent.ReturnToPool = returnToPool;
			return ref returnToPoolActionComponent;
		}

		public static void DelReturnToPoolAction(this ProtoEntity entity)
			=> s_GameAspect.ReturnToPoolAction.Del(entity);

		//ClearableData
		public static bool HasClearableData(this ProtoEntity entity) =>
			s_GameAspect.ClearableData.Has(entity);

		public static ref ClearableDataComponent AddClearableData(this ProtoEntity entity)
		{
			ref ClearableDataComponent clearableDataComponent = ref s_GameAspect.ClearableData.Add(entity);
			return ref clearableDataComponent;
		}

		public static void DelClearableData(this ProtoEntity entity)
			=> s_GameAspect.ClearableData.Del(entity);

		//ClearDataEvent
		public static bool HasClearDataEvent(this ProtoEntity entity) =>
			s_GameAspect.ClearDataEvent.Has(entity);

		public static ref ClearDataEvent AddClearDataEvent(this ProtoEntity entity)
		{
			ref ClearDataEvent clearDataEvent = ref s_GameAspect.ClearDataEvent.Add(entity);
			return ref clearDataEvent;
		}

		public static void DelClearDataEvent(this ProtoEntity entity)
			=> s_GameAspect.ClearDataEvent.Del(entity);

		//SavableData
		public static bool HasSavableData(this ProtoEntity entity) =>
			s_GameAspect.SavableData.Has(entity);

		public static ref SavableDataComponent AddSavableData(this ProtoEntity entity)
		{
			ref SavableDataComponent savableDataComponent = ref s_GameAspect.SavableData.Add(entity);
			return ref savableDataComponent;
		}

		public static void DelSavableData(this ProtoEntity entity)
			=> s_GameAspect.SavableData.Del(entity);

		//SaveDataEvent
		public static bool HasSaveDataEvent(this ProtoEntity entity) =>
			s_GameAspect.SaveDataEvent.Has(entity);

		public static ref SaveDataEvent AddSaveDataEvent(this ProtoEntity entity)
		{
			ref SaveDataEvent saveDataEvent = ref s_GameAspect.SaveDataEvent.Add(entity);
			return ref saveDataEvent;
		}

		public static void DelSaveDataEvent(this ProtoEntity entity)
			=> s_GameAspect.SaveDataEvent.Del(entity);

		//Active
		public static bool HasActive(this ProtoEntity entity) =>
			s_GameAspect.Active.Has(entity);

		public static ref ActiveComponent AddActive(this ProtoEntity entity)
		{
			ref ActiveComponent activeComponent = ref s_GameAspect.Active.Add(entity);
			return ref activeComponent;
		}

		public static void DelActive(this ProtoEntity entity)
			=> s_GameAspect.Active.Del(entity);

		//DisableGameObjectEvent
		public static bool HasDisableGameObjectEvent(this ProtoEntity entity) =>
			s_GameAspect.DisableGameObjectEvent.Has(entity);

		public static ref DisableGameObjectEvent AddDisableGameObjectEvent(this ProtoEntity entity)
		{
			ref DisableGameObjectEvent disableGameObjectEvent = ref s_GameAspect.DisableGameObjectEvent.Add(entity);
			return ref disableGameObjectEvent;
		}

		public static void DelDisableGameObjectEvent(this ProtoEntity entity)
			=> s_GameAspect.DisableGameObjectEvent.Del(entity);

		//EnableGameObjectEvent
		public static bool HasEnableGameObjectEvent(this ProtoEntity entity) =>
			s_GameAspect.EnableGameObjectEvent.Has(entity);

		public static ref EnableGameObjectEvent AddEnableGameObjectEvent(this ProtoEntity entity)
		{
			ref EnableGameObjectEvent enableGameObjectEvent = ref s_GameAspect.EnableGameObjectEvent.Add(entity);
			return ref enableGameObjectEvent;
		}

		public static void DelEnableGameObjectEvent(this ProtoEntity entity)
			=> s_GameAspect.EnableGameObjectEvent.Del(entity);

		//GameObject
		public static bool HasGameObject(this ProtoEntity entity) =>
			s_GameAspect.GameObject.Has(entity);

		public static ref GameObjectComponent GetGameObject(this ProtoEntity entity) =>
			ref s_GameAspect.GameObject.Get(entity);

		public static void ReplaceGameObject(this ProtoEntity entity, GameObject value)
		{
			ref GameObjectComponent gameObjectComponent = ref s_GameAspect.GameObject.Get(entity);
			gameObjectComponent.Value = value;
		}

		public static ref GameObjectComponent AddGameObject(this ProtoEntity entity, GameObject value)
		{
			ref GameObjectComponent gameObjectComponent = ref s_GameAspect.GameObject.Add(entity);
			gameObjectComponent.Value = value;
			return ref gameObjectComponent;
		}

		public static void DelGameObject(this ProtoEntity entity)
			=> s_GameAspect.GameObject.Del(entity);

		//EntityLink
		public static bool HasEntityLink(this ProtoEntity entity) =>
			s_GameAspect.EntityLink.Has(entity);

		public static ref EntityLinkComponent GetEntityLink(this ProtoEntity entity) =>
			ref s_GameAspect.EntityLink.Get(entity);

		public static void ReplaceEntityLink(this ProtoEntity entity, EntityLink entityLink, int entityId, ProtoEntity protoEntity)
		{
			ref EntityLinkComponent entityLinkComponent = ref s_GameAspect.EntityLink.Get(entity);
			entityLinkComponent.EntityLink = entityLink;
			entityLinkComponent.EntityId = entityId;
			entityLinkComponent.ProtoEntity = protoEntity;
		}

		public static ref EntityLinkComponent AddEntityLink(this ProtoEntity entity, EntityLink entityLink, int entityId, ProtoEntity protoEntity)
		{
			ref EntityLinkComponent entityLinkComponent = ref s_GameAspect.EntityLink.Add(entity);
			entityLinkComponent.EntityLink = entityLink;
			entityLinkComponent.EntityId = entityId;
			entityLinkComponent.ProtoEntity = protoEntity;
			return ref entityLinkComponent;
		}

		public static void DelEntityLink(this ProtoEntity entity)
			=> s_GameAspect.EntityLink.Del(entity);

		//InPool
		public static bool HasInPool(this ProtoEntity entity) =>
			s_GameAspect.InPool.Has(entity);

		public static ref InPoolComponent AddInPool(this ProtoEntity entity)
		{
			ref InPoolComponent inPoolComponent = ref s_GameAspect.InPool.Add(entity);
			return ref inPoolComponent;
		}

		public static void DelInPool(this ProtoEntity entity)
			=> s_GameAspect.InPool.Del(entity);

		//Transform
		public static bool HasTransform(this ProtoEntity entity) =>
			s_GameAspect.Transform.Has(entity);

		public static ref TransformComponent GetTransform(this ProtoEntity entity) =>
			ref s_GameAspect.Transform.Get(entity);

		public static void ReplaceTransform(this ProtoEntity entity, Transform value)
		{
			ref TransformComponent transformComponent = ref s_GameAspect.Transform.Get(entity);
			transformComponent.Value = value;
		}

		public static ref TransformComponent AddTransform(this ProtoEntity entity, Transform value)
		{
			ref TransformComponent transformComponent = ref s_GameAspect.Transform.Add(entity);
			transformComponent.Value = value;
			return ref transformComponent;
		}

		public static void DelTransform(this ProtoEntity entity)
			=> s_GameAspect.Transform.Del(entity);

		//InGameBoard
		public static bool HasInGameBoard(this ProtoEntity entity) =>
			s_GameAspect.InGameBoard.Has(entity);

		public static ref InGameBoardComponent AddInGameBoard(this ProtoEntity entity)
		{
			ref InGameBoardComponent inGameBoardComponent = ref s_GameAspect.InGameBoard.Add(entity);
			return ref inGameBoardComponent;
		}

		public static void DelInGameBoard(this ProtoEntity entity)
			=> s_GameAspect.InGameBoard.Del(entity);

		//Last
		public static bool HasLast(this ProtoEntity entity) =>
			s_GameAspect.Last.Has(entity);

		public static ref LastComponent AddLast(this ProtoEntity entity)
		{
			ref LastComponent lastComponent = ref s_GameAspect.Last.Add(entity);
			return ref lastComponent;
		}

		public static void DelLast(this ProtoEntity entity)
			=> s_GameAspect.Last.Del(entity);

		//RectangleColor
		public static bool HasRectangleColor(this ProtoEntity entity) =>
			s_GameAspect.RectangleColor.Has(entity);

		public static ref RectangleColorComponent GetRectangleColor(this ProtoEntity entity) =>
			ref s_GameAspect.RectangleColor.Get(entity);

		public static void ReplaceRectangleColor(this ProtoEntity entity, RectangleColors value)
		{
			ref RectangleColorComponent rectangleColorComponent = ref s_GameAspect.RectangleColor.Get(entity);
			rectangleColorComponent.Value = value;
		}

		public static ref RectangleColorComponent AddRectangleColor(this ProtoEntity entity, RectangleColors value)
		{
			ref RectangleColorComponent rectangleColorComponent = ref s_GameAspect.RectangleColor.Add(entity);
			rectangleColorComponent.Value = value;
			return ref rectangleColorComponent;
		}

		public static void DelRectangleColor(this ProtoEntity entity)
			=> s_GameAspect.RectangleColor.Del(entity);

		//RectangleModule
		public static bool HasRectangleModule(this ProtoEntity entity) =>
			s_GameAspect.RectangleModule.Has(entity);

		public static ref RectangleModuleComponent GetRectangleModule(this ProtoEntity entity) =>
			ref s_GameAspect.RectangleModule.Get(entity);

		public static void ReplaceRectangleModule(this ProtoEntity entity, RectangleModule value)
		{
			ref RectangleModuleComponent rectangleModuleComponent = ref s_GameAspect.RectangleModule.Get(entity);
			rectangleModuleComponent.Value = value;
		}

		public static ref RectangleModuleComponent AddRectangleModule(this ProtoEntity entity, RectangleModule value)
		{
			ref RectangleModuleComponent rectangleModuleComponent = ref s_GameAspect.RectangleModule.Add(entity);
			rectangleModuleComponent.Value = value;
			return ref rectangleModuleComponent;
		}

		public static void DelRectangleModule(this ProtoEntity entity)
			=> s_GameAspect.RectangleModule.Del(entity);

		//Rectangle
		public static bool HasRectangle(this ProtoEntity entity) =>
			s_GameAspect.Rectangle.Has(entity);

		public static ref RectangleTag AddRectangle(this ProtoEntity entity)
		{
			ref RectangleTag rectangleTag = ref s_GameAspect.Rectangle.Add(entity);
			return ref rectangleTag;
		}

		public static void DelRectangle(this ProtoEntity entity)
			=> s_GameAspect.Rectangle.Del(entity);

		//ChildRectangle
		public static bool HasChildRectangle(this ProtoEntity entity) =>
			s_GameAspect.ChildRectangle.Has(entity);

		public static ref ChildRectangleComponent GetChildRectangle(this ProtoEntity entity) =>
			ref s_GameAspect.ChildRectangle.Get(entity);

		public static void ReplaceChildRectangle(this ProtoEntity entity, ProtoEntity value)
		{
			ref ChildRectangleComponent childRectangleComponent = ref s_GameAspect.ChildRectangle.Get(entity);
			childRectangleComponent.Value = value;
		}

		public static ref ChildRectangleComponent AddChildRectangle(this ProtoEntity entity, ProtoEntity value)
		{
			ref ChildRectangleComponent childRectangleComponent = ref s_GameAspect.ChildRectangle.Add(entity);
			childRectangleComponent.Value = value;
			return ref childRectangleComponent;
		}

		public static void DelChildRectangle(this ProtoEntity entity)
			=> s_GameAspect.ChildRectangle.Del(entity);

		//RectangleSlotColor
		public static bool HasRectangleSlotColor(this ProtoEntity entity) =>
			s_GameAspect.RectangleSlotColor.Has(entity);

		public static ref RectangleSlotColor GetRectangleSlotColor(this ProtoEntity entity) =>
			ref s_GameAspect.RectangleSlotColor.Get(entity);

		public static void ReplaceRectangleSlotColor(this ProtoEntity entity, RectangleColors value)
		{
			ref RectangleSlotColor rectangleSlotColor = ref s_GameAspect.RectangleSlotColor.Get(entity);
			rectangleSlotColor.Value = value;
		}

		public static ref RectangleSlotColor AddRectangleSlotColor(this ProtoEntity entity, RectangleColors value)
		{
			ref RectangleSlotColor rectangleSlotColor = ref s_GameAspect.RectangleSlotColor.Add(entity);
			rectangleSlotColor.Value = value;
			return ref rectangleSlotColor;
		}

		public static void DelRectangleSlotColor(this ProtoEntity entity)
			=> s_GameAspect.RectangleSlotColor.Del(entity);

		//RectangleSlotModule
		public static bool HasRectangleSlotModule(this ProtoEntity entity) =>
			s_GameAspect.RectangleSlotModule.Has(entity);

		public static ref RectangleSlotModuleComponent GetRectangleSlotModule(this ProtoEntity entity) =>
			ref s_GameAspect.RectangleSlotModule.Get(entity);

		public static void ReplaceRectangleSlotModule(this ProtoEntity entity, RectangleSlotModule value)
		{
			ref RectangleSlotModuleComponent rectangleSlotModuleComponent = ref s_GameAspect.RectangleSlotModule.Get(entity);
			rectangleSlotModuleComponent.Value = value;
		}

		public static ref RectangleSlotModuleComponent AddRectangleSlotModule(this ProtoEntity entity, RectangleSlotModule value)
		{
			ref RectangleSlotModuleComponent rectangleSlotModuleComponent = ref s_GameAspect.RectangleSlotModule.Add(entity);
			rectangleSlotModuleComponent.Value = value;
			return ref rectangleSlotModuleComponent;
		}

		public static void DelRectangleSlotModule(this ProtoEntity entity)
			=> s_GameAspect.RectangleSlotModule.Del(entity);

		//RectangleSlot
		public static bool HasRectangleSlot(this ProtoEntity entity) =>
			s_GameAspect.RectangleSlot.Has(entity);

		public static ref RectangleSlotTag AddRectangleSlot(this ProtoEntity entity)
		{
			ref RectangleSlotTag rectangleSlotTag = ref s_GameAspect.RectangleSlot.Add(entity);
			return ref rectangleSlotTag;
		}

		public static void DelRectangleSlot(this ProtoEntity entity)
			=> s_GameAspect.RectangleSlot.Del(entity);

		//ParentSlot
		public static bool HasParentSlot(this ProtoEntity entity) =>
			s_GameAspect.ParentSlot.Has(entity);

		public static ref ParentSlotComponent GetParentSlot(this ProtoEntity entity) =>
			ref s_GameAspect.ParentSlot.Get(entity);

		public static void ReplaceParentSlot(this ProtoEntity entity, ProtoEntity value)
		{
			ref ParentSlotComponent parentSlotComponent = ref s_GameAspect.ParentSlot.Get(entity);
			parentSlotComponent.Value = value;
		}

		public static ref ParentSlotComponent AddParentSlot(this ProtoEntity entity, ProtoEntity value)
		{
			ref ParentSlotComponent parentSlotComponent = ref s_GameAspect.ParentSlot.Add(entity);
			parentSlotComponent.Value = value;
			return ref parentSlotComponent;
		}

		public static void DelParentSlot(this ProtoEntity entity)
			=> s_GameAspect.ParentSlot.Del(entity);

		//GameBoardModule
		public static bool HasGameBoardModule(this ProtoEntity entity) =>
			s_GameAspect.GameBoardModule.Has(entity);

		public static ref GameBoardModuleComponent GetGameBoardModule(this ProtoEntity entity) =>
			ref s_GameAspect.GameBoardModule.Get(entity);

		public static void ReplaceGameBoardModule(this ProtoEntity entity, GameBoardModule value)
		{
			ref GameBoardModuleComponent gameBoardModuleComponent = ref s_GameAspect.GameBoardModule.Get(entity);
			gameBoardModuleComponent.Value = value;
		}

		public static ref GameBoardModuleComponent AddGameBoardModule(this ProtoEntity entity, GameBoardModule value)
		{
			ref GameBoardModuleComponent gameBoardModuleComponent = ref s_GameAspect.GameBoardModule.Add(entity);
			gameBoardModuleComponent.Value = value;
			return ref gameBoardModuleComponent;
		}

		public static void DelGameBoardModule(this ProtoEntity entity)
			=> s_GameAspect.GameBoardModule.Del(entity);

		//GameBoard
		public static bool HasGameBoard(this ProtoEntity entity) =>
			s_GameAspect.GameBoard.Has(entity);

		public static ref GameBoardTag AddGameBoard(this ProtoEntity entity)
		{
			ref GameBoardTag gameBoardTag = ref s_GameAspect.GameBoard.Add(entity);
			return ref gameBoardTag;
		}

		public static void DelGameBoard(this ProtoEntity entity)
			=> s_GameAspect.GameBoard.Del(entity);

		//Game
		public static bool HasGame(this ProtoEntity entity) =>
			s_GameAspect.Game.Has(entity);

		public static ref GameTag AddGame(this ProtoEntity entity)
		{
			ref GameTag gameTag = ref s_GameAspect.Game.Add(entity);
			return ref gameTag;
		}

		public static void DelGame(this ProtoEntity entity)
			=> s_GameAspect.Game.Del(entity);

		//DestroyEvent
		public static bool HasDestroyEvent(this ProtoEntity entity) =>
			s_GameAspect.DestroyEvent.Has(entity);

		public static ref DestroyEvent AddDestroyEvent(this ProtoEntity entity)
		{
			ref DestroyEvent destroyEvent = ref s_GameAspect.DestroyEvent.Add(entity);
			return ref destroyEvent;
		}

		public static void DelDestroyEvent(this ProtoEntity entity)
			=> s_GameAspect.DestroyEvent.Del(entity);

		//DropRectanglesEvent
		public static bool HasDropRectanglesEvent(this ProtoEntity entity) =>
			s_GameAspect.DropRectanglesEvent.Has(entity);

		public static ref DropRectanglesEvent AddDropRectanglesEvent(this ProtoEntity entity)
		{
			ref DropRectanglesEvent dropRectanglesEvent = ref s_GameAspect.DropRectanglesEvent.Add(entity);
			return ref dropRectanglesEvent;
		}

		public static void DelDropRectanglesEvent(this ProtoEntity entity)
			=> s_GameAspect.DropRectanglesEvent.Del(entity);

		//FillSlotEvent
		public static bool HasFillSlotEvent(this ProtoEntity entity) =>
			s_GameAspect.FillSlotEvent.Has(entity);

		public static ref FillSlotEvent AddFillSlotEvent(this ProtoEntity entity)
		{
			ref FillSlotEvent fillSlotEvent = ref s_GameAspect.FillSlotEvent.Add(entity);
			return ref fillSlotEvent;
		}

		public static void DelFillSlotEvent(this ProtoEntity entity)
			=> s_GameAspect.FillSlotEvent.Del(entity);

		//LoadGameEvent
		public static bool HasLoadGameEvent(this ProtoEntity entity) =>
			s_GameAspect.LoadGameEvent.Has(entity);

		public static ref LoadGameEvent AddLoadGameEvent(this ProtoEntity entity)
		{
			ref LoadGameEvent loadGameEvent = ref s_GameAspect.LoadGameEvent.Add(entity);
			return ref loadGameEvent;
		}

		public static void DelLoadGameEvent(this ProtoEntity entity)
			=> s_GameAspect.LoadGameEvent.Del(entity);

		//MoveToEvent
		public static bool HasMoveToEvent(this ProtoEntity entity) =>
			s_GameAspect.MoveToEvent.Has(entity);

		public static ref MoveToEvent GetMoveToEvent(this ProtoEntity entity) =>
			ref s_GameAspect.MoveToEvent.Get(entity);

		public static void ReplaceMoveToEvent(this ProtoEntity entity, Vector3 value, Action onComplete)
		{
			ref MoveToEvent moveToEvent = ref s_GameAspect.MoveToEvent.Get(entity);
			moveToEvent.Value = value;
			moveToEvent.OnComplete = onComplete;
		}

		public static ref MoveToEvent AddMoveToEvent(this ProtoEntity entity, Vector3 value, Action onComplete)
		{
			ref MoveToEvent moveToEvent = ref s_GameAspect.MoveToEvent.Add(entity);
			moveToEvent.Value = value;
			moveToEvent.OnComplete = onComplete;
			return ref moveToEvent;
		}

		public static void DelMoveToEvent(this ProtoEntity entity)
			=> s_GameAspect.MoveToEvent.Del(entity);

		//OnBeginDragEvent
		public static bool HasOnBeginDragEvent(this ProtoEntity entity) =>
			s_GameAspect.OnBeginDragEvent.Has(entity);

		public static ref OnBeginDragEvent GetOnBeginDragEvent(this ProtoEntity entity) =>
			ref s_GameAspect.OnBeginDragEvent.Get(entity);

		public static void ReplaceOnBeginDragEvent(this ProtoEntity entity, GameObject value)
		{
			ref OnBeginDragEvent onBeginDragEvent = ref s_GameAspect.OnBeginDragEvent.Get(entity);
			onBeginDragEvent.Value = value;
		}

		public static ref OnBeginDragEvent AddOnBeginDragEvent(this ProtoEntity entity, GameObject value)
		{
			ref OnBeginDragEvent onBeginDragEvent = ref s_GameAspect.OnBeginDragEvent.Add(entity);
			onBeginDragEvent.Value = value;
			return ref onBeginDragEvent;
		}

		public static void DelOnBeginDragEvent(this ProtoEntity entity)
			=> s_GameAspect.OnBeginDragEvent.Del(entity);

		//OnDropEvent
		public static bool HasOnDropEvent(this ProtoEntity entity) =>
			s_GameAspect.OnDropEvent.Has(entity);

		public static ref OnDropEvent GetOnDropEvent(this ProtoEntity entity) =>
			ref s_GameAspect.OnDropEvent.Get(entity);

		public static void ReplaceOnDropEvent(this ProtoEntity entity, RectangleModule value)
		{
			ref OnDropEvent onDropEvent = ref s_GameAspect.OnDropEvent.Get(entity);
			onDropEvent.Value = value;
		}

		public static ref OnDropEvent AddOnDropEvent(this ProtoEntity entity, RectangleModule value)
		{
			ref OnDropEvent onDropEvent = ref s_GameAspect.OnDropEvent.Add(entity);
			onDropEvent.Value = value;
			return ref onDropEvent;
		}

		public static void DelOnDropEvent(this ProtoEntity entity)
			=> s_GameAspect.OnDropEvent.Del(entity);

		//OnEndDragEvent
		public static bool HasOnEndDragEvent(this ProtoEntity entity) =>
			s_GameAspect.OnEndDragEvent.Has(entity);

		public static ref OnEndDragEvent GetOnEndDragEvent(this ProtoEntity entity) =>
			ref s_GameAspect.OnEndDragEvent.Get(entity);

		public static void ReplaceOnEndDragEvent(this ProtoEntity entity, GameObject value)
		{
			ref OnEndDragEvent onEndDragEvent = ref s_GameAspect.OnEndDragEvent.Get(entity);
			onEndDragEvent.Value = value;
		}

		public static ref OnEndDragEvent AddOnEndDragEvent(this ProtoEntity entity, GameObject value)
		{
			ref OnEndDragEvent onEndDragEvent = ref s_GameAspect.OnEndDragEvent.Add(entity);
			onEndDragEvent.Value = value;
			return ref onEndDragEvent;
		}

		public static void DelOnEndDragEvent(this ProtoEntity entity)
			=> s_GameAspect.OnEndDragEvent.Del(entity);

		//PrintEvent
		public static bool HasPrintEvent(this ProtoEntity entity) =>
			s_GameAspect.PrintEvent.Has(entity);

		public static ref PrintEvent GetPrintEvent(this ProtoEntity entity) =>
			ref s_GameAspect.PrintEvent.Get(entity);

		public static void ReplacePrintEvent(this ProtoEntity entity, String key)
		{
			ref PrintEvent printEvent = ref s_GameAspect.PrintEvent.Get(entity);
			printEvent.Key = key;
		}

		public static ref PrintEvent AddPrintEvent(this ProtoEntity entity, String key)
		{
			ref PrintEvent printEvent = ref s_GameAspect.PrintEvent.Add(entity);
			printEvent.Key = key;
			return ref printEvent;
		}

		public static void DelPrintEvent(this ProtoEntity entity)
			=> s_GameAspect.PrintEvent.Del(entity);

	}
}
