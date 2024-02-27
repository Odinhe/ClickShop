using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMove : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public GameObject FirePotion;
    private Vector3 initialPosition;
    // Update is called once per frame
    void Update()
    {
        // When right mouse was clicked, start dragging the item
        if (Input.GetMouseButtonDown(1))
        {
            // Check if the mouse is on the potion or not
            initialPosition = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                //Get the offest
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        // When right mouse was released, stop dragging the item
        else if (Input.GetMouseButtonUp(1))
        {
            //create a copy of the potion at the origional place when player draged the potion away
            Instantiate(FirePotion, initialPosition, Quaternion.identity);
            isDragging = false;
        }

        // When the object has been draged by player, change its position
        if (isDragging)
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(cursorPosition.x, cursorPosition.y, transform.position.z);
        }
    }
}
