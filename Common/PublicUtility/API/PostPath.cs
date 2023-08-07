using System.Collections.Generic;

namespace PublicUtility.API
{
    public enum POSTReqPath
    {
        REQ_NONE = -1,
        //Channels
        REQ_CH_REGISTER = 0,
        REQ_CH_GET = 1,
        REQ_CH_LIST = 2,
        REQ_CH_UPDATE = 3,
        REQ_CH_REMOVE = 4,
        REQ_CH_SNAPSHOT = 5,
        REQ_CH_CALLIBRATION = 6,
        //ComputingNode
        REQ_COMPUTING_ND_CREATE = 10,
        REQ_COMPUTING_ND_GET = 11,
        REQ_COMPUTING_ND_LIST = 12,
        REQ_COMPUTING_ND_UPDATE = 13,
        REQ_COMPUTING_ND_REMOVE = 14,
        //System
        REQ_SYSTEM_GET_STATUS = 20,
        //Roi
        REQ_ROI_CREATE = 30,
        REQ_ROI_GET = 31,
        REQ_ROI_LIST = 32,
        REQ_ROI_UPDATE = 33,
        REQ_ROI_REMOVE = 34,
        //RoiLink
        REQ_ROI_LINK_CREATE = 40,
        REQ_ROI_LINK_GET = 41,
        REQ_ROI_LINK_LIST = 42,
        REQ_ROI_LINK_UPDATE = 43,
        REQ_ROI_LINK_REMOVE = 44,
        //VAControl
        REQ_VA_META_REQUEST = 50,
        REQ_VA_CONTROL = 51,
        REQ_FACE_REGISTER = 52,
        REQ_FACE_GET = 53,
        REQ_FACE_LIST = 54,
        REQ_FACE_UPDATE = 55,
        REQ_FACE_REMOVE = 56,
    }
    public class PostPath
    {
        private static Dictionary<string, POSTReqPath> _dicPathCommand = new()
        {
            //ComputingNode
            { "create-computing-node", POSTReqPath.REQ_COMPUTING_ND_CREATE },
            { "get-computing-node", POSTReqPath.REQ_COMPUTING_ND_GET },
            { "list-computing-node", POSTReqPath.REQ_COMPUTING_ND_LIST },
            { "update-computing-node", POSTReqPath.REQ_COMPUTING_ND_UPDATE },
            { "remove-computing-node", POSTReqPath.REQ_COMPUTING_ND_REMOVE },

            //Channel
            { "register-channel", POSTReqPath.REQ_CH_REGISTER },
            { "get-channel", POSTReqPath.REQ_CH_GET },
            { "list-channel", POSTReqPath.REQ_CH_LIST },
            { "update-channel", POSTReqPath.REQ_CH_UPDATE },
            { "remove-channel", POSTReqPath.REQ_CH_REMOVE },
            { "callibrate", POSTReqPath.REQ_CH_CALLIBRATION },
            { "snapshot", POSTReqPath.REQ_CH_SNAPSHOT },

            //Roi
            { "create-roi", POSTReqPath.REQ_ROI_CREATE },
            { "get-roi", POSTReqPath.REQ_ROI_GET },
            { "list-roi", POSTReqPath.REQ_ROI_LIST },
            { "update-roi", POSTReqPath.REQ_ROI_UPDATE },
            { "remove-roi", POSTReqPath.REQ_ROI_REMOVE },

            //va control
            { "control", POSTReqPath.REQ_VA_CONTROL },

            //Link
            { "create-link", POSTReqPath.REQ_ROI_LINK_CREATE },
            { "get-link", POSTReqPath.REQ_ROI_LINK_GET },
            { "list-link", POSTReqPath.REQ_ROI_LINK_LIST },
            { "update-link", POSTReqPath.REQ_ROI_LINK_UPDATE },
            { "remove-link", POSTReqPath.REQ_ROI_LINK_REMOVE },

            //System
            { "get-system-status", POSTReqPath.REQ_SYSTEM_GET_STATUS },

            //face
            { "register-facedb", POSTReqPath.REQ_FACE_REGISTER },
            { "remove-facedb", POSTReqPath.REQ_FACE_REMOVE },
            { "update-facedb", POSTReqPath.REQ_FACE_UPDATE },
            { "get-facedb", POSTReqPath.REQ_FACE_GET },
            { "list-facedb", POSTReqPath.REQ_FACE_LIST },
        };

        public static POSTReqPath GetPostFlag(string post)
        {
            if (_dicPathCommand.ContainsKey(post))
                return _dicPathCommand[post];

            return POSTReqPath.REQ_NONE;
        }

        public static string GetPostString(POSTReqPath post)
        {
            foreach (string keyVar in _dicPathCommand.Keys)
            {
                if (_dicPathCommand[keyVar] == post)
                    return keyVar;
            }
            return null;
        }
    }
}
