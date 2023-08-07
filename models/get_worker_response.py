import json
from models.common import *

class ResponseGetWorker:
    eno: str
    team: str
    name: str
    position: str
    #code: int
    
    def __init__(self, eno, team, name, position) -> None:
        self.eno = eno
        self.team = team
        self.name = name
        self.position = position
        #self.code = code
        
    @staticmethod
    def from_dict(obj: Any) -> 'ResponseGetWorker':
        assert isinstance(obj, dict)
        eno = from_str(obj.get("eno"))
        team = from_str(obj.get("team"))
        name = from_str(obj.get("name"))
        position = from_str(obj.get("position"))
        #code = from_int(obj.get("code"))
        return ResponseGetWorker(eno, team, name, position)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["eno"] = from_str(self.eno)
        result["team"] = from_str(self.team)
        result["name"] = from_str(self.name)
        result["position"] = from_str(self.position)
        #result["code"] = from_int(self.code)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def request_getworker_to_dict(x: Any) -> ResponseGetWorker:
    return to_class(ResponseGetWorker, x)

def request_getworker_from_dict(s: Any) -> ResponseGetWorker:
    return ResponseGetWorker.from_dict(s)