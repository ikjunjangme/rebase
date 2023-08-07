import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import *

class RoiAggregatedData:
    max_: float
    min_: float
    avg: float
    current: float

    def __init__(self, max_: float, min_: float, avg: float, current: float) -> None:
        self.max_ = max_
        self.min_ = min_
        self.avg = avg
        self.current = current

    @staticmethod
    def from_dict(obj: Any) -> 'RoiAggregatedData':
        assert isinstance(obj, dict)
        max_ = from_float(obj.get("Max"))
        min_ = from_float(obj.get("Min"))
        avg = from_float(obj.get("Avg"))
        current = from_float(obj.get("Cur"))
        return RoiAggregatedData(max_, min_, avg, current)

    def to_dict(self) -> dict:
        result: dict = {}
        result["Max"] = from_float(self.max_)
        result["Min"] = from_float(self.min_)
        result["Avg"] = from_float(self.avg)
        result["Cur"] = from_float(self.current)
        return result