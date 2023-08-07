namespace PublicUtility.Event.Enum
{
    public enum EventType
    {
        EVT_LONGSTAY = 0,                   // 장기체류
        EVT_LOITERING = 1,                  // 배회
        EVT_INTRUSION = 2,                  // 침입
        EVT_FALLDOWN = 3,                   // 쓰러짐
        EVT_ABANDONMENT = 4,                // 유기 (미구현)
        EVT_VIOLENCE = 5,                   // 싸움
        //EVT_PEOPLE_COUNTING = 8,          // 출입자 카운팅 (KISA)
        //EVT_QUEUEING = 9,                 // 대기열 (KISA)
        EVT_ABNORMAL_CONGESTION = 10,       // 영역 ROI 내 이동흐름 정체 (정상흐름 대비 상대적 정체도)
        EVT_ABNORMAL_OBJ_COUNT = 11,        // 영역 ROI 내 개체밀집 (정의된 개체수 이상의 객체 존재)
        EVT_ROI_COUNT = 12,                 // 영역 ROI 카운팅
        EVT_LINE_COUNT = 13,                // Line ROI 카운팅
        EVT_ILLEGAL_PARKING = 14,           // 불법 주정차
        EVT_WRONG_WAY = 15,                 // Line ROI 역주행
        EVT_DIRECTION_COUNTING = 16,        // 방향성 이동(직전, 좌/우회전, 유턴) 카운팅
        EVT_PEOPLE_CONGESTION_LEVEL = 17,   // 영역내 혼잡도 (상,중,하)
        EVT_VEHICLE_SPEED = 18,             // 차량 속도
        EVT_VEHICLE_DENSITY = 19,           // 차량 밀도
        EVT_STOP_VEHICLE_COUNTING = 20,     // 정지 차량 카운팅
        EVT_SIGNAL_WAITING_TIME = 21,       // 신호 대기 시간
        EVT_LINE_ENTER = 23,                // Line ROI In 카운팅
        EVT_FIRE_SMOKE = 25,
        EVT_FIRE_FLAME = 26,
        EVT_FACE_MATCHING = 27,
        EVT_FACE_MASKED = 28,
        EVT_HEAD_NO_HELMET = 29,
        EVT_HEAD_HELMET = 30,
    }
}
