using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using OpenCV.Net;
using System.Drawing;

[Combinator]
[WorkflowElementCategory(ElementCategory.Source)]
public class CreateTarget
{
    readonly Collection<PointF> targetPositions = new Collection<PointF>();

    public Collection<PointF> TargetPositions
    {
        get { return targetPositions; }
    }

    public IObservable<CalibrationTarget> Process()
    {
        var next = 0;
        var random = new Random();
        var count = targetPositions.Count;
        CalibrationTarget[] targets = null;
        targets = targetPositions.Select(position => new CalibrationTarget(position, () =>
        {
            var temp = targets[count - 1];
            targets[count - 1] = targets[next];
            targets[next] = temp;
            count--;
        })).ToArray();
        return Observable.Defer(() =>
        {
            if (count == 0) return Observable.Empty<CalibrationTarget>();
            else
            {
                next = random.Next(count);
                return Observable.Return(targets[next]);
            }
        });
    }
}
