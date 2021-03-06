﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratableValue
{
    float maxMagnitude;
    float maxDeltaPerSec;
    float current = 0;

    public AcceleratableValue(float maxMagnitude, float secondsToReachMaxMagnitudeFromZero)
    {
        this.maxMagnitude = maxMagnitude;
        this.maxDeltaPerSec = maxMagnitude / secondsToReachMaxMagnitudeFromZero;
    }

    public float getCurrentValue()
    {
        return current;
    }

    public void fromBools(bool shouldDecrease, bool shouldIncrease, float deltaTime)
    {
        if (shouldDecrease && !shouldIncrease) this.decrease(deltaTime);
        else if (shouldIncrease && !shouldDecrease) this.increase(deltaTime);
        else this.equalize(deltaTime);
    }

    public void increase(float deltaTime)
    {
        current = maxMagnitude;
    }

    public void decrease(float deltaTime)
    {
        current = -maxMagnitude;
    }

    public void equalize(float deltaTime)
    {
        current = 0;
    }

    //public void increase(float deltaTime)
    //{
    //    float maxDelta = deltaTime * maxDeltaPerSec;

    //    current += Mathf.Lerp(0, 2 * maxDelta, 1);
    //    if (current > maxMagnitude)
    //    {
    //        current = maxMagnitude;
    //    }
    //}

    //public void decrease(float deltaTime)
    //{
    //    float maxDelta = deltaTime * maxDeltaPerSec;

    //    current += Mathf.Lerp(0, -2 * maxDelta, 1);
    //    if (current < -maxMagnitude)
    //    {
    //        current = -maxMagnitude;
    //    }
    //}

    //public void equalize(float deltaTime)
    //{
    //    float maxDelta = deltaTime * maxDeltaPerSec;

    //    if (current > 0)
    //    {
    //        current -= maxDelta;
    //        if (current < 0)
    //        {
    //            current = 0;
    //        }
    //    }
    //    else
    //    {
    //        current += maxDelta;
    //        if (current > 0)
    //        {
    //            current = 0;
    //        }
    //    }
    //}

}
