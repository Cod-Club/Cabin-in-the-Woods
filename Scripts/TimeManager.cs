using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    Transform ui;
    TextMeshProUGUI timeText;
    TextMeshProUGUI dayText;
    public int day = 1;
    float time;
    public int timeOfDay;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<GameManager>().ui;
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
        time += Time.deltaTime;
        timeOfDay = (int)Mathf.Floor(time);
        if (timeOfDay >= 1200)
        {
            time = 0;
            dayText.text = (++day).ToString();
        }

        timeText.text = string.Format(
            "{0}:{1}",
            timeOfDay / 60,
            timeOfDay % 60
        );
    }
}
