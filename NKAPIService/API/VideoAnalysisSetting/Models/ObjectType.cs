namespace NKAPIService.API.VideoAnalysisSetting.Models
{
    public enum ObjectType
    {
        PERSON = 0,                   // 사람
        BIKE = 1,                     // 자전거
        CAR = 2,                      // 승용차
        MOTORCYCLE = 3,               // 오토바이
        BUS = 4,                      // 버스
        TRUCK = 5,                    // 트럭
        EXCAVATOR = 6,                // 굴착기
        TANKTRUCK = 7,                // 탱크트럭
        FORKLIFT = 8,                 // 지게차
        LEMICON = 9,                  // 레미콘
        CULTIVATOR = 10,              // 경운기
        TRACTOR = 11,                 // 트랙터
        FLAME = 50,                   // 불꽃
        SMOKE = 51,                   // 연기
        FACE_MAN = 200,               // 얼굴_남자
        FACE_WOMAN = 201,             // 얼굴_여자
    }
}
