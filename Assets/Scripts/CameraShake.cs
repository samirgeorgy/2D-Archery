using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Animator _anim;        //The Camera Animator

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        Arrow.onTargetHit += ShakeCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Arrow.onTargetHit -= ShakeCamera;
    }

    #endregion

    #region Supporting Functions

    /// <summary>
    /// Shakes the camera when an arror hits the target - called by an event to the DetectArrowHit
    /// </summary>
    private void ShakeCamera()
    {
        _anim.SetTrigger("Shake");
    }

    #endregion
}
