using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;

    int finished = 0;

    string place1 = "1st PLACE";
    string place2 = "2nd PLACE";
    string place3 = "3rd PLACE";
    string place4 = "4th PLACE";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreboard();
    }

    public void AddToScoreboard(Sled sled)
    {
        string tab = "\t";
        if (sled.Name == "Red")
        {
            tab += "\t";
        }
        switch (finished)
        {
            case 0:
                place1 = "1." + sled.Name.ToUpper() + tab + GameManager.Instance.GetCurrentTime();
                break;
            case 1:
                place2 = "2." + sled.Name.ToUpper() + tab + GameManager.Instance.GetCurrentTime();
                break;
            case 2:
                place3 = "3." + sled.Name.ToUpper() + tab + GameManager.Instance.GetCurrentTime();
                break;
            case 3:
                place4 = "4." + sled.Name.ToUpper() + tab + GameManager.Instance.GetCurrentTime();
                break;
        }
        finished++;
    }

    void UpdateScoreboard()
    {
        text.text = place1 + "\n" +
        place2 + "\n" + place3 + "\n" + place4;
    }
}
