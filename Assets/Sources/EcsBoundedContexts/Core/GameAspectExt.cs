using System;
using UnityEngine;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.GameServices.EntityPools.Domain.Components;
using System;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using UnityEngine;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Animators;
using Sources.BoundedContexts.Components;
using Sources.BoundedContexts.Domain;

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

		//Timer
		public static bool HasTimer(this ProtoEntity entity) =>
			s_GameAspect.Timer.Has(entity);

		public static ref TimerComponent GetTimer(this ProtoEntity entity) =>
			ref s_GameAspect.Timer.Get(entity);

		public static void ReplaceTimer(this ProtoEntity entity, float value)
		{
			ref TimerComponent timerComponent = ref s_GameAspect.Timer.Get(entity);
			timerComponent.Value = value;
		}

		public static ref TimerComponent AddTimer(this ProtoEntity entity, float value)
		{
			ref TimerComponent timerComponent = ref s_GameAspect.Timer.Add(entity);
			timerComponent.Value = value;
			return ref timerComponent;
		}

		public static void DelTimer(this ProtoEntity entity)
			=> s_GameAspect.Timer.Del(entity);

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

		//Available
		public static bool HasAvailable(this ProtoEntity entity) =>
			s_GameAspect.Available.Has(entity);

		public static ref AvailableComponent AddAvailable(this ProtoEntity entity)
		{
			ref AvailableComponent availableComponent = ref s_GameAspect.Available.Add(entity);
			return ref availableComponent;
		}

		public static void DelAvailable(this ProtoEntity entity)
			=> s_GameAspect.Available.Del(entity);

		//Complete
		public static bool HasComplete(this ProtoEntity entity) =>
			s_GameAspect.Complete.Has(entity);

		public static ref CompleteComponent AddComplete(this ProtoEntity entity)
		{
			ref CompleteComponent completeComponent = ref s_GameAspect.Complete.Add(entity);
			return ref completeComponent;
		}

		public static void DelComplete(this ProtoEntity entity)
			=> s_GameAspect.Complete.Del(entity);

		//DecreaseEvent
		public static bool HasDecreaseEvent(this ProtoEntity entity) =>
			s_GameAspect.DecreaseEvent.Has(entity);

		public static ref DecreaseEvent AddDecreaseEvent(this ProtoEntity entity)
		{
			ref DecreaseEvent decreaseEvent = ref s_GameAspect.DecreaseEvent.Add(entity);
			return ref decreaseEvent;
		}

		public static void DelDecreaseEvent(this ProtoEntity entity)
			=> s_GameAspect.DecreaseEvent.Del(entity);

		//Distance
		public static bool HasDistance(this ProtoEntity entity) =>
			s_GameAspect.Distance.Has(entity);

		public static ref DistanceComponent GetDistance(this ProtoEntity entity) =>
			ref s_GameAspect.Distance.Get(entity);

		public static void ReplaceDistance(this ProtoEntity entity, float value)
		{
			ref DistanceComponent distanceComponent = ref s_GameAspect.Distance.Get(entity);
			distanceComponent.Value = value;
		}

		public static ref DistanceComponent AddDistance(this ProtoEntity entity, float value)
		{
			ref DistanceComponent distanceComponent = ref s_GameAspect.Distance.Add(entity);
			distanceComponent.Value = value;
			return ref distanceComponent;
		}

		public static void DelDistance(this ProtoEntity entity)
			=> s_GameAspect.Distance.Del(entity);

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

		//IncreaseEvent
		public static bool HasIncreaseEvent(this ProtoEntity entity) =>
			s_GameAspect.IncreaseEvent.Has(entity);

		public static ref IncreaseEvent AddIncreaseEvent(this ProtoEntity entity)
		{
			ref IncreaseEvent increaseEvent = ref s_GameAspect.IncreaseEvent.Add(entity);
			return ref increaseEvent;
		}

		public static void DelIncreaseEvent(this ProtoEntity entity)
			=> s_GameAspect.IncreaseEvent.Del(entity);

		//Initialized
		public static bool HasInitialized(this ProtoEntity entity) =>
			s_GameAspect.Initialized.Has(entity);

		public static ref InitializedComponent AddInitialized(this ProtoEntity entity)
		{
			ref InitializedComponent initializedComponent = ref s_GameAspect.Initialized.Add(entity);
			return ref initializedComponent;
		}

		public static void DelInitialized(this ProtoEntity entity)
			=> s_GameAspect.Initialized.Del(entity);

		//InitializeEvent
		public static bool HasInitializeEvent(this ProtoEntity entity) =>
			s_GameAspect.InitializeEvent.Has(entity);

		public static ref InitializeEvent AddInitializeEvent(this ProtoEntity entity)
		{
			ref InitializeEvent initializeEvent = ref s_GameAspect.InitializeEvent.Add(entity);
			return ref initializeEvent;
		}

		public static void DelInitializeEvent(this ProtoEntity entity)
			=> s_GameAspect.InitializeEvent.Del(entity);

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

		//Scale
		public static bool HasScale(this ProtoEntity entity) =>
			s_GameAspect.Scale.Has(entity);

		public static ref ScaleComponent GetScale(this ProtoEntity entity) =>
			ref s_GameAspect.Scale.Get(entity);

		public static void ReplaceScale(this ProtoEntity entity, Vector3 targetScale, Vector3 currentScale)
		{
			ref ScaleComponent scaleComponent = ref s_GameAspect.Scale.Get(entity);
			scaleComponent.TargetScale = targetScale;
			scaleComponent.CurrentScale = currentScale;
		}

		public static ref ScaleComponent AddScale(this ProtoEntity entity, Vector3 targetScale, Vector3 currentScale)
		{
			ref ScaleComponent scaleComponent = ref s_GameAspect.Scale.Add(entity);
			scaleComponent.TargetScale = targetScale;
			scaleComponent.CurrentScale = currentScale;
			return ref scaleComponent;
		}

		public static void DelScale(this ProtoEntity entity)
			=> s_GameAspect.Scale.Del(entity);

		//StringId
		public static bool HasStringId(this ProtoEntity entity) =>
			s_GameAspect.StringId.Has(entity);

		public static ref StringIdComponent GetStringId(this ProtoEntity entity) =>
			ref s_GameAspect.StringId.Get(entity);

		public static void ReplaceStringId(this ProtoEntity entity, String value)
		{
			ref StringIdComponent stringIdComponent = ref s_GameAspect.StringId.Get(entity);
			stringIdComponent.Value = value;
		}

		public static ref StringIdComponent AddStringId(this ProtoEntity entity, String value)
		{
			ref StringIdComponent stringIdComponent = ref s_GameAspect.StringId.Add(entity);
			stringIdComponent.Value = value;
			return ref stringIdComponent;
		}

		public static void DelStringId(this ProtoEntity entity)
			=> s_GameAspect.StringId.Del(entity);

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

		//Animator
		public static bool HasAnimator(this ProtoEntity entity) =>
			s_GameAspect.Animator.Has(entity);

		public static ref AnimatorComponent GetAnimator(this ProtoEntity entity) =>
			ref s_GameAspect.Animator.Get(entity);

		public static void ReplaceAnimator(this ProtoEntity entity, Animator value)
		{
			ref AnimatorComponent animatorComponent = ref s_GameAspect.Animator.Get(entity);
			animatorComponent.Value = value;
		}

		public static ref AnimatorComponent AddAnimator(this ProtoEntity entity, Animator value)
		{
			ref AnimatorComponent animatorComponent = ref s_GameAspect.Animator.Add(entity);
			animatorComponent.Value = value;
			return ref animatorComponent;
		}

		public static void DelAnimator(this ProtoEntity entity)
			=> s_GameAspect.Animator.Del(entity);

		//RectangleColor
		public static bool HasRectangleColor(this ProtoEntity entity) =>
			s_GameAspect.RectangleColor.Has(entity);

		public static ref RectangleColorComponent GetRectangleColor(this ProtoEntity entity) =>
			ref s_GameAspect.RectangleColor.Get(entity);

		public static void ReplaceRectangleColor(this ProtoEntity entity, RectangleColors color)
		{
			ref RectangleColorComponent rectangleColorComponent = ref s_GameAspect.RectangleColor.Get(entity);
			rectangleColorComponent.Color = color;
		}

		public static ref RectangleColorComponent AddRectangleColor(this ProtoEntity entity, RectangleColors color)
		{
			ref RectangleColorComponent rectangleColorComponent = ref s_GameAspect.RectangleColor.Add(entity);
			rectangleColorComponent.Color = color;
			return ref rectangleColorComponent;
		}

		public static void DelRectangleColor(this ProtoEntity entity)
			=> s_GameAspect.RectangleColor.Del(entity);

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

	}
}
