import json
from models.common import *

class WorkingTime:
    start: str
    finish: str
    extenstion: int
    
    def __init__(self, start, finish, extenstion) -> None:
        self.start = start
        self.finish = finish
        self.extenstion = extenstion
        
    @staticmethod
    def from_dict(obj: Any) -> 'WorkingTime':
        assert isinstance(obj, dict)
        start = from_str(obj.get("start"))
        finish = from_str(obj.get("finish"))
        extenstion = from_int(obj.get("extenstion"))
        return WorkingTime(start, finish, extenstion)

    def to_dict(self) -> dict:
        result: dict = {}
        result["start"] = from_str(self.start)
        result["finish"] = from_str(self.finish)
        result["extenstion"] = from_str(self.extenstion)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)
          
class TableInfo:
    employee_no: str
    name: str
    position: str
    team: str
    workcontent: str
    worksubject: str
    working_time: str
    start_date: str
    finish_date: str
    extension_date: str

    def __init__(self, employee_no, name, position, team, workcontent, worksubject, working_time, start_date, finish_date, extension_date) -> None:
        self.employee_no = employee_no
        self.name = name
        self.position = position
        self.team = team
        self.workcontent = workcontent
        self.worksubject = worksubject
        self.working_time = working_time
        self.start_date = start_date
        self.finish_date = finish_date
        self.extension_date = extension_date
        
    @staticmethod
    def from_dict(obj: Any) -> 'TableInfo':
        assert isinstance(obj, dict)
        employee_no = from_str(obj.get("employee_no"))
        name = from_str(obj.get("name"))
        position = from_str(obj.get("position"))
        team = from_str(obj.get("team"))
        workcontent = from_str(obj.get("workcontent"))
        worksubject = from_str(obj.get("worksubject"))
        working_time = WorkingTime.from_dict(obj.get("working_time"))
        start_date = from_str(obj.get("start_date"))
        finish_date = from_str(obj.get("finish_date"))
        extension_date = from_str(obj.get("extension_date"))
        return TableInfo(employee_no, name, position, team, workcontent, worksubject, working_time, start_date, finish_date, extension_date)

    def to_dict(self) -> dict:
        result: dict = {}
        result["employee_no"] = from_str(self.employee_no)
        result["name"] = from_str(self.name)
        result["position"] = from_str(self.position)
        result["team"] = from_str(self.team)
        result["workcontent"] = from_str(self.workcontent)
        result["worksubject"] = from_str(self.worksubject)
        result["working_time"] = to_class(WorkingTime, self.working_time)
        result["start_date"] = from_str(self.start_date)
        result["finish_date"] = from_str(self.finish_date)
        result["extension_date"] = from_str(self.extension_date)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def request_tableinfo_to_dict(x: Any) -> TableInfo:
    return to_class(TableInfo, x)

def request_tableinfo_from_dict(s: Any) -> TableInfo:
    return TableInfo.from_dict(s)

class RequestWorktable:
    tableId: str
    tableInfo: TableInfo
    
    def __init__(self, tableId, tableInfo) -> None:
        self.tableId = tableId
        self.tableInfo = tableInfo
        
    @staticmethod
    def from_dict(obj: Any) -> 'RequestWorktable':
        assert isinstance(obj, dict)
        tableId = from_str(obj.get("tableId"))
        tableInfo = TableInfo.from_dict(obj.get("tableInfo"))
        return RequestWorktable(tableId, tableInfo)

    def to_dict(self) -> dict:
        result: dict = {}
        result["tableId"] = from_str(self.tableId)
        result["tableInfo"] = to_class(TableInfo, self.tableInfo)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def request_worktable_to_dict(x: Any) -> RequestWorktable:
    return to_class(RequestWorktable, x)

def request_worktable_from_dict(s: Any) -> RequestWorktable:
    return RequestWorktable.from_dict(s)