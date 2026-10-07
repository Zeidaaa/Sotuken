using System;
using UnityEngine;

[Serializable]
public struct Timer
{
    [Tooltip("カウンター")]
    public float counter;
    [Tooltip("時間")]
    public float time;

    public Timer(float counter, float time)
    {
        this.counter = counter;
        this.time = time;
    }

    // タイマーのカウントを進める、time秒経過したらtrueを返す、AutoCounterResetがtrueならカウンターをリセットする
    public bool TimeCount(float deltaTime, bool AutoCounterReset = false)
	{

        counter += deltaTime;
		if (counter >= time)
		{
            if (AutoCounterReset) counter = 0f;
			return true;
		}
        return false;
	}

    // アクセサ
    public float Counter
    {
        get => counter;
        set => counter = value;
    }
    public float Time
    {
        get => time;
        set => time = value;
    }
}