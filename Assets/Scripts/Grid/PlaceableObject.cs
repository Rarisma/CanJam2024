using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]

public class PlaceableObject : MonoBehaviour 
{

    private Vector3 screenPoint;
    private Vector3 offset;

    public bool isMovable = true;
    [SerializeField] GridManager gridManager;

    public float rotationSpeed = 90f;
    public float currentRotation = 0f;

    [SerializeField] public int timesMoved = 0;

    [SerializeField] private AudioClip moveSound, rotateSound;

    public void Start() {
        moveSound = Resources.Load<AudioClip>("Sounds/SoundEffects/move");
        rotateSound = Resources.Load<AudioClip>("Sounds/SoundEffects/rotat");

        print("Finding GridManager");
        gridManager = (GridManager)FindObjectOfType(typeof(GridManager));

        //Make all objects moveable in level editor
        if (SceneManager.GetActiveScene().name == "LevelEditor")
        {
            isMovable = true;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));

    }

    public void Update() {
    }

    void OnMouseDown() {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        gridManager = (GridManager)FindObjectOfType(typeof(GridManager));

        transform.DOScale(1.2f, 0.2f);

    }

    void OnMouseUp() {
        if (TryGetComponent<FanScript>(out FanScript FA))
        {
            transform.DOScale(0.75f, 0.2f);
        }
        else
        {
            transform.DOScale(1f, 0.2f);
        }
        timesMoved++;
        if (timesMoved % 3 == 0) {
            //play jam sound
            JAM.PlayVoiceLine(JAMSounds.VoiceLineType.MovingObjects);
        }
    }



    void OnMouseDrag() {

        if (!isMovable) {
            return;
        }
        if (Input.GetMouseButton(1))
        {
            Debug.Log($"DELETING {gameObject.name}");
            Destroy(gameObject);

            GameObject gman = GameObject.Find("gman");
            ObjectInventory Inventory = gman.GetComponent<ObjectInventory>();
            Inventory.FindAndIncrement(gameObject.name);
            Debug.Log($"Addded 1 of {gameObject.name} to player inventory");
            return;
        }


        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition = new Vector3(
            Mathf.Round(curPosition.x),
            Mathf.Round(curPosition.y),
            0f
        );

        Vector3 clampedPos = clampPosition(curPosition);
        if (IsCellEmpty(clampedPos)) {
            transform.DOMove(clampedPos, 0.2f);
        }



        if (Input.mouseScrollDelta.y != 0) {
            FindObjectOfType<ObjectSummon>().GetComponent<AudioSource>().PlayOneShot(rotateSound);
            currentRotation = Globals.mod(currentRotation + (rotationSpeed * Mathf.Sign(Input.mouseScrollDelta.y)), 360);
            transform.DORotate(new Vector3(0, 0, currentRotation), 0.2f);
        }
    }
    Vector3 clampPosition(Vector3 position) {
        position.x = Mathf.Clamp(position.x, 0, gridManager.width - 1);
        position.y = Mathf.Clamp(position.y, 0, gridManager.height - 1);

        return position;
    }

    private bool IsCellEmpty(Vector3 position) {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, GetComponent<BoxCollider2D>().size, 0f, 1 << 6);
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject != gameObject) {
                return false;
            }
        }
        return true;
    }
}