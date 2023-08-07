import json
from models.common import *

class EventFilter:
    min_detect_size: int
    max_detect_size: int
    objects_target: list()
    
    def __init__(self, min_detect_size, max_detect_size, objects_target) -> None:
        self.min_detect_size = min_detect_size
        self.max_detect_size = max_detect_size
        self.objects_target = objects_target
    
    @staticmethod
    def from_dict(obj: Any) -> 'EventFilter':
        assert isinstance(obj, dict)
        min_detect_size = from_int(obj.get("minDetectSize"))
        max_detect_size = from_int(obj.get("maxDetectSize"))
        objects_target = []
        return EventFilter(min_detect_size, max_detect_size, objects_target)

    def to_dict(self) -> dict:
        result: dict = {}
        result["minDetectSize"] = from_int(self.min_detect_size)
        result["maxDetectSize"] = from_int(self.max_detect_size)
        result["objectsTarget"] = self.objects_target
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

class RoiDot:
    x: float
    y: float
    line_until_nextk_dot: None
    
    def __init__(self, x, y, line_until_nextk_dot) -> None:
        self.x = x
        self.y = y
        self.line_until_nextk_dot = line_until_nextk_dot
    
    @staticmethod
    def from_dict(obj: Any) -> 'RoiDot':
        assert isinstance(obj, dict)
        x = from_float(obj.get("x"))
        y = from_float(obj.get("y"))
        line_until_nextk_dot = None
        return RoiDot(x, y, line_until_nextk_dot)

    def to_dict(self) -> dict:
        result: dict = {}
        result["x"] = from_float(self.x)
        result["y"] = from_float(self.y)
        result["lineUntilNextkDot"] = self.line_until_nextk_dot
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

class RequestCreateRoi:
    nodeId: str
    channel_id: str
    roi_type: int
    event_type: str
    roi_name: str
    description: str
    stay_time: int
    number_of: int
    feature: int
    roi_dots: List[RoiDot]
    event_filter: EventFilter
    
    def __init__(self, nodeId, channel_id, roi_type, event_type, roi_name, description, stay_time, number_of, feature, roi_dots, event_filter) -> None:
        self.nodeId = nodeId
        self.channel_id = channel_id
        self.roi_type = roi_type
        self.event_type = event_type
        self.roi_name = roi_name
        self.description = description
        self.stay_time = stay_time
        self.number_of = number_of
        self.feature = feature
        self.roi_dots = roi_dots
        self.event_filter = event_filter
        
    @staticmethod
    def from_dict(obj: Any) -> 'RequestCreateRoi':
        assert isinstance(obj, dict)
        nodeId = from_str(obj.get("nodeId"))
        channel_id = from_str(obj.get("channelId"))
        roi_type = from_int(obj.get("roiType"))
        event_type = from_str(obj.get("eventType"))
        roi_name = from_str(obj.get("roiName"))
        description = from_str(obj.get("description"))
        stay_time = from_int(obj.get("stayTime"))
        number_of = from_int(obj.get("numberOf"))
        feature = from_int(obj.get("feature"))
        roi_dots = from_list(RoiDot.from_dict, obj.get("roiDots"))
        event_filter = EventFilter.from_dict(obj.get("EventFilter"))
        return RequestCreateRoi(nodeId, channel_id, roi_type, event_type, roi_name, description, stay_time, number_of, feature, roi_dots, event_filter)

    def to_dict(self) -> dict:
        result: dict = {}
        result["nodeId"] = from_str(self.nodeId)
        result["channelId"] = from_str(self.channel_id)
        result["roiType"] = from_int(self.roi_type)
        result["eventType"] = from_str(self.event_type)
        result["roiName"] = from_str(self.roi_name)
        result["description"] = from_str(self.description)
        result["stayTime"] = from_int(self.stay_time)
        result["numberOf"] = from_int(self.number_of)
        result["feature"] = from_int(self.feature)
        result["roiDots"] = from_list(lambda x: to_class(RoiDot, x), self.roi_dots)
        result["EventFilter"] = to_class(EventFilter, self.event_filter)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def createroi_to_dict(x: Any) -> RequestCreateRoi:
    return to_class(RequestCreateRoi, x)

def createroi_from_dict(s: Any) -> RequestCreateRoi:
    return RequestCreateRoi.from_dict(s)