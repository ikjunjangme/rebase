from enum import Enum
    
class ClassId(Enum):
    Person = 0
    Person_Falldown = 2
    Person_Violence = 3
    Person_Head = 4
    Person_Helmet = 5
    Vehicle_Bike = 100
    Vehicle = 102
    Vehicle_Motorcycle = 103
    Vehicle_Bus = 104
    Vehicle_Truck = 105
    Vehicle_Excavator = 106
    Vehicle_TankTruck = 107
    Vehicle_Forklift = 108
    Vehicle_Lemicon = 109
    Vehicle_Cultivator = 110
    Vehicle_Tractor = 111
    Vehicle_ElectricCar = 112
    Fire_Smoke = 200
    Fire_Flame = 201
    Face_FaceMan = 300
    Face_FaceWoman = 301
    Head_Helmet = 402
    Head_Head = 403
    Bag = 500
    
class InputType(Enum):
    SRC_IPCAM_NORMAL = 0   #일반 카메라 (Defualt)
    SRC_IPCAM_THERMAL= 1  #열화상 카메라 
    SRC_IPCAM_DEPTH= 2    #깊이 센서 카메라
    SRC_ETC = 3            #기타 영상
    
class RoiFeature(Enum):
    ALL = 0
    INSIDE = 1
    OUTSIDE = 2
    IGNORE = 3
    
class ROI_TYPE(Enum):
    NONE = 0
    POLYGON = 1
    Rect = 2
    RECTANGLE = 3
    SINGLE_LINE = 4
    DOUBLE_LINE = 5
    
class OPERAION_TYPE(Enum):
    VA_START = 0
    VA_STOP = 1
    VA_RST = 2
    
class CHECK_RESULT(Enum):
    FAIL = -1
    PASS = 0
    WARNING = 1
    
class TOOL_RESULT(Enum):
    POSIVITE = 0
    NEGATIVE = 1
    UNKNOWN = 2