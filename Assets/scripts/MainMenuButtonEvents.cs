using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonEvents : MonoBehaviour
{
    public Button Targets;
    public Button Expts;
    public Button Instruments;
    public Button Options;
    public Button Exit;
    public Button ReturnOptions;
    public Button ReturnHelp;
    public Button Help;
    public Slider AngularSpeed;
    public Slider RadialSpeed;
    private Scene myScene;
    public GameObject MainMenu;
    public GameObject OptionMenu;
    public GameObject HelpPanel;
    public static float AngularSpeedValue;
    public static float RadialSpeedValue;
    

    // Start is called before the first frame update
    void Start()
    {
        //Register Button Events
        Targets.onClick.AddListener(() => buttonCallBackTargets());
        Expts.onClick.AddListener(() => buttonCallBackExpts());
        Instruments.onClick.AddListener(() => buttonCallBackInstruments());
        Help.onClick.AddListener(() => buttonCallBackHelp());
        Options.onClick.AddListener(() => buttonCallBackOptions());
        ReturnOptions.onClick.AddListener(() => buttonCallBackReturnOptions());
        ReturnHelp.onClick.AddListener(() => buttonCallBackReturnHelp());
        Exit.onClick.AddListener(() => buttonCallBackExit());
        AngularSpeed.onValueChanged.AddListener(delegate { sliderCallBackAngularSpeed(); });
        RadialSpeed.onValueChanged.AddListener(delegate { sliderCallBackRadialSpeed(); });

        
        AngularSpeedValue = AngularSpeed.value;
        RadialSpeedValue = RadialSpeed.value;

        HelpPanel.SetActive(false);
        OptionMenu.SetActive(false);
        
    }

    public void buttonCallBackTargets() {
        SceneManager.LoadSceneAsync("Targets", LoadSceneMode.Single);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Targets"));
    }
        
    public void buttonCallBackExpts() {
        SceneManager.LoadSceneAsync("Expts", LoadSceneMode.Single);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Expts"));
    }

    public void buttonCallBackInstruments()
    {
        SceneManager.LoadSceneAsync("Instruments", LoadSceneMode.Single);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Expts"));
    }

    public void buttonCallBackOptions()
    {
        //Options
        OptionMenu.SetActive(true);
        MainMenu.SetActive(false);
        
    }

    public void buttonCallBackHelp()
    {
        //Options
        HelpPanel.SetActive(true);
        MainMenu.SetActive(false);

    }

    public void buttonCallBackExit() {
        Application.Quit();
    }

    public void buttonCallBackReturnOptions()
    {
        MainMenu.SetActive(true);
        OptionMenu.SetActive(false);
    }

    public void buttonCallBackReturnHelp()
    {
        MainMenu.SetActive(true);
        HelpPanel.SetActive(false);    
    }

    public void sliderCallBackAngularSpeed()
    {
        AngularSpeedValue = AngularSpeed.value;
    }

    public void sliderCallBackRadialSpeed()
    {
        RadialSpeedValue = RadialSpeed.value;
    }
}


