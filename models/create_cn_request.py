import json
from models.common import *

class RequestCreateComputingNode:
    host: str
    node_name: str
    license: str
    
    def __init__(self, host, node_name, license) -> None:
        self.host = host
        self.node_name = node_name
        self.license = license
        
    @staticmethod
    def from_dict(obj: Any) -> 'RequestCreateComputingNode':
        assert isinstance(obj, dict)
        host = from_str(obj.get("host"))
        node_name = from_str(obj.get("nodeName"))
        license = from_str(obj.get("license"))
        return RequestCreateComputingNode(host, node_name, license)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["host"] = from_str(self.host)
        result["nodeName"] = from_str(self.node_name)
        result["license"] = from_str(self.license)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def requestcn_to_dict(x: Any) -> RequestCreateComputingNode:
    return to_class(RequestCreateComputingNode, x)

def requestcn_from_dict(s: Any) -> RequestCreateComputingNode:
    return RequestCreateComputingNode.from_dict(s)