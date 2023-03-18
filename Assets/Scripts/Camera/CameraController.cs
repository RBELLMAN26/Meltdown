using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform focusObject;
    public bool focusOnPlayer;

    // Update is called once per frame
    void Update()
    {
        if(!focusOnPlayer)
        {
            RotateAroundLevel();
        }
    }

    void RotateAroundLevel()
    {
        transform.RotateAround(focusObject.position, Vector3.up, 20 * Time.deltaTime);
    }

    public void SnapToPlayer(Transform cameraPOS)
    {
        focusOnPlayer = true;
        transform.position = cameraPOS.position;
        transform.rotation = cameraPOS.rotation;
    }
}
