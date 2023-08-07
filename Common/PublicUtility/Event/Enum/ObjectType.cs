namespace PublicUtility.Event.Enum
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
        SMOKE = 50,                   // 불꽃
        FLAME = 51,                   // 연기
        FIGHT = 55,                   // 싸움
        FACE_MAN = 200,               // 얼굴_남자
        FACE_WOMAN = 201,             // 얼굴_여자
        FACE_HEAD = 202,              // 얼굴_머리
        FACE_HELMET = 203,            // 얼굴_헬멧

        //비활성화 객체.. (0~79)
        //4,    // 비행기
        //15,   // 개
        //16,   // 고양이
        //26,   // 가방
        //28,   // 의류가방
        //57,   // 의자

        NOT_DEFINE = 9999,
    }
}
