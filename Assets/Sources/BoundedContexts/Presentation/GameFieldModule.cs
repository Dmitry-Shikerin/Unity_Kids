using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Presentation
{
    public class GameFieldModule : EntityModule, IDropHandler
    {
        [SerializeField] private GameBoardModule _gameBoard;
        [SerializeField] private Image _image; 

        public void BlockRay(bool isBlock)
        {
            _image.enabled = isBlock;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            //Debug.Log($"OnDrag field");
            
            RectangleModule module =
                (RectangleModule)eventData.pointerDrag.GetComponent<RectangleModule>();

            if (module._isOnGameBoard)
                _gameBoard.Remove(module);
            
            gameObject.SetActive(false);
        }
    }
}