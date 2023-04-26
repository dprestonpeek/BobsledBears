using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    Scoreboard scoreboard;

    [SerializeField]
    GameObject EndTrigger;

    [SerializeField]
    public List<GameObject> finished = new List<GameObject>();

    bool raceStarted = true;
    float timer = 0;
    string timerText = "00:00";

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (raceStarted)
        {
            timer += Time.deltaTime;
            string mins = "" + (timer / 60);
            float secs = timer % 60;
            timerText = mins[0] + ":" + secs;
        }
    }

    public void AddToScoreboard(Sled sled)
    {
        finished.Add(sled.gameObject);
        scoreboard.AddToScoreboard(sled);
    }

    public string GetCurrentTime()
    {
        return timerText;
    }
}
