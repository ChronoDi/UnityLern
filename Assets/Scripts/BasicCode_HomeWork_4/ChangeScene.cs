using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : Button
{
    [SerializeField] private string _sceneName;

    public void Change()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
