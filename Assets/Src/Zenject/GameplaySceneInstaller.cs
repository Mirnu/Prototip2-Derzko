using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private Character _characterPrefab;
    [SerializeField] private LevelState _level;

    public override void InstallBindings()
    {
        BindPlayer();
        Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelState>().FromInstance(_level).AsSingle();
    }

    private void BindPlayer()
    {
        Character character = Container.InstantiatePrefabForComponent<Character>(_characterPrefab);
        Container.BindInterfacesAndSelfTo<Character>().FromInstance(character).AsSingle();
        Container.BindInterfacesAndSelfTo<Player>().FromNew().AsSingle();
    }
}
