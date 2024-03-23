using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    Transform ui;
    TextMeshProUGUI timeText;
    public int day = 1;
    float time;
    public int timeOfDay;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<GameManager>().ui;
        timeText = ui.transform
            .Find("TimeInfo")
            .GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeOfDay = (int)Mathf.Floor(time);
        if (timeOfDay >= 1200)
        {
            time = 0;
            day++;
        }

        timeText.text = string.Format(
            "{0:D2}:{1:D2} Day {2}",
            timeOfDay / 60,
            timeOfDay % 60,
            day
        );
    }
}
