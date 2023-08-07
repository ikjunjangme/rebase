import json
from models.common import *

class Node:
    node_id: str
    node_name: str
    http_host: str
    http_port: int
    rpc_host: str
    rpc_port: int
    product_version: str
    release_date: str
    
    def __init__(self, node_id, node_name, http_host, http_port, rpc_host, rpc_port, product_version, release_date) -> None:
        self.node_id = node_id
        self.node_name = node_name
        self.http_host = http_host
        self.http_port = http_port
        self.rpc_host = rpc_host
        self.rpc_port = rpc_port
        self.product_version = product_version
        self.release_date = release_date
        
    @staticmethod
    def from_dict(obj: Any) -> 'Node':
        assert isinstance(obj, dict)
        node_id = from_str(obj.get("nodeId"))
        node_name = from_str(obj.get("nodeName"))
        http_host = from_str(obj.get("httpHost"))
        http_port = from_int(obj.get("httpPort"))
        rpc_host = from_str(obj.get("rpcHost"))
        rpc_port = from_int(obj.get("rpcPort"))
        product_version = from_str(obj.get("productVersion"))
        release_date = from_str(obj.get("releaseDate"))
        return Node(node_id, node_name, http_host, http_port, rpc_host, rpc_port, product_version, release_date)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["nodeId"] = from_str(self.node_id)
        result["nodeName"] = from_str(self.node_name)
        result["httpHost"] = from_str(self.http_host)
        result["httpPort"] = from_int(self.http_port)
        result["rpcHost"] = from_str(self.rpc_host)
        result["rpcPort"] = from_int(self.rpc_port)
        result["productVersion"] = from_str(self.product_version)
        result["releaseDate"] = from_str(self.release_date)
        
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

class ResponseGetComputingNode:
    node: Node
    code: int
    message: str
    
    def __init__(self, node_id) -> None:
        self.node_id = node_id
        
    @staticmethod
    def from_dict(obj: Any) -> 'ResponseGetComputingNode':
        assert isinstance(obj, dict)
        node = Node.from_dict(obj.get("node"))
        code = from_int(obj.get("code"))
        message = from_str(obj.get("message"))
        return ResponseGetComputingNode(node, code, message)
    
    def to_dict(self) -> dict:
        result: dict = {}
        result["node"] = to_class(Node, self.node)
        result["code"] = from_str(self.code)
        result["message"] = from_str(self.message)
        return result
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
        sort_keys=False, indent=4)

def response_getcn_to_dict(x: Any) -> ResponseGetComputingNode:
    return to_class(ResponseGetComputingNode, x)

def response_getcn_from_dict(s: Any) -> ResponseGetComputingNode:
    return ResponseGetComputingNode.from_dict(s)