import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import *

class ObjectColor:
    b: int
    g: int
    r: int

    def __init__(self, b: int, g: int, r: int) -> None:
        self.b = b
        self.g = g
        self.r = r

    @staticmethod
    def from_dict(obj: Any) -> 'ObjectColor':
        assert isinstance(obj, dict)
        b = from_int(obj.get("B"))
        g = from_int(obj.get("G"))
        r = from_int(obj.get("R"))
        return ObjectColor(b, g, r)

    def to_dict(self) -> dict:
        result: dict = {}
        result["B"] = from_int(self.b)
        result["G"] = from_int(self.g)
        result["R"] = from_int(self.r)
        return result