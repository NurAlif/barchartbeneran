using System.Collections;
using UnityEngine;


public class ChartDataset2{

    public static int STATE_IDLE = 0;
    public static int STATE_CHANGED = 1;
    public static int STATE_UPDATING = 2;

    public ChartDataset2(string y, int x)
    {
        this.y = y;
        this.x = x;
        this.state = STATE_IDLE;
    }

    public string y
    {
        get;
        set;
    }

    public int x
    {
        get;
        set;
    }

    public int state
    {
        get;
        set;
    }
}