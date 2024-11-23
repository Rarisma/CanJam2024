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

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lightCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    public void Extinguish(){
        spriteRenderer.color = extinguishedColor;
        lightCollider.enabled = false;
        isIgnited = false;
    }

    public void Ignite(){
        spriteRenderer.color = ignitedColor;
        lightCollider.enabled = true;
        isIgnited = true;
    }
}
