namespace NKAPIService.API.VideoAnalysisSetting.Models
{
    public enum EventType
    {
        Default = 0,
        Loitering = 1,                // 배회 (1)
        Intrusion = 2,                // 침입 (1)
        Falldown = 3,                // 쓰러짐 (1)
        Violence = 5,                 // 싸움,폭력 (1)
        Congestion = 10,              // 객체 혼잡도 (1)
        LineCrossing = 13,            // 양방향 라인 통과 (1)
        IllegalParking = 14,          // 불법 주정차_10분 (1)
        DirectionCount = 16,          // 방향성 이동 카운트 (좌, 우, 유턴 등)
        CongestionLevel = 17,         // 사람 혼잡도 레벨 (1)
        VehicleDensity = 19,          // 차량 밀도 (1)
        StopVehicleCount = 20,        // 정차 중인 차량 수 카운트 (1)
        LineEnter = 23,               // 단방향 라인 통과(1)
        FireSmoke = 25,               // 화재 연기 (1)
        FireFlame = 26,               // 화재 불꽃 (1)
        MatchingFace = 27,            // 등록 얼굴 매칭 (1)
        UnMaskedFace = 28,            // 얼굴 마스크 착용 (1)
    }
}