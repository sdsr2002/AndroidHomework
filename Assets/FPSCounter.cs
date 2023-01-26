using System.Threading;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private float timer;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (Application.isMobilePlatform)
            Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    private void Update()
    {
        if (timer < 1f)
        {
            timer += Time.deltaTime;
            return;
        }
        text.SetText((1f / Time.unscaledDeltaTime).ToString("0.0"));
        timer = 0f;
    }
}
