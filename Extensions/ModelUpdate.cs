using Bonsai;
using Bonsai.Dsp;
using OpenCV.Net;
using MathNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using MathNet.Numerics.LinearAlgebra;

[Combinator]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ModelUpdate
{
    public IObservable<CalibrationModel> Process(IObservable<Tuple<Point2f, Point2f>[]> source)
    {
        return source.Select(observations =>
        {
            var x = CreateMatrix.Dense<float>(observations.Length, 4);
            var y1 = CreateMatrix.Dense<float>(observations.Length, 1);
            var y2 = CreateMatrix.Dense<float>(observations.Length, 1);
            for (int i = 0; i < observations.Length; i++)
            {
                var target = observations[i].Item1;
                var eye = observations[i].Item2;
                x[i, 0] = eye.X;
                x[i, 1] = eye.Y;
                x[i, 2] = eye.X * eye.X;
                x[i, 3] = eye.Y * eye.Y;
                y1[i, 0] = target.X;
                y2[i, 0] = target.Y;
            }

            var m1 = x.QR().Solve(y1);
            var m2 = x.QR().Solve(y2);
            return new CalibrationModel(m1, m2);
        });
    }
}
