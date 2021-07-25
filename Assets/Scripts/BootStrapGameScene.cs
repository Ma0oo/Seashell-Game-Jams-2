using System;
using System.Collections;
using HubObject;
using Plugins.HubObject.GlobalSystem;
using Services.Inputs;
using Services.Interfaces;
using UnityEngine;

public class BootStrapGameScene : MonoBehaviour
{
    public const string CanvasInventoryUi = "CanvasInventoryUi";
    public const string PlayerId = "Player";
    [SerializeField] private Canvas _canvasWithInventoryUi;
    [SerializeField] private Actor _player;
    
    private void Awake()
    {
        ServicesLocator.MainContainer.RegisterSingle<IInput>(new InputKeyboard());
        StartCoroutine(UpdateInput(ServicesLocator.MainContainer.ResolveSingle<IInput>()));
        ServicesLocator.MainContainer.RegisterSingle<Canvas>(_canvasWithInventoryUi, CanvasInventoryUi);
        ServicesLocator.MainContainer.RegisterSingle<Actor>(_player, PlayerId);
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