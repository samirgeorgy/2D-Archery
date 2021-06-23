using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    #region Events

    public delegate void TargetHit();
    public static event TargetHit onTargetHit;      //An event that objects can subscribe to to know whether a target has been hit or not

    #endregion

    #region Private Variables

    private CapsuleCollider2D _collider;
    private Rigidbody2D _rb;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();

        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Target"))
        {
            if (onTargetHit != null)
                onTargetHit();

            GameManager.Instance.AddScore(10);
            _collider.enabled = false;
            _rb.velocity = Vector3.zero;
            _rb.gravityScale = 0;

            Destroy(this.gameObject, 0.5f);
        }
    }

    #endregion
}
