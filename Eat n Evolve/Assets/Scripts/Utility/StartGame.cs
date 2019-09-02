using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartGame : MonoBehaviour
{

    void Start()
    {
        Debug.Log("LoadSceneB");
    }

    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestBed");

    }
}
