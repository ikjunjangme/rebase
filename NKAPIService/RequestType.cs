namespace NKAPIService
{
    public enum RequestType
    {
        #region Computing Node
        CreateComputingNode,
        UpdateComputingNode,
        RemoveComputingNode,
        GetComputingNode,
        ListComputingNode,
        #endregion

        #region Channel
        GetChannel,
        ListChannel,
        RegisterChannel,
        RemoveChannel,
        UpdateChannel,
        #endregion

        #region Events
        CreateROI,
        GetROI,
        UpdateROI,
        RemoveROI,
        ListROI,
        Control,
        RegisterFaceDB,
        UpdateFaceDB,
        UnRegisterFaceDB,
        #endregion


        Calibrate,
        Snapshot,
    }
}
