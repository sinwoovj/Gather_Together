using System.Collections;
using UnityEngine;

public class Slide : MonoBehaviour
{
    private IEnumerator currentCoroutine;

    public Vector2 startPosition;
    public Vector2 endPosition;

    public float lerpTime = 0.5f;
    private float currentTime = 0;
    private bool isOpen = true;

    void Start()
    {
        transform.position = startPosition;
    }

    public void SlidePanelFunc()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentTime = 0;
        currentCoroutine = SlidePanelCoroutine(!isOpen);
        isOpen = !isOpen;
        StartCoroutine(currentCoroutine);
    }

    private IEnumerator SlidePanelCoroutine(bool isopen)
    {
        Vector2 ed = endPosition;
        Vector2 st = startPosition;

        while (currentTime <= lerpTime)
        {
            currentTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, currentTime / lerpTime);
            this.transform.position = Vector2.Lerp(isopen ? ed : st, isopen ? st : ed, t);
            //SlidePanelArrow.transform.rotation = Quaternion.Euler(new Vector2(0, 0, isopen ? 180 - 180 * t : 180 * t));
            yield return null;
        }
        this.transform.position = isopen ? st : ed;
        currentCoroutine = null;
    }
}