using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class SpinObject : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private TextMeshProUGUI count;
    private Vector2 startDragDirection;
    private float totalRotation;
    private int direction = 0;
    [SerializeField] private int spinCount = 0;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragDirection = eventData.position - (Vector2)transform.position;
        totalRotation = 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 circledragdirection = eventData.position - (Vector2)transform.position;
        float angleDifference = Vector2.SignedAngle(startDragDirection, circledragdirection);

        int circledirection = (angleDifference > 0) ? 1 : -1;

        if (direction == 0)
        {
            direction = circledirection;
        }
        else if (direction != circledirection)
        {
            startDragDirection = circledragdirection;
            totalRotation = 0f;
            direction = circledirection;
            return;
        }

        transform.Rotate(Vector3.forward * angleDifference);

        totalRotation += Mathf.Abs(angleDifference);

        if (totalRotation >= 360f)
        {
            spinCount++;
            totalRotation -= 360f;
            count.text = spinCount.ToString();
        }

        startDragDirection = circledragdirection;
    }
}
