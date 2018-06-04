namespace CMiX.ViewModels
{
    public enum PostFXView
    {
        [ShortCode("NONE", "NONE")]
        NONE,

        [ShortCode("MIR_CTR", "Mirror Center")]
        MIR_CTR,

        [ShortCode("MIR_LT", "Mirror Left")]
        MIR_LT,

        [ShortCode("MIR_TOP", "Mirror Top")]
        MIR_TOP,

        [ShortCode("MIR_RT", "Mirror Right")]
        MIR_RT,

        [ShortCode("MIR_BOT", "Mirror Bottom")]
        MIR_BOT,

        [ShortCode("CLA_LT", "Clamp Left")]
        CLA_LT,

        [ShortCode("CLA_TOP", "Clamp Top")]
        CLA_TOP,

        [ShortCode("CLA_RT", "Clamp Right")]
        CLA_RT,

        [ShortCode("CLA_BOT", "Clamp Bottom")]
        CLA_BOT,
    };
}