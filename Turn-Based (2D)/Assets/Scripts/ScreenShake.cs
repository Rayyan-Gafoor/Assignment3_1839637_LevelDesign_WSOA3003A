using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
   public IEnumerator ShakeScreen(float Length, float Strength)
    {
        Vector2 orignalPostion = transform.localPosition;
        float timePassed = 0f;

        while  (timePassed< Length)
        {
            float x = Random.Range(-1f, 1f) * Strength;
            float y = Random.Range(-1f, 1f) * Strength;

            transform.localPosition = new Vector2(x, y);
            timePassed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = orignalPostion;
        
    }
}
