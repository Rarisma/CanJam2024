using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    void Start() {

        StartCoroutine(CenterButton());
    }

    private IEnumerator CenterButton() {
        yield return new WaitForFixedUpdate();
        int numOfButtons = transform.parent.childCount;
        int buttonIndex = transform.GetSiblingIndex();

        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null) {
            rectTransform.anchorMin = new Vector2(0.5f, 0);
            rectTransform.anchorMax = new Vector2(0.5f, 0);
            rectTransform.pivot = new Vector2(0.5f, 0);

            

            float totalWidth = rectTransform.rect.width * numOfButtons;
            float buttonWidth = rectTransform.rect.width;
            float xPos = (buttonWidth * buttonIndex) - (totalWidth / 2 + 5f) + (buttonWidth / 2);
            rectTransform.anchoredPosition = new Vector2(xPos, 10f);
        }
    }

    public void PunchButton(){
        transform.localScale = new Vector3(1, 1, 1);
        transform.DOKill();
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
    }
}