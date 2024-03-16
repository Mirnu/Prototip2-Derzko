using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private StartButton _startButton;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StartButton>().FromInstance(_startButton).AsSingle();
    }
}
