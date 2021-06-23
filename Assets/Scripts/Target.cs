using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Transform _pointA;                     //Point A
    [SerializeField] private Transform _pointB;                     //Point B
    [SerializeField] private float _movementSpeed = 10f;            //The Movement speed of the target

    [SerializeField] private ParticleSystem _fireWorks_FX;          //An effect that plays when the target is hit by an arrow

    [SerializeField] private AudioSource _audioSource;              //The audio Source

    private Vector3 _positionToMoveTo;                              //The next position to move towards

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        Arrow.onTargetHit += TargetHit;
        _positionToMoveTo = _pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnDisable()
    {
        Arrow.onTargetHit -= TargetHit;
    }

    #endregion

    #region Supporting Function

    /// <summary>
    /// Makes the target move back and forth between point A and B
    /// </summary>
    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, _positionToMoveTo, _movementSpeed * Time.deltaTime);

        if (transform.position == _pointA.position)
            _positionToMoveTo = _pointB.position;
        else if (transform.position == _pointB.position)
            _positionToMoveTo = _pointA.position;
    }

    /// <summary>
    /// This function is called when the target is hit by an arrow. It plays a firework effect and increase the movement speed for more challenge - called by an event to the DetectArrowHit
    /// </summary>
    private void TargetHit()
    {
        if (_movementSpeed < 10)
            _movementSpeed += 1;

        _audioSource.Play();

        _fireWorks_FX.Play();
    }

    #endregion
}
