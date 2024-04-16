using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    public Transform camera;

    private float oldPosition;

    void Start()
    {
        oldPosition = camera.position.x;
    }

    void Update()
    {
        
        if (camera.position.x != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                float delta = oldPosition - camera.position.x;
                onCameraTranslate(delta);
            }

            oldPosition = camera.position.x;
        }
    }
}