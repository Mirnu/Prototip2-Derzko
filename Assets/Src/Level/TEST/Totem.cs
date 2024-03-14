using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// пацаны, а просто тест на переход левла, потом поменяем на interactable пон
public class Totem : MonoBehaviour
{
    private LevelController _levelController;

    [Inject]
    public void Construct(LevelController levelController)
    {
        _levelController = levelController;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Character character))
        {
            _levelController.NextLevel();
        }
    }
}
