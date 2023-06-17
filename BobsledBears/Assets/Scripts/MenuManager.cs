using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField]
    public GameObject PreGame;
    [SerializeField]
    public GameObject PostGame;

    [SerializeField]
    GameObject BlueMenu;
    [SerializeField]
    GameObject RedMenu;
    [SerializeField]
    GameObject YellowMenu;
    [SerializeField]
    GameObject GreenMenu;

    [HideInInspector]
    public Slider blueDiff;
    [HideInInspector]
    public Slider redDiff;
    [HideInInspector]
    public Slider yellowDiff;
    [HideInInspector]
    public Slider greenDiff;
    Toggle blueCpu;
    Toggle redCpu;
    Toggle yellowCpu;
    Toggle greenCpu;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        blueDiff = BlueMenu.GetComponentInChildren<Slider>();
        redDiff = RedMenu.GetComponentInChildren<Slider>();
        yellowDiff = YellowMenu.GetComponentInChildren<Slider>();
        greenDiff = GreenMenu.GetComponentInChildren<Slider>();
        blueCpu = BlueMenu.GetComponentInChildren<Toggle>();
        redCpu = RedMenu.GetComponentInChildren<Toggle>();
        yellowCpu = YellowMenu.GetComponentInChildren<Toggle>();
        greenCpu = GreenMenu.GetComponentInChildren<Toggle>();

    }

    // Update is called once per frame
    void Update()
    {
        blueDiff.interactable = blueCpu.isOn;
        redDiff.interactable = redCpu.isOn;
        yellowDiff.interactable = yellowCpu.isOn;
        greenDiff.interactable = greenCpu.isOn;
    }
}
