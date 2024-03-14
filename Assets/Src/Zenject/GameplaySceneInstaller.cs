using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private Character _characterPrefab;
    [SerializeField] private Totem _totemPrefab;

    public override void InstallBindings()
    {
        BindPlayer();
        Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<Totem>().FromInstance(_totemPrefab).AsSingle();
    }

    private void BindPlayer()
    {
        Character character = Container.InstantiatePrefabForComponent<Character>(_characterPrefab);
        Container.BindInterfacesAndSelfTo<Character>().FromInstance(character).AsSingle();
    }
}
