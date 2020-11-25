namespace Nono.Engine
{
    public enum Box : byte
    {
        _ = 255,
        X = 0,
        O = 1,

        Empty = 255,
        Crossed = 0,
        Filled = 1
    }
}