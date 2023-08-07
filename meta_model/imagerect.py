import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import *

class ImageRect:
    height: float
    width: float
    x: float
    y: float

    def __init__(self, height: float, width: float, x: float, y: float) -> None:
        self.height = height
        self.width = width
        self.x = x
        self.y = y

    @staticmethod
    def from_dict(obj: Any) -> 'ImageRect':
        assert isinstance(obj, dict)
        height = from_float(obj.get("Height"))
        width = from_float(obj.get("Width"))
        x = from_float(obj.get("X"))
        y = from_float(obj.get("Y"))
        return ImageRect(height, width, x, y)

    def to_dict(self) -> dict:
        result: dict = {}
        result["Height"] = to_float(self.height)
        result["Width"] = to_float(self.width)
        result["X"] = to_float(self.x)
        result["Y"] = to_float(self.y)
        return result



