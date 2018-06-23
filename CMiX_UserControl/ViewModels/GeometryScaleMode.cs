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

        [ShortCode("SLD_RDM_DMP", "Slide Random Damped")]
        SLD_RDM_DMP,

        [ShortCode("SLD_RDM_LIN", "Slide Random Linear")]
        SLD_RDM_LIN,

        [ShortCode("SLD_GRW", "Slide Grow")]
        SLD_GRW,

        [ShortCode("SLD_SHK", "Slide Shrink")]
        SLD_SHK,
    }
}