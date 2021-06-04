using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Inputs;
using UnityEngine;
using UnityEngine.UI;

public class CameraRecorder : MonoBehaviour
{
    
    private Text txt;

    async void Start()
    {
        txt = gameObject.GetComponentInChildren<Text>();
        ShowToast("Start");
        var recorder = new MP4Recorder(1280, 720, 30);
        var clock = new RealtimeClock();
        var cameraInput = new CameraInput(recorder, clock, Camera.main);
        await Task.Delay(TimeSpan.FromSeconds(5));
        cameraInput.Dispose();
        var path = await recorder.FinishWriting();
        ShowToast(path);
        

}
   

    void ShowToast(string text
       )
    {
        txt.color = Color.red;
        txt.text = text;
    }

    private IEnumerator showToastCOR(string text,
        int duration)
    {
        Color orginalColor = Color.red;

        txt.text = text;
        txt.enabled = true;

        //Fade in
        yield return fadeInAndOut(txt, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(txt, false, 0.5f);

        txt.enabled = false;
        txt.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = Color.clear;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }


}
