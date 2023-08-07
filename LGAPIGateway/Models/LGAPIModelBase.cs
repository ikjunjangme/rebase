using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.Models
{
    public enum Supported_HTTP_Method
    {
        GET = 1,
        POST = 2,
        PUT = 4,
        DELETE = 8,
    }

    public enum LG_Event_Type
    {
        Intrusion = 1,                       //위험 구역 접근 감지(침입)
        Loiter = 2,                          //배회 감지
        Flame_Detect = 3,                    //불꽃 감지
        Steam_Smoke = 4,                     //스팀/연기 감지
        FallDown = 5,                        //쓰러짐 감지
        Fight = 6,                           //폭력 감지
        Run = 7,                             //달리기 감지
        Crowding = 8,                        //군집 감지
        Abandon = 9,                         //유기 감지
        Steal = 10,                          //도난 감지
        Pedestrian = 11,                     //보행자 감지
        StopedCar = 12,                      //정지차량 감지
        SafetyBeltNotWear = 13,              //안전벨트 미착용 감지
        ReverseRun = 14,                     //역주행 감지
        Drop = 15,                           //낙하물 감지
        IlegalCar = 16,                      //불법 주/정차 감지
        LineIntrusionTwoWay = 17,            //라인 침입 감지(양방향)
        LineIntrusionOneWay = 18,            //라인 침입 감지(단방향)
        AuthorizedPerson = 19,               //인가자 출입
        UnAuthorizedPerson = 20,             //비인가자 출입
        HelmetWear = 21,                     //안전모 착용
        HelmetUnWear = 22,                   //안전모 미착용
        MaskUnWear = 24,                     //마스크 미착용
        Abnormal = 25,                       //이상행위 감지
        PhoneCalling = 26,                   //휴대폰 통화 행위 감지
        PhonePicture = 27,                   //휴대폰 촬영 행위 감지
                                             
        LineCrossCount = 201,                //라인 통과 카운트
        LineCrossPersonCount = 202,          //라인 통과 카운트(사람)
        LineCrossCar = 203,                  //라인 통과 카운트(차량)
        StopedCarCount = 204,                //정차 중 차량 카운트


        NotDefine = 999999,                  //내부 처리용으로 추가함
    }

    public enum LG_Object_Type
    {
        NOTCLASSIFICATION,    //미분류
        PERSON,                //사람
        CAR,                  //자동차
        FACE,                 //얼굴
        FLAME,                 //불꽃
        SMOKE,                //연기
        BIKE,                 //자전거
        SEDAN,                //세단
        SUV,                  //SUV
        MOTORCYCLE,           //오토바이
        BUS,                  //버스
        TRUCK,                //트럭
        EXCAVATOR,            //굴착기
        TANKTRUCK,            //탱크/트럭
        FORKLIFT,             //지게차
        LEMICON,              //레미콘
        CULTIVATOR,           //경운기
        TRACTOR,              //트랙터
        SPECIALVEHICLE,       //특수차량
        FACEMAN,              //얼굴(남자)
        FACEWOMAN,            //얼굴(여자)
        FACEHELMET            //얼굴(헬멧)
    }

    public class ROI_Positions : LGAPIModelBase
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class LGAPIModelBase
    {
    }

    public class LGAPI_Link_Base : LGAPIModelBase
    {
        public string href { get; set; }
        public IList<string> method { get; set; }

        public LGAPI_Link_Base(Supported_HTTP_Method method)
        {
            this.method = new List<string>();
            int method_parse = (int)method;
            for(int i = 0; i < 4; i++)
            {
                int mask = (1 << i);
                if ((method_parse & mask) == mask)
                {
                    Supported_HTTP_Method check = (Supported_HTTP_Method)(method_parse & (1 << i));
                    this.method.Add(check.ToString());
                }
            }
        }
    }
}
