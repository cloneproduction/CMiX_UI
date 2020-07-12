namespace CMiX.MVVM.ViewModels
{
    public enum CurveType
    {
        Linear,
        Quadratic,
        Cubic,
        Quartic,
        Quintic,
        Sine,
        Circular,
        Exponential,
        Elastic,
        Back,
        Bounce
    }

    public enum EaseType
    {
        EaseIn,
        EaseOut,
        EaseInOut
    }

    public enum EasingType
    {
        None,
        Linear,

        QuadraticEaseIn,
        QuadraticEaseOut,
        QuadraticEaseInOut,

        CubicEaseIn,
        CubicEaseOut,
        CubicEaseInOut,

        QuarticEaseIn,
        QuarticEaseOut,
        QuarticEaseInOut,

        QuinticEaseIn,
        QuinticEaseOut,
        QuinticEaseInOut,

        SineEaseIn,
        SineEaseOut,
        SineEaseInOut,

        CircularEaseIn,
        CircularEaseOut,
        CircularEaseInOut,

        ExponentialEaseIn,
        ExponentialEaseOut,
        ExponentialEaseInOut,

        ElasticEaseIn,
        ElasticEaseOut,
        ElasticEaseInOut,

        BackEaseIn,
        BackEaseOut,
        BackEaseInOut,

        BounceEaseIn,
        BounceEaseOut,
        BounceEaseInOut
    }
}