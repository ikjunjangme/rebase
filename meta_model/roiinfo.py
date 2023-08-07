import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import * 
from meta_model.objectcolor import ObjectColor
from meta_model.roi_object_counting import RoiObjectCounting
from meta_model.roi_aggregated_data import RoiAggregatedData

class RoiInfo:
    roi_u_id: str
    roi_name: str
    roi_number : int
    avg_stay_time: float

    roi_object_counting : RoiObjectCounting
    roi_aggregated_dataitems : RoiAggregatedData

    # object_color: ObjectColor
    # object_count: int
    # entered_count: int
    # exited_count: int


    def __init__(self, roi_u_id: str, roi_name: str, roi_number : int, avg_stay_time: float, roi_object_counting : RoiObjectCounting, roi_aggregated_dataitems : RoiAggregatedData) -> None:
        self.roi_u_id = roi_u_id
        self.roi_name = roi_name
        self.roi_number = roi_number
        self.avg_stay_time = avg_stay_time

        self.roi_object_counting = roi_object_counting
        self.roi_aggregated_dataitems = roi_aggregated_dataitems

    @staticmethod
    def from_dict(obj: Any) -> 'RoiInfo':
        assert isinstance(obj, dict)

        roi_u_id = from_str(obj.get("RoiUid"))
        roi_name = from_str(obj.get("RoiName"))
        roi_number = from_int(obj.get("RoiNumber"))
        avg_stay_time = from_float(obj.get("AvgStayTime"))

        roi_object_counting = RoiObjectCounting.from_dict(obj.get("RoiObjectCounting"))
        #roi_aggregated_dataitems = RoiAggregatedData.from_dict(obj.get("RoiAggregatedData"))
        tmp_roi_aggregated = obj.get("RoiAggregatedData")
        if tmp_roi_aggregated:
            roi_aggregated_dataitems = RoiAggregatedData.from_dict(tmp_roi_aggregated)
        else:
            roi_aggregated_dataitems = None

        return RoiInfo(roi_u_id, roi_name, roi_number, avg_stay_time, roi_object_counting, roi_aggregated_dataitems)

    def to_dict(self) -> dict:
        result: dict = {}
        result["RoiUId"] = from_str(self.roi_u_id)
        result["RoiName"] = from_str(self.roi_name)
        result["RoiName"] = from_str(self.roi_name)
        result["RoiNumber"] = from_int(self.roi_number)

        result["RoiObjectCounting"] = to_class(RoiObjectCounting, self.roi_object_counting)
        result["RoiAggregatedData"] = to_class(RoiAggregatedData, self.roi_aggregated_dataitems)
        return result