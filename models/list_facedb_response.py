import json
from models.common import *

class FaceDB:
    node_id: str
    uuid: str
    user_id: str
    user_name: str
    user_age: int
    identifier: int
    gender: int
    face_images: list()
    memo: str
    
    def __init__(self, node_id, uuid, user_id, user_name, user_age, identifier, gender, face_images, memo) -> None:
        self.node_id = node_id
        self.uuid = uuid
        self.user_id = user_id
        self.user_name = user_name
        self.user_age = user_age
        self.identifier = identifier
        self.gender = gender
        self.face_images = face_images
        self.memo = memo
        
    @staticmethod
    def from_dict(obj: Any) -> 'FaceDB':
        assert isinstance(obj, dict)
        node_id = from_str(obj.get("nodeId"))
        uuid = from_str(obj.get("uuid"))
        user_id = from_str(obj.get("userId"))
        user_name = from_str(obj.get("userName"))
        user_age = from_int(obj.get("userAge"))
        identifier = from_int(obj.get("identifier"))
        gender = from_int(obj.get("gender"))
        face_images = []
        memo = from_str(obj.get("memo"))
        return FaceDB(node_id, uuid, user_id, user_name, user_age, identifier, gender, face_images, memo)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["nodeId"] = from_str(self.node_id)
        result["uuid"] = from_str(self.uuid)
        result["userId"] = from_str(self.user_id)
        result["userName"] = from_str(self.user_name)
        result["userAge"] = from_int(self.user_age)
        result["identifier"] = from_int(self.identifier)
        result["gender"] = from_int(self.gender)
        result["faceImages"] = self.face_images
        result["memo"] = from_str(self.memo)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

class ResponseListFacedb:
    face_dbs: FaceDB
    
    def __init__(self, face_dbs) -> None:
        self.face_dbs = face_dbs
        
    @staticmethod
    def from_dict(obj: Any) -> 'ResponseListFacedb':
        assert isinstance(obj, dict)
        face_dbs = FaceDB.from_dict(obj.get("faceDBs"))
        return ResponseListFacedb(face_dbs)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["faceDBs"] = to_class(FaceDB, self.face_dbs)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def request_facedb_to_dict(x: Any) -> ResponseListFacedb:
    return to_class(ResponseListFacedb, x)

def request_facedb_from_dict(s: Any) -> ResponseListFacedb:
    return ResponseListFacedb.from_dict(s)