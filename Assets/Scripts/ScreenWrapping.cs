using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    private Renderer RendererComponent;

    private void Start()
    {
        RendererComponent = GetComponent<Renderer>();
    }

    private void Update()
    {
        if(RendererComponent.isVisible)
        {
            WrapScreen();
        }
    }

    private void WrapScreen()
    {

        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (viewportPosition.x > 1 || viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x;
        }

        if (viewportPosition.y > 1 || viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y;
        }

        transform.position = newPosition;
    }

}
