using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Change(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}