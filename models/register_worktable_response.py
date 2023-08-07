import json
from models.common import *

class ResponseRegisterWorktable:
    code: int
    
    def __init__(self, code) -> None:
        self.code = code
        
    @staticmethod
    def from_dict(obj: Any) -> 'ResponseRegisterWorktable':
        code = from_int(obj.get("code"))
        return ResponseRegisterWorktable(code)
    
    def to_dict(self) -> dict:
        result: dict = {}
        #result["code"] = from_int(self.code)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def response_register_worktable_to_dict(x: Any) -> ResponseRegisterWorktable:
    return to_class(ResponseRegisterWorktable, x)

def response_register_worktable_from_dict(s: Any) -> ResponseRegisterWorktable:
    return ResponseRegisterWorktable.from_dict(s)