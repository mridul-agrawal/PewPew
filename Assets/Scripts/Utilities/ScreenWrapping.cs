using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to implement screen wrapping for the object it is attached to.
/// </summary>
namespace PewPew.Utilities
{
    public class ScreenWrapping : MonoBehaviour
    {
        private Renderer RendererComponent;
        private bool isAsteroid;
        private bool hasEnteredScreen;
        private float wrappingOffset = 0.05f;

        private void Start()
        {
            RendererComponent = GetComponent<Renderer>();
            isAsteroid = GetComponent<Asteroid>() != null;
            hasEnteredScreen = false;
        }

        private void Update()
        {
            if (isAsteroid)
            {
                if (!hasEnteredScreen)
                {
                    CheckIfEntered();
                    return;
                }
            }

            if (RendererComponent.isVisible)
            {
                WrapScreen();
            }
        }

        // Method to wrap object on screen if it goes out of bounds.
        private void WrapScreen()
        {

            var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
            var newPosition = transform.position;

            if (viewportPosition.x > 1)
            {
                newPosition.x = -(newPosition.x - wrappingOffset);
            }
            if (viewportPosition.x < 0)
            {
                newPosition.x = -(newPosition.x + wrappingOffset);
            }
            if (viewportPosition.y > 1)
            {
                newPosition.y = -(newPosition.y - wrappingOffset);
            }
            if (viewportPosition.y < 0)
            {
                newPosition.y = -(newPosition.y + wrappingOffset);
            }

            transform.position = newPosition;
        }

        // Method to check if the asteroid has entered the Screen.
        private void CheckIfEntered()
        {
            Vector3 asteroidPoint = Camera.main.WorldToViewportPoint(transform.position);
            bool onScreen = asteroidPoint.x > 0 && asteroidPoint.x < 1 && asteroidPoint.y > 0 && asteroidPoint.y < 1;
            if (onScreen)
            {
                hasEnteredScreen = true;
            }
        }
    }
}