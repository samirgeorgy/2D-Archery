using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bow : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private GameObject _predictionPointPrefab;     //The prediction point prefab
    [SerializeField] private int _predictionBallCount = 10;         //The number of points to be displayed for prediction
    [SerializeField] private Transform _predictionPointsParent;     //A parent that holds the points as children

    [SerializeField] private GameObject _arrowPrefab;               //The arrow prefab
    [SerializeField] private float _velocityChargeSpeed = 5;        //The rate in which the speed charge is increased
    [SerializeField] private float _minVelocity = 12f;              //The minimum velocity of the arrow
    [SerializeField] private float _maxVelocity = 20f;              //The maximum velocity of the arrow

    [SerializeField] private float _fireRate = 0.5f;                //Arrow Fire rate

    [SerializeField] private AudioClip _bowSFX;                     //The Bow sound effect
    [SerializeField] private AudioClip _arrowSFX;                   //The Arrow shot sound effect
    [SerializeField] private AudioSource _audioSource;              //The audio source of the bow

    private GameObject[] _predictionPointArray;                     //The list of the prediction points to be displayed
    private float _initialVelocity;                                 //The initial velocity of the arrow
    private Vector3 _aimDirection;                                  //The aim direction
    private float _canFire = -1f;                                   //Calculates the time in which the player can fire the next arrow

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        _initialVelocity = _minVelocity;
        _predictionPointArray = new GameObject[_predictionBallCount];

        //Instatiating the prediction points
        for (int i = 0; i < _predictionBallCount; i++)
        {
            _predictionPointArray[i] = Instantiate(_predictionPointPrefab, transform.position, Quaternion.identity, _predictionPointsParent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateBowRotation();

        //We only process the bow logic if the game is not paused
        if (!GameManager.Instance.GamePaused)
        {
            //When the player holds down the left mouse button the initial speed charges
            if (Input.GetMouseButton(0) && (Time.time > _canFire))
            {
                if (_initialVelocity < _maxVelocity)
                {
                    _initialVelocity += Time.deltaTime * _velocityChargeSpeed;

                    if (!_audioSource.isPlaying)
                        _audioSource.PlayOneShot(_bowSFX);
                }
            }

            //When the player leaves the left mouse button the arrow is show with the speed assigned
            if (Input.GetMouseButtonUp(0) && (Time.time > _canFire))
            {
                _audioSource.PlayOneShot(_arrowSFX);

                ShootArrow();

                //Resetting the initial speed to min speed
                _initialVelocity = _minVelocity;
            }
        }

            //Calculate the positions of the prediction points
            CalculateArrowPrediction();
    }

    #endregion

    #region Supporting Functions

    /// <summary>
    /// Calculates the rotation of the bow based on the mouse position
    /// </summary>
    private void CalculateBowRotation()
    {
        //Get the mousae position in world space
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Calculate the direction between the mouse and the bow
        _aimDirection = mousePos - transform.position;
        _aimDirection.Normalize();

        //Calculate the rotation of the bow with respect to the mouse position
        var angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;

        //Update the angle of the bow
        transform.localEulerAngles = transform.forward * angle;
    }

    /// <summary>
    /// Shoots the arrow
    /// </summary>
    private void ShootArrow()
    {
        _canFire = Time.time + _fireRate;

        //Create an arrow
        var projectile = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);

        //Set initial velocity of the object to the right direction
        projectile.GetComponent<Rigidbody2D>().velocity = _aimDirection * _initialVelocity;
    }

    /// <summary>
    /// Calculates the positions of the prediction points
    /// </summary>
    private void CalculateArrowPrediction()
    {
        //Calculate Prediction
        //Projectile formula = 1/2 * g * t^2 + v0 * t + x0
        //Reference: https://www.youtube.com/watch?v=rqhAOc9gvC4
        for (int i = 0; i < _predictionPointArray.Length; i++)
        {
            var g = Physics2D.gravity;
            var t = (float)i / _predictionPointArray.Length;    //I want to distribute the points in equal distances depending on the number of points in the array acorss time
            var v0 = _aimDirection.normalized * _initialVelocity;
            var x0 = transform.position;

            var pointPosition = 0.5f * g * Mathf.Pow(t, 2) + (Vector2)v0 * t + (Vector2)x0;

            _predictionPointArray[i].transform.position = pointPosition;
        }
    }

    #endregion
}
