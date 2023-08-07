from models.enum import CHECK_RESULT

def init():
    global ToolCheckResult
    global isFaceShieldOn
    global isLeftGloveOn
    global isRightGloveOn
    global isLeftWeldingSleeveOn
    global isRightWeldingSleeveOn
    global isLeftSafetyShoesOn
    global isRightSafetyShoesOn
    global isMaskOn
    global stayTime
    global isTable1Empty
    global isTable2Empty
    global isTable3Empty
    global isTable3Empty
    #global TableInfoDict
    ToolCheckResult = CHECK_RESULT.PASS
    isFaceShieldOn = False
    isLeftGloveOn = False
    isRightGloveOn = False
    isLeftWeldingSleeveOn = False
    isRightWeldingSleeveOn = False
    isLeftSafetyShoesOn = False
    isRightSafetyShoesOn = False
    isMaskOn = True
    stayTime = 0
    isTable1Empty = False
    isTable2Empty = False
    isTable3Empty = False
    #TableInfoDict = dict