import json
from models.common import *

class ResponseCreateComputingNode:
    nodeId: str
    product_code: str
    release_date: str
    code: int
    
    def __init__(self, nodeId, product_code, release_date, code) -> None:
        self.nodeId = nodeId
        self.product_code = product_code
        self.release_date = release_date
        self.code = code
        
    @staticmethod
    def from_dict(obj: Any) -> 'ResponseCreateComputingNode':
        assert isinstance(obj, dict)
        nodeId = from_str(obj.get("nodeId"))
        product_code = from_int(obj.get("product_code"))
        release_date = from_int(obj.get("release_date"))
        code = from_int(obj.get("code"))
        return ResponseCreateComputingNode(nodeId, product_code, release_date, code)
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def responsecn_to_dict(x: Any) -> Any:
    return to_class(ResponseCreateComputingNode, x)

def responsecn_from_dict(s: Any) -> ResponseCreateComputingNode:
    return ResponseCreateComputingNode.from_dict(s)