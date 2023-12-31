using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStart : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button NextStageButton;
    [SerializeField] private Button EndButton;

    [SerializeField] private Text Score;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(HandleStart);
        NextStageButton.onClick.AddListener(HandleNext);
        EndButton.onClick.AddListener(HandleEnd);

        GameManager.Instance.OnChangeStatEvent.AddListener(HandleOnChangeState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleStart()
    {
        GameManager.Instance.OnChangeStatEvent.Invoke(EGameState.Play);
        StartButton.gameObject.SetActive(false);
    }

    void HandleNext()
    {
        GameManager.Instance.OnChangeStatEvent.Invoke(EGameState.Play);
        NextStageButton.gameObject.SetActive(false);
    }

    void HandleEnd()
    {
        GameManager.Instance.OnChangeStatEvent.Invoke(EGameState.Ready);
        EndButton.gameObject.SetActive(false);
        Score.gameObject.SetActive(false);
    }

    void HandleOnChangeState(EGameState state)
    {
        if (state == EGameState.NextStage)
        {
            NextStageButton.gameObject.SetActive(true);
        }
        else if (state == EGameState.End)
        {
            EndButton.gameObject.SetActive(true);
            Score.gameObject.SetActive(true);

            Score.text = "Score : " + GameManager.Instance.Score;
        }
        else if (state == EGameState.Ready)
        {
            StartButton.gameObject.SetActive(true);
        }
    }
}
