using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool raceInSession = false;

    [SerializeField]
    Scoreboard scoreboard;

    [SerializeField]
    GameObject EndTrigger;

    [SerializeField]
    SledManager sleds;

    [SerializeField]
    public List<GameObject> finished = new List<GameObject>();

    bool raceStarted = false;
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
            if (!raceInSession)
            {
                raceInSession = true;
            }
            if (finished.Contains(sleds.sleds[0].gameObject))
            {
                EndRace();
            }
            if (finished.Count < 4)
            {
                timer += Time.deltaTime;
                string mins = "" + (timer / 60);
                float secs = timer % 60;
                timerText = mins[0] + ":" + secs;
            }
        }
        else
        {
            foreach (Rigidbody rb in sleds.sledRbs)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }

    public void StartRace()
    {
        foreach (Rigidbody rb in sleds.sledRbs)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        MenuManager.Instance.PreGame.SetActive(false);
        raceStarted = true;
    }

    public void EndRace()
    {
        raceInSession = false;
        MenuManager.Instance.PostGame.SetActive(true);
    }

    public void SetSledDifficulty(int sled)
    {
        switch (sled)
        {
            case 0:
                sleds.sleds[0].SetDifficulty(MenuManager.Instance.blueDiff.value);
                break;
            case 1:
                sleds.sleds[1].SetDifficulty(MenuManager.Instance.redDiff.value);
                break;
            case 2:
                sleds.sleds[2].SetDifficulty(MenuManager.Instance.yellowDiff.value);
                break;
            case 3:
                sleds.sleds[3].SetDifficulty(MenuManager.Instance.greenDiff.value);
                break;
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

    public void RestartScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
