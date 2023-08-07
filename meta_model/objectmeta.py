import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import *
from meta_model.eventlist import *

class ObjectMeta:
    camera_name: str
    camera_uid: str
    event_list: List[EventList]
    frame_height: int
    frame_number: int
    frame_width: int
    time_stamp: datetime

    def __init__(self, camera_name: str, camera_uid: str, event_list: List[EventList], frame_height: int, frame_number: int, frame_width: int, time_stamp: datetime) -> None:
        self.camera_name = camera_name
        self.camera_uid = camera_uid
        self.event_list = event_list
        self.frame_height = frame_height
        self.frame_number = frame_number
        self.frame_width = frame_width
        self.time_stamp = time_stamp

    @staticmethod
    def from_dict(obj: Any) -> 'ObjectMeta':
        assert isinstance(obj, dict)
        camera_name = from_str(obj.get("CameraName"))
        camera_uid = from_str(obj.get("CameraUID"))
        event_list = from_list(EventList.from_dict, obj.get("EventList"))
        frame_height = from_int(obj.get("FrameHeight"))
        frame_number = from_int(obj.get("FrameNumber"))
        frame_width = from_int(obj.get("FrameWidth"))
        time_stamp = from_datetime(obj.get("TimeStamp"))
        return ObjectMeta(camera_name, camera_uid, event_list, frame_height, frame_number, frame_width, time_stamp)

    def to_dict(self) -> dict:
        result: dict = {}
        result["CameraName"] = from_str(self.camera_name)
        result["CameraUID"] = from_str(self.camera_uid)
        result["EventList"] = from_list(lambda x: to_class(EventList, x), self.event_list)
        result["FrameHeight"] = from_int(self.frame_height)
        result["FrameNumber"] = from_int(self.frame_number)
        result["FrameWidth"] = from_int(self.frame_width)
        result["TimeStamp"] = self.time_stamp.isoformat()
        return result


def objectmeta_from_dict(s: Any) -> ObjectMeta:
    return ObjectMeta.from_dict(s)


def objectmeta_to_dict(x: ObjectMeta) -> Any:
    return to_class(ObjectMeta, x)