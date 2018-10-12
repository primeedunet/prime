using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Collections;
using System.Data;

namespace SendGoMessages
{
    public class SendMsg : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // ToDo
            }

        }

        /// <summary>
        /// 보내고 문자메세지 전송
        /// </summary>
        /// <param name="smsLms">문자메시지 Sms/Lms 여부(S : SMS, L: LMS/MMS)</param>
        /// <param name="returnurl">전송후 전송결과값을 리턴받을 주소 (http://는 생략)</param>
        /// <param name="num">문자메세지 전송할 개수</param>
        /// <param name="phone">문자메시지 수신번호 (0101003000 형식, 다수에게 전송시에는 쉼표로 구분 0101003000,0101003000,0101003000 형식)</param>
        /// <param name="callback">문자메시지 발신번호 (숫자만 입력)</param>
        /// <param name="msg">문자메시지 내용 (sms는 90바이트 이내로 작성,lms는 2000바이트 이내로)URL인코딩 필수적용 해야 함.(메시지 내용에 & 기호가 들어가는 경우 내용이 짤릴 수 있음.)</param>
        /// <param name="name">문자메시지 수신번호에 해당되는 이름을 입력합니다. (홍길동 형식, 다수에게 전송시에는 쉼표로 구분 홍길동,이순신,강감찬 형식)</param>
        /// <param name="reserve">문자메시지 예약전송 체크 (0 : 즉시전송, 1: 예약전송)</param>
        /// <param name="reservetime">문자메시지 예약시간 (형식 : 2005-05-18 13:30 ) 년-월-일 시:분</param>
        /// <param name="subject">Lms 전송시 제목입력 20글자이내로 작성, 특수문자제외 (“, ‘, <, >, [, ], @, # 등)</param>
        /// <param name="contents">이미지 파일명 입력시 MMS로 발송 처리됨 이미지의 url을 입력, JPG 파일형식만 가능 사이즈 가로 * 세로 600px* 600px 크기, 300kb 이내 용량 권장. Ex) http://www.sendgo.co.kr/mms.jpg </param>
        /// <param name="etc1">사용자정의 (발송후 리턴페이지로 값그대로 리턴됨)</param>
        /// <param name="etc2">사용자정의 (발송후 리턴페이지로 값그대로 리턴됨)</param>
        public void SendGo(string smsLms, string returnurl, string num, string phone , string callback, string msg, string name,
                                string reserve="0", string reservetime = "", string subject = "", string contents = "", string etc1 = "", string etc2 = "")
        {
            try
            {
                string id = "primeedunet";   //보내고 아이디
                string pass = "vmfkdla1126"; //보내고 패스워드

                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("remote_id={0}", id));
                sb.Append(string.Format("&remote_pass={0}", pass));
                sb.Append(string.Format("&remote_returnurl={0}", returnurl));
                sb.Append(string.Format("&remote_num={0}", num));
                sb.Append(string.Format("&remote_reserve={0}", reserve));
                sb.Append(string.Format("&remote_reservetime={0}", reservetime));
                sb.Append(string.Format("&remote_phone={0}", phone));
                sb.Append(string.Format("&remote_name={0}", name));
                sb.Append(string.Format("&remote_subject={0}", subject));
                sb.Append(string.Format("&remote_callback={0}", callback));
                sb.Append(string.Format("&remote_msg={0}", msg));
                sb.Append(string.Format("&remote_contents={0}", contents));
                sb.Append(string.Format("&remote_etc1={0}", etc1));
                sb.Append(string.Format("&remote_etc2={0}", etc2));
                string PostData = sb.ToString();

                //SMS/LMS 전송URL
                string sendUrl = smsLms == "S" ? "http://www.sendgo.co.kr/Remote/RemoteSms.html" : "http://www.sendgo.co.kr/Remote/RemoteMms.html";

                SendRequest.SendWebRequest("Post", PostData, sendUrl);
            }
            catch(Exception e)
            {
                string exMessage = e.Message;
            }
        }
    }
}
