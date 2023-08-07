import json
from models.common import *

class RequestListRoi:
    nodeId: str
    channelId: str
    
    def __init__(self, nodeId, channelId) -> None:
        self.nodeId = nodeId
        self.channelId = channelId

    @staticmethod
    def from_dict(obj: Any) -> 'RequestListRoi':
        assert isinstance(obj, dict)
        nodeId = from_str(obj.get("nodeId"))
        channelId = from_str(obj.get("channelId"))
        return RequestListRoi(nodeId, channelId)

    def to_dict(self) -> dict:
        result: dict = {}
        result["nodeId"] = from_str(self.nodeId)
        result["channelId"] = from_str(self.channelId)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def request_listroi_to_dict(x: Any) -> RequestListRoi:
    return to_class(RequestListRoi, x)

def request_listroi_from_dict(s: Any) -> RequestListRoi:
    return RequestListRoi.from_dict(s)