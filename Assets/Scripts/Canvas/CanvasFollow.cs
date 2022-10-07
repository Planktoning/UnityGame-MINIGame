using UnityEngine;
using DG.Tweening;

public class CanvasFollow : MonoBehaviour
{
    public GameObject camera;

    void FixedUpdate()
    {
        this.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y,
            this.transform.position.z);
    }
}