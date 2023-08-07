import requests
from models.register_ch_request import *
from models.create_cn_request import *
from models.create_roi_request import *
from models.update_roi_request import *
from models.update_roi_request import *
from models.list_facedb_request import *
from models.worktable_request import *
from models.list_roi_request import *
from models.enum import *
from configdata import Config

with open('./config.json', 'r') as f:
    config_data = json.load(f)

config = Config(config_data)

def request_get_cn():
    response = requests.post(config.nk_api_url + "/v2/va/get-computing-node", json={})
    json = request.json()
    return json

def request_list_channel():
    response = requests.post(config.nk_api_url + "/v2/va/list-channel", json={})
    json = request.json()
    return json

def request_list_roi(nodeId: str, chId: str):
    request = RequestListRoi(nodeId, chId)
    result = request_listroi_to_dict(request)
    response = requests.post(config.nk_api_url + "/v2/va/list-roi", json=result)
    json = .json()
    return json

def request_list_facedb(nodeId: str):
    request = RequestListFacedb(nodeId)
    result = request_facedb_to_dict(request)
    response = requests.post(config.nk_api_url + "/v2/va/list-facedb", json=result)
    json = response.json()
    return json