import json
from models.common import *
from models.create_roi_request import *

class RequestUpdateRoi:
    nodeId: str
    channel_id: str
    roi_id: str
    event_type: str
    roi_name: str
    description: str
    stay_time: int
    number_of: int
    feature: int
    roi_dots: List[RoiDot]
    event_filter: EventFilter
    
    def __init__(self, nodeId, channel_id, roi_id, event_type, roi_name, description, stay_time, number_of, feature, roi_dots, event_filter) -> None:
        self.nodeId = nodeId
        self.channel_id = channel_id
        self.roi_id = roi_id
        self.event_type = event_type
        self.roi_name = roi_name
        self.description = description
        self.stay_time = stay_time
        self.number_of = number_of
        self.feature = feature
        self.roi_dots = roi_dots
        self.event_filter = event_filter
        
    @staticmethod
    def from_dict(obj: Any) -> 'RequestUpdateRoi':
        assert isinstance(obj, dict)
        nodeId = from_str(obj.get("nodeId"))
        channel_id = from_str(obj.get("channelId"))
        roi_id = from_str(obj.get("roiId"))
        event_type = from_str(obj.get("eventType"))
        roi_name = from_str(obj.get("roiName"))
        description = from_str(obj.get("description"))
        stay_time = from_int(obj.get("stayTime"))
        number_of = from_int(obj.get("numberOf"))
        feature = from_int(obj.get("feature"))
        roi_dots = from_list(RoiDot.from_dict, obj.get("roiDots"))
        event_filter = EventFilter.from_dict(obj.get("EventFilter"))
        return RequestUpdateRoi(nodeId, channel_id, roi_id, event_type, roi_name, description, stay_time, number_of, feature, roi_dots, event_filter)

    def to_dict(self) -> dict:
        result: dict = {}
        result["nodeId"] = from_str(self.nodeId)
        result["channelId"] = from_str(self.channel_id)
        result["roiId"] = from_str(self.roi_id)
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

def updateroi_to_dict(x: Any) -> RequestUpdateRoi:
    return to_class(RequestUpdateRoi, x)

def updateroi_from_dict(s: Any) -> RequestUpdateRoi:
    return RequestUpdateRoi.from_dict(s)