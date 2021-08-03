using System;
using System.Collections;
using HabObjects;
using Plugins.HabObject.DIContainer;
using Services.Inputs;
using Services.Interfaces;
using UnityEngine;

public class BootStrapGameScene : MonoBehaviour
{
    public const string CanvasInventoryUi = "CanvasInventoryUi";
    public const string PlayerId = "Player";
    public const string ParentRoomId = "Player";
    
    [SerializeField] private Canvas _canvasWithInventoryUi;
    [SerializeField] private Actor _player;
    [SerializeField] private Transform _parentRoom;
    
    private void Awake()
    {
        DiServices.MainContainer.RegisterSingle<IInput>(new InputKeyboard());
        StartCoroutine(UpdateInput(DiServices.MainContainer.ResolveSingle<IInput>()));
        DiServices.MainContainer.RegisterSingle<Canvas>(_canvasWithInventoryUi, CanvasInventoryUi);
        DiServices.MainContainer.RegisterSingle<Actor>(_player, PlayerId);
        DiServices.MainContainer.RegisterSingle<Transform>(_parentRoom, ParentRoomId);
    }

    private IEnumerator UpdateInput(IInput inputForUpdate)
    {
        IInput input = inputForUpdate;
        while (input != null)
        {
            input.Update();
            yield return null;
        }
    }
}