using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    Transform ui;
    TextMeshProUGUI timeText;
    TextMeshProUGUI dayText;
    public int day = 1;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("GameManager").GetComponent<GameManager>().ui;
        timeText = ui.transform
            .Find("TimeInfo/Time")
            .GetComponent<TextMeshProUGUI>();
        dayText = ui.transform
            .Find("TimeInfo/Day")
            .GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int timeOfDay = (int)Mathf.Floor(Time.time);
        if (timeOfDay >= 1200)
        {
            timeOfDay = 0;
            dayText.text = (++day).ToString();
        }

        timeText.text = string.Format(
            "{0}:{1}",
            timeOfDay / 60,
            timeOfDay % 60
        );
    }
}
