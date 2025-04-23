using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedText : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private TMPro.TextMeshProUGUI text;


    public void StartScoreAnimation(int score)
    {
        this.gameObject.SetActive(true);
        text.text = score.ToString();
        anim.Play();
    }

    public void OnAnimationEnd()
    {
        anim.Rewind();
        this.gameObject.SetActive(false);
    }
}
