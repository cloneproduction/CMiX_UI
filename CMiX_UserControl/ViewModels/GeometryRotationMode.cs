namespace CMiX.ViewModels
{
    public enum GeometryRotationMode
    {
        #region STEADY

        [ShortCode("STD_CTR", "Steady Center")]
        STD_CTR,

        [ShortCode("STD_45", "Steady 45")]
        STD_45,

        [ShortCode("STD_90", "Steady 90")]
        STD_90,

        [ShortCode("STD_135", "Steady 135")]
        STD_135,

        [ShortCode("STD_180", "Steady 180")]
        STD_180,

        [ShortCode("STD_225", "Steady 225")]
        STD_225,

        [ShortCode("STD_270", "Steady 270")]
        STD_270,

        #endregion

        #region FLASH

        [ShortCode("FLA_RDM", "Flash Random")]
        FLA_RDM,

        [ShortCode("FLA_RDM_45", "Flash Random 45")]
        FLA_RDM_45,

        [ShortCode("FLA_RDM_90", "Flash Random 90")]
        FLA_RDM_90,

        [ShortCode("FLA_CLK_45", "Flash Clockwise 45")]
        FLA_CLK_45,

        [ShortCode("FLA_CLK_90", "Flash Clockwise 90")]
        FLA_CLK_90,

        [ShortCode("FLA_CCK_45", "Flash CounterClockwise 45")]
        FLA_CCK_45,

        [ShortCode("FLA_CCK_90", "Flash CounterClockwise 90")]
        FLA_CCK_90,

        [ShortCode("FLA_180", "Flash 180")]
        FLA_180,

        #endregion

        #region SLIDE

        [ShortCode("SLD_RDM", "Slide Random")]
        SLD_RDM,

        [ShortCode("SLD_CLK_LIN", "Slide Clockwise Linear")]
        SLD_CLK_LIN,

        [ShortCode("SLD_CCK_LIN", "Slide CounterClockwise Linear")]
        SLD_CCK_LIN,

        #endregion
    }
}
