namespace PredefineConstant.Enum.Analysis
{
    //PERSON = 0;                   // 사람
    //BIKE = 1;                     // 자전거
    //CAR = 2;                      // 승용차
    //MOTORCYCLE = 3;               // 오토바이
    //BUS = 4;                      // 버스
    //TRUCK = 5;                    // 트럭
    //EXCAVATOR = 6;                // 굴착기
    //TANKTRUCK = 7;                // 탱크트럭
    //FORKLIFT = 8;                 // 지게차
    //LEMICON = 9;                  // 레미콘
    //CULTIVATOR = 10;              // 경운기
    //TRACTOR = 11;                 // 트랙터
    //FLAME = 50;                   // 불꽃
    //SMOKE = 51;                   // 연기
    //FACE_MAN = 200;               // 얼굴_남자
    //FACE_WOMAN = 201;             // 얼굴_여자
    public enum ClassId
    {
        Person = 0,
        Vehicle = 2,
        Face_FaceMan=200,
        Face_FaceWoman=201,
        Fire_Smoke=50,
        Fire_Flame=51,
        Vehicle_Bike=1,
        Vehicle_Motorcycle=3,
        Vehicle_Bus=4,
        Vehicle_Truck=5,
        Vehicle_Excavator=6,
        Vehicle_TankTruck=7,
        Vehicle_Forklift=8,
        Vehicle_Lemicon=9,
        Vehicle_Cultivator=10,
        Vehicle_Tractor=11,
        Vehicle_ElectricCar
    }

    public static class ClassIdEx
    {
        public static ObjectType ToObjectType(this ClassId classid)
        {
            switch (classid)
            {
                case ClassId.Face_FaceMan:
                case ClassId.Face_FaceWoman:
                    return ObjectType.Face;
                case ClassId.Person:
                    return ObjectType.Person;
                case ClassId.Fire_Smoke:
                case ClassId.Fire_Flame:
                    return ObjectType.Fire;
                case ClassId.Vehicle:
                case ClassId.Vehicle_Bike:
                case ClassId.Vehicle_Motorcycle:
                case ClassId.Vehicle_Bus:
                case ClassId.Vehicle_Truck:
                case ClassId.Vehicle_Excavator:
                case ClassId.Vehicle_TankTruck:
                case ClassId.Vehicle_Forklift:
                case ClassId.Vehicle_Lemicon:
                case ClassId.Vehicle_Cultivator:
                case ClassId.Vehicle_Tractor:
                case ClassId.Vehicle_ElectricCar:
                    return ObjectType.Vehicle; 
                default:
                    return ObjectType.Vehicle;
            }
        }
    }
}
