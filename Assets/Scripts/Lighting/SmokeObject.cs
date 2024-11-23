using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeObject : MonoBehaviour
{
    [SerializeField] private Color extinguishedColor;
    [SerializeField] private Color ignitedColor;

    [SerializeField] private bool isIgnited = true;
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D lightCollider;

    Coroutine Reignition;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lightCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    public void Extinguish(){
        spriteRenderer.color = extinguishedColor;
        lightCollider.enabled = false;
        isIgnited = false;

        if (Reignition != null)
        {
            StopCoroutine(Reignition);
        }
        Reignition = StartCoroutine(Ignite());
    }

    public IEnumerator Ignite(){
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = ignitedColor;
        lightCollider.enabled = true;
        isIgnited = true;
    }
}
