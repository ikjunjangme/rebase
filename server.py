from zmqhelper import *
from flask import Flask, request, abort, jsonify, Response
from flask_cors import CORS
from models.enum import *
from client import *
from loghelper import *
from models.get_worker_response import ResponseGetWorker
from models.register_worktable_response import *
from models.vacontrol_request import *
from models.tool_check_response import *
from models.va_stop_response import *
import globals

app = Flask(__name__)

NodeId = ""
TableId = ""
TableInfo_1 = None
TableInfo_2 = None
TableInfo_3 = None

def get_tool_result(tool):
  if tool == True:      
    return 'Positive'
  else:
    return 'Negative'

def server(makeLogging):
  CORS(app)
  
  @app.route("/v2/va/command", methods=['POST'])
  def post_vastop():
    makeLogging.logger.info('%s %s', request.method, request.path)
    body = request.get_json()
    
    try:
      if body["operation"] != "VA_STOP":
        return
    except:
      makeLogging.logger.error(e)
    
    tableId = body["tableId"]
    tableResult = False
    
    global TableInfo_1
    global TableInfo_2
    global TableInfo_3
    
    # now = datetime.now()
    
    # date = str(now.year) + '-' + format(int(now.month), '02d') + '-' + format(int(now.day), '02d') \
    #        + ' ' + format(int(now.hour), '02d') + ":" + format(int(now.minute), '02d') + ":" + format(int(now.second), '02d')
    
    if body["retry"] == False:
      tableResult = True
    elif tableId == config.worktable_name_1:
      tableResult = globals.isTable1Empty
    elif tableId == config.worktable_name_2:
      tableResult = globals.isTable2Empty
    elif tableId == config.worktable_name_3:
      tableResult = globals.isTable3Empty
      
    if tableId == config.worktable_name_1:
      #TableInfo_1["tableInfo"]["finish_date"] = date
      result = request_tableinfo_from_dict(TableInfo_1["tableInfo"])
    elif tableId == config.worktable_name_2:
      #TableInfo_2["tableInfo"]["finish_date"] = date
      result = request_tableinfo_from_dict(TableInfo_2["tableInfo"])
    elif tableId == config.worktable_name_3:
      #TableInfo_3["tableInfo"]["finish_date"] = date
      result = request_tableinfo_from_dict(TableInfo_3["tableInfo"])
    
    try:
      response = EmptyTableInfo(tableId, '', tableResult, result)
    except Exception as e:
      makeLogging.logger.error(e)
      abort(500)
    return Response(response.toJSON(), mimetype='application/json') 
  
  @app.route("/v2/va/register-worktable", methods=['POST'])
  def post_worktable():
    makeLogging.logger.info('%s %s', request.method, request.path)
    
    globals.ToolCheckResult = CHECK_RESULT.FAIL
    globals.isFaceShieldOn = False
    globals.isLeftGloveOn = False
    globals.isRightGloveOn = False
    globals.isLeftWeldingSleeveOn = False
    globals.isRightWeldingSleeveOn = False
    globals.isLeftSafetyShoesOn = False
    globals.isRightSafetyShoesOn = False
    globals.isMaskOn = True
    globals.stayTime = 0
    
    global TableInfo_1
    global TableInfo_2
    global TableInfo_3
             
    global TableId
    body = request.get_json()
    TableId = body["tableId"]

    try:    
      if TableId == config.worktable_name_1:
        TableInfo_1 = request.get_json()
        if TableInfo_1["tableInfo"]["finish_date"] != "" or TableInfo_1["tableInfo"]["extension_date"] != "":
          requests.post(config.link_api_url + "/v2/va/register-worktable", json=TableInfo_1)
      elif TableId == config.worktable_name_2:        
        TableInfo_2 = request.get_json()
        if TableInfo_2["tableInfo"]["finish_date"] != "" or TableInfo_2["tableInfo"]["extension_date"] != "":
          requests.post(config.link_api_url + "/v2/va/register-worktable", json=TableInfo_2)
      elif TableId == config.worktable_name_3:
        TableInfo_3 = request.get_json()
        if TableInfo_3["tableInfo"]["finish_date"] != "" or TableInfo_3["tableInfo"]["extension_date"] != "":
          requests.post(config.link_api_url + "/v2/va/register-worktable", json=TableInfo_3)
    
      response = ResponseRegisterWorktable(CHECK_RESULT.PASS.value)
    except Exception as e:
      response = ResponseRegisterWorktable(CHECK_RESULT.FAIL.value)
      makeLogging.logger.error(e)
      abort(500)
    return Response(response.toJSON(), mimetype='application/json') 
  
  @app.route("/v2/va/register-worktable", methods=['GET'])
  def get_worktable():
    makeLogging.logger.info('%s %s', request.method, request.path)
    
    global TableId    
    global TableInfo_1
    global TableInfo_2
    global TableInfo_3
    
    if globals.isFaceShieldOn & globals.isLeftGloveOn & globals.isRightGloveOn & globals.isLeftWeldingSleeveOn \
      & globals.isRightWeldingSleeveOn & globals.isLeftSafetyShoesOn & globals.isRightSafetyShoesOn:
      globals.ToolCheckResult = CHECK_RESULT.PASS
      if TableId == config.worktable_name_1:
        requests.post(config.link_api_url + "/v2/va/register-worktable", json=TableInfo_1)
      elif TableId == config.worktable_name_2:
        requests.post(config.link_api_url + "/v2/va/register-worktable", json=TableInfo_2)
      elif TableId == config.worktable_name_3:
        requests.post(config.link_api_url + "/v2/va/register-worktable", json=TableInfo_3)

      global NodeId
      if NodeId == "":
        return
      
      chId = ""
      channels = request_list_channel()
      for ch in channels["channels"]:
        rois = request_list_roi(NodeId, ch["channelId"])
        for roi in rois["rois"]:
          if roi["name"] == TableId:
            chId = ch["channelId"]
            break
    
      vacontrol = RequestVAConrol(NodeId, [chId], OPERAION_TYPE.VA_RST.value, None)
      va_result = vacontrol_to_dict(vacontrol)
        
      try:
        requests.post(config.nk_api_url + "/v2/va/control", json=va_result, timeout=0.5)
      except requests.exceptions.ReadTimeout: 
        pass
    
    worker = Worker()
    worker.stayTime = 0
    worker.gloves = get_tool_result(globals.isLeftGloveOn)
    worker.gloves = get_tool_result(globals.isRightGloveOn)
    worker.boots = get_tool_result(globals.isLeftSafetyShoesOn)
    worker.boots = get_tool_result(globals.isRightSafetyShoesOn)
    worker.sleeves = get_tool_result(globals.isLeftWeldingSleeveOn)
    worker.sleeves = get_tool_result(globals.isRightWeldingSleeveOn)
    worker.helmet = get_tool_result(globals.isFaceShieldOn)
    worker.gloves = get_tool_result(globals.isRightGloveOn)
    worker.mask = "Positive"
    
    response = ResponseKioskMeta(TableId, worker)
    return Response(response.toJSON(), mimetype='application/json')
  
  @app.route("/welding/", methods=['GET'])
  def get_worker():
    makeLogging.logger.info('%s %s', request.method, request.path)        
    cn = request_get_cn()
    node = cn["node"]
    global NodeId
    NodeId = node["nodeId"]
    facedb = request_list_facedb(NodeId)
    list = facedb["faceDBs"]
    body = request.full_path.split("=")
    
    response = ResponseGetWorker("", "", "", "")
    for item in list:
      if body[1] == item["userId"]:
        str = item["memo"].split('/')
        response = ResponseGetWorker(item["userId"], str[1], item["userName"], str[2])
        break
    return Response(response.toJSON(), mimetype='application/json')
      
  @app.errorhandler(400)
  def bad_request(error):
      return jsonify({
          "error_code": 400,
          "error_message": "bad request"
      }), 400
      
  @app.errorhandler(500)
  def internal_server_error(error):
      return jsonify({
          "error_code": 500,
          "error_message": "internal server error"
      }), 500
  
  return app