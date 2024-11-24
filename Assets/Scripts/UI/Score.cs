using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


// Here is the formula for scores

// MAX SCORE =====================
// GOALS : each goal adds 2000 points to the max
// OBJECTS : objects in the players inventory add 250 points each

// PLAYER SCORE ==================
// The players score at the end will be equal to the max score MINUS the following
// OBJECTS PLACED : Each placed object present will subtract 250 points
// OBJECT MOVES : Each time an object has been moved MORE THAN ONCE, subtract 50 points
// TIME : each second you took to beat the level subtracts 10 points (5 mins = 3000 points)
// SCORE cannot go below 0

public class Score : MonoBehaviour
{
    float startTime;
    TextMeshProUGUI scoreText;
    float maxScore = 0;
    float existingMoveableObjects;
    float plantCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        plantCount = (FindObjectsOfType<LightReceiver>().Length);
        if (FindObjectOfType<ObjectInventory>() != null)
        {
            maxScore =
                (plantCount * 2000)
                +
                (FindObjectOfType<ObjectInventory>().objectAmount.Values.Sum() * 250);
            
        }
        else
        {
            maxScore =
                (FindObjectsOfType<LightReceiver>().Length * 2000);
        }

        existingMoveableObjects =
                (FindObjectsOfType<PlaceableObject>().Where((o) => o.isMovable).Count() * 250); // This accounts for moveable objects already on there

        startTime = Time.time;
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void CalculateScore()
    {
        float endTime = Time.time;
        float timeTaken = endTime - startTime;
        float SubtractedScore;

        SubtractedScore =
            (FindObjectsOfType<PlaceableObject>().Sum((o) => Mathf.Max(o.timesMoved - 1, 0)) * 100)
            +
            timeTaken * 20;
        if (FindObjectOfType<ObjectInventory>() != null)
        {
            SubtractedScore -= (FindObjectOfType<ObjectInventory>().objectAmount.Values.Sum() * 100);
        }

        float finalScore = (maxScore - SubtractedScore) / (plantCount * 2);
        if (finalScore < 0) finalScore = 0;

        scoreText.text = finalScore.ToString();
        //scoreText.text = ("Score: " + (maxScore / (plantCount * 2)).ToString() + " " + finalScore.ToString() + " " + timeTaken * 10 + " " + 
                //(FindObjectsOfType<PlaceableObject>().Sum((o) => Mathf.Max(o.timesMoved - 2, 0)) * 75));

    }

    void AnimateScore(float fromScore, float toScore, float duration)
    {
        // Tween the score value over time and update the text
        DOTween.To(() => fromScore, x => {
            fromScore = x; // Update the value
            scoreText.text = "Score: " +  Mathf.FloorToInt(fromScore).ToString(); // Update the text
        }, toScore, duration);
    }
}
