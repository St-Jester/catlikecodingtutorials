using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMeasurment : MonoBehaviour {

    int[] fpsBuffer;
    int fpsBufferIndex;
    
    public int frameRange = 60;

    public int HighestFPS { get; private set; }
    public int LowestFPS { get; private set; }
    public int AverageFPS { get; private set; }

    void Update() {
        if (fpsBuffer == null || fpsBuffer.Length!=frameRange)
        {
            InitializeBuffer();
        }
        UpdateBuffer();
        CalculateFPS();
        
	}

    void InitializeBuffer()
    {
        if (frameRange <= 0)
        {
            frameRange = 1;
        }
        fpsBuffer = new int[frameRange];
        fpsBufferIndex = 0;
    }

    void UpdateBuffer()
    {
        fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if (fpsBufferIndex >= frameRange)
            fpsBufferIndex = 0;
    }

    void CalculateFPS()
    {
        int sum = 0, highest = 0, lowest = int.MaxValue;
        for (int i = 0; i < frameRange; i++)
        {
            sum += fpsBuffer[i];
            if (fpsBuffer[i] > highest)
                highest = fpsBuffer[i];
            if (fpsBuffer[i] < lowest)
                lowest = fpsBuffer[i];

        }
        AverageFPS = sum / frameRange;
        HighestFPS = highest;
        LowestFPS = lowest;
    }

}
