using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnDirection : MonoBehaviour
{
    #region Private Variables

    private Vector3 _initialPosition;       //The initial position of the projectile

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - _initialPosition;
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    #endregion
}
