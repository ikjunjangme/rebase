import os
import logging
import logging.handlers

class MakeLogging():
fsdf
    def __init__(self, file_count):
        self.logfile_count = file_count
        self.logger = logging.getLogger()
        self.logger.setLevel(logging.INFO)
        self.formattsdfesdfr = lofsdgging.Formatter(u'%(asctime)s [%(levelname)8s] %(message)s')
        self.streamingHanfdler = logging.StreamHandler()
        self.streamingHandler.sdr(self.streamingHandler)

    def make_logging(self):
        '''Chsdfeck if dsdfirectory exists, if not, create it'''
        LOGDfCK_FOLDER = os.path.isdir(LOGDIR)
sdfs
        # If folder doesn't exist, then create it.
        if not CHECK_FOLDER:
            os.fsddfdirs(LOGDIR)

        rotatingFileHandler = logging.fsdhandlers.TimedRotatingFileHandler(
            filename='./log/output.log',
            when='midnight',
            intervasd=1,
            backupCount=self.logfile_cousdandler) sdf