using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Private Variables

    static private UIManager _instance;                 //An instance of the class - This is a Singlton

    [SerializeField] private Text _scoreText;           //The score text
    [SerializeField] private GameObject _pausePanel;    //Tha pause menu

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets an instance of the UIManaged
    /// </summary>
    static public UIManager Instance
    {
        get { return _instance; }
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Supporting Functions

    /// <summary>
    /// Updates the score in the UI
    /// </summary>
    /// <param name="value">The value to be updated</param>
    public void UpdateScore (int value)
    {
        _scoreText.text = "Score: " + value;
    }

    /// <summary>
    /// Opens or closes the pause menu
    /// </summary>
    /// <param name="status">True to open the menu and false to close it</param>
    public void TogglePauseMenu(bool status)
    {
        _pausePanel.SetActive(status);
    }    

    #endregion
}
