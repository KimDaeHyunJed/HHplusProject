using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHud : MonoBehaviour
{
    public Text Score;
    public Text Stage;

    void Start()
    {
        GameManager.Instance.OnChangeScoreEvent.AddListener(HandleOnChangeScoreEvent);
        GameManager.Instance.OnChangeStageEvent.AddListener(HandleOnChangeStageEvent);
    }

    void HandleOnChangeScoreEvent(int score)
    {
        Score.text = score.ToString();
    }

    void HandleOnChangeStageEvent(int stage)
    {
        Stage.text = stage.ToString();
    }
}
