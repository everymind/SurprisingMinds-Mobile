using System;
using System.Drawing;

public class CalibrationTarget
{
    readonly Action onHit;

    public CalibrationTarget(PointF position, Action onHit)
    {
        Position = position;
    }

    public PointF Position { get; private set; }

    public void Hit()
    {
        onHit();
    }
}
