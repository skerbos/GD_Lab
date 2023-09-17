using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumberBehavior : MonoBehaviour
{

    public float fadeOutTime = 0.3f;
    public float damageValue = 1f;
    private float startTime;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        originalColor = gameObject.GetComponent<TextMeshProUGUI>().color;
        gameObject.GetComponent<TextMeshProUGUI>().text = damageValue.ToString();
        transform.SetParent(GameObject.Find("Canvas").transform);
        Destroy(gameObject, fadeOutTime);
    }

    // Update is called once per frame
    void Update()
    {
        textFade();
    }

    public void textFade()
    {
        float currentTime = Time.time;
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.Lerp(originalColor, Color.clear, easingFade());
    }

    public float easingFade()
    {
        float currentTime = Time.time;
        return Mathf.Pow(((currentTime - startTime) / fadeOutTime), 4);
    }
}
