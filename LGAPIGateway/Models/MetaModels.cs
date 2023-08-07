using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.Models
{
    public class LGAPI_Meta : LGAPIModelBase
    {
        //analytic engine 아이디
        public string engine_id { get; set; }
        //yyyy-MM-ddThh:mm:ss.zzz
        public string utc_time { get; set; }
        //화면 구성을 위한 정보
        public LGAPI_Frame frame { get; set; }
        //알람 발생 정보
        public IList<LGAPI_Alarm> alarms { get; set; }
    }

    public class LGAPI_Frame : LGAPIModelBase
    {
        //현재 엔진에 설정되어 있는 룰 정보
        public IList<LGAPI_Rule> rules { get; set; }
        //화면에 검출된 오브젝트 정보
        public IList<LGAPI_Object> objects { get; set; }
    }

    public class LGAPI_Rule : LGAPIModelBase
    {
        //rule engine 아이디
        public string id { get; set; }
        //이벤트를 발생시키는 룰 유형
        public int type { get; set; }
        //1: 선, 2: 다각형
        public int roi_type { get; set; }
        //ROI 좌표 배열</br>
        //[x1, y1, x2, y2, x3, y3….]
        public IList<float> roi { get; set; }
    }

    public class LGAPI_Object : LGAPIModelBase
    {
        //오브젝트 아이디
        public int id { get; set; }
        //오브젝트 유형(3.3 절)
        public int type { get; set; }
        //오브젝트 박스 좌표</br>
        //[top, left, bottom, right]
        public IList<float> box { get; set; }
        //오브젝트로 인해 이벤트 발생시 이벤트를 발생시킨 룰의 아이디 리스트
        public IList<string> evts { get; set; }
    }

    public class LGAPI_Alarm : LGAPIModelBase
    {
        //이벤트를 발생시킨 룰의 아이디
        public string id { get; set; }
        //이벤트를 발생시킨 룰의 코드(3.2 절))
        public int type { get; set; }
        //이벤트를 발생시킨 오브젝트의 이미지(JPG) Base64 encode
        public string img { get; set; }
        //룰 구분이 카운트일 경우(3.2 절)
        public int count { get; set; }
        //룰 구분이 레벨일 경우(3.2 절)
        public int level { get; set; }
    }

    public class NK_Meta_Model : LGAPIModelBase
    {
        public string ChannelID { get; set; }
        public string RuleID { get; set; }
        public List<ROI_Positions> ROI_Position { get; set; }
        public LGAPI_Meta_Link Meta_Link { get; set; }
    }
}
