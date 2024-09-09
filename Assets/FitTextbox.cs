using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FitTextbox : MonoBehaviour
{
    [SerializeField] int charCountThreshold;
    [SerializeField] int smallTextSize;
    [SerializeField] int largeTextSize;
    TextMeshProUGUI textMeshProText;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textMeshProText == null) return;

        if (textMeshProText.text.Length >= charCountThreshold && textMeshProText.fontSize != smallTextSize) textMeshProText.fontSize = smallTextSize;
        else if(textMeshProText.text.Length < charCountThreshold && textMeshProText.fontSize != largeTextSize) textMeshProText.fontSize = largeTextSize;
    }
}
