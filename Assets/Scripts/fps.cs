using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fps : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fps = (int)fps;
        fpsDisplay.SetText(fps.ToString());
    }
}
