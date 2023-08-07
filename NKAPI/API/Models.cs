using PublicUtility.Event.Enum;
using System.Collections.Generic;

namespace NKAPI.API.Model
{
    public class ChannelInfo
    {
        public string nodeId { get; set; }
        public string channelId { get; set; }
        public string inputUri { get; set; }
        public string channelName { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public List<string> siblings { get; set; }
        public ChannelStatus status { get; set; }
    }

    public class RoiDot
    {
        public double x { get; set; }
        public double y { get; set; }
        public List<RoiLine> lineUntilNextDot { get; set; }
    }
    public class RoiLine
    {
        public bool disable { get; set; }
        public string direction { get; set; }
        public List<ObjectType> target { get; set; }
    }
    public class Calibration
    {
        public double focalLengthX { get; set; }
        public double focalLengthY { get; set; }
        public double principalX { get; set; }
        public double principalY { get; set; }

        public double fov { get; set; }

        public double imageWorldX { get; set; }
        public double imageWorldY { get; set; }
        public double imageWorldZ { get; set; }

        public double cameraPanDegree { get; set; }
        public double cameraTiltDegree { get; set; }
        public double cameraHorizontalAngle { get; set; }
    }

    public enum Funtions // 컴퓨팅 노드 관련
    {
        FNC_CORE // 핵심 기능..?
    }
    public enum InputType
    {
        SRC_IPCAM_NORMAL,   //일반 카메라 (Defualt)
        SRC_IPCAM_THERMAL,  //열화상 카메라 
        SRC_IPCAM_DEPTH,    //깊이 센서 카메라
        SRC_ETC,            //기타 영상
    }

    public enum SystemWarnings
    {
        SYS_RESOURCE_FULL,     //시스템 리스소 초과
        SYS_UNSTABLE_NETWORK,  //네트워크 불안정
        SYS_NO_LICENSE,        //라이선스 없음
        SYS_LICENSE_EXPIRED    //라이선스 만료
    }
    public enum Gender
    {
        MAN,
        WOMAN
    }
    public class FaceInfo
    {
        public string faceId { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public Gender gender { get; set; }
        public string infomation { get; set; }
    }
    public class SystemPerformanceInfo
    {
        public string cpuUsage { get; set; }
        public string gpuUsage { get; set; }
        public string gpuTemperature { get; set; }
        public string memoryUsage { get; set; }
        public string memoryTotal { get; set; }
        public string memoryFree { get; set; }
        public string diskUsage { get; set; }
        public string diskTotal { get; set; }
        public string diskFree { get; set; }
    }

    public class SystemVersionInfo
    {
        public string software { get; set; }
        public List<string> detectorModel { get; set; }
        public string firmware { get; set; }
        public string gpuModel { get; set; }
        public string gpuVersion { get; set; }
        public string disk { get; set; }
    }

    public enum VAOperations
    {
        VA_START,
        VA_STOP,
        VA_RESET,
    }
    public enum ErrorCode
    {
        ERROR_NONE = 0,
        ERROR_UNREGISTERED_CHANNEL = 100,
        ERROR_BADFORMAT_RTSP = 101,
        ERROR_ACCESS_USER = 102,
        ERROR_CONNECT_CHANNEL = 103,

        ERROR_CALIBRATION = 110,
        ERROR_SNAPSHOT =111,

        ERROR_CONNECT_COMPUTING_ND = 200,
        //ERROR_FAIL_CONNECT_COMPUTING_ND = 201,

        ERROR_USDED_VA = 300,
        ERROR_UNUSED_VA = 301,
        ERROR_START_VA_UNREGISTERED_CHANNEL = 302,      // 100이랑 동일..?
        ERROR_START_VA_UNCONNECT_CHANNEL = 303,         // 103이랑 동일..?
        ERROR_ROI_POINT = 310
    }
    public class FailResponse
    {
        public string channelId { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
}
