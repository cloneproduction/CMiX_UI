namespace CMiX.ViewModels
{
    public enum GeometryTranslateMode
    {
        #region STEADY

        [ShortCode("STD_CTR", "Steady_Center")]
        STD_CTR,

        [ShortCode("STD_LT", "Steady Left")]
        STD_LT,

        [ShortCode("STD_TOP_LT", "Steady Top Left")]
        STD_TOP_LT,

        [ShortCode("STD_TOP_RT", "Steady Top Right")]
        STD_TOP_RT,

        [ShortCode("STD_RT", "Steady Right")]
        STD_RT,

        [ShortCode("STD_BOT_RT", "Steady Bottom Right")]
        STD_BOT_RT,

        [ShortCode("STD_BOT", "Steady Bottom")]
        STD_BOT,

        [ShortCode("STD_BOT_LT", "Steady Bottom Left")]
        STD_BOT_LT,

        #endregion

        #region FLASH

        [ShortCode("FLA_RDM", "Flash Random")]
        FLA_RDM,

        [ShortCode("FLA_RDM_X", "Flash Random X")]
        FLA_RDM_X,

        [ShortCode("FLA_RDM_Y", "Flash Random Y")]
        FLA_RDM_Y,

        [ShortCode("FLA_RDM_Z", "Flash Random Z")]
        FLA_RDM_Z,

        #endregion

        #region SLIDE

        [ShortCode("SLD_RDM", "Slide Random")]
        SLD_RDM,

        [ShortCode("SLD_RDM_X", "Slide Random X")]
        SLD_RDM_X,

        [ShortCode("SLD_RDM_Y", "Slide Random Y")]
        SLD_RDM_Y, 

        [ShortCode("SLD_RDM_Z", "Slide Random Z")]
        SLD_RDM_Z,

        [ShortCode("SLD_LT", "Slide Left")]
        SLD_LT,

        [ShortCode("SLD_RT", "Slide Right")]
        SLD_RT,

        [ShortCode("SLD_DN", "Slide Down")]
        SLD_DN,

        [ShortCode("SLD_DNUP", "Slide Down Up")]
        SLD_DNUP, 

        [ShortCode("SLD_UP", "Slide Up")]
        SLD_UP, 

        [ShortCode("SLD_BA", "Slide Backward")]
        SLD_BA, 

        [ShortCode("SLD_BAFO", "Slide Back Forth")]
        SLD_BAFO,

        [ShortCode("SLD_FO", "Slide Forward")]
        SLD_FO,

        #endregion
    };
}