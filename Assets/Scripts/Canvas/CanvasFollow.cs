using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    public GameObject camera;

    void Update()
    {
        this.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y,
            this.transform.position.z);
    }
}