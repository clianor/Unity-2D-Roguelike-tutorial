using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReStartButton : MonoBehaviour {
    private GameObject btn;

    void Start()
    {
        btn = GameObject.Find("ReStartButton");
        btn.SetActive(false);
    }

    public void ReStart()
    {
        GameManager.instance.LevelReset();
    }
}