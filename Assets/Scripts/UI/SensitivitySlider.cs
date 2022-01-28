using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivitySlider : MonoBehaviour
{
    FirstPersonPerspectiveController playerController;
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<FirstPersonPerspectiveController>();
    }

    public void ChangeSensitivity(float speed)
    {
        playerController.SetRotateSpeed(speed);
    }
}
