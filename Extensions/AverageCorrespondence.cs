using Bonsai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Dsp;
using OpenCV.Net;

[Combinator]
[WorkflowElementCategory(ElementCategory.Transform)]
public class AverageCorrespondence
{
    public IObservable<Tuple<Point2f, Point2f>> Process(IObservable<Tuple<ArrayExtrema, Point2f>[]> source)
    {
        return source.Select(value =>
        {
            Point2f meanCorrespondence1;
            Point2f meanCorrespondence2;
            if (value.Length > 0)
            {
                meanCorrespondence1 = Point2f.Zero;
                meanCorrespondence2 = Point2f.Zero;
                var meanWeight = 1f / value.Length;
                for (int i = 0; i < value.Length; i++)
                {
                    meanCorrespondence1 += new Point2f(value[i].Item1.MaxLocation) * meanWeight;
                    meanCorrespondence2 += value[i].Item2 * meanWeight;
                }
            }
            else
            {
                meanCorrespondence1 = new Point2f(float.NaN, float.NaN);
                meanCorrespondence2 = new Point2f(float.NaN, float.NaN);
            }

            return Tuple.Create(meanCorrespondence1, meanCorrespondence2);
        });
    }
}
