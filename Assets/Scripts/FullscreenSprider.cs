using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenSprider : MonoBehaviour {

       
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;

        float worlHeight = Camera.main.orthographicSize * 2f;
        float worlWidth = worlHeight * Screen.width / Screen.height;

        tempScale.y = worlHeight / height;
        tempScale.x = worlWidth / width;

        transform.localScale = tempScale;
    }
}
