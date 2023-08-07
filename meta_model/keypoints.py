import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import *

class KeyPoint:
    score: float
    x: float
    y: float

    def __init__(self, score: float, x: float, y: float) -> None:
        self.score = score
        self.x = x
        self.y = y

    @staticmethod
    def from_dict(obj: Any) -> 'KeyPoint':
        assert isinstance(obj, dict)
        score = from_float(obj.get("Score"))
        x = from_float(obj.get("X"))
        y = from_float(obj.get("Y"))
        return KeyPoint(score, x, y)

    def to_dict(self) -> dict:
        result: dict = {}
        result["Score"] = to_float(self.score)
        result["X"] = to_float(self.x)
        result["Y"] = to_float(self.y)
        return result
