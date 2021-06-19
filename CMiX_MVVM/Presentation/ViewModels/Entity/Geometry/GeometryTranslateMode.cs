namespace CMiX.Core.Presentation.ViewModels
{
    public enum GeometryTranslateMode 
    {
        None,

        #region FLASH

        //[ShortCode("FLA_RDM_XYZ", "Flash Random XYZ")]
        FLA_RDM_XYZ,

        //[ShortCode("FLA_RDM_X", "Flash Random X")]
        FLA_RDM_X,

        //[ShortCode("FLA_RDM_Y", "Flash Random Y")]
        FLA_RDM_Y,

        //[ShortCode("FLA_RDM_Z", "Flash Random Z")]
        FLA_RDM_Z,

        //[ShortCode("FLA_RDM_SLDXYZ", "Flash Random Slide XYZ")]
        FLA_RDM_SLDXYZ,

        //[ShortCode("FLA_RDM_SLDXYZ", "Flash Random Slide X")]
        FLA_RDM_SLDX,

        //[ShortCode("FLA_RDM_SLDY", "Flash Random Slide Y")]
        FLA_RDM_SLDY,

        //[ShortCode("FLA_RDM_SLDZ", "Flash Random Slide Z")]
        FLA_RDM_SLDZ,
        #endregion

        #region SLIDE

        //[ShortCode("SLD_RDM_XYZ", "Slide Random")]
        SLD_RDM_XYZ,

        //[ShortCode("SLD_RDM_X", "Slide Random X")]
        SLD_RDM_X,

        //[ShortCode("SLD_RDM_Y", "Slide Random Y")]
        SLD_RDM_Y, 

        //[ShortCode("SLD_RDM_Z", "Slide Random Z")]
        SLD_RDM_Z,

        //[ShortCode("SLD_LT", "Slide Left")]
        SLD_LT,

        //[ShortCode("SLD_RT", "Slide Right")]
        SLD_RT,

        //[ShortCode("SLD_LTRT", "Slide LeftRight")]
        SLD_LTRT,

        //[ShortCode("SLD_DN", "Slide Down")]
        SLD_DN,

        //[ShortCode("SLD_DNUP", "Slide Down Up")]
        SLD_DNUP, 

        //[ShortCode("SLD_UP", "Slide Up")]
        SLD_UP, 

        //[ShortCode("SLD_BA", "Slide Backward")]
        SLD_BA, 

        //[ShortCode("SLD_BAFO", "Slide Back Forth")]
        SLD_BAFO,

        //[ShortCode("SLD_FO", "Slide Forward")]
        SLD_FO,

        #endregion
    };
}