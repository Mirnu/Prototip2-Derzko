using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LevelController>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<KeyboardHandler>().AsSingle();
    }
}
