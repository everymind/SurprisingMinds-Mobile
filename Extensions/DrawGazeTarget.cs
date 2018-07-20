using Bonsai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;

[Combinator]
[WorkflowElementCategory(ElementCategory.Transform)]
public class DrawGazeTarget
{
    public int Radius{ get; set; }

    public int Thickness{ get; set; }

    public IObservable<IplImage> Process(IObservable<Tuple<IplImage, Point2f>> source)
    {
        return source.Select(value =>
        {
            var result = value.Item1.Clone();
            var point = value.Item2;
            CV.Circle(result, new Point(point), Radius, Scalar.Rgb(255, 0, 0), Thickness);
            return result;
        });
    }
}
