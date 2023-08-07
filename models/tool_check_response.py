import json
from models.common import *

class Worker:
    stayTime: int
    helmet: str
    gloves: str
    sleeves: str
    boots: str
    mask: str
    
    # def __init__(self, stayTime, helmet, gloves, sleeves, boots, mask) -> None:
    #     self.stayTime = stayTime
    #     self.helmet = helmet
    #     self.gloves = gloves
    #     self.sleeves = sleeves
    #     self.boots = boots
    #     self.mask = mask
        
    @staticmethod
    def from_dict(obj: Any) -> 'Worker':
        stayTime = from_int(obj.get("stayTime"))
        helmet = from_int(obj.get("helmet"))
        gloves = from_int(obj.get("gloves"))
        sleeves = from_int(obj.get("sleeves"))
        boots = from_int(obj.get("boots"))
        mask = from_int(obj.get("mask"))
        return Worker(stayTime, helmet, gloves, sleeves, boots, mask)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["stayTime"] = from_int(self.stayTime)
        result["helmet"] = from_str(self.helmet)
        result["gloves"] = from_str(self.gloves)
        result["sleeves"] = from_str(self.sleeves)
        result["boots"] = from_str(self.boots)
        result["mask"] = from_str(self.mask)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

class ResponseKioskMeta:
    tableId: str
    worker: Worker
    
    def __init__(self, stayTime, worker) -> None:
        self.stayTime = stayTime
        self.worker = worker
        
    @staticmethod
    def from_dict(obj: Any) -> 'ResponseKioskMeta':
        stayTime = from_int(obj.get("tableId"))
        worker = Worker.from_dict(obj.get("worker"))
        return ResponseKioskMeta(stayTime, worker)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["stayTime"] = from_int(self.stayTime)
        result["worker"] = to_class(Worker, self.worker)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def response_register_worktable_to_dict(x: Any) -> ResponseKioskMeta:
    return to_class(ResponseKioskMeta, x)

def response_register_worktable_from_dict(s: Any) -> ResponseKioskMeta:
    return ResponseKioskMeta.from_dict(s)