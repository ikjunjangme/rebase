class Config():    
    nk_api_host: str
    nk_api_port: int
    nk_api_url: str
    link_api_host: str
    link_api_port: int
    link_api_url: str
    surv_api_host: str
    surv_api_port: int
    request_node_create: str
    request_register_channel: str
    request_create_roi: str
    request_update_roi: str
    request_remove_roi: str
    request_remove_channel: str
    kiosk_name: str
    worktable_name_1: str
    worktable_name_2: str
    worktable_name_3: str
    logfile_count: int  
    
    def __init__(self, config_data: dict):
        self.nk_api_host = config_data["NkApiHost"]
        self.nk_api_port = config_data["NkApiPort"]
        self.nk_api_url = config_data["NkApiUrl"]
        self.link_api_host = config_data["LinkApiHost"]
        self.link_api_port = config_data["LinkApiPort"]
        self.link_api_url = config_data["LinkApiUrl"]
        self.surv_api_host = config_data["SurvApiHost"]
        self.surv_api_port = config_data["SurvApiPort"]
        self.request_node_create = config_data["RequestNodeCreate"]
        self.request_register_channel = config_data["RequestRegisterChannel"]
        self.request_create_roi = config_data["RequestCreateRoi"]
        self.request_update_roi = config_data["RequestUpdateRoi"]
        self.request_remove_roi = config_data["RequestRemoveRoi"]
        self.request_remove_channel = config_data["RequestRemoveChannel"]
        self.kiosk_name = config_data["KioskName"]
        self.worktable_name_1 = config_data["WorktableName1"]
        self.worktable_name_2 = config_data["WorktableName2"]
        self.worktable_name_3 = config_data["WorktableName3"]
        self.logfile_count = config_data["logfileCount"]
        