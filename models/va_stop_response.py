import json
from models.common import *
from models.worktable_request import *

class EmptyTableInfo:
    tableId: str
    message: str
    empty: bool
    tableInfo: TableInfo
    
    def __init__(self, tableId, message, empty, tableInfo) -> None:
        self.tableId = tableId
        self.message = message
        self.empty = empty
        self.tableInfo = tableInfo
        
    @staticmethod
    def from_dict(obj: Any) -> 'EmptyTableInfo':
        tableId = from_int(obj.get("tableId"))
        message = from_int(obj.get("message"))
        empty = from_bool(obj.get("empty"))
        tableInfo = TableInfo.from_dict(obj.get("tableInfo"))
        return EmptyTableInfo(tableId, message, empty, tableInfo)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["tableId"] = from_int(self.tableId)
        result["message"] = from_str(self.message)
        result["empty"] = from_bool(self.empty)
        result["tableInfo"] = to_class(TableInfo, self.tableInfo)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)
