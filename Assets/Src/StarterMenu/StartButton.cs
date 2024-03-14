using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartButton : MonoBehaviour
{
    private LevelController _levelController;
    private Button _button;

    [Inject]
    public void Construct(LevelController levelController)
    {
        _levelController = levelController;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(() => { 
            _button.onClick.RemoveAllListeners();
            _levelController.StartCycle();
        });   
    }
}
