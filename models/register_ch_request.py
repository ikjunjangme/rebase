import json
from models.common import *

class RequestRegisterCh:
    nodeId: str
    channel_name: str
    input_url: str
    input_type: int
    group_name: str
    description: str
    
    def __init__(self, nodeId, channel_name, input_url, input_type, group_name, description) -> None:
        self.nodeId = nodeId
        self.channel_name = channel_name
        self.input_url = input_url
        self.input_type = input_type
        self.group_name = group_name
        self.description = description
        
    @staticmethod
    def from_dict(obj: Any) -> 'RequestRegisterCh':
        assert isinstance(obj, dict)
        nodeId = from_str(obj.get("nodeId"))
        channel_name = from_str(obj.get("channelName"))
        input_url = from_str(obj.get("inputUrl"))
        input_type = from_int(obj.get("inputType"))
        group_name = from_str(obj.get("groupName"))
        description = from_str(obj.get("description"))
        return RequestRegisterCh(nodeId, channel_name, input_url, input_type, group_name, description)

    def to_dict(self) -> dict:
        result: dict = {}
        result["nodeId"] = from_str(self.nodeId)
        result["channelName"] = from_str(self.channel_name)
        result["inputUrl"] = from_str(self.input_url)
        result["inputType"] = from_int(self.input_type)
        result["groupName"] = from_str(self.group_name)
        result["description"] = from_str(self.description)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def registerch_to_dict(x: Any) -> RequestRegisterCh:
    return to_class(RequestRegisterCh, x)

def registerch_from_dict(s: Any) -> RequestRegisterCh:
    return RequestRegisterCh.from_dict(s)