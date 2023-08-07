import json
from models.common import *

class RequestVAConrol:
    nodeId: str
    channel_ids: list()
    operation: int
    parameter: list()
    
    def __init__(self, nodeId, channel_ids, operation, parameter) -> None:
        self.nodeId = nodeId
        self.channel_ids = channel_ids
        self.operation = operation
        self.parameter = parameter
        
    @staticmethod
    def from_dict(obj: Any) -> 'RequestVAConrol':
        assert isinstance(obj, dict)
        nodeId = from_str(obj.get("nodeId"))
        channel_ids = obj.get("channelIds")
        operation = from_int(obj.get("operation"))
        parameter = obj.get("parameter")
        return RequestVAConrol(nodeId, channel_ids, operation, parameter)

    def to_dict(self) -> dict:
        result: dict = {}
        result["nodeId"] = from_str(self.nodeId)
        result["channelIds"] = self.channel_ids
        result["operation"] = from_int(self.operation)
        result["parameter"] = self.parameter
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def vacontrol_to_dict(x: Any) -> RequestVAConrol:
    return to_class(RequestVAConrol, x)

def vacontrol_from_dict(s: Any) -> RequestVAConrol:
    return RequestVAConrol.from_dict(s)