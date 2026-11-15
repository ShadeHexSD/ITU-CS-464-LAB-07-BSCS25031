using UnityEngine;

// Owns pausing. Put it on an empty object called Controllers.
public class PauseController : MonoBehaviour
{
    [SerializeField] UIScreen matchScreen;
    [SerializeField] UIScreen pauseScreen;
    [SerializeField] UIScreen homeScreen;
    [SerializeField] GameplayStub gameplay;

    void OnEnable() { ScreenManager.ScreenChanged += OnScreenChanged; }

    void OnDisable()
    {
        ScreenManager.ScreenChanged -= OnScreenChanged;
        Time.timeScale = 1f;   // never leave the editor frozen
    }

    void Update()
    {
        // Esc / Android back during a match opens Pause instead of leaving the match.
        var manager = ScreenManager.Instance;
        if (Input.GetKeyDown(KeyCode.Escape) && manager != null && manager.Current == matchScreen) Pause();
    }

    // Safety net: whenever a screen finishes opening, time runs only if it is not Pause.
    // This also covers Esc and the Quit button, so the game can never stay frozen by accident.
    void OnScreenChanged(UIScreen screen) { Time.timeScale = screen == pauseScreen ? 0f : 1f; }

    public void Pause()
    {
        Time.timeScale = 0f;   // stop the game right away, not after the animation
        ScreenManager.Instance.Show(pauseScreen, Transition.Modal);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        ScreenManager.Instance.Back();
    }

    public void Restart()
    {
        gameplay.ResetTimer();
        Resume();
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        ScreenManager.Instance.GoHome(homeScreen, Transition.Fade);
    }
}