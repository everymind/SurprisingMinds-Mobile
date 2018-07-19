using Bonsai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;

[Combinator]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ModelPredict
{
    static float Predict(float x, float y, float p0, float p1, float p2, float p3)
    {
        return x * p0 + y * p1 + x * x * p2 + y * y * p3;
    }

    public IObservable<Point2f> Process(IObservable<Tuple<Point2f, CalibrationModel>> source)
    {
        return source.Select(value =>
        {
            var eye = value.Item1;
            var pX = value.Item2.Px;
            var pY = value.Item2.Py;
            var wX = Predict(eye.X, eye.Y, pX[0, 0], pX[1, 0], pX[2, 0], pX[3, 0]);
            var wY = Predict(eye.X, eye.Y, pY[0, 0], pY[1, 0], pY[2, 0], pY[3, 0]);
            return new Point2f(wX, wY);
        });
    }
}
