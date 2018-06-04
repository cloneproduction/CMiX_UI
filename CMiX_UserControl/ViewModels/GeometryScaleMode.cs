namespace CMiX.ViewModels
{
    public enum GeometryScaleMode
    {
        [ShortCode("STD_CTR", "Steady Center")]
        STD_CTR,

        [ShortCode("STD_RDM", "Steady Random")]
        STD_RDM,

        [ShortCode("FLA_RDM", "Flash Random")]
        FLA_RDM,

        [ShortCode("SLD_RDM", "Slide Random")]
        SLD_RDM,

        [ShortCode("SLD_GRW", "Slide Grow")]
        SLD_GRW,

        [ShortCode("SLD_SHK", "Slide Shrink")]
        SLD_SHK,
    }
}