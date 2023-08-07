namespace NKAPIService.API
{
    public enum ErrorCodes
    {
        Success = 0,
        APIUrlPathError = 10,//	api uri path 문제
        APIRequsetDataFormatError = 11,//11	api 요청 데이터 포맷 문제
        NotSupportFunction = 12,//12	지원하지 않는 기능
        InvalidLicense = 20,//20	라이선스 비활성화
        ExpiredLicense = 21,//21	라이선스 기간 만료
        NoSupportAPIVersion = 22,//22	지원하지 않는 API 버전
        RPCPortError = 23,//23	RPC 포트 문제
        HttpPortError = 24,//24	HTTP 포트 문제
        FailCreateComputingNode = 100,
        FailStreamingCamera = 200,
        FailCreateChannel = 201,
        FailCalibration = 210,
        FailCreateSnapshot = 220,
        FailSettingROI = 300,
        CUDA_BUG = 400,
        NodeConnectFail = 500,
        RTSPFormatNotCorrect = 501,

        RequsetTimeout
    }
}
