using TMPro;
using UnityEngine;

public class ScrambleManager : MonoBehaviour
{
    public TMP_Text initialText;
    public TMP_Text finalText;
    public float time = 1f;

    public void ButtonScramble()
    {
        this.Scramble(initialText.text, finalText.text, time, (result) => { initialText.text = result; });
    }
}
