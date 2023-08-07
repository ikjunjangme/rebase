import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import *
from meta_model.face import Face
from meta_model.keypoints import KeyPoint
from meta_model.vehicleinfo import Vehicle


class EventDetail:
    event_score: int
    face: Face
    face_uid: str
    key_points: dict
    vehicle : Vehicle

    def __init__(self, event_score: int, face: Face, face_uid: str, key_points: dict, vehicle) -> None:
        self.event_score = event_score
        self.face = face
        self.face_uid = face_uid
        self.key_points = key_points
        self.vehicle = vehicle

    @staticmethod
    def from_dict(obj: Any) -> 'EventDetail':
        assert isinstance(obj, dict)
        event_score = from_float(obj.get("EventScore"))
        face = Face.from_dict(obj.get("Face"))
        face_uid = from_str(obj.get("FaceUID"))
        key_points = obj.get("KeyPoints")
        #tmp_key_points = obj.get("KeyPoints")
        #key_points = KeyPoint.from_dict(obj.get("KeyPoints")) #from_list(KeyPoint.from_dict, obj.get("KeyPoints"))
        # if tmp_key_points:
        #     key_points = KeyPoint.from_dict(tmp_key_points)
        # else:
        #     key_points = None
        vehicle = Vehicle.from_dict(obj.get("Vehicle"))
        return EventDetail(event_score, face, face_uid, key_points, vehicle)

    def to_dict(self) -> dict:
        result: dict = {}
        result["EventScore"] = from_int(self.event_score)
        result["Face"] = to_class(Face, self.face)
        result["Vehicle"] = to_class(Vehicle, self.vehicle)
        result["FaceUID"] = from_str(self.face_uid)
        result["KeyPoints"] = self.key_points
        #result["KeyPoints"] = from_list(lambda x: to_class(KeyPoint, x), self.key_points)
        return result
