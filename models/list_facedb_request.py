import json
from models.common import *

class RequestListFacedb:
    node_id: str
    
    def __init__(self, node_id) -> None:
        self.node_id = node_id
        
    @staticmethod
    def from_dict(obj: Any) -> 'RequestListFacedb':
        assert isinstance(obj, dict)
        node_id = from_str(obj.get("nodeId"))
        return RequestListFacedb(node_id)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["nodeId"] = from_str(self.node_id)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def request_facedb_to_dict(x: Any) -> RequestListFacedb:
    return to_class(RequestListFacedb, x)

def request_facedb_from_dict(s: Any) -> RequestListFacedb:
    return RequestListFacedb.from_dict(s)