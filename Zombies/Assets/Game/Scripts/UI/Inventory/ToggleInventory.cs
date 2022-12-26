using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInventory : MonoBehaviour
{
    public bool isVisible = false;
    public CanvasRenderer canvasRenderer;
    void Start()
    {
        canvasRenderer.gameObject.SetActive(isVisible);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     isVisible = !isVisible;
        //     canvasRenderer.gameObject.SetActive(isVisible);
        // }
    }
}
