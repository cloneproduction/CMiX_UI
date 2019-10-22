namespace CMiX.MVVM.ViewModels
{
    public enum GeometryRotationMode
    {
        #region STEADY

        None,
        #endregion

        #region FLASH
        FLA_RDM,
        FLA_RDM_45,
        FLA_RDM_90,
        FLA_CLK_45,
        FLA_CLK_90,
        FLA_CCK_45,
        FLA_CCK_90,
        FLA_180,

        #endregion

        #region SLIDE
        SLD_RDM,
        SLD_CLK_LIN,
        SLD_CCK_LIN,

        #endregion
    }
}
