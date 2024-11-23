using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using DG.Tweening;

public class ButtonScript : MonoBehaviour {

    void Start() {
        int numOfButtons = transform.parent.childCount;
        int buttonIndex = transform.GetSiblingIndex();

        CenterButton(numOfButtons, buttonIndex);
    }

    private void CenterButton(int numOfButtons, int buttonIndex) {
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