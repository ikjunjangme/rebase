import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import *

class RoiObjectCounting:
    object_count: int
    entered_count: int
    exited_count: int

    def __init__(self, object_count: int, entered_count: int, exited_count: int) -> None:
        self.object_count = object_count
        self.entered_count = entered_count
        self.exited_count = exited_count

    @staticmethod
    def from_dict(obj: Any) -> 'RoiObjectCounting':
        assert isinstance(obj, dict)
        object_count = from_int(obj.get("ObjectCount"))
        entered_count = from_int(obj.get("EnteredCount"))
        exited_count = from_int(obj.get("ExitedCount"))
        return RoiObjectCounting(object_count, entered_count, exited_count)

    def to_dict(self) -> dict:
        result: dict = {}
        result["ObjectCount"] = from_int(self.object_count)
        result["EnteredCount"] = from_int(self.entered_count)
        result["ExitedCount"] = from_int(self.exited_count)
        return result