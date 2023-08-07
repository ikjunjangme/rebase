import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import * 

class Face:
    age: int
    gender: int
    name: str
    identify : int

    def __init__(self, age: int, gender: int, name: str, identify: int) -> None:
        self.age = age
        self.gender = gender
        self.name = name
        self.identify = identify

    @staticmethod
    def from_dict(obj: Any) -> 'Face':
        assert isinstance(obj, dict)
        age = from_int(obj.get("Age"))
        gender = from_int(obj.get("Gender"))
        name = from_str(obj.get("Name"))
        identify = from_int(obj.get("Identify"))
        return Face(age, gender, name, identify)

    def to_dict(self) -> dict:
        result: dict = {}
        result["Age"] = from_int(self.age)
        result["Gender"] = from_int(self.gender)
        result["Name"] = from_str(self.name)
        result["Identify"] = from_int(self.identify)
        return result