import json
from models.common import *

class RequestGetWorker:
    eno: str
    
    def __init__(self, eno) -> None:
        self.eno = eno
        
    @staticmethod
    def from_dict(obj: Any) -> 'RequestGetWorker':
        assert isinstance(obj, dict)
        eno = from_str(obj.get("eno"))
        return RequestGetWorker(eno)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["eno"] = from_str(self.eno)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def request_facedb_to_dict(x: Any) -> RequestGetWorker:
    return to_class(RequestGetWorker, x)

def request_facedb_from_dict(s: Any) -> RequestGetWorker:
    return RequestGetWorker.from_dict(s)