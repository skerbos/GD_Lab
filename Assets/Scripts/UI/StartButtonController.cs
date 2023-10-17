using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartButtonController : MonoBehaviour, IInteractiveButton
{
    public GameObject buttonIndicator;
    public GameObject menuPipe;
    public GameObject titleLogo;

    private TextMeshProUGUI buttonText;
    private Image titleLogoImage;
    // Start is called before the first frame update
    void Start()
    {
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        titleLogoImage = titleLogo.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointerEnter()
    {
        buttonIndicator.SetActive(true);
    }

    public void PointerExit()
    {
        buttonIndicator.SetActive(false);
    }

    public void ButtonClick()
    {
        menuPipe.SetActive(true);

        StartCoroutine(TextImageFade());

        StartCoroutine(TransitionToFirstLevel());
        
    }

    IEnumerator TextImageFade()
    {
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 1);
        titleLogoImage.color = new Color(titleLogoImage.color.r, titleLogoImage.color.g, titleLogoImage.color.b, 1);
        while (buttonText.color.a > 0f && titleLogoImage.color.a > 0f)
        {
            buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, buttonText.color.a - (Time.deltaTime / 2f));
            titleLogoImage.color = new Color(titleLogoImage.color.r, titleLogoImage.color.g, titleLogoImage.color.b, titleLogoImage.color.a - (Time.deltaTime / 2f));
            yield return null;
        }
        titleLogo.SetActive(false);
        transform.GetComponent<Button>().enabled = false;

    }

    IEnumerator TransitionToFirstLevel()
    {
        yield return new WaitForSecondsRealtime(5f);

        SceneManager.LoadSceneAsync("Shmup_1_Week 5", LoadSceneMode.Single);


    }
}
