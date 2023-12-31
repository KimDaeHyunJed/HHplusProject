using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EGameState
{
    Ready,
    Play,
    FinishGame,
    NextStage,
    End,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController player;
    public float Speed = -5.0f;
    public float GameEndTime = 30.0f;
    public float Elapsed = 0;
    public float ScoreElapsed = 0;

    public int Score = 0;
    public int Stage = 1;

    public UnityEvent<EGameState> OnChangeStatEvent = new UnityEvent<EGameState>();
    public UnityEvent<int> OnChangeScoreEvent = new UnityEvent<int>();
    public UnityEvent<int> OnChangeStageEvent = new UnityEvent<int>();

    public EGameState gameState = EGameState.Ready;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnChangeStatEvent.AddListener(HandleOnChangeStateEvent);

        HandleOnChangeStateEvent(gameState);
        SetScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == EGameState.Play)
        {
            Elapsed += Time.deltaTime;
            ScoreElapsed += Time.deltaTime;

            if (Elapsed > GameEndTime)
            {
                OnChangeStatEvent.Invoke(EGameState.FinishGame);
            }

            if (ScoreElapsed > 1.0f)
            {
                AddScore(1);
                ScoreElapsed = 0;
            }
        }
    }

    public void AddScore(int add)
    {
        Score += add;

        SetScore(Score);
    }

    public void SetScore(int set)
    {
        Score = set;
        OnChangeScoreEvent.Invoke(Score);
    }

    public void SetStage(int stage)
    {
        Stage = stage;
        OnChangeStageEvent.Invoke(Stage);
    }

    void HandleOnChangeStateEvent(EGameState state)
    {
        gameState = state;

        switch (state)
        {
            case EGameState.Ready:
            {
                Elapsed = 0;
                ScoreElapsed = 0;
                Speed = -5;

                SetStage(1);
                SetScore(0);

            } break;
            case EGameState.Play:
            {
                // 몬스터 소환
            } break;
            case EGameState.FinishGame:
            {
                // 몬스터 모두제거 피니쉬 생성
            }
                break;
            case EGameState.NextStage:
            {
                Elapsed = 0;
                ScoreElapsed = 0;
                Speed -= 0.5f;

                SetStage(++Stage);
            } break;
            case EGameState.End:
            {
                Speed = 0;

            } break;
        }
    }
}
