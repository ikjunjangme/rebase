import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import *
from meta_model.eventdetail import EventDetail
from meta_model.imagerect import ImageRect
from meta_model.objectcolor import ObjectColor
from meta_model.roiinfo import RoiInfo

class EventList:
    abnormal_score: float
    object_prob: float
    class_id: int
    event_detail: EventDetail
    event_status: int
    event_type: str
    image_buffer: str
    image_rect: ImageRect
    inner_image_rect: ImageRect
    is_detected: bool
    is_event: bool
    is_tracked: bool
    object_color: ObjectColor
    object_id: int
    roi_info: RoiInfo
    stay_time: int
    clsitems : list[str]
    event_id : int

    def __init__(self, abnormal_score: float, object_prob: float, event_id: int, class_id: int, event_detail: EventDetail, event_status: int, 
                 event_type: str, image_buffer: str, image_rect: ImageRect, inner_image_rect: ImageRect, 
                 is_detected: bool, is_event: bool, is_tracked: bool, object_color: ObjectColor, 
                 object_id: int, roi_info: RoiInfo, stay_time: int, clsitems : list[str]) -> None:
        self.abnormal_score = abnormal_score
        self.object_prob = object_prob
        self.is_detected = is_detected
        self.is_tracked = is_tracked
        self.is_event = is_event
        self.class_id = class_id
        self.object_id = object_id
        self.event_id = event_id
        self.event_status = event_status
        self.event_type = event_type
        self.roi_info = roi_info
        self.stay_time = stay_time
        self.image_buffer = image_buffer
        self.image_rect = image_rect
        self.inner_image_rect = inner_image_rect
        self.event_detail = event_detail
        self.object_color = object_color
        self.clsitems = clsitems

    @staticmethod
    def from_dict(obj: Any) -> 'EventList':
        assert isinstance(obj, dict)
        abnormal_score = from_float(obj.get("AbnormalScore"))
        object_prob = from_float(obj.get("ObjectProb"))
        event_id = from_int(obj.get("EventID"))
        class_id = from_int(obj.get("ClassID"))
        tmp_evt_detail = obj.get("EventDetail")
        if tmp_evt_detail:
            event_detail = EventDetail.from_dict(tmp_evt_detail)
        else:
            event_detail = None
        event_status = from_int(obj.get("EventStatus"))
        event_type = from_str(obj.get("EventType"))
        image_buffer = from_str(obj.get("ImageBuffer"))
        image_rect = ImageRect.from_dict(obj.get("ImageRect"))
        inner_image_rect = ImageRect.from_dict(obj.get("InnerImageRect"))
        is_detected = from_bool(obj.get("IsDetected"))
        is_event = from_bool(obj.get("IsEvent"))
        is_tracked = from_bool(obj.get("IsTracked"))
        object_color = ObjectColor.from_dict(obj.get("ObjectColor"))
        object_id = from_int(obj.get("ObjectID"))
        roi_info = RoiInfo.from_dict(obj.get("RoiInfo"))
        stay_time = from_float(obj.get("StayTime"))
        clsitems = from_list(lambda x: str(x), obj.get("ClsItems"))

        return EventList(abnormal_score, object_prob, event_id ,class_id, event_detail, event_status, event_type, image_buffer, image_rect, inner_image_rect, is_detected, is_event, is_tracked, object_color, object_id, roi_info, stay_time, clsitems)

    def to_dict(self) -> dict:
        result: dict = {}
        result["AbnormalScore"] = to_float(self.abnormal_score)
        result["ObjectProb"] = from_float(self.object_prob)
        result["IsDetected"] = from_bool(self.is_detected)
        result["IsTracked"] = from_bool(self.is_tracked)              
        result["IsEvent"] = from_bool(self.is_event)
        result["ClassID"] = from_int(self.class_id)
        result["ObjectID"] = from_int(self.object_id)
        result["EventID"] = from_int(self.event_id)
        result["EventStatus"] = from_int(self.event_status)
        result["EventType"] = from_str(self.event_type)
        result["RoiInfo"] = to_class(RoiInfo, self.roi_info)
        result["StayTime"] = from_int(self.stay_time)
        result["ImageBuffer"] = from_str(self.image_buffer)
        result["ImageRect"] = to_class(ImageRect, self.image_rect)
        result["InnerImageRect"] = to_class(ImageRect, self.inner_image_rect)
        result["ObjectColor"] = to_class(ObjectColor, self.object_color)
        result["EventDetail"] = to_class(EventDetail, self.event_detail)
        result["ClsItems"] = from_list(self.clsitems)
        return result