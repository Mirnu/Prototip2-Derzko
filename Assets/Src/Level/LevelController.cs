using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController
{
    private int currentLevel;

    private void init()
    {
        // ����� �� ����� ������� ������������ �� ����������, �� ���� ���
        currentLevel = 1;
    }

    public void StartCycle()
    {
        init();
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("Level" + currentLevel);
    }
}
