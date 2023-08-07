import json
from models.common import *

class RequestVASchedule:
    nodeId: str
    channel_Id: int
    schedule: list()
    exception: list()
    
    def __init__(self, nodeId, channel_Id, schedule, exception) -> None:
        self.nodeId = nodeId
        self.channel_Id = channel_Id
        self.schedule = schedule
        self.exception = exception
        
    @staticmethod
    def from_dict(obj: Any) -> 'RequestVASchedule':
        assert isinstance(obj, dict)
        nodeId = from_str(obj.get("nodeId"))
        channel_Id = from_str(obj.get("channelId"))
        schedule = obj.get("schedule")
        exception = obj.get("except")
        return RequestVASchedule(nodeId, channel_Id, schedule, exception)

    def to_dict(self) -> dict:
        result: dict = {}
        result["nodeId"] = from_str(self.nodeId)
        result["channelId"] = from_str(self.channel_Id)
        result["schedule"] = self.schedule
        result["except"] = self.exception
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def vaschedule_to_dict(x: Any) -> RequestVASchedule:
    return to_class(RequestVASchedule, x)

def vaschedule_from_dict(s: Any) -> RequestVASchedule:
    return RequestVASchedule.from_dict(s)