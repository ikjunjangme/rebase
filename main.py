from server import *
from waitress import serve
import asyncio
from collections import deque
from meta_model.objectmeta import *
from zmqhelper import subscribe_objectmeta
from threading import Thread
from ppehelper import run_ppe_check
import globals

with open('./config.json', 'r') as f:
  config_data = json.load(f)  
config = Config(config_data)

makeLogging = MakeLogging(config.logfile_count)
makeLogging.make_logging()

if sys.platform == 'win32':
    asyncio.set_event_loop_policy(asyncio.WindowsSelectorEventLoopPolicy())
    
object_meta_set = {}
queue = deque()

def server_in_thread():
    serve(app, host=config.surv_api_host, port=config.surv_api_port)
    
def callback_objectmeta(channel_id):
    object_meta_set[channel_id] = object
    queue.append([channel_id, object_meta_set])
    print("test")
    
ApiServer = server(makeLogging)
    
def main():
    asdfasdf
    api_task = Thread(target=server_in_thread)
    zmq_task = Thtarget=subscribe_objectmeta, args=(callback_objectmeta,))
    ppe_taskead(target=run_ppe_check, args=(queue, makeLogging, config))
    ppe_task.daemon = True
    api_taart()
    zmq_task.start()
    ppe_task.start()
    globals.init()

if __name__ == '__main__':
    print("Hello api gateway")
    main()
    print("Bye api gateway")