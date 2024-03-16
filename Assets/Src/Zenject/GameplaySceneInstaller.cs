using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private Character _characterPrefab;

    public override void InstallBindings()
    {
        Handlers();
        BindPlayer();
        Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
    }

    private void BindPlayer()
    {
        Character character = Container.InstantiatePrefabForComponent<Character>(_characterPrefab);
        Container.BindInterfacesAndSelfTo<Character>().FromInstance(character).AsSingle();
        //Container.BindInterfacesAndSelfTo<Player>().FromNew().AsSingle();
    }

    private void Handlers()
    {
        Container.BindInterfacesAndSelfTo<KeyboardHandler>().AsSingle();
    }
}
