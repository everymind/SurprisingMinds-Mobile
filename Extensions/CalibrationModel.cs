using System;
using MathNet.Numerics.LinearAlgebra;

public struct CalibrationModel
{
    public Matrix<float> Px;
    public Matrix<float> Py;

    public CalibrationModel(Matrix<float> px, Matrix<float> py)
    {
        Px = px;
        Py = py;
    }
}