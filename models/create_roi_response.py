import json
from models.common import *

class ResponseCreateRoi:
    roi_id: str
    code: int
    message: str
        
    @staticmethod
    def from_dict(obj: Any) -> 'ResponseCreateRoi':
        assert isinstance(obj, dict)
        roi_id = from_str(obj.get("roi_id"))
        code = from_int(obj.get("code"))
        message = from_str(obj.get("message"))
        return ResponseCreateRoi(roi_id, code, message)
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def responseroi_to_dict(x: Any) -> Any:
    return to_class(ResponseCreateRoi, x)

def responseroi_from_dict(s: Any) -> ResponseCreateRoi:
    return ResponseCreateRoi.from_dict(s)