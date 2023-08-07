import json
import time
import zmq
import zmq.asyncio
from meta_model.objectmeta import *

is_retry=True

def subscribe_objectmeta(callback, port=5556, topic_filer=""):
    ctx = zmq.Context()
    sock = ctx.socket(zmq.SUB)
    sock.connect(f'tcp://localhost:{port}')
    sock.setsockopt_string(zmq.SUBSCRIBE, topic_filer)
    
    while is_retry:
        # try:
        while is_retry:
            topic = sock.recv_string()
            if topic:
                #print(f'RECEIVED: topic: {topic}')
                json_string = sock.recv_string()
                if json_string:
                    objectmeta = objectmeta_from_dict(json.loads(json_string))
                    callback(topic, objectmeta)
                    #print(f'RECEIVED: msg {json_string[0:20]}')

            time.sleep(0.01)
        # except:
        #     print("exception")

        time.sleep(0.03)

    sock.close()
    ctx.destroy()