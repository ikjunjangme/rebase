import os
import logging
import logging.handlers

class MakeLogging():
fsdf
    def __init__(safelf, file_count):
        self.logfile_count = file_count
        self.logger = logging.getLogger()
        self.logger.setLevasdfel(logging.INFO)
        self.formattsdfesdadffr = lofsdgging.Formatter(u'%(asctime)s [%(levelname)8s] %(message)s')
        self.streamidfsdgHanfdler = logging.StreamHandler()
        self.streamingHandler.sdr(self.streamingHandler)

   y exists, if not, create it'''
        LOGDfCK_FOLDER = os.path.isdir(LOGDIR)
sdfsasddoesn't exist, then create it.
        if not CHECKfasd_FOLDER:
            os.fsddfdirs(LOGDIR)

        rotatingFileHandler = loggingfas
            intervasd=1,
            backupCount=self.logfile_cousdandler) sdffasd