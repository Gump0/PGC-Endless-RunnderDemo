using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    public string gameScene = "GameScene";

    //Button Animations
    private float defualtButtonScale = 2.5f, maxButtonScale = 3.5f, elapsedHoverTime;
    private Button hoveredButton;
    [SerializeField] private bool isHovered = false;

    void Start(){
        startButton = GameObject.Find("Start").GetComponent<Button>();
        quitButton = GameObject.Find("Quit").GetComponent<Button>();

        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);

        // Add EventTrigger component to handle pointer events
        AddEventTrigger(startButton); //Pass StartButton Through AddEventTrigger Function
        AddEventTrigger(quitButton); //Pass QuitButton Through AddEventTrigger Function
    }
    void Update(){
        if(isHovered){
            elapsedHoverTime += Time.deltaTime;
            float t = Mathf.Sin(elapsedHoverTime / 2 * Mathf.PI * 2f -1) * 0.5f + 0.5f;
            float scale = Mathf.SmoothStep(defualtButtonScale, maxButtonScale, t);
            
            hoveredButton.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
    void AddEventTrigger(Button button){
        EventTrigger eventTrigger = button.GetComponent<EventTrigger>();
        if(eventTrigger == null)
        eventTrigger = button.gameObject.AddComponent<EventTrigger>();

        //Add pointer enter event listener
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => { OnButtonHovered(button); });
        eventTrigger.triggers.Add(pointerEnter);
        //Add pointer exit event listener
        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => { OnButtonLeave(button); });
        eventTrigger.triggers.Add(pointerExit);
    }
    void StartGame(){
        SceneManager.LoadScene(gameScene);
    }
    void QuitGame(){
        Application.Quit();
    }
    void OnButtonHovered(Button button){
        isHovered = true;
        hoveredButton = button;
    }
    void OnButtonLeave(Button button){
        isHovered = false;
        hoveredButton.transform.localScale = new Vector3(defualtButtonScale, defualtButtonScale, defualtButtonScale);
        hoveredButton = null;

        elapsedHoverTime = 0;
    }
}