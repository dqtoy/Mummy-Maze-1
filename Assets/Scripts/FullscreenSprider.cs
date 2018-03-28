using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenSprider : MonoBehaviour {

        public Camera camera;
    void Start()
    {
        if (camera == null)
            camera = Camera.main;
        // ensure calculations are done when the camera is not rotated. Otherwise the z-axis will incorrectly have some depth
        Vector3 camRotation = camera.transform.rotation.eulerAngles;
        camera.transform.rotation = Quaternion.Euler(Vector3.zero);
        // find corners of the cameras view frustrum at the distance of the gameobject
        float distance = Vector3.Distance(this.transform.position, camera.transform.position);
        Vector3 viewBottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 viewTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        // scale the gameobject so it touches the cameras view frustrum
        Vector3 scale = viewTopRight - viewBottomLeft;
        scale.z = transform.localScale.z;
        transform.localScale = scale;
        //return the camera to it's original rotation
        camera.transform.rotation = Quaternion.Euler(camRotation);
    }
}
