using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider2D))]

public class PlaceableObject : MonoBehaviour 
{

    private Vector3 screenPoint;
    private Vector3 offset;

    public float rotationSpeed = 90f;

    void OnMouseDown() {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
    }

    void OnMouseDrag() {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition = new Vector3(
            Mathf.Round(curPosition.x),
            Mathf.Round(curPosition.y),
            0f
        );

        Vector3 clampedPos = clampPosition(curPosition);
        if (IsCellEmpty(clampedPos)) {
            transform.position = clampedPos;
        }

        if (Input.mouseScrollDelta.y != 0) {
            transform.Rotate(0, 0, rotationSpeed * Mathf.Sign(Input.mouseScrollDelta.y));
        }
    }


    Vector3 clampPosition(Vector3 position){
        position.x = Mathf.Clamp(position.x, 0, 15);
        position.y = Mathf.Clamp(position.y, 0, 8);
        
        return position;
    }

    private bool IsCellEmpty(Vector3 position) {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, GetComponent<BoxCollider2D>().size, 0f);
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject != gameObject) {
                return false;
            }
        }
        return true;
    }

    
}