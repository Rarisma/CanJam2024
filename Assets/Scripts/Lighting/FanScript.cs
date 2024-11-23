
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : LightPowered
{
    [SerializeField] private float fanRange = 1.0f;
    private Animator animator;

    public override void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isPowered)
        {
            PoweredRotate();
            animator.SetBool("Powered", true);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, fanRange);
            foreach (var collider in colliders)
            {
                if(collider.gameObject.TryGetComponent(out SmokeObject smokeObject)){
                    smokeObject.Extinguish();
                }
            }
        }
        else{
            animator.SetBool("Powered", false);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, fanRange);
            foreach (var collider in colliders)
            {
                if(collider.gameObject.TryGetComponent(out SmokeObject smokeObject)){
                    smokeObject.Ignite();
                }
            }
        }

    }

    private void PoweredRotate()
    {
        transform.Rotate(0, 0, .25f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fanRange);
    }
}
