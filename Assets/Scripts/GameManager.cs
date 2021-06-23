using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Private Variables

    static private GameManager _instance;             //An instance of the class - This is a Singlton

    private int _gameScore = 0;
    private bool _gamePaused = false;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets an instance of the UIManaged
    /// </summary>
    static public GameManager Instance
    {
        get { return _instance; }
    }

    /// <summary>
    /// Checks whether the game is paused or not
    /// </summary>
    public bool GamePaused
    {
        get { return _gamePaused; }
    }

    #endregion

    #region Unity Functions

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        UIManager.Instance.UpdateScore(_gameScore);
    }

    // Update is called once per frame
    void Update()
    {
        //Pause and resume the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_gamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    #endregion

    #region Supporting Functions

    /// <summary>
    /// Adds to the game score
    /// </summary>
    /// <param name="value">The value to be added.</param>
    public void AddScore(int value)
    {
        _gameScore += value;
        UIManager.Instance.UpdateScore(_gameScore);
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void PauseGame()
    {
        Cursor.visible = true;

        _gamePaused = true;
        Time.timeScale = 0;
        UIManager.Instance.TogglePauseMenu(true);
    }

    /// <summary>
    /// Resumes the game
    /// </summary>
    public void ResumeGame()
    {
        Cursor.visible = false;

        _gamePaused = false;
        Time.timeScale = 1;
        UIManager.Instance.TogglePauseMenu(false);
    }

    #endregion
}
