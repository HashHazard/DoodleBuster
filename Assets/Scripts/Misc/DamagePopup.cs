using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;

    [SerializeField] private float DestroyTime = 1f;
    private Vector3 Offset = new Vector3(0, 1.8f, 0);
    private Vector3 RandomizeIntensity = new Vector3(0.8f, 0, 0);

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        
    }


    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), 0f);
        Invoke("ResetScale", DestroyTime);
    }

    private void ResetScale()
    {
        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
