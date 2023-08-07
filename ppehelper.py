from meta_model.objectmeta import *
from models.enum import *
from loghelper import *
import time
import globals
    
def run_ppe_check(queue, makeLogging, config):
    while True:
        try:                                    
            if not queue:
                time.sleep(0.015)
                continue
            
            channel_id, object_meta_set = queue.popleft()
            event_count = 0
            
            if object_meta_set[channel_id].event_list is not None:
                for event in object_meta_set[channel_id].event_list:        
                    if event.event_type == "Unknown":
                        continue
                    
                    event_count = event_count + 1
                    
                    if event.event_type == "TidyUpWorkbench":
                        if event.roi_info.roi_name == config.worktable_name_1:
                            if event.abnormal_score == 0:
                                globals.isTable1Empty = True;
                            else:
                                globals.isTable1Empty = False;
                        elif event.roi_info.roi_name == config.worktable_name_2:
                            if event.abnormal_score == 0:
                                globals.isTable2Empty = True;
                            else:
                                globals.isTable2Empty = False;
                        elif event.roi_info.roi_name == config.worktable_name_3:
                            if event.abnormal_score == 0:
                                globals.isTable3Empty = True;
                            else:
                                globals.isTable3Empty = False;
                    
                    if object_meta_set[channel_id].camera_name == config.kiosk_name:
                        globals.stayTime = event.stay_time
                        
                        if event.event_type == "NotWearingMask":
                            globals.isMaskOn = False
                        
                        if event.clsitems != None:                            
                            for item in event.clsitems:
                                if item == 'face_shield':
                                    globals.isFaceShieldOn = True
                                elif item == 'left_glove':
                                    globals.isLeftGloveOn = True
                                elif item == 'right_glove':
                                    globals.isRightGloveOn = True
                                elif item == 'left_welding_sleeve':
                                    globals.isLeftWeldingSleeveOn = True
                                elif item == 'right_welding_sleeve':
                                    globals.isRightWeldingSleeveOn = True
                                elif item == 'left_safety_shoes':
                                    globals.isLeftSafetyShoesOn = True
                                elif item == 'right_safety_shoes':
                                    globals.isRightSafetyShoesOn = True
                        
            # if event_count > 0:
            #     makeLogging.logger.info("[{}]EventStamp({})".format(channel_id, event_count))

        except Exception as e:
            makeLogging.logger.error(e)
                    