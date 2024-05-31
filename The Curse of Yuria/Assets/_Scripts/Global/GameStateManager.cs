using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager: MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    enum State { playing, paused, stopped }
    State state = State.stopped;

    public bool isPlaying => state == State.playing;
    public bool isPaused => state == State.paused;
    public bool isStopped => state == State.stopped;

    void Awake()
    {
        Instance = this;
    }

    public void Play()
    {
        state = State.playing;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Pause()
    {
        state = State.paused;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Stop()
    {
        state = State.stopped;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
