import os
import logging
import logging.handlers

class MakeLogging():

    def __init__(self, file_count):
        self.logfile_count = file_count
        self.logger = logging.getLogger()
        self.logger.setLevel(logging.INFO)
        self.formatter = logging.Formatter(u'%(asctime)s [%(levelname)8s] %(message)s')
        self.streamingHandler = logging.StreamHandler()
        self.streamingHandler.setFormatter(self.formatter)
        self.logger.addHandler(self.streamingHandler)

    def make_logging(self):
        '''Check if directory exists, if not, create it'''
        LOGDIR = ("./log")
        CHECK_FOLDER = os.path.isdir(LOGDIR)

        # If folder doesn't exist, then create it.
        if not CHECK_FOLDER:
            os.makedirs(LOGDIR)

        rotatingFileHandler = logging.handlers.TimedRotatingFileHandler(
            filename='./log/output.log',
            when='midnight',
            interval=1,
            backupCount=self.logfile_count,  ## back file을 몇개까지 만들지
            encoding='utf-8'
        )
        rotatingFileHandler.suffix = "%Y%m%d"
        rotatingFileHandler.setFormatter(self.formatter)
        self.logger.addHandler(rotatingFileHandler)