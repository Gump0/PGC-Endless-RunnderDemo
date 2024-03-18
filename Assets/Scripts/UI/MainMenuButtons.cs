using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    public string gameScene = "GameScene";

    void Start(){
        startButton = GameObject.Find("Start").GetComponent<Button>();
        quitButton = GameObject.Find("Quit").GetComponent<Button>();

        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }
    public void StartGame(){
        SceneManager.LoadScene(gameScene);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
