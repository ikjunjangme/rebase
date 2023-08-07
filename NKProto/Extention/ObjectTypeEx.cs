using PredefineConstant.Enum.Analysis;

namespace NKProto.Extention
{
    public static class ObjectTypeEx
    {
        public static ClassId ToCalssId(this VAMetaService.ObjectType ot)
        {
            switch (ot)
            {
                case VAMetaService.ObjectType.Person:
                    return ClassId.Person;
                case VAMetaService.ObjectType.Bike:
                    return ClassId.Vehicle_Bike;
                case VAMetaService.ObjectType.Car:
                    return ClassId.Vehicle;
                case VAMetaService.ObjectType.Motorcycle:
                    return ClassId.Vehicle_Motorcycle;
                case VAMetaService.ObjectType.Bus:
                    return ClassId.Vehicle_Bus;
                case VAMetaService.ObjectType.Truck:
                    return ClassId.Vehicle_Truck;
                case VAMetaService.ObjectType.Excavator:
                    return ClassId.Vehicle_Excavator;
                case VAMetaService.ObjectType.Forklift:
                    return ClassId.Vehicle_Forklift;
                case VAMetaService.ObjectType.Lemicon:
                    return ClassId.Vehicle_Lemicon;
                case VAMetaService.ObjectType.Cultivator:
                    return ClassId.Vehicle_Cultivator;
                case VAMetaService.ObjectType.Tractor:
                    return ClassId.Vehicle_Tractor;
                case VAMetaService.ObjectType.Tanktruck:
                    return ClassId.Vehicle_TankTruck;
                case VAMetaService.ObjectType.FaceMan:
                    return ClassId.Face_FaceMan;
                case VAMetaService.ObjectType.FaceWoman:
                    return ClassId.Face_FaceWoman;
                case VAMetaService.ObjectType.Flame:
                    return ClassId.Fire_Flame;
                case VAMetaService.ObjectType.Smoke:
                    return ClassId.Fire_Smoke;

                default:
                    return ClassId.Person;
                    //throw new NotImplementedException();
            }
        }
    }
}
